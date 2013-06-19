using Antix.Data.Keywords;
using Antix.Html;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.App_Start
{
    public class KeywordsConfig
    {
        public static void RegisterKeywordIndexing(IKeywordsIndexer indexer)
        {
            var htmlParser = HtmlParser.Create();

            indexer
                .Entity<BlogEntryData>()
                .Index(entry => entry.Title)
                .Index(entry => entry.Author)
                .Index(entry => htmlParser.Parse(entry.Summary).ToText())
                .Index(entry => htmlParser.Parse(entry.Content).ToText())
                .ForEach(entry => entry.Tags,
                         tagBuilder => tagBuilder.Index(tag => tag.Title));
        }
    }
}