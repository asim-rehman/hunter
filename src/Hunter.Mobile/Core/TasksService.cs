using Android.App;
using Android.Content;
using Hunter.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hunter.Mobile.Core
{
    [Service]
    public class TasksService : IntentService
    {      
        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                HunterBLL hunterBLL = new HunterBLL(BaseContext);

                Tasks[] tasks = GetTasksFromServer(hunterBLL).Result;
                bool go = true;
                int totalTasks = tasks.Length;
                int completedTasks = 0;
                DateTime nextTask = DateTime.UtcNow;
                if (totalTasks > 0)
                {

                    foreach (var item in tasks)
                    {
                        if(item.End > DateTime.UtcNow)
                        {
                            DoTask(item);
                        }                           
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void DoTask(Tasks task)
        {
            int milliseconds = task.IntervalSeconds * 1000;
            Timer timer = new Timer((e) => {
                SendTask(task);
            },null, milliseconds, Timeout.Infinite);
        }

        private void SendTask(Tasks task)
        {
            HunterBLL hunterBLL = new HunterBLL(BaseContext);
            if (task.Start <= DateTime.UtcNow && DateTime.UtcNow <= task.End)
            {
                if (task.NextRun <= DateTime.UtcNow)
                {
                    TaskData taskData = new TaskData();
                    taskData.TasksId = task.Id;

                    if (task.TaskType == Enums.TaskType.GetDeviceInfo)
                        taskData.Data = JsonConvert.SerializeObject(hunterBLL.GetDeviceInfo);
                    else
                        taskData.Data = JsonConvert.SerializeObject(hunterBLL.GetDeviceLocationAsync());
                    task.NextRun = DateTime.UtcNow.AddSeconds(task.IntervalSeconds);
                    task.LastRun = DateTime.UtcNow;

                    hunterBLL.PostTasksData(taskData);
                }
                DoTask(task);
            }
         
        }

        private async Task<Tasks[]> GetTasksFromServer(HunterBLL hunterBLL)
        {
            Tasks[] tasks = new Tasks[] { };
            var result = await hunterBLL.GetDeviceTasks();

            if (result.Entity == null)
                return tasks;
            else
                return result.Entity;
        }
    }
}