using Hunter.API.Framework;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hunter.API.Controllers
{
    /// <summary>
    /// API Controller for Tasks
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : BaseAPIController
    {
        private ITasksRepository tasksRepository = null;
        private IDevicesRepository devicesRepository = null;
        private readonly IHunterDBContext hunter;

        public TasksController(IHunterDBContext hunterDBContext)
        {
            tasksRepository = new TasksRepository(hunterDBContext);
            devicesRepository = new DevicesRepository(hunterDBContext);
            this.hunter = hunterDBContext;
        }


        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id, bool all = false)
        {
            try
            {
                var tasks = tasksRepository.RetrieveByPK(id);
                if (tasks == null)
                {
                    if (!devicesRepository.Exists(id))
                    {
                        return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                    }

                    Expression<Func<Tasks, bool>> predicate = p => p.Device.Id == id && p.End > DateTime.UtcNow && p.Device.User == CurrentUser(hunter);
                    if (all)
                        predicate = p => p.Device.Id == id && p.Device.User == CurrentUser(hunter);

                    IList<Tasks> tasksList = tasksRepository.RetrieveAll(predicate).Include(p => p.TaskData)
                        .OrderBy(p => p.DateCreated).ToList();

                    for (int i = 0; i < tasksList.Count; i++)
                    {
                        var task = tasksList[i];
                        if (task.End <= DateTime.UtcNow && task.Status != DataBase.Enums.Status.Completed)
                        {
                            task.Status = DataBase.Enums.Status.Completed;
                            tasksRepository.Update(task);
                        }
                        tasksRepository.SaveChanges();
                    }

                    return JsonResponse(tasksList);
                }
                return JsonResponse(tasks);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // POST: api/Schedules
        [HttpPost]
        public JsonResult Post([FromBody] Tasks tasks)
        {
            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error, ValidationErrors());
            }
            try
            {
                tasks.Status = DataBase.Enums.Status.Waiting;
                tasksRepository.Add(tasks);
                tasksRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // PUT: api/Schedules/5
        [HttpPut()]
        public JsonResult Put([FromBody] Tasks tasks)
        {

            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error, ValidationErrors());
            }

            try
            {
                tasksRepository.Update(tasks);
                tasksRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            try
            {
                var tasks = tasksRepository.RetrieveByPK(id);
                if (tasks == null)
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                if(tasks.Device!=null && tasks.Device.User != CurrentUser(hunter))
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                tasksRepository.Delete(tasks);
                tasksRepository.SaveChanges();

                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
    }
}
