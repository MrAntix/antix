using System;

namespace Antix.Logging
{
    public static partial class Log
    {
        public class Event
        {
            readonly Level _level;
            readonly Exception _exception;
            readonly string _format;
            readonly object[] _args;

            readonly DateTime _on;

            public Event(
                Level level,
                Exception exception,
                string format, object[] args)
            {
                _level = level;
                _exception = exception;
                _format = format;
                _args = args;

                _on = DateTime.UtcNow;
            }

            public Level Level
            {
                get { return _level; }
            }

            public Exception Exception
            {
                get { return _exception; }
            }

            public string Format
            {
                get { return _format; }
            }

            public object[] Args
            {
                get { return _args; }
            }

            public DateTime On
            {
                get { return _on; }
            }
        }
    }
}