using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hunter.DataBase
{
    /// <summary>
    /// For development purposes only
    /// </summary>
    public class HunterDBContextFactory : IDesignTimeDbContextFactory<HunterDBContext>
    {
        public HunterDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HunterDBContext>();
            string connection = @"Server=.\SQLEXPRESS;Database=HunterDBTest;User Id=sa;password=asim;";
            builder.UseSqlServer(connection);
            return new HunterDBContext(builder.Options);
        }
    }
}
