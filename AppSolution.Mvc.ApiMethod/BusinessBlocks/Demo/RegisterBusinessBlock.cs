using AppSolution.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Mvc.ApiMethod.Models;
using Microsoft.AspNet.Identity;

namespace AppSolution.Mvc.ApiMethod.BusinessBlocks.Demo
{
    public class RegisterBusinessBlock : IApiBusinessBlock
    {
        public void Execute(UserContext userContext, AppBusinessModel inpModel, ref AppBusinessModel outModel)
        {            
            MessageModel model = inpModel.ToModel<MessageModel>();
            userContext.Transaction.Insert<Notification>(new Notification()
            {
                Message = model.Message,
                Title = model.Title,
                Sender = "System",
                TranDate = System.DateTime.Now
            });
                        
            
            outModel = new AppBusinessModel(model);
        }
    }
}