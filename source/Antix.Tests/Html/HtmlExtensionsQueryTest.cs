using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlExtensionsQueryTest
    {
        [Fact]
        public void single_element()
        {
            Exec(
                "<div attr='value'/>",
                n=>n.Name.Equals("div"),
                "<div attr=\"value\"/>");
        }
        
        [Fact]
        public void nested_element()
        {
            Exec(
                "<div><span attr='value'/></div>",
                n => n.Name.Equals("span"),
                "<span attr=\"value\"/>");
        }

        [Fact]
        public void nested_element_multiple_result()
        {
            Exec(
                "<div><span attr='value'/><span attr='other-value'/></div>",
                n => n.Name.Equals("span"),
                "<span attr=\"value\"/><span attr=\"other-value\"/>");
        }

        static IEnumerable<IHtmlNode> Exec(
            string htmlString,
            Func<HtmlElement, bool> isMatch,
            string expected)
        {
            var sut = HtmlParser.Create();

            var html = sut.Parse(htmlString).ToArray();
            var result = html.Query(isMatch).ToArray();

            Assert.Equal(expected, result.ToHtml());

            return result;
        }
    }
}