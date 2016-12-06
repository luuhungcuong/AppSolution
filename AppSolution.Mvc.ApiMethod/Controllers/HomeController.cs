using AppSolution.Mvc.ApiMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using AppSolution.Infrastructure.Core;
using AppSolution.Infrastructure.Compression;
using AppSolution.Infrastructure.Utils;
using AppSolution.Infrastructure.Module.Compression;
using RestSharp.Authenticators;

namespace AppSolution.Mvc.ApiMethod.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Demo(MessageModel m)
        {
            ViewBag.Title = "Demo";
            ViewBag.Output = "Trying call now!!!";

            var client = new RestClient("http://localhost:16834");
            client.Authenticator = new HttpBasicAuthenticator("cuonglh.hus@gmail.com", "123456");

            var request = new RestRequest("/api/App", Method.POST);
            request.AddHeader("Accept", "application/json");
            
            InputMsgModel input = new InputMsgModel();

            MessageModel model = new MessageModel() { Message = "Hello, this is message from client", Title = "This is title from client" };
            if (!String.IsNullOrEmpty(m.Message))
            {
                model = m;
            }
            
            input.Action = "Demo";

            input.EncryptData = GZipString.CompressToBase64(JsonHelper.ToString(model));

            request.AddJsonBody(input);

            IRestResponse response = client.Post(request);

            ServerResponse ret = JsonHelper.ToModel<ServerResponse>(response.Content);            

            ret.Response = GZipString.DecompressFromBase64(ret.Response);

            ViewBag.Output = JsonHelper.ToString(ret);
            ViewBag.Content = response.StatusDescription;
            return View("Index", m);
        }
    }
    public class ServerResponse
    {
        public string Response { get; set; }
    }
}
