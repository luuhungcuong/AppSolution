using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Core
{    
    public interface IAppBusinessModel
    {
        T ToModel<T>() where T : class, new();
        dynamic ToViewBag();
        void AddParam(string key, object value);
        object GetParam(string key);
    }
}
