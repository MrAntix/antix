using System;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class TextTest
    {
        [Fact]
        public void br_gives_a_newline()
        {
            const string htmlString =
                "<span>Line1<br>Line2</span>";

            var htmlParser = HtmlParser.Create();
            var html = htmlParser.Parse(htmlString);

            var result = html.ToText();

            Assert.Equal(2, result.Split('\n').Count());
        }

        [Fact]
        public void inline_elements_on_same_line()
        {
            const string htmlString =
                "<span>Line1 <b>Line2</b></span>";

            var htmlParser = HtmlParser.Create();
            var html = htmlParser.Parse(htmlString);

            var result = html.ToText();

            Console.Write(result);

            Assert.Equal(1, result.Split('\n').Count());
        }
    }
}