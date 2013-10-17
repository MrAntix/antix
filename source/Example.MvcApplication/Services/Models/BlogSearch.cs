using System.Collections.Generic;

namespace Example.MvcApplication.Services.Models
{
    public class BlogSearch
    {
        public string Text { get; set; }
        public bool IncludeUnpublished { get; set; }
        public string Continuation { get; set; }

        public IEnumerable<BlogEntryInfo> Results { get; set; }
        public string NextContinuation { get; set; }
    }
    
}