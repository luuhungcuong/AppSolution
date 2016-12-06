using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppSolution.Mvc.Website.Models;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.Core;

namespace AppSolution.Mvc.Website.Business
{    
    public class DemoBzTemplate : IMvcBusinessTemplate
    {                
        public IAppBusinessModel Execute(UserContext userContext, IAppBusinessModel input)
        {
            var d = input.ToModel<LoginViewModel>();
            return input;
        }               
    }
    public class SampleManager
    {
        public static IAppBusinessModel DoSomething(UserContext userContext, IAppBusinessModel input)
        {
            var d = input.ToModel<LoginViewModel>();
            return input;
        }
    }

    public partial class BusinessAction : IAppBusinessAction
    {
        public static IAppBusinessModel Elements_Home(UserContext userContext, IAppBusinessModel input)
        {
            var d = input.ToModel<LoginViewModel>();
            return input;
        }
    }
}