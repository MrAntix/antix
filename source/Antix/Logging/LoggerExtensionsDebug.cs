using System;

namespace Antix.Logging
{
    public static class LoggerExtensionsDebug
    {
        public static void Debug(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage,
            Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Debug, formatProvider, getMessage, ex);
        }

        public static void Debug(
            this ILogAdapter logAdapter,
            IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Debug, formatProvider, getMessage, null);
        }

        public static void Debug(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Debug, null, getMessage, null);
        }

        public static void Debug(
            this ILogAdapter logAdapter,
            Func<LogMessageDelegate, string> getMessage, Exception ex)
        {
            if (logAdapter == null) return;

            logAdapter.Log(LogLevel.Debug, null, getMessage, ex);
        }
    }
}