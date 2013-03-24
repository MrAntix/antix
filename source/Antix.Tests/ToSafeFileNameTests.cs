using System;
using Antix.IO;
using Xunit;

namespace Antix.Tests
{
    public class ToSafeFileNameTests
    {
        [Fact]
        public void ReplacesInvalidWithDefault()
        {
            var result = "thing\\whatsit".ToSafeFileName();

            Assert.Equal("thing_whatsit", result);
        }

        [Fact]
        public void ReplacesInvalidWithSpecifiedReplacement()
        {
            var result = "thing\\whatsit".ToSafeFileName('-');

            Assert.Equal("thing-whatsit", result);
        }

        [Fact]
        public void SpecifiedReplacementMustBeValid()
        {
            Assert.Throws<ArgumentException>(
                () => "thing\\whatsit".ToSafeFileName('\\')
                );
        }

        [Fact]
        public void ValueCannotBeNull()
        {
            Assert.Throws<ArgumentException>(
                () => default(string).ToSafeFileName()
                );
        }

        [Fact]
        public void ValueCannotBeEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => string.Empty.ToSafeFileName()
                );
        }

        [Fact]
        public void ValueCannotBeWhitespace()
        {
            Assert.Throws<ArgumentException>(
                () => "  ".ToSafeFileName()
                );
        }
    }
}