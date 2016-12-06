using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppSolution.Mvc.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        Infrastructure.Module.WebSocket.BroadcastServerManager server = null;
        protected void Application_Start()
        {
            UnityConfig.RegisterUnity();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var server = new Infrastructure.Module.WebSocket.BroadcastServerManager();
            server.StartSuperWebSocketByConfig();
        }
        protected void Application_End()
        {
            if (server != null)
            {
                server.StopServer();
            }
        }

    }
}
