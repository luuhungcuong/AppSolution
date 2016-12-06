using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using AppSolution.Infrastructure;
using AppSolution.Infrastructure.DataModel;
namespace AppSolution.Infrastructure.Module.Notification
{
    public class NotificationData
    {
        /// <summary>
        /// Set/Get To List
        /// </summary>
        public List<String> ToList { get; set; }
        /// <summary>
        /// Set/Get Cc List
        /// </summary>
        public List<String> CcList { get; set; }
        /// <summary>
        /// Set/Get Notification type
        /// </summary>
        public NotificationType NotificationType { get; set; }
        /// <summary>
        /// Set/get params for title
        /// </summary>
        public List<String> TitleParams { get; set; }
        /// <summary>
        /// Set/get params for body
        /// </summary>
        public List<String> BodyParams { get; set; }
        
        /// <summary>
        /// Title template. Dont need set value for this property
        /// </summary>
        public String TitleTemplate { get; set; }
        /// <summary>
        /// Body template. Dont need set value for this property
        /// </summary>
        public String BodyTemplate { get; set; }
        
        public NotificationData()
        {
            TitleParams = new List<string>();
            BodyParams = new List<string>();
            ToList = new List<String>();
            CcList = new List<String>();
        }

        public String GetToList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in ToList)
            {
                sb.Append(s);
                sb.Append(";");
            }
            return sb.ToString();
        }
        public String GetCcList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in CcList)
            {
                sb.Append(s);
                sb.Append(";");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return title of message
        /// </summary>
        /// <returns></returns>
        public String GetTitle()
        {

            return String.Format(this.TitleTemplate, 
                (String[])this.TitleParams.ToArray<String>()).Replace('\r', ' ').Replace('\n', ' '); 
        }
        /// <summary>
        /// Return body of message
        /// </summary>
        /// <returns></returns>
        public String GetBody()
        {
            return String.Format(this.BodyTemplate, 
                (String[])this.BodyParams.ToArray<String>());
        }
    }
}