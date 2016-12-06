using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace AppSolution.Mvc.Website
{
    public class UnityConfig
    {
        public static UnityContainer Container { get; set; }
        public static void RegisterUnity()
        {
            Container = new UnityContainer();
            Container.LoadConfiguration();      
            //Add customs class     
        }
    }
}