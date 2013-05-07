using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Data.Keywords.Stemming;

namespace Antix.Data.Keywords.Processing
{
    public class SplitByWhitespaceKeywordProcessor : IKeywordProcessor
    {
        readonly IStemmer _stemmer;

        public SplitByWhitespaceKeywordProcessor() :
            this(new EnglishStemmer())
        {
        }

        public SplitByWhitespaceKeywordProcessor(IStemmer stemmer)
        {
            _stemmer = stemmer;
        }

        public IEnumerable<string> Process(object value)
        {
            var valueString = value as string;
            return valueString == null
                       ? null
                       : valueString
                             .ToLower()
                             .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                             .Select(word => _stemmer.Stem(word))
                             .Except(_stopWords);
        }

        readonly string[] _stopWords = new[]
            {
                "a", "an", "and", "are", "as", "at", "be", "but", "by",
                "for", "if", "in", "into", "is", "it",
                "no", "not", "of", "on", "or", "such",
                "that", "the", "their", "then", "there", "these",
                "they", "this", "to", "was", "will", "with"
            };
    }
}