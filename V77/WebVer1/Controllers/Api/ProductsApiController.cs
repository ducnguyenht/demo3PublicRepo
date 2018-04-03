using KiotVietBO;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace WebVer1.Controllers.Api
{
    public class ProductsApiController : UmbracoApiController
    {
        public ProductsApiController()
        {

        }
        //http://localhost:60580/umbraco/api/ProductsApi/Get
        [HttpGet]
        public RootChiTietHangHoaBO Get()
        {
            return MemoryCacheKiot.dsHangHoa;
        }
    }
}