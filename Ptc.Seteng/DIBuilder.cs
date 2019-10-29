using Autofac;
using AutoMapper;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    internal static class DIBuilder
    {
        public static Autofac.IContainer container;

        public static T GetObject<T>()
        {
            //建立Builder
            var builder = new ContainerBuilder();

            //選擇載入的dll
            Assembly[] assColl = new Assembly[]
            {
                //System.Reflection.Assembly.Load("Ptc.Workflow"),
                //System.Reflection.Assembly.Load("Ptc.Spcc.CCEng.Service"),
                 System.Reflection.Assembly.Load("Ptc.Seteng.Service"),

            };


            //註冊assembly
            builder.RegisterAssemblyTypes(assColl).AsImplementedInterfaces().SingleInstance();


            // Generic class 
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                   .As(typeof(Ptc.Seteng.Repository.IBaseRepository<,>))
                   .InstancePerDependency();

            //註冊log
            builder.Register(x => Ptc.Logger.Nlogger.NLogManager.CreateInstance().SystemLog).As<Ptc.Logger.Service.ISystemLog>().SingleInstance();



            builder.RegisterAssemblyTypes(assColl)
              .AsClosedTypesOf(typeof(AutoMapper.ValueResolver<,>))
              .AsSelf();

            builder.Register(x => Mapper.Engine).As<IMappingEngine>().SingleInstance();


            container = builder.Build();

            return container.Resolve<T>();
        }
    }
}
