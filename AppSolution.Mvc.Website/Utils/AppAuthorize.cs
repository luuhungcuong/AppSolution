using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppSolution.Mvc.Website.Utils
{
    public class AppAuthorize : AuthorizeAttribute
    {
        public AppAuthorize() :base()
        {         
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticated = false;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                // here I will check users exists in database.
                // if yes , isAuthenticated=true;
                
            }
            return isAuthenticated;
        }
        
    }
}