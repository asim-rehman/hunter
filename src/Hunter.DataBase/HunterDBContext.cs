using Hunter.DataBase.Configurations;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Hunter.DataBase
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class HunterDBContext : DbContext, IHunterDBContext
    {
        public HunterDBContext(DbContextOptions<HunterDBContext> options) : base(options)
        {

        }

        public DbSet<Devices> Devices { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskData> TaskData { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DevicesConfiguration());
            builder.ApplyConfiguration(new TaskDataConfiguration());
            builder.ApplyConfiguration(new TasksConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
