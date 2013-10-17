using System;

namespace Antix
{
    public static class StringExtensions
    {
        public static string TrimEnd(
            this string value, string trimString,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (string.IsNullOrEmpty(value)
                || string.IsNullOrEmpty(trimString)) return value;

            var lastIndex = value.Length;
            int index;
            while ((index = value.LastIndexOf(trimString, lastIndex, comparisonType)) != -1)
                lastIndex = index;

            return lastIndex != value.Length
                       ? value.Substring(0, lastIndex)
                       : value;
        }
    }
}