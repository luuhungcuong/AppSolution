using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using AppSolution.Infrastructure.Core;
using System.Reflection;
using AppSolution.Infrastructure.Module.LogModule;
using System.Text;

namespace AppSolution.Mvc.Website.Utils
{
    public class AppBaseController : Controller
    {
        #region Transaction
        protected DbContextHelper<AppSolutionDbContext> Trans { get; set; }

        protected void InitDataContext()
        {
            if (Trans == null) Trans = new DbContextHelper<AppSolutionDbContext>();
        }
        protected void StartTransaction()
        {
            InitDataContext();
            Trans.BeginTransaction();
        }
        protected void CommitTransaction()
        {
            if (Trans != null)
            {
                Trans.CommitTransaction();
                Trans = null;
            }
        }
        protected void RollbackTransaction()
        {
            if (Trans != null)
            {
                Trans.RollbackTransaction();
                Trans = null;
            }
        }
        #endregion

        #region Notification
        public void ShowSuccess(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void ShowInformation(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void ShowWarning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void ShowDanger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }
        public void ShowUnknowError(Exception ex, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, "Đã có lỗi xảy ra trong hệ thống. Xin vui lòng liên hệ quản trị hệ thống.", ex.ToString(), dismissable);
        }
        public void ShowAppError(AppException ex, bool dismissable = false)
        {
            AddAlert(ex.ErrorType.ToString().ToLower(), ex.BzMessageDetail, ex.ToString(), dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }
        private void AddAlert(string alertStyle, string message, string messagedetail, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                MessageDetail = messagedetail
            });

            TempData[Alert.TempDataKey] = alerts;
        }
        #endregion

        #region Utility
        public string ControllerName
        {
            get
            {
                return this.ControllerContext.RouteData.Values["controller"].ToString();                
            }
        }
        public string ActionName
        {
            get
            {
                return this.ControllerContext.RouteData.Values["action"].ToString();
            }
        }
        public string UnityActionID
        {
            get
            {
                string functionid = String.Format("{0},{1}", this.ActionName, this.ControllerName);
                return functionid;
            }
        }
        public string ActionID
        {
            get
            {
                string functionid = String.Format("{0}_{1}", this.ActionName, this.ControllerName);
                return functionid;
            }
        }
        public static string GetRequiredRoles(string actionId)
        {
            StringBuilder roles = new StringBuilder();
            using (var helper = new DbContextHelper<AppSolutionDbContext>())
            {
                var lstRoles = helper.GetTable<FunctionRoles>().Where(x => x.FunctionId == actionId);
                foreach (var it in lstRoles)
                {
                    roles.Append(",");
                    roles.Append(it.RoleId);                    
                }
            }
            string ret = roles.ToString();        
            return ret.Length > 0? ret.Substring(1) : string.Empty;
        }
        #endregion

        #region Business executing                     
        public async Task<IAppBusinessModel> ExecuteAppBzFunction(BzFunctionHandler func,
          IAppBusinessModel inputModel)
        {
            return await Task.Run(() =>
            {
                Log.LogEnterMethod(this.ActionID, inputModel.ToViewBag());
                IAppBusinessModel ret = UnityConfig.Container.Resolve<IAppBusinessModel>();
                try
                {
                    this.StartTransaction();
                    ret = func.Invoke(new UserContext() { Transaction = this.Trans, UserName = User.Identity.Name }, inputModel);
                    this.CommitTransaction();
                }
                catch (AppException ap)
                {
                    Log.LogError(this.ActionID, ap.BzMessageDetail, ap);
                    this.ShowAppError(ap);
                    if (ap.ProcessResult == ProcessResult.Failure)
                    {
                        this.RollbackTransaction();
                    }
                    return Task.FromResult(ret);
                }
                catch (Exception ex)
                {
                    Log.LogError(this.ActionID, ex.Message, ex);
                    this.ShowUnknowError(ex);
                    this.RollbackTransaction();
                    return Task.FromResult(ret);
                }
                finally
                {
                    Log.LogLeaveMethod(this.ActionID, ret.ToViewBag());                   
                }                
                return Task.FromResult(ret);
            });
        }
        public async Task<IAppBusinessModel> ExecuteAppBzTempate(IAppBusinessModel model)
        {
            return await Task.Run(() =>
            {
                Log.LogEnterMethod(this.ActionID, model.ToViewBag());
                IAppBusinessModel ret = UnityConfig.Container.Resolve<IAppBusinessModel>();
                try
                {
                    this.StartTransaction();
                    IMvcBusinessTemplate bz = UnityConfig.Container.Resolve<IMvcBusinessTemplate>(this.UnityActionID);
                    ret = bz.Execute(new UserContext() {Transaction=this.Trans, UserName=User.Identity.Name} ,model);
                    this.CommitTransaction();
                }
                catch (AppException ap)
                {
                    Log.LogError(this.ActionID, ap.BzMessageDetail, ap);
                    this.ShowAppError(ap);
                    if (ap.ProcessResult == ProcessResult.Failure)
                    {
                        this.RollbackTransaction();
                    }
                    return Task.FromResult(ret);
                }
                catch (Exception ex)
                {
                    Log.LogError(this.ActionID, ex.Message, ex);
                    this.ShowUnknowError(ex);
                    this.RollbackTransaction();
                    return Task.FromResult(ret);
                }
                finally
                {
                    Log.LogLeaveMethod(this.ActionID, ret.ToViewBag());
                }                            
                return Task.FromResult(ret);
            });
        }
        public async Task<IAppBusinessModel> ExecuteAppBzAction(IAppBusinessModel model)
        {
            return await Task.Run(() =>
            {
                Log.LogEnterMethod(this.ActionID, model.ToViewBag());
                IAppBusinessModel ret = UnityConfig.Container.Resolve<IAppBusinessModel>();
                try
                {
                    this.StartTransaction();
                    IAppBusinessAction bz = UnityConfig.Container.Resolve<IAppBusinessAction>();
                    MethodInfo methodInfo = bz.GetType().GetMethod(this.ActionID);
                    ret = (IAppBusinessModel)methodInfo.Invoke(bz, new object[] {new UserContext() {Transaction=this.Trans, UserName=User.Identity.Name}, model});                    
                }
                catch (AppException ap)
                {
                    Log.LogError(this.ActionID, ap.BzMessageDetail, ap);
                    this.ShowAppError(ap);
                    if (ap.ProcessResult == ProcessResult.Failure)
                    {
                        this.RollbackTransaction();
                    }
                    return Task.FromResult(ret);
                }
                catch (Exception ex)
                {
                    Log.LogError(this.ActionID, ex.Message, ex);
                    this.ShowUnknowError(ex);
                    this.RollbackTransaction();
                    return Task.FromResult(ret);
                }
                finally
                {
                    Log.LogLeaveMethod(this.ActionID, ret.ToViewBag());
                }
                return Task.FromResult(ret);
            });
        }

    }
    #endregion
}