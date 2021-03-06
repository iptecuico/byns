﻿using Codetecuico.Byns.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codetecuico.Byns.Data.Configuration
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Currency).HasMaxLength(10);
            builder.Property(x => x.Category).HasMaxLength(50);
            builder.Property(x => x.Condition).HasMaxLength(50);
            builder.Property(x => x.Status).HasMaxLength(20);
            builder.Property(x => x.Remarks).HasMaxLength(1500);
            builder.HasOne(x => x.User)
                        .WithMany(x => x.Items)
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.Organization)
                        .WithMany(x => x.Items)
                        .HasForeignKey(x => x.OrganizationId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
