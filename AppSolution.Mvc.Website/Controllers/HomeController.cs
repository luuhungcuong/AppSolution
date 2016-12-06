using System;
using System.Web.Mvc;
using AppSolution.Mvc.Website.Utils;
using AppSolution.Mvc.Website.Models;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.DataModel;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AppSolution.Infrastructure.Core;

namespace AppSolution.Mvc.Website.Controllers
{
    public class HomeController : AppBaseController
    {
        public ActionResult Blank()
        {
            return View();
        }
        
        public async Task<ActionResult> Elements()
        {
            //Call from unity    

            //ViewBag      
            IAppBusinessModel model = new AppBusinessModel();
            model.AddParam("P1", "CuongLH");
            model.AddParam("P2", System.DateTime.Now);
            model = await this.ExecuteAppBzTempate(model);

            //Model
            model = new AppBusinessModel(new LoginViewModel() { Email = "cuonglh@fpt.com.vn" });
            model = await this.ExecuteAppBzTempate(model);

            //Function
            BzFunctionHandler fun =
                 delegate (UserContext userContext , IAppBusinessModel input)
                 {
                     var d = input.ToModel<LoginViewModel>();
                     return input;
                 };            
            model = await this.ExecuteAppBzFunction(fun, model);
            //Manager class
            fun = new BzFunctionHandler(Business.SampleManager.DoSomething);
            model = await this.ExecuteAppBzFunction(fun, model);

            //Automatic mapping
            model = await this.ExecuteAppBzAction(model);

            this.ShowAppError(new AppException() { BzMessageDetail = "Co loi roi" });
            return View();
        }
        public ActionResult Tabs()
        {
            return View();
        }
        public ActionResult Modals()
        {
            return View();
        }
        public ActionResult Buttons()
        {
            return View();
        }
        public ActionResult FormLayouts()
        {
            return View();
        }
        public ActionResult FormInputs()
        {
            return View();
        }
        public ActionResult Widgets()
        {
            return View();
        }
        public ActionResult Databoxes()
        {
            return View();
        }
        public ActionResult Alerts()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FontAwesome()
        {
            return View();
        }
        public ActionResult GlyphIcons()
        {
            return View();
        }
        public ActionResult Typicons()
        {
            return View();
        }
        public ActionResult WeatherIcons()
        {
            return View();
        }
        public ActionResult NestableList()
        {
            return View();
        }
        public ActionResult TreeView()
        {
            return View();
        }
        public ActionResult SimpleTables()
        {
            return View();
        }
        public ActionResult DataTables()
        {
            return View();
        }
        public ActionResult DataPickers()
        {
            return View();
        }

        public ActionResult Wizards()
        {
            return View();
        }

        public ActionResult FormValidation()
        {
            return View();
        }
        public ActionResult FormEditors()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult FlotCharts()
        {
            return View();
        }
        public ActionResult MorrisCharts()
        {
            return View();
        }
        public ActionResult SparklineCharts()
        {
            return View();
        }
        public ActionResult EasyPieCharts()
        {
            return View();
        }
        public ActionResult ChartJS()
        {
            return View();
        }
        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult Compose()
        {
            return View();
        }
        public ActionResult ViewMessage()
        {
            return View();
        }
        public ActionResult Timeline()
        {
            return View();
        }
        public ActionResult PricingTables()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult Typography()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult Grid()
        {
            return View();
        }
        public ActionResult Persian()
        {
            return View();
        }
        public ActionResult Arabic()
        {
            return View();
        }
    }
}
