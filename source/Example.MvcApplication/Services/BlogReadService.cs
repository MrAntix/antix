using System.Collections.Generic;
using System.Linq;
using Antix.Data.Keywords.EF;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Services
{
    public class BlogReadService : IBlogReadService
    {
        readonly DataContext _dataContext;

        public BlogReadService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<BlogEntry> Search(string text, int index, int pageSize)
        {
            var blogEntries = _dataContext.BlogEntries.AsQueryable();

            if (!string.IsNullOrEmpty(text))
                blogEntries = blogEntries.Matches(text);

            return blogEntries
                .OrderBy(e => e.Title)
                .Skip(index * pageSize)
                .Take(pageSize);
        }
    }
}