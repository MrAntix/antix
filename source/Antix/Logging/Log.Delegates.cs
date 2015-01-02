using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Antix.Logging
{
    public static partial class Log
    {
        const string MESSAGE_FORMAT = "{0:yyyy-MM-dd hh:mm:ss:ffff} [{1}]: {2}";

        public static readonly Delegate ToConsole
            = l => (ex, f, a) =>
            {
                var m = string.Format(f, a);
                Console.WriteLine(
                    MESSAGE_FORMAT, DateTime.UtcNow, l, m);
                if (ex != null)
                {
                    Console.WriteLine(ex);
                }
            };

        public static readonly Delegate ToDebug
            = l => (ex, f, a) =>
            {
                var m = string.Format(f, a);
                System.Diagnostics.Debug.WriteLine(
                    MESSAGE_FORMAT, DateTime.UtcNow, l, m);
                if (ex != null)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            };

        public static readonly Delegate ToTrace
            = l => (ex, f, a) =>
            {
                var m = string.Format(f, a);
                Trace.WriteLine(
                    string.Format(
                        MESSAGE_FORMAT,
                        DateTime.UtcNow, l, m));
                if (ex != null)
                {
                    Trace.WriteLine(ex);
                }

                Trace.Flush();
                Trace.Close();
            };

        public static Delegate ToList(List<Event> list)
        {
            return
                l => (ex, f, a) => list.Add(new Event(l, ex, f, a));
        }
    }
}