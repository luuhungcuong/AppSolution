using System.Web.Mvc;
using AppSolution.Mvc.Bootstrap.BootstrapMethods;

namespace AppSolution.Mvc.Bootstrap
{
    public static class BootstrapHtmlExtension
    {
        public static Bootstrap<TModel> Bootstrap<TModel>(this HtmlHelper<TModel> helper)
        {
            return new Bootstrap<TModel>(helper);
        }
    }
}
