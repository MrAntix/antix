using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Data.Keywords.EF.Entities;

namespace Example.MvcApplication.App_Start.Data
{
    public class BlogEntry : IndexedEntity
    {
        public BlogEntry()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Identifier { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public bool IsPublished { get; set; }

        public string Content { get; set; }
        public string Summary { get; set; }

        public string Author { get; set; }

        public ICollection<BlogTag> Tags { get; set; }
        public ICollection<BlogEntryRedirect> Redirects { get; set; }

        public static string GetIdentifier(string title)
        {
            return new string(
                title.Select(c => char.IsLetter(c) ? c : '-').ToArray()
                );
        }
    }
}