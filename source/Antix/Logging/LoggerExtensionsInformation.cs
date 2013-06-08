using System;

namespace Antix.Logging
{
    public static class LoggerExtensionsInformation
    {
        public static void Information(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage,
            Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Information, formatProvider, getMessage, ex);
        }

        public static void Information(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Information, formatProvider, getMessage, null);
        }

        public static void Information(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Information, null, getMessage, null);
        }

        public static void Information(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage, Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Information, null, getMessage, ex);
        }
    }
}