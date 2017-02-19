using HomeCinema.Web.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HomeCinema.Web
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutofacWebapiConfig.Initialize(config);
            BundleConfig.RegisterBundle(BundleTable.Bundles);
            Bootstrapper.Run();
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}