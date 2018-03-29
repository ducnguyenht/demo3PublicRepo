using Newtonsoft.Json;
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
        public string CartCount()
        {
            List<string> dsIdHangHoa = null;
            var session = System.Web.HttpContext.Current.Session;
            if (session != null &&  session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = session["dsIdHangHoa"] as List<string>;
            }
            else
            {
                dsIdHangHoa = new List<string>();
                session["dsIdHangHoa"] = dsIdHangHoa;
            }
            return JsonConvert.SerializeObject(dsIdHangHoa.Count);
        }
    }
}