using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;

//[assembly: OwinStartup(typeof(WebVer1.BLL.Startup))]
[assembly: OwinStartup("CustomSignalROwinStartup", typeof(WebVer1.BLL.Startup))]

namespace WebVer1.BLL
{
    public class Startup :UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
            app.MapSignalR();
        }
    }
}