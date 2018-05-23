using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using KiotVietBO;
using Newtonsoft.Json;
using System.Net;
using Umbraco.Core.Models;
using Umbraco.Core;
/// <summary>
/// Summary description for CartBLL
/// </summary>
public class CartBLL
{
	public CartBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public RootCardBO GetCartItems(List<string> dsIdHangHoaDuocDat)
    {
        List<CartBO> cartBOs = new List<CartBO>();
        var dsHangHoaDuocDat = HttpContext.Current.Session["dsIdHangHoa"];
        if (dsHangHoaDuocDat!=null)
        {
            var grouped = dsIdHangHoaDuocDat
             .GroupBy(s => s)
             .Select(g => new { Id = g.Key, Count = g.Count() });
            foreach (var item in grouped)
            {
                var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id ==Convert.ToInt32(item.Id)).FirstOrDefault();            
                var cartBO = new CartBO()
                {
                    basePrice = hangHoa.basePrice,
                    code=hangHoa.code,
                    description = hangHoa.description,
                    id = hangHoa.id,
                    name = hangHoa.name,
                    image = hangHoa.images != null ? hangHoa.images[0] : "",
                    quantity = item.Count,
                    
                };
                cartBOs.Add(cartBO);
            }
            cartBOs = cartBOs.OrderBy(o => o.name).ToList();
        }
        RootCardBO rootCardBO = new RootCardBO() { carts = cartBOs };
        return rootCardBO;
    }
    public int CartCount()
    {
        var dsHangHoaDuocDat = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
        var cartCount = 0;
        if (dsHangHoaDuocDat != null)
        {
            var grouped = dsHangHoaDuocDat
             .GroupBy(s => s)
             .Select(g => new { Id = g.Key, Count = g.Count() });
            foreach (var item in grouped)
            {
                cartCount += item.Count;
            }
        }
        return cartCount;
    }
    public bool PostCart(RootOrderBO rootOrderBO)
    {
        var clientRequest = new RestClient("https://public.kiotapi.com/orders");
        var requestConfig = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token + "");
        requestConfig.AddHeader("Content-Type", "application/json");
        requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(rootOrderBO), ParameterType.RequestBody);

        var result = clientRequest.Execute(requestConfig);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            return true;//result.Data;
        }
        return true;//result.Data;
    }
    public bool PostCartAdmin(RootDatHangBO rootOrderBO)
    {
        var clientRequest = new RestClient("https://nzt.kiotviet.com/api/orders");
        var requestConfig = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
        requestConfig.AddHeader("BranchId", "13933");
        requestConfig.AddHeader("Authorization", "Bearer " + MemoryCashier.Token+ "");
        //requestConfig.AddHeader("Content-Type", "application/json");
        requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
        requestConfig.AddParameter("application/json", JsonConvert.SerializeObject(rootOrderBO), ParameterType.RequestBody);

        var result = clientRequest.Execute(requestConfig);
        if (result.StatusCode == HttpStatusCode.OK)
        {
            var dh = Newtonsoft.Json.Linq.JObject.Parse(result.Content);
            var test = (string)dh["Code"];
            var session = System.Web.HttpContext.Current.Session;
            session["maDonHang"] = test;
            //var t = result.Data;
            return true;//result.Data;
        }
        return true;//result.Data;
    }
    public RootCardBO checkCuponDiscount()
    {
        var session = System.Web.HttpContext.Current.Session;
        var rootCartBO = new CartBLL().GetCartItems(session["dsIdHangHoa"] as List<string>);
        Dictionary<string, Cupon> cupon = session["dsCupon"] != null ? session["dsCupon"] as Dictionary<string, Cupon> : null;
        var items = rootCartBO.carts;
        if (cupon != null)
        {
            var Cupon = cupon.Values.FirstOrDefault();
            var cuponAvailable = false;//ap dung giam gia
            if (Cupon.suDung1Lan && Cupon.coHieuLuc)
            {
                var contentService = ApplicationContext.Current.Services.ContentService;
                var content = contentService.GetById(2116);
                var query = content.Children().Where(o => o.Name == Cupon.name).FirstOrDefault();
                var coHieuLuc = query.GetValue<bool>("coHieuLuc");
                if (coHieuLuc)
                {
                    //query.SetValue("coHieuLuc", false);
                    //contentService.SaveAndPublishWithStatus(query);
                    cuponAvailable = true;
                }
            }
            else if (Cupon.suDung1Lan == false && Cupon.coHieuLuc)
            {
                cuponAvailable = true;
            }
            else
            {
                cuponAvailable = false;
            }
            if (cuponAvailable)
            {
                var maHang_NhomHang = Cupon.maHang_NhomHang;
                //neu cupon la ma hang hoa
                var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == maHang_NhomHang).FirstOrDefault();
                if (hangHoa != null)
                {
                    foreach (var item in items.Where(o => o.id == hangHoa.id))
                    {
                        item.disCount = Cupon.giamGia;
                    }
                }
                else
                {//nguoc lai cupon la ma nhom hang
                    var nhomHang = new NhomHangBLL().DanhNhomHangPlane().Where(o => o.categoryId == maHang_NhomHang).FirstOrDefault();
                    var nhomHangPlane = new NhomHangBLL().DanhNhomHangPlane();
                    var parents =
                                nhomHangPlane
                                    .Where(x => x.parentId != 0)
                                    .ToDictionary(x => x.categoryId, x => x.parentId);

                    Func<int, IEnumerable<int>> getParents = null;
                    getParents = i =>
                        parents.ContainsKey(i)
                            ? new[] { parents[i] }.Concat(getParents(parents[i]))
                            : Enumerable.Empty<int>();
                    foreach (var cart in items)
                    {
                        var hanghoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == cart.id).FirstOrDefault();
                        var sdf = getParents(hanghoa.categoryId);
                        var find = sdf.ToList();
                        find.Add(hanghoa.categoryId);
                        if (find.Contains(Cupon.maHang_NhomHang))
                        {
                            cart.disCount = Cupon.giamGia;
                        }
                    }
                }
            }
        }
        return rootCartBO;
    }
}

//var clientRequest = new RestClient("https://public.kiotapi.com/products/" + item.Id);
//var requestConfig = new RestRequest(Method.GET);
//requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
//requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
//var hangHoa = clientRequest.Execute<ChiTietHangHoaBO>(requestConfig).Data;