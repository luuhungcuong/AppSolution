using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using AppSolution.Infrastructure.Module.LogModule;

namespace AppSolution.Infrastructure.Core
{
    public class BusinessTemplate : IApiBusinessTemplate
    {
        private IApiBusinessBlock[] BusinessBlocks { get; set; }

        public BusinessTemplate(IApiBusinessBlock[] blocks)
        {
            this.BusinessBlocks = blocks;
        }
        public async Task<OutputMsgModel> Execute(UserContext userContext, IMsgModel inpModel)
        {
            return await Task.Run(() =>
            {
                try
                {
                    AppBusinessModel inpAppModel = new AppBusinessModel(inpModel);
                    AppBusinessModel outAppModel = new AppBusinessModel();
                    new List<IApiBusinessBlock>(BusinessBlocks).ForEach((block) => 
                    {
                        Log.LogInfo(block.GetType().Name, "- Invoked");
                        block.Execute(userContext, inpAppModel, ref outAppModel);
                    });                    
                    return new OutputMsgModel()
                    {
                        Result = ProcessResult.Success,
                        Message = "Process successfull.",
                        Data = outAppModel.ToViewBag()
                    };
                }
                catch (AppException ap)
                {
                    //Log or do somthing
                    throw ap;
                }
                catch (Exception ex)
                {
                    //Log or do something
                    throw ex;
                }
            });
        }
    }
}