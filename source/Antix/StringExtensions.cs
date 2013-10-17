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

        /// <summary>
        ///   Get the head of a string, 
        /// 
        ///   destructive, ie leaving only the body in the text variable
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Head cut of point (neck)</param>
        /// <param name = "comparisonType">String Comparison</param>
        /// <returns>Head only</returns>
        public static string Head(ref string text, string uptoItem, StringComparison comparisonType)
        {
            var position = text.IndexOf(uptoItem, comparisonType);
            string headText;

            if (position == -1)
            {
                headText = text;
                text = "";
            }
            else
            {
                headText = text.Substring(0, position);
                text = text.Substring(position + uptoItem.Length);
            }

            return headText;
        }

        /// <summary>
        ///   Get the head of a string, 
        /// 
        ///   destructive, ie leaving only the body in the text variable
        ///   case sensitive
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Head cut of point (neck)</param>
        /// <returns>Head only</returns>
        public static string Head(ref string text, string uptoItem)
        {
            return Head(ref text, uptoItem, StringComparison.CurrentCulture);
        }

        /// <summary>
        ///   Get the head of a string
        /// 
        ///   non destuctive, ie leaves the text as it was
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Head cut of point (neck)</param>
        /// <param name = "comparisonType">String Comparison</param>
        /// <returns>Head only</returns>
        public static string Head(this string text, string uptoItem, StringComparison comparisonType)
        {
            var position = text.IndexOf(uptoItem, comparisonType);
            string headText;

            if (position == -1)
            {
                headText = text;
            }
            else
            {
                headText = text.Substring(0, position);
            }

            return headText;
        }

        /// <summary>
        ///   Get the head of a string
        /// 
        ///   non destuctive, ie leaves the text as it was
        ///   case sensitive
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Head cut of point (neck)</param>
        /// <returns>Head only</returns>
        public static string Head(this string text, string uptoItem)
        {
            return Head(text, uptoItem, StringComparison.CurrentCulture);
        }

        /// <summary>
        ///   Remove the tail from text
        /// 
        ///   destructive, ie leaving only the body in the text variable
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Tail cut off point</param>
        /// <param name = "comparisonType">String Comparison</param>
        /// <returns>Tail only</returns>
        public static string Tail(ref string text, string uptoItem, StringComparison comparisonType)
        {
            var position = text.LastIndexOf(uptoItem, comparisonType);
            string tailText;

            if (position == -1)
            {
                tailText = text;
                text = "";
            }
            else
            {
                tailText = text.Substring(position + uptoItem.Length);
                text = text.Substring(0, position);
            }

            return tailText;
        }

        /// <summary>
        ///   Remove the tail from text
        /// 
        ///   destructive, ie leaving only the body in the text variable
        ///   case sensitive
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Tail cut off point</param>
        /// <returns>Tail only</returns>
        public static string Tail(ref string text, string uptoItem)
        {
            return Tail(ref text, uptoItem, StringComparison.CurrentCulture);
        }

        /// <summary>
        ///   Remove the tail from text
        /// 
        ///   non destuctive, ie leaves the text as it was
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Tail cut off point</param>
        /// <param name = "comparisonType">String Comparison</param>
        /// <returns>Tail only</returns>
        public static string Tail(this string text, string uptoItem, StringComparison comparisonType)
        {
            var position = text.LastIndexOf(uptoItem, comparisonType);
            string tailText;

            if (position == -1)
            {
                tailText = text;
            }
            else
            {
                tailText = text.Substring(position + uptoItem.Length);
            }

            return tailText;
        }

        /// <summary>
        ///   Remove the tail from text
        /// 
        ///   non destuctive, ie leaves the text as it was
        ///   case sensitive
        /// </summary>
        /// <param name = "text">Text string</param>
        /// <param name = "uptoItem">Tail cut off point</param>
        /// <returns>Tail only</returns>
        public static string Tail(this string text, string uptoItem)
        {
            return Tail(text, uptoItem, StringComparison.CurrentCulture);
        }
    }
}