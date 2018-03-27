using KiotVietBO;
using System.Web.Http;
using Umbraco.Web.WebApi;
using WebVer1.Services;

namespace WebVer1.Controllers.Api
{
    public class CategoriesApiController : UmbracoApiController
    {
        public CategoriesApiController()
        {

        }
        //http://localhost/WebVer1/umbraco/api/CategoriesApi/Get
        [HttpGet]
        public RootNhomHangBO Get()
        {
            return MemoryCacheKiot.dsNhomHang;
        }
    }
}
