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
            Delegate log, Level level, Action<MessageException> getMessage,
            string[] tags)
        {
            if (log == null) return default(Guid);

            var identifier = Guid.NewGuid();
            getMessage(log(level, identifier, tags));

            return identifier;
        }

        static Guid Write(
            Delegate log, Level level, Action<Message> getMessage,
            string[] tags)
        {
            return Write(
                log, level,
                (MessageException m) => getMessage((f, a) => m(null, f, a)),
                tags);
        }

        public delegate MessageException Delegate(Level level, Guid id, string[] tags);

        public delegate void Message(string format, params object[] args);

        public delegate void MessageException(Exception ex, string format, params object[] args);
    }
}