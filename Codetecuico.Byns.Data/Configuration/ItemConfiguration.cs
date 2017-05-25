using Codetecuico.Byns.Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Codetecuico.Byns.Data.Configuration
{
    class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            ToTable("Items");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Description).IsRequired().HasMaxLength(500);
            Property(x => x.Currency).HasMaxLength(10);
            Property(x => x.Category).HasMaxLength(50);
            Property(x => x.Condition).HasMaxLength(50);
            Property(x => x.Status).HasMaxLength(20);
            Property(x => x.Remarks).HasMaxLength(500);

            HasRequired(x => x.User);
        }
    }
}
