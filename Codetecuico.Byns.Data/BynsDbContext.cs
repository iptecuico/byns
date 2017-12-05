using Codetecuico.Byns.Data.Entity;
using Codetecuico.Byns.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Codetecuico.Byns.Data
{
    public class BynsDbContext : DbContext
    {
        public BynsDbContext()
        {

        }
        public BynsDbContext(DbContextOptions<BynsDbContext> options) : base(options)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BynsDbContext, Migrations.Configuration>("DefaultConnection"));
            //Database.SetInitializer(new DropCreateDatabaseAlways<BynsDbContext>());
            //Configuration.LazyLoadingEnabled = false;
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
