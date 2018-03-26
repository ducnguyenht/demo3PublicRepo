using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVer1.Services
{
    public interface ISampleService
    {
        IList<string> GetItems();
    }
}