using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace WebVer1.Models
{
    public class MemberLoginModel 
    {
        public MemberLoginModel()
        {

        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
