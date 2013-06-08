using System;
using System.ComponentModel;

namespace Antix.Logging
{
    public interface ILogAdapter
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Log(LogLevel logLevel,
                 IFormatProvider formatProvider, Func<LogMessageDelegate, string> getMessage,
                 Exception ex);
    }
}