using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class ContactController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpPost]
        public ActionResult SendMail(Models.Contact form)
        {
            string retValue = "There was an error submitting the form, please try again later.";
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }

            if (ModelState.IsValid)
            {
            }
            return Content(retValue);
        }
    }
}