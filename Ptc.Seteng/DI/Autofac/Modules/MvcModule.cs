using System.Web.Mvc;
using Autofac;
using MvcSiteMapProvider.Web.Mvc;
using RequestGetImg;

namespace Ptc.Seteng.DI.Autofac.Modules
{
    public class MvcModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentAssembly = typeof(MvcModule).Assembly;

            //builder.RegisterType<GetCallLogImgForApi>().As<IProcessCallLogFile>();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => typeof(IController).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
