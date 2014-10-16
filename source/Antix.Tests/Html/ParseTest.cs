using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class ParseTest
    {
        [Fact]
        public void consecutive_elements()
        {
            Exec("<div>Hello</div><div>Hello</div>", 2);
        }

        [Fact]
        public void consecutive_elements_with_whitespace()
        {
            var result = Exec("<span>Hello </span><span>Hello</span>", 2);

            var firstTextNode =
                result.OfType<HtmlElement>().First()
                    .Children.OfType<HtmlTextElement>().First();

            Assert.Equal("Hello ", firstTextNode.Value);
        }

        [Fact]
        public void basic_document()
        {
            Exec("<!doctype html><html><head></head><body></body></html>", 2);
        }

        static IEnumerable<IHtmlNode> Exec(
            string html,
            int expectedCount = 0)
        {
            var sut = HtmlParser.Create();

            var result = sut.Parse(html).ToArray();

            Assert.NotNull(result);
            Console.Write(result.ToHtml());

            Assert.Equal(expectedCount, result.Count());

            return result;
        }
    }
}