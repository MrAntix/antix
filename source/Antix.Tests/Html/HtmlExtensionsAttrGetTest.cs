using System.Collections.Generic;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlExtensionsAttrGetTest
    {
        [Fact]
        public void single_element()
        {
            Exec(
                "<div attr='value'/>",
                "attr",
                "value");
        }

        [Fact]
        public void case_insensitive()
        {
            Exec(
                "<div attr='value'/>",
                "ATTR",
                "value");
        }

        [Fact]
        public void nested_element()
        {
            Exec(
                "<div><div attr='value'/></div>",
                "attr",
                "value");
        }

        [Fact]
        public void nested_element_multiple_result()
        {
            Exec(
                "<div><div attr='value'/><div attr='other-value'/></div>",
                "attr",
                "value", "other-value");
        }

        static IEnumerable<string> Exec(
            string htmlString,
            string attributeName,
            params string[] expected)
        {
            var sut = HtmlParser.Create();

            var html = sut.Parse(htmlString).ToArray();
            var result = html.Attr(attributeName).ToArray();

            Assert.Equal(expected, result);

            return result;
        }
    }
}