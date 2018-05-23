using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using WebVer1.Models;

namespace WebVer1.Controllers.AjaxSurfaceController
{
    public class MemberLoginSurfaceController : SurfaceController
    {
        [HttpGet]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginGet()
        {
            return PartialView("MemberLoginGet", new MemberLoginModel());
        }

        [HttpGet]
        public ActionResult MemberLogout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [HttpPost]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginPost(MemberLoginModel model)
        {
            if (model.Username!=null && model.Password!=null && Membership.ValidateUser(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToCurrentUmbracoPage();

            }
            else
            {
                TempData["Status"] = "Sai tên / mật khẩu.";
                return RedirectToCurrentUmbracoPage();
            }
        }

    }
}

