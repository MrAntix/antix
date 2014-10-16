using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class HtmlExtensionsAttrSetTest
    {
        [Fact]
        public void changes_existing()
        {
            Exec(
                "<div attr='value'/>",
                "attr", "new-value",
                "<div attr=\"new-value\"/>");
        }

        [Fact]
        public void adds_attribute()
        {
            Exec(
                "<div attr='value'/>",
                "attr", "new-value",
                "<div attr=\"new-value\"/>");
        }

        [Fact]
        public void changes_and_adds_attribute()
        {
            Exec(
                "<div><div attr='value'/></div>",
                "attr", "new-value",
                "<div attr=\"new-value\"><div attr=\"new-value\"/></div>");
        }

        [Fact]
        public void removes_duplicates()
        {
            Exec(
                "<div attr='value' attr='other-value'/>",
                "attr", "new-value",
                "<div attr=\"new-value\"/>");
        }

        static void Exec(
            string htmlString,
            string name,
            string value,
            string expected)
        {
            var sut = HtmlParser.Create();

            var html = sut.Parse(htmlString).ToArray();
            html.Attr(name, value);

            Assert.Equal(expected, html.ToHtml());
        }
    }
}