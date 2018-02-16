using Codetecuico.Byns.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codetecuico.Byns.Data.Configuration
{
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                                        .HasDefaultValueSql("NEW Guid()");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            //builder.HasMany(x => x.Users)
            //           .WithOne()
            //           .HasForeignKey(x => x.OrganizationId)
            //           .OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(x => x.Items)
            //           .WithOne()
            //           .HasForeignKey(x => x.OrganizationId)
            //           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
