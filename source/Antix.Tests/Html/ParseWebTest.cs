using System;
using System.IO;
using System.Linq;
using Antix.Html;
using Xunit;

namespace Antix.Tests.Html
{
    public class ParseWebTest
    {
        [Fact]
        public void load_google()
        {
            var htmlString = FindResource("Resources.Google.htm");

            var html = HtmlParser.Create()
                                 .Parse(htmlString)
                                 .ToArray();

            Assert.NotNull(html);
            Assert.Equal(2, html.Count());

            Console.Write(html.ToHtml());
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