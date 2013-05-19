using System.Collections.Generic;

namespace Example.MvcApplication.Models
{
    public class BlogEntryModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public DateTimeModel PublishedOn { get; set; }
        public string Author { get; set; }
    }
}