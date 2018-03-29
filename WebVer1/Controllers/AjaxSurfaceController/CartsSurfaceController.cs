using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class CartsSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpGet]
        public JsonResult CartCount()
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
            }
            else
            {
                dsIdHangHoa = new List<string>();
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            //return JsonConvert.SerializeObject(dsIdHangHoa.Count);
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AddToCart(string pk)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                dsIdHangHoa.Add(pk);
            }
            else
            {
                dsIdHangHoa = new List<string>();
                dsIdHangHoa.Add(pk);
            }
            session["dsIdHangHoa"] = dsIdHangHoa;
            //return JsonConvert.SerializeObject(dsIdHangHoa.Count);
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchProduct(string pk)
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
                                 Image = String.Format("<a title=\"{0}\" href=\"{1}\"><img src=\"{2}\" alt=\"{3}\" width=\"79\" height=\"79\" /></a>", comp.name, @"/danh-muc/chung-loai/san-pham.aspx?pt=" + comp.id, comp.images != null ? comp.images[0] : @"media/4728/noimageb.png", comp.name),
                                 Product = String.Format("<a title=\"{0}\" href=\"{1}\">{2}</a>", comp.fullName, @"/danh-muc/chung-loai/san-pham.aspx?pt=" + comp.id, comp.name),
                                 Status = comp.ConHang > 0 ? "Còn hàng" : "Hết Hàng",
                             }));
                }
            }
            var asdf = arr.AsJEnumerable().Take(10);
            return Json(asdf, JsonRequestBehavior.AllowGet);
            //return JsonConvert.SerializeObject(asdf);
        }
        [System.Web.Http.HttpGet]
        public JsonResult RemoveFromCart(string pk)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                while (dsIdHangHoa.Contains(pk))
                {
                    dsIdHangHoa.Remove(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            else
            {
                dsIdHangHoa = new List<string>();
            }
            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);

            //return dsIdHangHoa.Count + "";
        }
        [HttpGet]
        public JsonResult UpdateQuantityTempp(string pk, string sl)
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null && session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
                var hangHoa = MemoryCacheKiot.dsHangHoa.data.Where(o => o.id == Convert.ToInt32(pk)).FirstOrDefault();
                var tonKhoLHP = hangHoa.inventories.Where(o => o.branchId == 13933).FirstOrDefault().onHand;
                if (tonKhoLHP < Convert.ToInt32(sl))
                {
                    JObject jo = new JObject();
                    jo.Add("message", "Error");
                    jo.Add("product", hangHoa.name);
                    jo.Add("stock", tonKhoLHP);
                    jo.Add("count", dsIdHangHoa.Count);
                    //return JsonConvert.SerializeObject(jo);
                    return Json(jo, JsonRequestBehavior.AllowGet);
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
                session["dsIdHangHoa"] = dsIdHangHoa;
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
                    //return JsonConvert.SerializeObject(jo);
                    return Json(jo, JsonRequestBehavior.AllowGet);
                }
                for (int i = 1; i <= Convert.ToInt32(sl); i++)
                {
                    dsIdHangHoa.Add(pk);
                }
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            //return JsonConvert.SerializeObject(new { message = "", count = dsIdHangHoa.Count });
            return Json(new { message = "", count = dsIdHangHoa.Count }, JsonRequestBehavior.AllowGet);

        }
    }
}