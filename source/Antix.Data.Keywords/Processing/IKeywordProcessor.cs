using System.Collections.Generic;

namespace Antix.Data.Keywords.Processing
{
    /// <summary>
    /// <para>Process a string to extract keywords</para>
    /// </summary>
    public interface IKeywordProcessor
    {
        /// <summary>
        /// <para>Process a string to extract keywords</para>
        /// </summary>
        /// <para>String to process</para>
        IEnumerable<string> Process(string value);
    }
}