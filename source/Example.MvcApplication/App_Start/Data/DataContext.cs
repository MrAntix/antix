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
        public DataContext() : this(null)
        {
        }

        public IDbSet<BlogEntryData> BlogEntries
        {
            get { return Set<BlogEntryData>(); }
        }

        public IDbSet<BlogTagData> BlogTags
        {
            get { return Set<BlogTagData>(); }
        }

        public IDbSet<UserData> Users
        {
            get { return Set<UserData>(); }
        }

        public IDbSet<UserSessionData> UserSessions
        {
            get { return Set<UserSessionData>(); }
        }

        public override int SaveChanges()
        {
            _keywordsManager.UpdateKeywords(this);

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogEntryData>()
                        .HasMany(e => e.Tags)
                        .WithMany();

            modelBuilder.Entity<BlogEntryData>()
                        .HasMany(e => e.Redirects)
                        .WithRequired();

            modelBuilder.Entity<BlogEntryRedirectData>()
                        .HasKey(e => new
                            {
                                e.Identifier,
                                e.BlogEntryId
                            });

            modelBuilder.Entity<UserData>()
                        .HasRequired(e => e.Email);

            modelBuilder.Entity<UserSessionData>()
                        .HasRequired(e => e.User)
                        .WithMany();
        }
    }
}