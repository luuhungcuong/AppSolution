using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using AppSolution.Infrastructure;
using System.Xml;
using System.Net.Mail;
namespace AppSolution.Infrastructure.Module.Notification
{
    public class MailServerFactory
    {
        private static SmtpClient instance;

        public static MailServer MailServer { get; set; }
       
        public static SmtpClient GetInstance()
        {
            if (instance != null) return instance;
            else
            {
                var linqHelper = new DbContextHelper<AppSolutionDbContext>();
                //Get notification indicator configuration
                VendorCode param = linqHelper.GetTable<VendorCode>().Where(x => x.TableID == Constants.VC001 && x.CodeID == "MAIL_SERVER_PATH").FirstOrDefault();                
                String serverConfig = String.Empty;
                if (param != null)
                {
                    serverConfig = param.CodeValue;
                }
                else
                {
                    throw new Exception("You need set MAIL_SERVER_PATH in VC001 tables");
                }
                param = linqHelper.GetTable<VendorCode>().Where(x => x.TableID == Constants.VC001 && x.CodeID == "MAIL_SERVER_ID").FirstOrDefault();                   
                String serverID = String.Empty;
                if (param != null)
                {
                    serverID = param.CodeValue;
                }
                else
                {
                    throw new Exception("You need set MAIL_SERVER_PATH in VC001 tables");
                }

                MailServer mailServer = new MailServer();
                
                XmlDocument doc = new XmlDocument();
                doc.Load(serverConfig);
                XmlNode serverNode= doc.DocumentElement.SelectSingleNode(String.Format(".//Server[@ID='{0}']", serverID));

                XmlNode serverAddressNode = serverNode.SelectSingleNode("./ServerAddress");                
                mailServer.ServerAddress = serverAddressNode.InnerText;

                XmlNode portNode = serverNode.SelectSingleNode("./Port");
                mailServer.Port = Convert.ToInt32(portNode.InnerText);

                XmlNode fromEmailNode = serverNode.SelectSingleNode("./FromEmail");
                mailServer.FromEmail = fromEmailNode.InnerText;

                XmlNode useSSL = serverNode.SelectSingleNode("./UseSSL");
                XmlNode useDefaultCredentials = serverNode.SelectSingleNode("./UseDefaultCredentials");
                
                XmlNode userNameNode = serverNode.SelectSingleNode("./Credentials/UserName");
                XmlNode passwordNode = serverNode.SelectSingleNode("./Credentials/Password");
                mailServer.Credentical = new System.Net.NetworkCredential()
                {
                    UserName = userNameNode.InnerText,
                    Password = passwordNode.InnerText
                };
                SmtpClient client = new SmtpClient()
                {
                    Host = mailServer.ServerAddress,                    
                    Port = mailServer.Port,
                    EnableSsl = useSSL.InnerText.ToUpper() == "TRUE",                    
                };
                if (useDefaultCredentials.InnerText != "*")
                {
                    client.UseDefaultCredentials = useDefaultCredentials.InnerText.ToUpper() == "TRUE";
                }
                client.Credentials = mailServer.Credentical;

                instance = client;
                MailServer = mailServer;
                return instance;
            }
        }
    }
}