using System;
using System.Web;
using Antix.Logging;
using Elmah;

namespace Example.MvcApplication.App_Start.Logging
{
    public class ElmahLogAdapter : ILogAdapter
    {
        readonly ErrorLog _log;
        readonly string _name;
        readonly LogLevel _logLevel;

        public ElmahLogAdapter(
            ErrorLog log, string name, LogLevel logLevel)
        {
            _log = log;
            _name = name;
            _logLevel = logLevel;
        }

        public void Log(
            LogLevel logLevel,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> formatMessage,
            Exception ex)
        {
            var getMessage = LoggerHelper.GetMessageFunc(formatProvider, formatMessage);

            if (!IsEnabled(logLevel)) return;

            var error = ex != null
                            ? new Error(ex, HttpContext.Current)
                            : new Error();

            error.Time = DateTime.Now;
            error.Message = getMessage();
            error.Type = string.Format("{0} : {1}", _name, logLevel);
            _log.Log(error);
        }

        bool IsEnabled(LogLevel logLevel)
        {
            return (_logLevel & logLevel) == logLevel;
        }
    }
}