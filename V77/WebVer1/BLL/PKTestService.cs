using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;
using RestSharp;
using KiotVietBO;
using Newtonsoft.Json.Linq;
/// <summary>
/// Summary description for PKTestService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PKTestService : System.Web.Services.WebService
{
    public PKTestService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string AddToCart(string pk)
    {//string id
        List<string> dsIdHangHoa = null;
        if (HttpContext.Current.Session["dsIdHangHoa"] != null)
        {
            // Getting the value out of Session
            dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
            dsIdHangHoa.Add(pk);
        }
        else
        {
            dsIdHangHoa = new List<string>();
            dsIdHangHoa.Add(pk);
        }
        // Storing the value in Session
        HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        return JsonConvert.SerializeObject(dsIdHangHoa.Count); ;//new {Id=1,Name="asdfd",Dob= new DateTime() }
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string SearchProduct(string pk)
    {
        IEnumerable<object> result = new List<object>();
        JArray arr = new JArray();
        if (pk != null && pk.Length > 2)
        {
            var pr = MemoryCacheKiot.dsHangHoa.data;
            string[] searchTerms = Extension.RejectMarks(pk.ToString()).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < searchTerms.Length; i++)
            {

                pr = (from comp in pr
                      where comp.name.RejectMarksStr().ToLower().Contains(searchTerms[i].ToLower())
                      select comp).ToList();
            }
            pr = pr.OrderBy(comp => comp.name.RejectMarksStr().ToLower().StartsWith(searchTerms[0].RejectMarksStr().ToLower())).Take(10).ToList();
            for (int i = 0; i < searchTerms.Length; i++)
            {
                arr = new JArray(
                         from comp in pr
                         where comp.name.RejectMarksStr().ToLower().Contains(searchTerms[i].ToLower())
                         orderby comp.name
                         select JObject.FromObject(new
                         {
                             //Code = j++,
                             Image = String.Format("<a title=\"{0}\" href=\"{1}\"><img src=\"{2}\" alt=\"{3}\" width=\"79\" height=\"79\" /></a>", comp.name, @"/danh-muc/chung-loai/san-pham.aspx?pt=" + comp.id, comp.images != null ? comp.images[0] : @"media/4728/noimageb.png", comp.name),
                             Product = String.Format("<a title=\"{0}\" href=\"{1}\">{2}</a>", comp.fullName, @"/danh-muc/chung-loai/san-pham.aspx?pt=" + comp.id, comp.name),
                             Status = comp.ConHang > 0 ? "Còn hàng" : "Hết Hàng",
                             //Category = comp.categoryName
                         }));
            }
            //resulta = resulta.Take(20);
        }
        var asdf = arr.AsJEnumerable().Take(10);
        return JsonConvert.SerializeObject(asdf);

    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string CartCount()
    {//string id
        List<string> dsIdHangHoa = null;
        if (HttpContext.Current.Session["dsIdHangHoa"] != null)
        {
            dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
        }
        else
        {
            dsIdHangHoa = new List<string>();
        }
        return JsonConvert.SerializeObject(dsIdHangHoa.Count); ;
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string RemoveFromCart(string pk)
    {
        List<string> dsIdHangHoa = null;
        if (HttpContext.Current.Session["dsIdHangHoa"] != null)
        {
            dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
            while (dsIdHangHoa.Contains(pk))
            {
                dsIdHangHoa.Remove(pk);
            }
            HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        }
        else
        {
            dsIdHangHoa = new List<string>();
        }
        return dsIdHangHoa.Count + "";
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string UpdateQuantity(string pk, string sl)
    {
        //var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
        //var tonKhoLHP=checkHangHoa.inventories.Where(o=>o.branchId==13933).FirstOrDefault().onHand;
        List<string> dsIdHangHoa = null;
        if (HttpContext.Current.Session["dsIdHangHoa"] != null)
        {
            dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
            var clientRequest = new RestClient("https://public.kiotapi.com/products/" + pk);
            var requestConfig = new RestRequest(Method.GET);
            requestConfig.AddHeader(KiotVietConst.propNameRetailer, KiotVietConst.Retailer);
            requestConfig.AddHeader("Authorization", "Bearer " + KiotVietConst.kiot_token);
            var hangHoa = clientRequest.Execute<ChiTietHangHoaBO>(requestConfig).Data;
            var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
            if (tonKhoLHP < Convert.ToInt32(sl))
            {
                return JsonConvert.SerializeObject(new { message = "Error", product = hangHoa.name, stock = tonKhoLHP, count = dsIdHangHoa.Count });
            }
            while (dsIdHangHoa.Contains(pk))
            {
                dsIdHangHoa.Remove(pk);
            }
            var slsp = Convert.ToInt32(sl);
            for (int i = 1; i <= slsp; i++)
            {
                dsIdHangHoa.Add(pk);
            }
            HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        }
        else
        {
            dsIdHangHoa = new List<string>();
            for (int i = 1; i <= Convert.ToInt32(sl); i++)
            {
                dsIdHangHoa.Add(pk);
            }
            HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        }

        return JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count });
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public string UpdateQuantityTempp(string pk, string sl)
    {

        List<string> dsIdHangHoa = null;
        if (HttpContext.Current.Session["dsIdHangHoa"] != null)
        {
            dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
            var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
            var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
            if (tonKhoLHP < Convert.ToInt32(sl))
            {
                JObject jo = new JObject();
                jo.Add("message", "Error");
                jo.Add("product", hangHoa.name);
                jo.Add("stock", tonKhoLHP);
                jo.Add("count", dsIdHangHoa.Count);
                return JsonConvert.SerializeObject(jo);
            }
            while (dsIdHangHoa.Contains(pk))
            {
                dsIdHangHoa.Remove(pk);
            }
            var slsp = Convert.ToInt32(sl);
            for (int i = 1; i <= slsp; i++)
            {
                dsIdHangHoa.Add(pk);
            }
            HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        }
        else
        {
            dsIdHangHoa = new List<string>();
            var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
            var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
            if (tonKhoLHP < Convert.ToInt32(sl))
            {
                JObject jo = new JObject();
                jo.Add("message", "Error");
                jo.Add("product", hangHoa.name);
                jo.Add("stock", tonKhoLHP);
                jo.Add("count", dsIdHangHoa.Count);
                return JsonConvert.SerializeObject(jo);
            }
            for (int i = 1; i <= Convert.ToInt32(sl); i++)
            {
                dsIdHangHoa.Add(pk);
            }
            HttpContext.Current.Session["dsIdHangHoa"] = dsIdHangHoa;
        }
        return JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count });
    }
}
