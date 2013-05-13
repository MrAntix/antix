using System.Data.Entity.Migrations;
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

            var codingTag = new BlogTag { Title = "Coding" };
            var drawingTag = new BlogTag { Title = "Drawing" };

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
                                    Summary = "<p>How to write a blog entry in C#.NET</p>",
                                    Content =
                                        "<p>So you want to write yourself a blog engine eh?</p><p>Well lets get started</p>",
                                    Tags = new[] { codingTag }
                                });
            context.SaveChanges();
        }
    }
}