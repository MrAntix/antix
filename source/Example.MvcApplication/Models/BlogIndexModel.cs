using System.Collections.Generic;

namespace Example.MvcApplication.Models
{
    public class BlogIndexModel
    {
        public IEnumerable<BlogEntryInfoModel> BlogEntries { get; set; }
    }
}