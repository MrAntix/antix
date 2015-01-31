using System;
using Xunit;

namespace Antix.Tests
{
    public class TrimEndTests
    {
        [Fact]
        public void TrimsSingleEnd()
        {
            Assert.Equal("Left", "LeftEnd".TrimEnd("End"));
        }

        [Fact]
        public void DoesNotTrimNotEnd()
        {
            Assert.Equal("LeftEndRight", "LeftEndRight".TrimEnd("End"));
        }

        [Fact]
        public void TrimsMultiEnd()
        {
            Assert.Equal("Left", "LeftEndEnd".TrimEnd("End"));
        }

        [Fact]
        public void DoesNotTrimNoEnd()
        {
            Assert.Equal("Left", "Left".TrimEnd("End"));
        }

        [Fact]
        public void TrimsCaseInsensitive()
        {
            Assert.Equal("Left", "LeftEND".TrimEnd("End", StringComparison.OrdinalIgnoreCase));
        }
    }
}