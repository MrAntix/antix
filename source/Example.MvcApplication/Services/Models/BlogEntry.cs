using System;
using System.Collections.Generic;

namespace Example.MvcApplication.Services.Models
{
    public class BlogEntry
    {
        public string Identifier { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? PublishedOn { get; set; }
        public bool IsPublished { get; set; }

        public string Content { get; set; }
        public string Summary { get; set; }

        public string Author { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}