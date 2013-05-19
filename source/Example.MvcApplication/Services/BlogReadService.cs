using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antix.Data.Keywords.EF;
using Antix.Data.Keywords.Processing;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Services
{
    public class BlogReadService : IBlogReadService
    {
        readonly DataContext _dataContext;
        readonly IKeywordProcessor _keywordProcessor;

        public BlogReadService(
            DataContext dataContext,
            IKeywordProcessor keywordProcessor)
        {
            _dataContext = dataContext;
            _keywordProcessor = keywordProcessor;
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(
            string text, bool includeUnpublished, int index, int pageSize,
            Expression<Func<BlogEntry, T>> projection)
        {
            var blogEntries = _dataContext.BlogEntries.AsQueryable();

            if (!includeUnpublished)
                blogEntries = blogEntries.Where(e => e.IsPublished);

            if (!string.IsNullOrEmpty(text))
                blogEntries = blogEntries.Match(text, _keywordProcessor);

            return await blogEntries
                             .OrderBy(e => e.Title)
                             .Skip(index*pageSize)
                             .Take(pageSize)
                             .Select(projection)
                             .ToListAsync();
        }

        public T TryGetByIdentifier<T>(
            string identifier,
            Expression<Func<BlogEntry, T>> projection)
        {
            return _dataContext.BlogEntries
                               .Where(e => e.Identifier == identifier)
                               .Select(projection).SingleOrDefault();
        }
    }
}