using System;

namespace Antix.Logging
{
    public static class LoggerExtensionsWarning
    {
        public static void Warning(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage,
            Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Warning, formatProvider, getMessage, ex);
        }

        public static void Warning(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Warning, formatProvider, getMessage, null);
        }

        public static void Warning(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Warning, null, getMessage, null);
        }

        public static void Warning(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage, Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Warning, null, getMessage, ex);
        }
    }
}