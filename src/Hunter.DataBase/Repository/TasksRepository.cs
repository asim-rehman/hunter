using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hunter.DataBase.Repository
{
    /// <summary>
    /// Repo for performing CRUD against Tasks
    /// </summary>
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        private IHunterDBContext dBContext = null;
        public TasksRepository(IHunterDBContext hunterDBContext) : base(hunterDBContext)
        {
            dBContext = hunterDBContext;
        }

        public override void Add(Tasks Entity)
        {
            if(Entity.IntervalDays > 0)
            {
                Entity.End = Entity.Start.AddDays(Entity.IntervalDays);
            }

            base.Add(Entity);
        }

        /// <summary>
        /// Retrieves all tasks excepted deleted ones.
        /// </summary>
        /// <returns>Tasks including Task Data</returns>
        public override IQueryable<Tasks> RetrieveAll()
        {
            var query =  dBContext.Tasks.Include(p => p.Device).Include(p => p.TaskData);
            return query;
        }

        public override Tasks RetrieveByPK(Guid key)
        {
            return dBContext.Tasks.Include(p => p.Device).Include(p => p.TaskData).Where(p => p.Id == key).FirstOrDefault();
        }
    }
}
