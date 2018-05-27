using Autofac;
using Umbraco.Web;
using WebVer1.Services;
using WebVer1.Services.CartService;

namespace WebVer1
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SampleService>()
                .As<ISampleService>();
            builder.RegisterType<CartService>()
               .As<ICartService>();

            builder.Register(c => UmbracoContext.Current).AsSelf();
        }
    }
}