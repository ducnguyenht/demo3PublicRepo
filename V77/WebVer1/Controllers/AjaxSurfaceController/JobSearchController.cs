using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class JobSearchController : Umbraco.Web.Mvc.SurfaceController
    {
        //[HttpPost, Umbraco.Web.Mvc.NotChildAction]
        [HttpGet]
        public ActionResult GetOpportunityCount()
        {
            return Json(new { Count = 0 });
        }
    }
}