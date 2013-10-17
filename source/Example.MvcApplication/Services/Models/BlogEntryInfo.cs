using System;

namespace Example.MvcApplication.Services.Models
{
    public class BlogEntryInfo
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

        public DateTimeOffset? PublishedOn { get; set; }
    }
}