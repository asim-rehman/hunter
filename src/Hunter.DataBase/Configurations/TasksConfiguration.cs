using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hunter.DataBase.Configurations
{
    public class TasksConfiguration : BaseConfiguration<Tasks>
    {
        public override void Configure(EntityTypeBuilder<Tasks> builder)
        {
            base.Configure(builder);
            builder.HasOne(e => e.Device).WithMany(e => e.Tasks)
                .HasForeignKey(p => p.DeviceId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade).IsRequired();
        }
    }
}
