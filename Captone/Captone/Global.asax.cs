using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Captone.App_Start;
using Captone.Services;
using Captone.Services.Interfaces;
using Ninject;

namespace Captone
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly Timer Timer = new Timer(30000); // This will raise the event every one minute.
        public static IKernel Kernel;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Kernel = NinjectWebCommon.CreateKernel();
            Timer.Enabled = true;
            Timer.Elapsed += NinjectWebCommon.Timer_Elapsed;
        }
    }
}