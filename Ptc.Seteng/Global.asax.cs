
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.Seteng;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ptc.Seteng
{
    public class MvcApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// 程序进入点
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //输出错误讯息
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //准备MENU资料
            SiteMenu.Init(DefaultRootColl());

        }


        protected void Application_Error()
        {

            //TODO 程序最后例外处理


            Exception unhandledException = Server.GetLastError();

            //输出LOG
            //if (container != null)
            //{
            //    _logger = container.Resolve<Ptc.Logger.Service.ISystemLog>();
            //    _logger.Error(unhandledException);
            //}

            HttpException httpException = unhandledException as HttpException;
            if (httpException == null)
            {
                Exception innerException = unhandledException.InnerException;
                httpException = innerException as HttpException;
            }

            if (httpException != null)
            {
                int httpCode = httpException.GetHttpCode();
                //switch (httpCode)
                //{
                //    //case (int)HttpStatusCode.Unauthorized:
                //    //    Response.Redirect("/Http/Error401");
                //    //    break;
                //}
            }
        }

        /// <summary>
        /// menu
        /// </summary>
        /// <returns></returns>
        private List<MenuPrefixNode> DefaultRootColl()
        {
            List<MenuPrefixNode> rootColl = new List<MenuPrefixNode>();

            //TODO MenuNode取得方式
            //依controller->  accountController -> account
            //依controller内的groupname->accountController GroupName -> account_GroupName
            //其它如下例说明         

            rootColl.Add(new MenuPrefixNode()
            {
                Description = "推播帳號管理",
                Id = "home",
                Name = "推播帳號管理",
                ParentName = string.Empty,
                IconCss = "icon-star-half-full",

            });

            rootColl.Add(new MenuPrefixNode()
            {
                Description = "未完修案件管理",
                Id = "unFinishCaseManagement",
                Name = "未完修案件管理",
                ParentName = string.Empty,
                IconCss = "icon-star-half-full",

            });


            return rootColl;
        }

    }
}

