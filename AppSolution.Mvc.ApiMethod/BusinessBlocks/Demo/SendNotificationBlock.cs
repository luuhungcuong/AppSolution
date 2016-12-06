using AppSolution.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure.Module.Notification;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace AppSolution.Mvc.ApiMethod.BusinessBlocks.Demo
{
    public class SendNotificationBlock : IApiBusinessBlock
    {
        public void Execute(UserContext userContext, AppBusinessModel inpModel, ref AppBusinessModel outModel)
        {
            try
            {
                AppSolutionNotification notification = new AppSolutionNotification();
                //notification.Notify(new NotificationData()
                //{
                //    NotificationType = NotificationType.Register
                //});
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = MailServerFactory.GetInstance();
                mail.From = new MailAddress(MailServerFactory.MailServer.FromEmail);
                mail.IsBodyHtml = true;
                
                mail.To.Add("cuonglh@fpt.com.vn");

                //mail.CC.Add("");

                mail.Subject = inpModel.GetParam("Title") as string;
                mail.Body = inpModel.GetParam("Message") as string;
                ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw new AppException() {BaseException = ex, BzMessageDetail="Cannot send email to user"};
            }            
        }
    }
}