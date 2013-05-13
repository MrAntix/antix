using System.Diagnostics.Contracts;
using System.Linq;
using Antix.Data.Keywords.EF.Entities;
using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords.EF
{
    public static class EFKeywordsLinqExtensions
    {
        public static IKeywordProcessor KeywordProcessor
            = WordSplitKeywordProcessor.Create();

        /// <summary>
        ///     <para>
        ///         Find items of type <see cref="T" /> which have matching keywords for the text passed
        ///     </para>
        ///     <para>Text is processed by the passed processor, which should be the same one that builds the keywords for the elements</para>
        ///     <para>
        ///         The search is ranked by Tf-idf <seealso cref="http://en.wikipedia.org/wiki/Tf%E2%80%93idf" />
        ///     </para>
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="source">
        ///     Queryable source of <see cref="T" />
        /// </param>
        /// <param name="text">Text to find</param>
        /// <param name="keywordProcessor">Processes the text into keywords to search on</param>
        /// <returns>Matching ranked elements</returns>
        public static IQueryable<T> Matches<T>(
            this IQueryable<T> source,
            string text,
            IKeywordProcessor keywordProcessor)
            where T : IndexedEntity
        {
            Contract.Requires(source != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(text));
            Contract.Requires(keywordProcessor != null);

            var keywords = keywordProcessor.Process(text);

            return
                from e in source
                orderby
                    (
                        from ek in e.Keywords
                        where keywords.Any(k => k == ek.Keyword.Value)
                        select ek.Frequency/(decimal) ek.Keyword.Frequency
                    ).Sum() descending
                select e;
        }

        /// <summary>
        ///     <para>
        ///         Find items of type <see cref="T" /> which have matching keywords for the text passed
        ///     </para>
        ///     <para>
        ///         Text is processed by the static <see cref="KeywordProcessor" />, which should be the same one that builds the keywords for the elements
        ///     </para>
        ///     <para>
        ///         The search is ranked by Tf-idf <seealso cref="http://en.wikipedia.org/wiki/Tf%E2%80%93idf" />
        ///     </para>
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="source">
        ///     Queryable source of <see cref="T" />
        /// </param>
        /// <param name="text">Text to find</param>
        /// <returns>Matching ranked elements</returns>
        public static IQueryable<T> Matches<T>(
            this IQueryable<T> source,
            string text)
            where T : IndexedEntity
        {
            Contract.Requires(source != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(text));

            return Matches(source, text, KeywordProcessor);
        }
    }
}