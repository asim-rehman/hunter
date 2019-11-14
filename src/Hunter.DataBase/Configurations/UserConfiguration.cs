using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hunter.DataBase.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Username).IsRequired();
            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.PasswordHash).IsRequired();
            builder.Property(e => e.PasswordSalt).IsRequired();
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
        }
    }
}