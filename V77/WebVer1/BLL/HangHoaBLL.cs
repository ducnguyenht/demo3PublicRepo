using KiotVietBO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
public class HangHoaBLL
{
	public HangHoaBLL()
	{
	}
    public RootChiTietHangHoaBO DanhSachHangHoaCache()
    {
        if (MemoryCacheKiot.dsHangHoa==null)
        {
            var dshangHoa = DanhSachHangHoaALL();
            MemoryCacheKiot.dsHangHoa = dshangHoa;
            var numberOfItems = dshangHoa.total;
            var itemsPerPage = dshangHoa.pageSize;
            var numberOfPages = numberOfItems % itemsPerPage == 0 ? Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) : Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) + 1;
            for (int i = 1; i < numberOfPages; i++)
            {
                var dsHangHoaTheoTrangHienTai = DanhSachHangHoaALL((i*100).ToString(),"100");
                MemoryCacheKiot.dsHangHoa.data.AddRange(dsHangHoaTheoTrangHienTai.data);
            }
            MemoryCacheKiot.dsHangHoa.data = MemoryCacheKiot.dsHangHoa.data.DistinctBy(o => o.id).ToList();
        }
        return MemoryCacheKiot.dsHangHoa;
    }
    public RootChiTietHangHoaBO DanhSachHangHoaALL(string trangHienTai="0", string pageSize = "100")
    {
        string query = @"https://public.kiotapi.com/Products?format=json&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize;
        var clientRequest = new RestClient(query);
        var requestConfig = new RestRequest(Method.GET);
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        return clientRequest.Execute<RootChiTietHangHoaBO>(requestConfig).Data;
    }
    public RootChiTietHangHoaBO DanhSachHangHoa(string categoryId, string trangHienTai, string sx, string pageSize = "16")
    {
        var dsHangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o=>o.categoryId==Convert.ToInt32(categoryId));
        var hangHoaTotal = dsHangHoa.Count();
        string query = "";
        switch (sx)
        {
            case "": 
              dsHangHoa = dsHangHoa.OrderByDescending(o => o.modifiedDate);
                break;
            case "1": 
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.basePrice);
                break;
            case "2": 
                dsHangHoa = dsHangHoa.OrderBy(o => o.basePrice);
                break;
            case "3": 
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.name);
                break;
            case "4":
                dsHangHoa = dsHangHoa.OrderBy(o => o.name);
                break;
            default:
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.modifiedDate);
                break;
        }
        var dsPhanTrang = new List<KiotVietBO.ChiTietHangHoaBO>();
        dsPhanTrang = dsHangHoa.Skip(Convert.ToInt32(trangHienTai) * Convert.ToInt32(pageSize)).Take(Convert.ToInt32(pageSize)).ToList();
        dsPhanTrang = dsPhanTrang.DistinctBy(o => o.id).ToList();
        RootChiTietHangHoaBO cthh = new RootChiTietHangHoaBO()
        {
            total = hangHoaTotal,
            pageSize = Convert.ToInt32(pageSize),
            data = dsPhanTrang
        };
        return cthh;
    }
    public RootChiTietHangHoaBO SanPhamNoiBat()
    {
        RootChiTietHangHoaBO ret = new RootChiTietHangHoaBO();
        ret.data = MemoryCacheKiot.dsHangHoa.data.OrderByDescending(o => o.modifiedDate).Take(10).ToList();
        return ret;
    }
    public ChiTietHangHoaBO ChiTietHangHoa(string id)
    {
        return MemoryCacheKiot.dsHangHoa.data.Where(o=>o.id==Convert.ToInt32(id)).FirstOrDefault();
    }
    public List<ChiTietHangHoaBO> DanhSachOpLungBaoDa()
    {
        List<ChiTietHangHoaBO> ret = new List<ChiTietHangHoaBO>();
        var dsOpLungBaoDa = MemoryCacheKiot.dsNhomHang.data.Where(o => o.categoryId == 114045).FirstOrDefault();
        foreach (var item in dsOpLungBaoDa.children)
        {
            var dsHangHoaOpLungBaoDa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.categoryId == item.categoryId).OrderByDescending(o=>o.modifiedDate).Take(2).ToList();
            ret.AddRange(dsHangHoaOpLungBaoDa);
        }
        return ret;
    }
    public List<ChiTietHangHoaBO> DsHangHoaCungLoai(ChiTietHangHoaBO hangHoa)
    {
        List<ChiTietHangHoaBO> ret = new List<ChiTietHangHoaBO>();
        var hangHoaMaster = new ChiTietHangHoaBO();
        //1.Neu hang hoa la child thi masterProductId != 0
        if (hangHoa.masterProductId!=0)
        {
            //1.1 Tim hang hoa master
            hangHoaMaster = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == hangHoa.masterProductId).FirstOrDefault();
            //1.2 Tim ds hang hoa con
            ret = MemoryCacheKiot.dsHangHoa.data.Where(o => o.masterProductId == hangHoa.masterProductId).ToList();         
            ret.Add(hangHoaMaster);            
        }//2.Hang hoa la master
        else
        {//2.1 Tim ds hang hoa con
            ret = MemoryCacheKiot.dsHangHoa.data.Where(o => o.masterProductId == hangHoa.id).ToList();
            ret.Add(hangHoa);
        }
        return ret;
    }
}