using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using VkInstruments.Core;
using VkInstruments.Core.VkSystem;

namespace VkInstruments.MVC.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<VkSystem>().As<IVkSystem>().SingleInstance();
            builder.RegisterType<VkService>().As<IVkService>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}