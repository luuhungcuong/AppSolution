using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace AppSolution.Mvc.ApiMethod
{
    public static class UnityConfig
    {
        public static UnityContainer Container { get; set; }
        public static void RegisterComponents()
        {
			Container = new UnityContainer();
            Container.LoadConfiguration();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();                      
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}