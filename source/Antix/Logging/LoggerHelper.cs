using System;

namespace Antix.Logging
{
    public class LoggerHelper
    {
        public static Func<string> GetMessageFunc(
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> formatMessage)
        {
            if (formatProvider != null)
            {
                var fp = formatProvider;
                return () => formatMessage((format, args) =>
                                           args == null || args.Length == 0
                                               ? string.Format(fp, "{0}", format)
                                               : string.Format(fp, format, args));
            }

            return () => formatMessage((format, args) =>
                                       args == null || args.Length == 0
                                           ? string.Format("{0}", format)
                                           : string.Format(format, args));
        }
    }
}