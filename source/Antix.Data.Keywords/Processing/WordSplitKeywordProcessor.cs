using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antix.Data.Keywords.Stemming;

namespace Antix.Data.Keywords.Processing
{
    public class WordSplitKeywordProcessor :
        IKeywordProcessor
    {
        readonly IStemmer _stemmer;
        readonly string[] _stopWords;
        readonly Regex _splitter = new Regex("\\W", RegexOptions.Compiled, new TimeSpan(100));

        public WordSplitKeywordProcessor(
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
                : _splitter.Split(value.Replace("'", "").ToLower())
                    .Where(w => !string.IsNullOrWhiteSpace(w))
                    .Select(word => _stemmer.Stem(word))
                    .Except(_stopWords);
        }

        /// <summary>
        ///     <para>Creates a processor</para>
        /// </summary>
        /// <param name="stemmer">If not supplied creates and English Stemmer</param>
        /// <param name="stopWords">
        ///     If not supplied uses <see cref="EnglishStopWords" />
        /// </param>
        public static WordSplitKeywordProcessor Create(
            IStemmer stemmer = null, string[] stopWords = null)
        {
            return new WordSplitKeywordProcessor(
                stemmer ?? new EnglishStemmer(),
                stopWords ?? EnglishStopWords);
        }

        public static readonly string[] EnglishStopWords =
        {
            "a", "an", "and", "are", "as", "at", "be", "but", "by",
            "for", "if", "in", "into", "is", "it",
            "no", "not", "of", "on", "or", "such",
            "that", "the", "their", "then", "there", "these",
            "they", "this", "to", "was", "will", "with"
        };
    }
}