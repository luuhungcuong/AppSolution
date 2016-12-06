using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppSolution.Infrastructure.Module.Notification
{
    public class Constants
    {
        public const string VC001 = "VC001";
    }
    public enum NotificationIndicator
    {
        None = 1,
        Email = 2,
        SMS = 3,
        Both = 4
    }
    public enum NotificationType
    {
        Register = 1,
        PH = 2,
        GH = 3,
        CWallet = 4,
        PG = 5
    }
}