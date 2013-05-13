using System;
using System.IO;
using System.Linq;

namespace Antix.IO
{
    public static class IOExtensions
    {
        public static string ToSafeFileName(
            this string value, char invalidCharReplacement)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");

            var invalid = Path.GetInvalidFileNameChars();
            if (invalid.Contains(invalidCharReplacement)) throw new ArgumentException("invalidCharReplacement");

            return new string(value
                                  .Select(c => invalid.Contains(c) ? invalidCharReplacement : c)
                                  .ToArray());
        }

        public static string ToSafeFileName(
            this string value)
        {
            return value.ToSafeFileName('_');
        }
    }
}