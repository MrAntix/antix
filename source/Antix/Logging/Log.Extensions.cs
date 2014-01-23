using System;

namespace Antix.Logging
{
    public static partial class Log
    {
        public static void Debug(
            this Delegate log, Level level, Action<Message> getMessage)
        {
            Write(log, Level.Debug, getMessage);
        }

        public static void Information(
            this Delegate log, Action<Message> getMessage)
        {
            Write(log, Level.Information, getMessage);
        }

        public static void Warning(
            this Delegate log, Action<Message> getMessage)
        {
            Write(log, Level.Warning, getMessage);
        }

        public static void Warning(
            this Delegate log, Action<MessageException> getMessage)
        {
            Write(log, Level.Warning, getMessage);
        }

        public static void Error(
            this Delegate log, Action<MessageException> getMessage)
        {
            Write(log, Level.Error, getMessage);
        }

        public static void Error(
            this Delegate log, Action<Message> getMessage)
        {
            Write(log, Level.Error, getMessage);
        }

        public static void Fatal(
            this Delegate log, Action<MessageException> getMessage)
        {
            Write(log, Level.Fatal, getMessage);
        }

        public static void Fatal(
            this Delegate log, Action<Message> getMessage)
        {
            Write(log, Level.Fatal, getMessage);
        }
    }
}