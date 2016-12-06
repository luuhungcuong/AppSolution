using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure;
using System.Configuration;
using System.Net.Mail;
using System.Xml;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace AppSolution.Infrastructure.Module.Notification
{

    public class AppSolutionNotification
    {
        private void InvokeMessage(NotificationData data)
        {
            var linqHelper = new DbContextHelper<AppSolutionDbContext>();
            //Get notification indicator configuration
            VendorCode param = linqHelper.GetTable<VendorCode>().Where(x => x.TableID == Constants.VC001 && x.CodeID == "MESSAGE_TEMPLATE_PATH").FirstOrDefault();
            String pathMessageTemplate = String.Empty;
            if (param != null)
            {
                pathMessageTemplate = param.CodeValue;
            }
            else
            {
                throw new Exception("You need set mail template path in VC001 tables");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathMessageTemplate);
            XmlNode node = doc.DocumentElement.SelectSingleNode(String.Format("./Template[@type = '{0}']", data.NotificationType.ToString()));
            if (node != null)
            {
                //Invoke type
                data.TitleTemplate = node.SelectSingleNode("./Title").InnerText;
                data.BodyTemplate = node.SelectSingleNode("./Body").InnerXml;
            }
        }

        public void Notify(NotificationData data)
        {
            var linqHelper = new DbContextHelper<AppSolutionDbContext>();
            //Get notification indicator configuration            
            VendorCode param = linqHelper.GetTable<VendorCode>().Where(x => x.TableID == Constants.VC001 && x.CodeID == "NOTIFICATION_INDICATOR").FirstOrDefault();
            String indicator = "None";
            if (param != null)
            {
                indicator = param.CodeValue;
            }
            else
            {
                throw new Exception("You need set NOTIFICATION_INDICATOR in VC001 tables");
            }
            //Buid content message base on NotificationType
            NotificationIndicator notifyMode = (NotificationIndicator)Enum.Parse(typeof(NotificationIndicator), indicator);
            InvokeMessage(data);
            PushNotification(notifyMode, data);

        }

        private void PushNotification(NotificationIndicator notifyMode, NotificationData data)
        {
            switch (notifyMode)
            {
                case NotificationIndicator.Email:
                    SendEmail(data);
                    break;
                case NotificationIndicator.SMS:
                    SendSMS(data);
                    break;
                case NotificationIndicator.Both:
                    SendEmail(data);
                    SendSMS(data);
                    break;
                default:
                    return;
            }

        }
        private void SendEmail(NotificationData data)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = MailServerFactory.GetInstance();
            mail.From = new MailAddress(MailServerFactory.MailServer.FromEmail);
            mail.IsBodyHtml = true;
            foreach (string user in data.ToList)
            {
                mail.To.Add(user);
            }
            foreach (string user in data.CcList)
            {
                mail.CC.Add(user);
            }
            mail.Subject = data.GetTitle();
            mail.Body = data.GetBody();
            ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };
            SmtpServer.Send(mail);
        }

        private void SendSMS(NotificationData data)
        {

        }
    }
}