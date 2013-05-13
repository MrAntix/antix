using System;
using System.Collections.Generic;
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

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }

        public string Author { get; set; }

        public ICollection<BlogTag> Tags { get; set; }
    }
}