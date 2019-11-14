using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using System;
using System.Linq;

namespace Hunter.DataBase.Repository.Interfaces
{
    public interface ITasksDataRepository : IRepository<TaskData, Guid>
    {
        IQueryable<TaskData> RetrieveByTaskId(Guid Id);
        Tasks RetrieveTaskByPK(Guid Id);        
    }
}
