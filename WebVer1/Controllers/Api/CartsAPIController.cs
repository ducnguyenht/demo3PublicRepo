using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace WebVer1.Controllers.Api
{
    public class CartsAPIController : SurfaceController
    {

        [HttpGet]
        public JsonResult CartCount()
        {//string id
            List<string> dsIdHangHoa = null;
            if (HttpContext.Session != null && HttpContext.Session["dsIdHangHoa"] != null)
            {
                dsIdHangHoa = HttpContext.Session["dsIdHangHoa"] as List<string>;
            }
            else
            {
                dsIdHangHoa = new List<string>();
            }

            return Json(dsIdHangHoa.Count, JsonRequestBehavior.AllowGet);
            //return JsonConvert.SerializeObject(dsIdHangHoa.Count);
        }


    }
}
