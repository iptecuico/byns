using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Data.Configuration;
using System.Data.Entity;

namespace Codetecuico.Byns.Data
{
    public class BynsDbContext : DbContext
    {
        public BynsDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BynsDbContext, Migrations.Configuration>("DefaultConnection"));
            //Database.SetInitializer(new DropCreateDatabaseAlways<BynsDbContext>());
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ItemConfiguration());
        }
    }
}
