using System;
using System.IO;
using System.Linq;
using System.Net;
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

        [Fact]
        public void parse_twitter()
        {
            const string address = "http://twitter.com/skysportsfl/followers/";

            var webClient = new WebClient();
            webClient.Headers.Add("cookie",
                "lang=en-gb; guest_id=v1%3A137337600960092466; __utma=43838368.517491664.1373376012.1375298648.1375367885.10; __utmz=43838368.1375298648.9.8.utmcsr=myxi.co|utmccn=(referral)|utmcmd=referral|utmcct=/; __utmv=43838368.lang%3A%20en; twll=l%3D1375367981; remember_checked=1; remember_checked_on=1; external_referer=\"OTZIBTkFw3v0VTPNOWsL6Q==|0\"; __utmb=43838368.1.10.1375367885; auth_token=4a3b1bfbf0a104e0e01d4f884eb5b86b68972515; secure_session=true; _twitter_sess=BAh7CiIKZmxhc2hJQzonQWN0aW9uQ29udHJvbGxlcjo6Rmxhc2g6OkZsYXNo%250ASGFzaHsABjoKQHVzZWR7ADoPY3JlYXRlZF9hdGwrCKCEVzpAAToOcmV0dXJu%250AX3RvIi5odHRwczovL3R3aXR0ZXIuY29tL1NreVNwb3J0c0ZML2ZvbGxvd2Vy%250AczoHaWQiJTMzOTMxODA3OTllOGRjYTI3OGRlZTg2MDg0OThmZDg0Ogxjc3Jm%250AX2lkIiUyNjk3MWZkODY5ODg4MzBiZmQyNWJlNzYxODA0M2U3MA%253D%253D--1e1015aadd109ffcb0d3848ea85e54b0843c94f7");

            var webPage = webClient.DownloadString(address);

            var htmlParser = HtmlParser.Create();
            var html = htmlParser.Parse(webPage);

            Assert.True(webPage.Contains("Followers"));

            var names = html
                .HasClass("stream-container")
                .HasClass("username")
                .Where(n => n.ToText() != "@");

            foreach (var name in names)
            {
                Console.WriteLine(name.ToText());
            }
        }
    }
}