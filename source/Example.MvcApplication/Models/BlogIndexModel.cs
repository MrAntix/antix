using System.Collections.Generic;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Models
{
    public class BlogIndexModel
    {
        public IEnumerable<BlogEntry> BlogEntries { get; set; }
    }
}