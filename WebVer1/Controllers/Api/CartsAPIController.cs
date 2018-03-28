using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace WebVer1.Controllers.Api
{
    public class CartsAPIController :  UmbracoApiController
    {
        [System.Web.Http.HttpGet]
        public JsonResult CartCount()
        {//string id
            List<string> dsIdHangHoa = null;
            if (HttpContext.Current.Session!=null && HttpContext.Current.Session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = HttpContext.Current.Session["dsIdHangHoa"] as List<string>;
            }
            else
            {
                dsIdHangHoa = new List<string>();
            }

            return new JsonResult() { Data = new { d = dsIdHangHoa.Count }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return JsonConvert.SerializeObject(dsIdHangHoa.Count);
        }

    }
}
