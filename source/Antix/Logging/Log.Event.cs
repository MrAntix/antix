using System;

namespace Antix.Logging
{
    public static partial class Log
    {
        public class Event
        {
            readonly Guid _id;
            readonly Level _level;
            readonly Exception _exception;
            readonly string _message;
            readonly string[] _tags;

            readonly DateTime _on;

            public Event(
                Guid id,
                Level level,
                Exception exception,
                string message,
                string[] tags)
            {
                _id = id;
                _level = level;
                _exception = exception;
                _message = message;
                _tags = tags;

                _on = DateTime.UtcNow;
            }

            public Guid Id
            {
                get { return _id; }
            }

            public Level Level
            {
                get { return _level; }
            }

            public Exception Exception
            {
                get { return _exception; }
            }

            public string Message
            {
                get { return _message; }
            }

            public DateTime On
            {
                get { return _on; }
            }

            public string[] Tags
            {
                get { return _tags; }
            }
        }
    }
}