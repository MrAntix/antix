using System.Collections.Generic;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlDataTryConsumeToTest
    {
        [Fact]
        public void try_consume_succeeds_and_consumes()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "a", false, true);

            Assert.True(result);

            Assert.Equal('b', queue.Peek());
        }

        [Fact]
        public void try_consume_fails_and_does_not_consume()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "b", false, true);

            Assert.False(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void nothing_to_find()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "", true, true);

            Assert.True(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void not_in_queue()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "x", true, true);

            Assert.False(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void string_at_beginning()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "ab", true, true);

            Assert.True(result);

            Assert.Equal('c', queue.Peek());
        }

        [Fact]
        public void string_at_middle_including()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "bc", true, true);

            Assert.True(result);

            Assert.Equal('d', queue.Peek());
        }

        [Fact]
        public void string_at_middle_not_including()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "bc", true, false);

            Assert.True(result);

            Assert.Equal('b', queue.Peek());
        }

        [Fact]
        public void string_at_middle_including_get_consumed()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            string consumed;
            HtmlQueue.TryConsume(queue, "bc", true, true, out consumed);

            Assert.Equal("abc", consumed);
        }

        [Fact]
        public void string_at_middle_not_including_get_consumed()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            string consumed;
            HtmlQueue.TryConsume(queue, "bc", true, false, out consumed);

            Assert.Equal("a", consumed);
        }

        [Fact]
        public void string_at_end()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsume(queue, "cd", true, true);

            Assert.True(result);

            Assert.False(queue.Any());
        }
    }
}