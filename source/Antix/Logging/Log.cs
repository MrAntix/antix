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

        public delegate Message Delegate(Level level, Guid id, Exception ex, string[] tags);

        public delegate void Message(string format, params object[] args);
    }
}