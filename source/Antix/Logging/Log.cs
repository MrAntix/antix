using System;
using System.ComponentModel;

namespace Antix.Logging
{
    public static partial class Log
    {
        public enum Level
        {
            Debug,
            Information,
            Warning,
            Error,
            Fatal
        }

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

        public delegate MessageException Delegate(Level level);

        public delegate void Message(string format, params object[] args);

        public delegate void MessageException(Exception ex, string format, params object[] args);
    }
}