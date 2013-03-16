using System.Collections.Generic;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlDataTryConsumeToTest
    {
        [Fact]
        public void nothing_to_find()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsumeIncluding(queue, "");

            Assert.True(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void not_in_queue()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsumeIncluding(queue, "x");

            Assert.False(result);

            Assert.Equal('a', queue.Peek());
        }

        [Fact]
        public void string_at_beginning()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsumeIncluding(queue, "ab");

            Assert.True(result);

            Assert.Equal('c', queue.Peek());
        }

        [Fact]
        public void string_at_middle()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsumeIncluding(queue, "bc");

            Assert.True(result);

            Assert.Equal('d', queue.Peek());
        }

        [Fact]
        public void string_at_end()
        {
            var queue = new Queue<char>(new[] {'a', 'b', 'c', 'd'});

            var result = HtmlQueue.TryConsumeIncluding(queue, "cd");

            Assert.True(result);

            Assert.False(queue.Any());
        }
    }
}