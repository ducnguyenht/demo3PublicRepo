using KiotVietBO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HangHoaBLL
/// </summary>
public class HangHoaBLL
{
	public HangHoaBLL()
	{
		//
		// TODO: Add constructor logic here
		//
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
        //lay ds hang theo id danh muc
        var dsHangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o=>o.categoryId==Convert.ToInt32(categoryId));
        var hangHoaTotal = dsHangHoa.Count();

        string query = "";
        switch (sx)
        {
            case ""://"UpdateDate desc";
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=modifiedDate&orderDirection=desc&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
              dsHangHoa = dsHangHoa.OrderByDescending(o => o.modifiedDate);
                break;
            case "1"://Giaban desc
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=basePrice&orderDirection=desc&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.basePrice);
                break;
            case "2"://Giaban asc
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=basePrice&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
                dsHangHoa = dsHangHoa.OrderBy(o => o.basePrice);
                break;
            case "3"://"Name desc";
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=categoryName&orderDirection=desc&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.name);
                break;
            case "4":
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=categoryName&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
                dsHangHoa = dsHangHoa.OrderBy(o => o.name);
                break;
            default:
                //query = @"https://public.kiotapi.com/Products?format=json&orderBy=modifiedDate&orderDirection=desc&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=" + trangHienTai + "&pageSize=" + pageSize + "&categoryId=" + categoryId;
                dsHangHoa = dsHangHoa.OrderByDescending(o => o.modifiedDate);
                break;
        }

        //var clientRequest = new RestClient(query);
        //var requestConfig = new RestRequest(Method.GET);
        //requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        //requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //return clientRequest.Execute<RootChiTietHangHoaBO>(requestConfig).Data;

        var dsPhanTrang = dsHangHoa.Skip(Convert.ToInt32(trangHienTai) * Convert.ToInt32(pageSize)).Take(Convert.ToInt32(pageSize)).ToList();
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
        //string query = "https://public.kiotapi.com/Products?format=json&orderBy=modifiedDate&orderDirection=desc&includeInventory=true&currentItem=0&pageSize=10";
        //var clientRequest = new RestClient(query);
        //var requestConfig = new RestRequest(Method.GET);
        //requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        //requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //return clientRequest.Execute<RootChiTietHangHoaBO>(requestConfig).Data;
        RootChiTietHangHoaBO ret = new RootChiTietHangHoaBO();
        ret.data = MemoryCacheKiot.dsHangHoa.data.OrderByDescending(o => o.modifiedDate).Take(10).ToList();
        return ret;
    }
    public ChiTietHangHoaBO ChiTietHangHoa(string id)
    {
        //var clientRequest = new RestClient("https://public.kiotapi.com/products/" + id);
        //var requestConfig = new RestRequest(Method.GET);
        //requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        //requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //return clientRequest.Execute<ChiTietHangHoaBO>(requestConfig).Data;
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
        //var clientRequest = new RestClient("https://public.kiotapi.com/categories/114045");
        //var requestConfig = new RestRequest(Method.GET);
        //requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        //requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //var dsOpLungBaoDa= clientRequest.Execute<DSNhomHangBo>(requestConfig).Data;
        //foreach (var item in dsOpLungBaoDa.children)
        //{
        //    var query = @"https://public.kiotapi.com/Products?format=json&orderBy=modifiedDate&orderDirection=desc&includeRemoveIds=false&includeInventory=true&includePricebook=false&currentItem=0&pageSize=2&categoryId=" + item.categoryId;
        //    clientRequest = new RestClient(query);
        //     requestConfig = new RestRequest(Method.GET);
        //    requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        //    requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
        //    var data = clientRequest.Execute<RootChiTietHangHoaBO>(requestConfig).Data;
        //    foreach (var item1 in data.data)
        //    {
        //        ret.Add(item1);
        //    }
        //}
        return ret;
    }
}