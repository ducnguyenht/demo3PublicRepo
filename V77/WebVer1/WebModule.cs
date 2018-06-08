using Autofac;
using Umbraco.Web;
using WebVer1.Services;
using WebVer1.Services.CartService;
using WebVer1.Services.ChiNhanhService;
using WebVer1.Services.DatHangService;
using WebVer1.Services.HangHoaService;
using WebVer1.Services.NhomHangService;

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
            builder.RegisterType<ChiNhanhService>()
               .As<IChiNhanhService>();
            builder.RegisterType<DatHangService>()
               .As<IDatHangService>();
            builder.RegisterType<HangHoaService>()
               .As<IHangHoaService>();
            builder.RegisterType<NhomHangService>()
                .As<INhomHangService>();
            builder.RegisterSource(new Autofac.Integration.Mvc.ViewRegistrationSource());
            builder.Register(c => UmbracoContext.Current).AsSelf();
        }
    }
}