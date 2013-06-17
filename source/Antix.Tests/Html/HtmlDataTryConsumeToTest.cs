using System;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlDataTryConsumeToTest
    {
        [Fact]
        public void consume_some()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.Consume(2);

            Assert.Equal("ab", result);
            Assert.Equal('c', queue.Peek());
        }

        [Fact]
        public void consume_too_many()
        {
            var queue = new HtmlQueue("abcd");

            Assert.Throws<InvalidOperationException>(
                () => queue.Consume(5));
        }

        [Fact]
        public void try_consume_succeeds_and_consumes()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("b", true, true);

            Assert.True(result);

            Assert.Equal('c', queue.Peek());
        }

        [Fact]
        public void try_consume_fails_and_does_not_consume()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("x", true, true);

            Assert.False(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void consumed_does_not_include_the_target()
        {
            var queue = new HtmlQueue("abcd");

            string consumed;
            queue.TryConsume("c", true, true, out consumed);

            Assert.Equal("ab", consumed);
        }

        [Fact]
        public void nothing_to_find()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("", true, true);

            Assert.True(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void not_in_queue()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("x", true, true);

            Assert.False(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void string_at_beginning()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("ab", true, true);

            Assert.True(result);

            Assert.Equal('c', queue.Peek());
        }

        [Fact]
        public void string_at_end()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("cd", true, true);

            Assert.True(result);

            Assert.False(queue.Any());
        }

        [Fact]
        public void string_at_middle()
        {
            var queue = new HtmlQueue("abcd");

            var result = queue.TryConsume("bc", true, true);

            Assert.True(result);

            Assert.Equal('d', queue.Peek());
        }

        [Fact]
        public void string_at_middle_get_consumed()
        {
            var queue = new HtmlQueue("abcd");

            string consumed;
            queue.TryConsume("bc", true, true, out consumed);

            Assert.Equal("a", consumed);
            Assert.Equal('d', queue.Peek());
        }
    }
}