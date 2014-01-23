using System;
using System.Web;

using Antix.Logging;

using Elmah;

namespace Example.MvcApplication.Logging
{
    public class ElmahLog
    {
        readonly ErrorLog _log;
        readonly string _name;
        readonly Log.Level _logLevel;

        public ElmahLog(
            ErrorLog log, string name, Log.Level logLevel)
        {
            _log = log;
            _name = name;
            _logLevel = logLevel;
        }

        public Log.MessageException Delegate(Log.Level level)
        {
            return IsEnabled(level)
                       ? (ex, format, args) =>
                           {
                               var error = ex != null
                                               ? new Error(ex, HttpContext.Current)
                                               : new Error();

                               error.Time = DateTime.Now;
                               error.Message = string.Format(format, args);
                               error.Type = string.Format("{0} : {1}", _name, level);

                               _log.Log(error);
                           }

                       : (Log.MessageException) NullLogMethod;
        }

        static void NullLogMethod(Exception ex, string format, object[] args)
        {
        }

        bool IsEnabled(Log.Level logLevel)
        {
            return (_logLevel & logLevel) == logLevel;
        }
    }
}