using System;
using System.IO;
using System.Linq;
using Antix.Html;
using Antix.Testing;
using Antix.Tests.Html.Services;
using Xunit;

namespace Antix.Tests.Html
{
    public class ParseWebTest
    {
        [Fact(Skip = "Waaaay too slow")]
        public void parse_google_with_queue_based_reader()
        {
            var htmlString = FindResource("Resources.Google.htm");

            IHtmlNode[] html = null;
            var result = Benchmark.Run(
                () => html = new HtmlParser(s => new QueueHtmlReader(s))
                                 .Parse(htmlString)
                                 .ToArray(),
                5);

            Assert.NotNull(html);
            Assert.Equal(2, html.Count());

            Console.Write(result);
        }

        [Fact]
        public void parse_small_page_with_string_based_reader()
        {
            var htmlString = FindResource("Resources.SmallPage.htm");

            IHtmlNode[] html = null;
            var result = Benchmark.Run(
                () => html = new HtmlParser(s => new StringHtmlReader(s))
                                 .Parse(htmlString)
                                 .ToArray(),
                5);

            Assert.NotNull(html);
            Assert.Equal(2, html.Count());

            Console.Write(result);
        }

        [Fact]
        public void parse_big_page_with_string_based_reader()
        {
            var htmlString = FindResource("Resources.BigPage.htm");

            IHtmlNode[] html = null;
            var result = Benchmark.Run(
                () => html = new HtmlParser(s => new StringHtmlReader(s))
                                 .Parse(htmlString)
                                 .ToArray(),
                5);

            Assert.NotNull(html);
            Assert.Equal(2, html.Count());

            Console.Write(result);
        }

        public string FindResource(string name)
        {
            var type = GetType();

            var stream = (from r in type.Assembly.GetManifestResourceNames()
                          where r.EndsWith("." + name)
                          select type.Assembly.GetManifestResourceStream(r)).Single();

            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}