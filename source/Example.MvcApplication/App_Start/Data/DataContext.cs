using System.Data.Entity;
using Antix.Data.Keywords.EF;
using Microsoft.Practices.Unity;

namespace Example.MvcApplication.App_Start.Data
{
    public class DataContext : DbContext
    {
        readonly EFKeywordsManager _keywordsManager;

        public DataContext(EFKeywordsManager keywordsManager)
        {
            _keywordsManager = keywordsManager
                               ?? Bootstrapper
                                      .Initialise()
                                      .Resolve<EFKeywordsManager>();
        }

        // Required by migrations
        public DataContext():this(null){}

        public IDbSet<BlogEntry> BlogEntries
        {
            get { return Set<BlogEntry>(); }
        }

        public IDbSet<BlogTag> BlogTags
        {
            get { return Set<BlogTag>(); }
        }

        public override int SaveChanges()
        {
            _keywordsManager.UpdateKeywords(this);

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogEntry>()
                        .HasMany(entry => entry.Tags)
                        .WithMany();
        }
    }
}