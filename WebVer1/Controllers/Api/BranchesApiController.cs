using KiotVietBO;
using System.Web.Http;
using Umbraco.Web.WebApi;
using WebVer1.Services;

namespace WebVer1.Controllers.Api
{
    public class BranchesApiController : UmbracoApiController
    {
        public BranchesApiController()
        {

        }
        //http://localhost/WebVer1/umbraco/api/BranchesApi/Get
        [HttpGet]
        public RootChiNhanhBO Get()
        {
            return MemoryCacheKiot.dsChiNhanh;
        }
    }
}
