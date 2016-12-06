using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AppSolution.Mvc.Website.Startup))]
namespace AppSolution.Mvc.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
