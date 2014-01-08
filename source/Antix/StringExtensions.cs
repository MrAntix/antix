using System;
using System.Text;

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


        /// <summary>
        ///   <para>Case insensitive contains</para>
        /// </summary>
        /// <param name = "text">Text to check</param>
        /// <param name = "value">Value to check for</param>
        /// <param name = "comparisonType">Comparison Type</param>
        /// <returns>True if matched</returns>
        public static bool Contains(this string text, string value, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (string.IsNullOrEmpty(value)) return true;

            return text.IndexOf(value, comparisonType) > -1;
        }

        /// <summary>
        ///   <para>Case insensitive replace</para>
        /// </summary>
        /// <param name = "text">Incoming text string</param>
        /// <param name = "oldValue">Token to be replaced</param>
        /// <param name = "newValue">Replacement token</param>
        /// <param name = "comparisonType">Comparison provider</param>
        /// <returns>Resultant text</returns>
        public static string Replace(this string text, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (text == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(oldValue))
            {
                return text;
            }

            var length = oldValue.Length;
            var patternIndex = -1;
            var lastIndex = 0;
            var result = new StringBuilder();

            while (true)
            {
                patternIndex = text.IndexOf(oldValue, patternIndex + 1, comparisonType);

                if (patternIndex < 0)
                {
                    result.Append(text, lastIndex, text.Length - lastIndex);

                    break;
                }

                result.Append(text, lastIndex, patternIndex - lastIndex);
                result.Append(newValue);

                lastIndex = patternIndex + length;
            }

            return result.ToString();
        }
    }
}