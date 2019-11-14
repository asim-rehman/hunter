using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hunter.DataBase.Configurations
{
    public class TaskDataConfiguration : BaseConfiguration<TaskData>
    {
        public override void Configure(EntityTypeBuilder<TaskData> builder)
        {
            base.Configure(builder);
            builder.HasOne(e => e.Tasks).WithMany(e => e.TaskData).HasForeignKey(e => e.TasksId);
        }
    }
}
