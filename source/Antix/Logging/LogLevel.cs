using System;

namespace Antix.Logging
{
    [Flags]
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }
}