using System;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlDataTryConsumeToTest
    {
        static IHtmlReader GetReader(string htmlString)
        {
            return new HtmlReader(htmlString);
        }

        [Fact]
        public void consume_some()
        {
            var reader = GetReader("abcd");

            var result = reader.Consume(2);

            Assert.Equal("ab", result);
            Assert.Equal('c', reader.Peek());
        }

        [Fact]
        public void consume_too_many()
        {
            var reader = GetReader("abcd");

            Assert.Throws<InvalidOperationException>(
                () => reader.Consume(5));
        }

        [Fact]
        public void consume_just_enough()
        {
            var reader = GetReader("abcd");

            Assert.DoesNotThrow(() => reader.Consume(4));
            Assert.False(reader.Any());
        }

        [Fact]
        public void try_consume_succeeds_and_consumes()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("b", true, true);

            Assert.True(result);

            Assert.Equal('c', reader.Peek());
        }

        [Fact]
        public void try_consume_fails_and_does_not_consume()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("x", true, true);

            Assert.False(result);

            Assert.Equal('a', reader.Peek());
        }

        [Fact]
        public void consumed_does_not_include_the_target()
        {
            var reader = GetReader("abcd");

            string consumed;
            reader.TryConsume("c", true, true, out consumed);

            Assert.Equal("ab", consumed);
        }

        [Fact]
        public void nothing_to_find()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("", true, true);

            Assert.True(result);

            Assert.Equal('a', reader.Peek());
        }

        [Fact]
        public void not_in_reader()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("x", true, true);

            Assert.False(result);

            Assert.Equal('a', reader.Peek());
        }

        [Fact]
        public void string_at_beginning()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("ab", true, true);

            Assert.True(result);

            Assert.Equal('c', reader.Peek());
        }

        [Fact]
        public void string_at_end()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("cd", true, true);

            Assert.True(result);

            Assert.False(reader.Any());
        }

        [Fact]
        public void string_at_middle()
        {
            var reader = GetReader("abcd");

            var result = reader.TryConsume("bc", true, true);

            Assert.True(result);

            Assert.Equal('d', reader.Peek());
        }

        [Fact]
        public void string_at_middle_get_consumed()
        {
            var reader = GetReader("abcd");

            string consumed;
            reader.TryConsume("bc", true, true, out consumed);

            Assert.Equal("a", consumed);
            Assert.Equal('d', reader.Peek());
        }
    }
}