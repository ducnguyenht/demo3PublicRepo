using Autofac;
using Umbraco.Web;
using WebVer1.Services;

namespace WebVer1
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SampleService>()
                .As<ISampleService>();

            builder.Register(c => UmbracoContext.Current).AsSelf();
        }
    }
}