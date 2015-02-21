using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Antix.Logging
{
    public static partial class Log
    {
        const string MessageFormat = "{0:yyyy-MM-dd hh:mm:ss:ffff} [{1}] [{2}]: {3}";
        const string MessageFormatWithId = "{0} {1:yyyy-MM-dd hh:mm:ss:ffff} [{2}] [{3}]: {4}";

        public static readonly Delegate ToConsole
            = (l, id, ex, tags) => (f, a) =>
            {
                var m = string.Format(f, a);
                Console.WriteLine(
                    MessageFormat,
                    DateTime.UtcNow,
                    l,
                    string.Join(", ", tags),
                    m);
                if (ex != null)
                {
                    Console.WriteLine(ex);
                }
            };

        public static readonly Delegate ToDebug
            = (l, id, ex, tags) => (f, a) =>
            {
                var m = string.Format(f, a);
                System.Diagnostics.Debug.WriteLine(
                    MessageFormat,
                    DateTime.UtcNow,
                    l,
                    string.Join(", ", tags),
                    m);
                if (ex != null)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            };

        public static readonly Delegate ToTrace
            = (l, id, ex, tags) => (f, a) =>
            {
                var m = string.Format(f, a);
                Trace.WriteLine(
                    string.Format(
                        MessageFormatWithId,
                        id,
                        DateTime.UtcNow, l,
                        string.Join(", ", tags),
                        m));
                if (ex != null)
                {
                    Trace.WriteLine(ex);
                }

                Trace.Flush();
                Trace.Close();
            };

        public static Delegate ToList(List<Event> list)
        {
            return (l, id, ex, tags) =>
                    (f, a) => list.Add(new Event(id, l, ex, f, a, tags));
        }
    }
}