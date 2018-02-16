using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Codetecuico.Byns.Data
{
    public class BynsDbContext : DbContext
    {
        public BynsDbContext()
        { }

        public BynsDbContext(DbContextOptions<BynsDbContext> options) : base(options)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BynsDbContext, Migrations.Configuration>("DefaultConnection"));
            //Configuration.LazyLoadingEnabled = false;
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("DateCreated");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("DateModified");
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                                                .Where(e => e.State == EntityState.Added
                                                            || e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.Now;
                }

                entry.Property("DateModified").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}
