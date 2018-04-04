using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace WebVer1.Controllers.RenderMVC
{
    public class ProductsController : RenderMvcController
    {
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Search(string query)
        {
            var t = 1;
            var tt = 2;
            //if (ModelState.IsValid)
            //{
            //}
            //return View();
            return null;
        }
    }
}