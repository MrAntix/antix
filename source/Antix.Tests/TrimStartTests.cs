using System;
using Xunit;

namespace Antix.Tests
{
    public class TrimStartTests
    {
        [Fact]
        public void TrimsSingleStart()
        {
            Assert.Equal("Text", "StartText".TrimStart("Start"));
        }

        [Fact]
        public void TrimsMultiStart()
        {
            Assert.Equal("Text", "StartStartText".TrimStart("Start"));
        }

        [Fact]
        public void DoesNotTrimNoStart()
        {
            Assert.Equal("Text", "Text".TrimStart("Start"));
        }

        [Fact]
        public void DoesNotTrimNotAtStart()
        {
            Assert.Equal("TextStartText", "TextStartText".TrimStart("Start"));
        }

        [Fact]
        public void TrimsCaseInsensitive()
        {
            Assert.Equal("Text", "STARTText".TrimStart("Start", StringComparison.OrdinalIgnoreCase));
        }
    }
}