using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace POC.WebApi.ExLogger
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        ILog _logger = null;
        public CustomExceptionLogger()
        {
            log4net.GlobalContext.Properties["LogFileName"] = ConfigurationManager.AppSettings["LogFileName"]; //log file path
            log4net.Config.XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                _logger.Error(context.Exception.ToString() + Environment.NewLine);
            }
            catch { }

        }
        public void Log(string ex)
        {
            try
            {
                _logger.Error(ex);
            }
            catch { }

        }
    }
}