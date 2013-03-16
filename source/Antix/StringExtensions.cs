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

            return value.Substring(0,
                                   value.LastIndexOf(trimString, comparisonType));
        }
    }
}