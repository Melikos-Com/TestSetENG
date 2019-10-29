using System.Web.Mvc;
using Autofac;
using MvcSiteMapProvider.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Ptc.Seteng.Repository;

namespace Ptc.Seteng.DI.Autofac.Modules
{
    public class MyModule
        : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //��ܸ��J��dll
            System.Reflection.Assembly[] assColl = new System.Reflection.Assembly[]
            {
                //System.Reflection.Assembly.Load("Ptc.Workflow"),
                System.Reflection.Assembly.Load("Ptc.Seteng.Service"),
                System.Reflection.Assembly.Load("Ptc.AspNetMvc.Menu.SiteMap")

            };

            //���Uassembly
            builder.RegisterAssemblyTypes(assColl).AsImplementedInterfaces().SingleInstance();

            //���oControlller��m
            builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());

            // Generic class 
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                   .As(typeof(Ptc.Seteng.Repository.IBaseRepository<,>))
                   .InstancePerDependency();

            //���Ulog
            builder.Register(x => Ptc.Logger.Nlogger.NLogManager.CreateInstance().SystemLog).As<Ptc.Logger.Service.ISystemLog>().SingleInstance();


            //���U��filter�W
            builder.RegisterFilterProvider();


            builder.RegisterAssemblyTypes(assColl)
              .AsClosedTypesOf(typeof(AutoMapper.ValueResolver<,>))
              .AsSelf();

            builder.Register(x => Mapper.Engine).As<IMappingEngine>().SingleInstance();


        }

    }
}
