using System;
using System.Data.Entity.Migrations;
using System.Net;
using System.Web;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var codingTag = new BlogTag {Title = "Coding"};
            var drawingTag = new BlogTag {Title = "Drawing"};

            context.BlogTags
                   .AddOrUpdate(e => e.Title,
                                codingTag,
                                drawingTag);
            context.SaveChanges();

            context.BlogEntries
                   .AddOrUpdate(e => e.Title,
                                new BlogEntry
                                    {
                                        Title = "Writing a Blog Engine",
                                        Identifier = BlogEntry.GetIdentifier("Writing a Blog Engine"),
                                        Summary = "<p>How to write a blog entry in C#.NET</p>",
                                        Content =
                                            "<p>So you want to write yourself a blog engine eh?</p><p>Well lets get started</p>",
                                        Tags = new[] {codingTag},
                                        IsPublished = true,
                                        PublishedOn = DateTime.UtcNow
                                    },
                                new BlogEntry
                                    {
                                        Title = "Keyword search support for EF",
                                        Identifier = BlogEntry.GetIdentifier("Keyword search support for EF"),
                                        Summary = "<p>Adding support for keyword indexing to your entities</p>",
                                        Content =
                                            "<p>Adding support for keyword indexing to your entities</p>",
                                        Tags = new[] {codingTag},
                                        IsPublished = true,
                                        PublishedOn = DateTime.UtcNow
                                    },
                                new BlogEntry
                                    {
                                        Title = "Drawing on the iPad",
                                        Identifier = BlogEntry.GetIdentifier("Drawing on the iPad"),
                                        Summary = "<p>There are a number of good apps for drawing on the iPad</p>",
                                        Content =
                                            "<p>I have downloaded a few and compare them here</p>",
                                        Tags = new[] {drawingTag}
                                    });
            context.SaveChanges();
        }
    }
}