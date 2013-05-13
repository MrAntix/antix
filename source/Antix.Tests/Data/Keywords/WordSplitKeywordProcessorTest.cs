using System.Linq;
using Antix.Data.Keywords.Processing;
using Xunit;

namespace Antix.Tests.Data.Keywords
{
    public class WordSplitKeywordProcessorTest
    {
        [Fact]
        public void SplitsAtFullStops()
        {
            var sut = GetService();
            var result = sut.Process("a.b").ToArray();

            Assert.Equal(new[] { "a", "b" }, result);
        }

        [Fact]
        public void SplitsAtQuestionMarks()
        {
            var sut = GetService();
            var result = sut.Process("a?b").ToArray();

            Assert.Equal(new[] { "a", "b" }, result);
        }

        [Fact]
        public void IgnoresWhiteSpace()
        {
            var sut = GetService();
            var result = sut.Process("a? b").ToArray();

            Assert.Equal(new[] { "a", "b" }, result);
        }

        [Fact]
        public void RemovesApostrophies()
        {
            var sut = GetService();
            var result = sut.Process("a'b").ToArray();

            Assert.Equal(new[] { "ab" }, result);
        }

        IKeywordProcessor GetService()
        {
            return WordSplitKeywordProcessor.Create(stopWords: new string[] {});
        }
    }
}