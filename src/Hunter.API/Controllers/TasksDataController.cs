using Hunter.API.Framework;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository;
using Hunter.DataBase.Repository.Interfaces;
using Hunter.Web.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hunter.API.Controllers
{
    /// <summary>
    /// API Controller for Tasks Data
    /// </summary>
    [Route("api/tasksdata")]
    [ApiController]
    public class TasksDataController : BaseAPIController
    {
        private ITasksDataRepository tasksDataRepository = null;
        private ITasksRepository tasksRepository = null;
        private readonly IHunterDBContext hunter;
        public TasksDataController(IHunterDBContext hunterDBContext)
        {
            tasksDataRepository = new TasksDataRepository(hunterDBContext);
            tasksRepository = new TasksRepository(hunterDBContext);
            this.hunter = hunterDBContext;
        }

        // GET: api/Schedules
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                IQueryable<TaskData> tasks = tasksDataRepository.RetrieveAll();
                return JsonResponse(tasks);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            try
            {
                var task = tasksRepository.RetrieveByPK(id);
                
                if(task != null && task.Device.User != CurrentUser(hunter))
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                var tasksData = tasksDataRepository.RetrieveByTaskId(id).ToList();
                if (tasksData == null)
                {
                    return JsonResponse(ResponseType.error,ResponseMessage.ItemNotFound);
                }
                return JsonResponse(tasksData);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        [Route("location")]
        [HttpGet("location/{id}")]
        public JsonResult Location(Guid id)
        {
            try
            {
                var task = tasksRepository.RetrieveByPK(id);

                if (task != null && task.Device.User != CurrentUser(hunter))
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                IList<Location> locations = new List<Location>();
                var tasksData = tasksDataRepository.RetrieveByTaskId(id).ToList();
                if (tasksData == null)
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }
                else
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    for (int i=0;i < tasksData.Count();i++)
                    {
                        var item = tasksData[i];
                        Location location = JsonConvert.DeserializeObject<Location>(item.Data);
                        locations.Add(location);
                    }
                }

                return JsonResponse(locations);


            }
            catch (Exception ex)
            {
                return InternalException(ex);               
            }
        }

        // POST: api/Schedules
        [HttpPost]
        public JsonResult Post([FromBody] TaskData taskData)
        {
            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error,ValidationErrors());
            }
            try
            {
                tasksDataRepository.Add(taskData);
                tasksDataRepository.SaveChanges();
                return JsonResponse(ResponseType.success,ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public JsonResult Put([FromBody] TaskData taskData)
        {
            

            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error,ValidationErrors());
            }

            try
            {
                tasksDataRepository.Update(taskData);
                tasksDataRepository.SaveChanges();
                return JsonResponse(ResponseType.success,ResponseMessage.ChangesSaved);
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
                var taskData = tasksDataRepository.RetrieveByPK(id);
                if (taskData == null)
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                tasksDataRepository.Delete(taskData);
                tasksDataRepository.SaveChanges();

                return JsonResponse(ResponseType.success,ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
    }
}
