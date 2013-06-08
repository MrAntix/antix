using System;

namespace Antix.Logging
{
    public static class LoggerExtensionsError
    {
        public static void Error(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage,
            Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Error, formatProvider, getMessage, ex);
        }

        public static void Error(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Error, formatProvider, getMessage, null);
        }

        public static void Error(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Error, null, getMessage, null);
        }

        public static void Error(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage, Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Error, null, getMessage, ex);
        }
    }
}