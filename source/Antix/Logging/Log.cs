using System;

namespace Antix.Logging
{
    public static class Log
    {
        const string CONSOLE_MESSAGE_FORMAT = "{0:mm:ss:ffff} [{1}]: {2}";

        public static readonly Delegate ToConsole
            = l => (ex, f, a) =>
                {
                    var m = string.Format(f, a);
                    Console.WriteLine(
                        CONSOLE_MESSAGE_FORMAT, DateTime.UtcNow.Millisecond, l, m);
                    if (ex != null)
                    {
                        Console.WriteLine(ex);
                    }
                };

        static void Write(
            Delegate log, Level level, Action<MessageException> getMessage)
        {
            if (log == null) return;

            getMessage(log(level));
        }

        static void Write(
            Delegate log, Level level, Action<Message> getMessage)
        {
            Write(log, level, (MessageException m) => getMessage((f, a) => m(null, f, a)));
        }

        public delegate void Message(string format, params object[] args);

        public delegate void MessageException(Exception ex, string format, params object[] args);

        public delegate MessageException Delegate(Level level);

        public enum Level
        {
            Debug,
            Information,
            Warning,
            Error,
            Fatal
        }

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