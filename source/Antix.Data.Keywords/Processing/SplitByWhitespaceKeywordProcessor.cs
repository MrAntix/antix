using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Data.Keywords.Stemming;

namespace Antix.Data.Keywords.Processing
{
    public class SplitByWhitespaceKeywordProcessor :
        IKeywordProcessor
    {
        readonly IStemmer _stemmer;
        readonly string[] _stopWords;

        public SplitByWhitespaceKeywordProcessor(
            IStemmer stemmer,
            string[] stopWords)
        {
            _stemmer = stemmer;
            _stopWords = stopWords;
        }

        public IEnumerable<string> Process(string value)
        {
            return value == null
                       ? null
                       : value
                             .ToLower()
                             .Split(new[] {" ", "\r\n", "\n", "\t"}, StringSplitOptions.RemoveEmptyEntries)
                             .Select(word => _stemmer.Stem(word))
                             .Except(_stopWords);
        }

        /// <summary>
        ///     <para>Creates a processor</para>
        /// </summary>
        /// <param name="stemmer">If not supplied creates and English Stemmer</param>
        /// <param name="stopWords">If not supplied uses <see cref="EnglishStopWords" /></param>
        public static SplitByWhitespaceKeywordProcessor Create(
            IStemmer stemmer = null, string[] stopWords = null)
        {
            return new SplitByWhitespaceKeywordProcessor(
                new EnglishStemmer(),
                EnglishStopWords);
        }

        public static readonly string[] EnglishStopWords = new[]
            {
                "a", "an", "and", "are", "as", "at", "be", "but", "by",
                "for", "if", "in", "into", "is", "it",
                "no", "not", "of", "on", "or", "such",
                "that", "the", "their", "then", "there", "these",
                "they", "this", "to", "was", "will", "with"
            };
    }
}