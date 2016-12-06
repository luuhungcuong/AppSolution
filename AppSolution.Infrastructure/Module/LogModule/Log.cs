using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Module.LogModule
{
    public sealed class Log
    {
        #region Varables/Constants        
        private const string DEBUG_LOGGER_NAME = "Business";
        private const string PROPERTY_METHOD_NAME = "MethodName";
        private const string NULL_STRING = "(null)";
        #endregion

        #region Constructors and Initializers
        
        private Log()
        {
        }

        static Log()
        {
            try
            {
                String log4netConfigPath = Path.Combine(GetDirectoryPath(Assembly.GetExecutingAssembly()), @"Config\log4net.config") ;
                if (!File.Exists(log4netConfigPath))
                {
                    throw new Exception("Logfile is not existed.");
                }
                XmlConfigurator.Configure(new FileInfo(log4netConfigPath));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }
        public static string GetDirectoryPath(Assembly assembly)
        {
            string filePath = new Uri(assembly.CodeBase).LocalPath;
            return Path.GetDirectoryName(Path.GetDirectoryName(filePath));
        }

        private static ILog GetDebugLogger()
        {
            return LogManager.GetLogger(DEBUG_LOGGER_NAME);
        }
        #endregion

        #region Private stuff

       
        private static void SetProperty(string propertyName, object propertyValue)
        {
            if (propertyValue != null)
            {
                ThreadContext.Properties[propertyName] = propertyValue;
            }
            
        }

       
        private static string GetMethodFullName(MethodBase method)
        {
            if (method == null)
            {
                return null;
            }
            return method.DeclaringType.FullName + "." + method.Name;
        }

       
        private static string GetString(object obj)
        {
            if (obj == null) return NULL_STRING;
            return obj.ToString();
        }
        #endregion

        #region Log debug

       
        public static void LogDebug(string actionName, string message, Exception exception)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsDebugEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Debug(message, exception);
            }
        }

      
        public static void LogDebug(string actionName, string message)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsDebugEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Debug(message);
            }
        }

     
        public static void LogInfo(string actionName, string message, Exception exception)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsInfoEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Info(message, exception);
            }
        }

        
        public static void LogInfo(string actionName, string message)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            ILog logger = GetDebugLogger();
            if (logger.IsInfoEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Info(message);
            }
        }

       
        public static void LogWarning(string actionName, string message, Exception exception)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsWarnEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Warn(message, exception);
            }
        }

       
        public static void LogWarnings(string actionName, string message)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            MethodBase method = stackTrace.GetFrame(1).GetMethod();
            ILog logger = GetDebugLogger();
            if (logger.IsWarnEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Warn(message);
            }
        }

      
        public static void LogError(string actionName, string message)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsErrorEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Error(message);
            }
        }

       
        public static void LogError(string actionName, string message, Exception exception)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsErrorEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Error(message, exception);
            }
        }

       
        public static void LogFatal(string actionName, string message)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsErrorEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Fatal(message);
            }
        }

       
        public static void LogFatal(string actionName, string message, Exception exception)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsFatalEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Fatal(message, exception);
            }
        }

        #endregion           
        public static void LogEnterMethod(string actionName, params object[] input)
        {            
            ILog logger = GetDebugLogger();
            if (logger.IsDebugEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Debug("Enter Method:" + GetLogString(input));                
            }
        }

       private static string GetLogString(params object[] input)
        {
            string ret = String.Empty;
            foreach(var it in input)
            {
                ret += AppSolution.Infrastructure.Utils.JsonHelper.ToString(it);             
            }
            return ret;
        }
        public static void LogLeaveMethod(string actionName, params object[] input )
        {                        
            ILog logger = GetDebugLogger();
            if (logger.IsDebugEnabled)
            {
                SetProperty(PROPERTY_METHOD_NAME, actionName);
                logger.Debug("Leave Method:" + GetLogString(input));                
            }
        }

    }
}
