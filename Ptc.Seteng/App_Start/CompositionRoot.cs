using Ptc.Seteng.DI;
using Ptc.Seteng.DI.Autofac;
using Ptc.Seteng.DI.Autofac.Modules;
using Autofac;
using Autofac.Core;
using System.Web.Http;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using Ptc.Seteng.Repository;
using System.Linq;
using System;

public class CompositionRoot
{
    public static IDependencyInjectionContainer Compose()
    {


        // Create a container builder
        var builder = new ContainerBuilder();
        builder.RegisterModule(new MyModule());
        builder.RegisterModule(new MvcSiteMapProviderModule());
        builder.RegisterModule(new MvcModule());


        // Generic class 
        builder.RegisterGeneric(typeof(BaseRepository<,>))
               .As(typeof(Ptc.Seteng.Repository.IBaseRepository<,>))
               .InstancePerDependency();

        builder.RegisterFilterProvider();

        // Create the DI container
        var container = builder.Build();


        //Auto Mapper & include autoFac container
        Ptc.Seteng.Misc.AutoMapperConfig.Init(container);

        //Web 
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        //Web API
        GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);




        // Return our DI container wrapper instance
        return new AutofacDependencyInjectionContainer(container);
    }
}

