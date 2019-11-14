using Hunter.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Hunter.DataBase.Interfaces
{
    public interface IHunterDBContext
    {
        DbSet<Devices> Devices { get; set; }
        DbSet<Tasks> Tasks { get; set; }
        DbSet<TaskData> TaskData { get; set; }
        DbSet<User> User { get; set; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T Entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
