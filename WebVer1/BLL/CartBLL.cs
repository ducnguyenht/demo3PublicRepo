using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using KiotVietBO;
using Newtonsoft.Json;
using System.Net;
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
                //var clientRequest = new RestClient("https://public.kiotapi.com/products/" + item.Id);
                //var requestConfig = new RestRequest(Method.GET);
                //requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
                //requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
                //var hangHoa = clientRequest.Execute<ChiTietHangHoaBO>(requestConfig).Data;
                var cartBO = new CartBO()
                {
                    basePrice = hangHoa.basePrice,
                    code=hangHoa.code,
                    description = hangHoa.description,
                    id = hangHoa.id,
                    name = hangHoa.name,
                    image = hangHoa.images != null ? hangHoa.images[0] : "",
                    quantity = item.Count
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
}