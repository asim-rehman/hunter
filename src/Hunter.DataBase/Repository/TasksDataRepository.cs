using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hunter.DataBase.Repository
{
    /// <summary>
    /// Repo for performing CRUD against TasksData
    /// </summary>
    public class TasksDataRepository : BaseRepository<TaskData>, ITasksDataRepository
    {
        private IHunterDBContext dBContext = null;
        public TasksDataRepository(IHunterDBContext hunterDBContext) : base(hunterDBContext)
        {
            dBContext = hunterDBContext;
        }

        public override void Add(TaskData Entity)
        {
            Tasks item = RetrieveTaskByPK(Entity.TasksId);
            if(item == null && Entity.Tasks != null)
            {
                item = RetrieveTaskByPK(Entity.Tasks.Id);
            }
            
            if(item != null)
            {
                item.LastRun = DateTime.UtcNow;

                if (item.End.HasValue)
                {
                    if (DateTime.UtcNow < item.End.Value)
                    {
                        item.NextRun = DateTime.UtcNow.AddSeconds(item.IntervalSeconds);
                        item.Status = Enums.Status.Running;
                    }
                    else
                    {
                        item.Status = Enums.Status.Completed;
                    }
                }

                dBContext.Entry<Tasks>(item).State = EntityState.Modified;
            }

            base.Add(Entity);
        }

        /// <summary>
        /// Retrieves all tasks data excepted deleted ones.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<TaskData> RetrieveAll()
        {
            return base.RetrieveAll(p => p.IsDeleted == false);
        }
        /// <summary>
        /// Retrieve all tasks data including tasks
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<TaskData> RetrieveByTaskId(Guid Id)
        {
            return base.RetrieveAll(p => p.TasksId==Id).Include(p => p.Tasks);
        }

        public Tasks RetrieveTaskByPK(Guid Id)
        {
            return dBContext.Set<Tasks>().Find(Id);
        }
    }
}
