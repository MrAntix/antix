using System.Collections.Generic;

namespace Antix.Data.Keywords.Processing
{
    /// <summary>
    ///     <para>Process a string to extract keywords</para>
    /// </summary>
    public interface IKeywordProcessor
    {
        /// <summary>
        ///     <para>Process a string to extract keywords</para>
        /// </summary>
        /// <param name="value">String to process</param>
        /// <param name="allowStopWords">Allow stop words in processed result</param>
        IEnumerable<string> Process(string value, bool allowStopWords = false);
    }
}