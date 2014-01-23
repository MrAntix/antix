using System;
using System.Collections.Generic;

namespace Antix.Logging
{
    public static partial class Log
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

        public static Delegate ToList(List<Event> list)
        {
            return
                l => (ex, f, a) => list.Add(new Event(l, ex, f, a));
        }
    }
}