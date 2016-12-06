using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Core
{        
    public class AppBusinessModel : IAppBusinessModel
    {
        private Dictionary<string, object> Container { get; set; }

        public void AddParam(string key, object value)
        {
            this.Container.Add(key, value);
        }

        public object GetParam(string key)
        {
            return this.Container[key];
        }

        public T ToModel<T>() where T :class, new()
        {
            return this.Container.ToObject<T>();                               
        }
        public AppBusinessModel()
        {
            Container = new Dictionary<string, object>();
        }
        public AppBusinessModel(object model)
        {
            Container = model.ToDictionary() as Dictionary<String, object>;
        }
        
        public dynamic ToViewBag()
        {
            return this.Container.ToDynamic();
        }        
    }   
}
