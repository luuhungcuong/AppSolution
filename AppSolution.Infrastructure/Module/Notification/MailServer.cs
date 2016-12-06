using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
namespace AppSolution.Infrastructure.Module.Notification
{
    public class MailServer
    {
        public string ServerAddress { get; set; }
        public string FromEmail { get; set; }
        public int Port {get;set;}
        public NetworkCredential Credentical { get; set; }
    }
}