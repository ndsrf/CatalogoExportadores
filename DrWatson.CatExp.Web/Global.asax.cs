using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DrWatson.CatExp.Data.Infrastructura;
using DrWatson.CatExp.Web.Infrastructura;

namespace DrWatson.CatExp.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            // Unity!
            var unity = new UnityContainer();
            ContainerBootstrapper.RegisterTypes(unity);
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(unity);
        }
    }
}
