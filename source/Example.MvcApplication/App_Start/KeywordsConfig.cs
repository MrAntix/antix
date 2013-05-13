using Antix.Data.Keywords;
using Antix.Html;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.App_Start
{
    public class KeywordsConfig
    {
        public static void RegisterKeywordIndexing(IKeywordsIndexer indexer)
        {
            var htmlParser = new HtmlParser();

            indexer
                .Entity<BlogEntry>()
                .Index(entry => entry.Title)
                .Index(entry => htmlParser.Parse(entry.Content).ToText())
                .ForEach(entry => entry.Tags,
                         tagBuilder => tagBuilder.Index(tag => tag.Title));
        }
    }
}