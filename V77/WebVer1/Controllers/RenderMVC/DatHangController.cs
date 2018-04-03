using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
namespace WebVer1.Controllers.RenderMVC
{
    public class DatHangController : RenderMvcController
    {
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult GuiDathang(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }
    }
    public class DatHangModel  
    {
        [Required]
        public string contactFormName { get; set; }
        [Required]
        public string contactFormTelephone { get; set; }
        public string contactFormEmail { get; set; }
        [Required]
        public string contactFormAdress { get; set; }
        public string contactFormMessage { get; set; }
    }
}