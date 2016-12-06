using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Core
{
    public interface IAppBusinessAction
    {

    }

    public delegate IAppBusinessModel
        BzFunctionHandler(UserContext userContext,IAppBusinessModel inputModel);
}
