using Codetecuico.Byns.Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Codetecuico.Byns.Data.Configuration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Username).HasMaxLength(20);
            Property(x => x.FirstName).HasMaxLength(50);
            Property(x => x.LastName).HasMaxLength(50);
            Property(x => x.Email).HasMaxLength(50);
            Property(x => x.PersonalWebsite).HasMaxLength(50);
            Property(x => x.MobileNumber).HasMaxLength(20);
        }
    }
}
