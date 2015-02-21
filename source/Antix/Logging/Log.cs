using System;

namespace Antix.Logging
{
    public static partial class Log
    {
#if DEBUG
        public const bool DEBUG = true;
#else
        public const bool DEBUG = false;
#endif

        public enum Level
        {
            Debug,
            Information,
            Warning,
            Error,
            Fatal
        }

        static Guid Write(
            Delegate log, Level level, Action<Message> getMessage,
            Exception ex,
            string[] tags)
        {
            if (log == null) return default(Guid);

            var identifier = Guid.NewGuid();
            getMessage(log(level, identifier, ex, tags));

            return identifier;
        }

        public delegate Message Delegate(Level level, Guid id, Exception ex, string[] tags);

        public delegate void Message(string format, params object[] args);
    }
}