using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hunter.DataBase.Configurations
{
    public class DevicesConfiguration : BaseConfiguration<Devices>
    {
        public override void Configure(EntityTypeBuilder<Devices> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Model).IsRequired();
            builder.Property(e => e.Manufacturer).IsRequired();

            builder.HasOne(e => e.User).WithMany(e => e.Devices).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
