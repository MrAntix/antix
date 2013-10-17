using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antix.Data.Keywords.EF;
using Antix.Data.Keywords.Processing;
using Example.MvcApplication.App_Start.Data;
using Example.MvcApplication.Services.Models;

namespace Example.MvcApplication.Services
{
    public class BlogReadService : IBlogReadService
    {
        readonly DataContext _dataContext;
        readonly IKeywordProcessor _keywordProcessor;
        readonly SearchSettings _searchSettings;

        public BlogReadService(
            DataContext dataContext,
            IKeywordProcessor keywordProcessor,
            SearchSettings searchSettings
        )
        {
            _dataContext = dataContext;
            _keywordProcessor = keywordProcessor;
            _searchSettings = searchSettings;
        }

        public async Task<BlogSearch> SearchAsync(BlogSearch criteria)
        {
            var blogEntries = _dataContext.BlogEntries.AsQueryable();

            if (!criteria.IncludeUnpublished)
                blogEntries = blogEntries.Where(e => e.IsPublished);

            if (!string.IsNullOrEmpty(criteria.Text))
                blogEntries = blogEntries.Match(criteria.Text, _keywordProcessor);

            var index = int.Parse(criteria.Continuation);
            
            var results = await blogEntries
                                    .OrderBy(e => e.Title)
                                    .Skip(index * _searchSettings.PageSize)
                                    .Take(_searchSettings.PageSize + 1)
                                    .Select(InfoProjection)
                                    .ToListAsync();

            criteria.NextContinuation = (results.Count() > _searchSettings.PageSize).ToString();
            criteria.Results = results.Take(_searchSettings.PageSize);

            return criteria;
        }

        public BlogEntry TryGetByIdentifier(string identifier)
        {
            return _dataContext.BlogEntries
                               .Where(data => data.Identifier == identifier)
                               .Select(Projection).SingleOrDefault();
        }

        public readonly Expression<Func<BlogEntryData, BlogEntry>> Projection
            = data => new BlogEntry
                {
                    Identifier = data.Identifier,
                    Title = data.Title,
                    Author = data.Author,
                    PublishedOn = data.PublishedOn,
                    Summary = data.Summary,
                    Content = data.Content,
                    Tags = data.Tags.Select(et => et.Title)
                };

        public readonly Expression<Func<BlogEntryData, BlogEntryInfo>> InfoProjection
            = data => new BlogEntryInfo
                {
                    Identifier = data.Identifier,
                    Title = data.Title,
                    PublishedOn = data.PublishedOn,
                    Summary = data.Summary
                };
    }
}