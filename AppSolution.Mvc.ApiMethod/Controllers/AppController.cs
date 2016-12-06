using AppSolution.Infrastructure.Core;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Utils;
using AppSolution.Infrastructure.Compression;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AppSolution.Infrastructure.Module.Compression;
using AppSolution.Mvc.ApiMethod.Filters;
using AppSolution.Infrastructure.Module.LogModule;

namespace AppSolution.Mvc.ApiMethod.Controllers
{
    public class AppController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(OutputMsgModel))]
        [IdentityBasicAuthenticationAttribute]        
        [Authorize]
        public async Task<IHttpActionResult> Post(InputMsgModel msg)
        {
            Log.LogEnterMethod(msg.Action, msg);
            OutputMsgModel output = new OutputMsgModel();
            ICollection<ValidationResult> results = null;
            DbContextHelper<AppSolutionDbContext> db = null;
            try
            {
                //Check validate input action
                if (!ValidatorHelper.Validate<InputMsgModel>(msg, out results))
                {
                    output.Result = ProcessResult.Failure;
                    output.Message = "Invalid messsage header.";
                }
                else
                {
                    string action = msg.Action;
                    //Convert input msg                 
                    IMsgModel input = (IMsgModel)UnityConfig.Container.Resolve(typeof(IMsgModel), action);
                    IMsgModel inputModel = (IMsgModel)JsonHelper.ToObject(
                       GZipString.DecompressFromBase64(msg.EncryptData), input.GetType());
                    //Validate input model                
                    if (!ValidatorHelper.Validate(inputModel, out results))
                    {
                        output.Result = ProcessResult.Failure;
                        output.Message = "Invalid message body"; //Do something with result
                    }
                    else
                    {
                        //Get business object from action   
                        IApiBusinessTemplate bz = (IApiBusinessTemplate)UnityConfig.Container.Resolve(typeof(IApiBusinessTemplate), action);
                        //Invoke business
                        db = new DbContextHelper<AppSolutionDbContext>();
                        try
                        {
                            db.BeginTransaction();
                            //Add History
                            var actionHistory = db.GetOne<ActionLog>(x => x.UserID == User.Identity.Name && x.ActionID == action);
                            if (actionHistory != null)
                            {
                                //Check limited action    

                                //Check times use this function

                                //Check waiting time for this function                              
                            }
                            //Update times                            

                            actionHistory = new ActionLog()
                            {
                                UserID = User.Identity.Name?? "Anonymous",
                                ActionID = action,
                                TranDate = DateTime.Now,
                            };
                            db.Insert<ActionLog>(
                                actionHistory
                            );

                            //if ok allow access to bz
                            output = await bz.Execute(new UserContext() {Transaction = db, UserName=User.Identity.Name}, inputModel);                            
                        }
                        catch (AppException ap)
                        {
                            Log.LogError(msg.Action,ap.BzMessageDetail, ap);
                            output.Result = ProcessResult.Failure;
                            output.Message = ap.BzMessageDetail;
                        }
                        catch (Exception ex)
                        {
                            Log.LogError(msg.Action, ex.Message, ex);
                            output.Result = ProcessResult.Failure;
                            output.Message = ex.ToString();
                        }
                        finally
                        {
                            if (output.Result == ProcessResult.Failure)
                            {
                                db.RollbackTransaction();
                            }
                            else
                            {
                                db.CommitTransaction();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError(msg.Action, ex.Message, ex);
                output.Result = ProcessResult.Failure;
                output.Message = ex.ToString();
            }
            Log.LogLeaveMethod(msg.Action, msg);
            return Ok(new
            {              
                Response = GZipString.CompressToBase64(
               JsonHelper.ToString(output))
            });
        }
    }
}
