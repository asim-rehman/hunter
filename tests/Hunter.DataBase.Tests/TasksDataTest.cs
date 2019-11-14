using Hunter.DataBase.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Hunter.DataBase.Tests
{
    public class TasksDataTest : BaseTest
    {
        Guid taskDataIdAdd = Guid.Parse("6b69541a-1ff7-4bcb-90e3-9eb589d07b17");
        Guid taskDataIdUpdate = Guid.Parse("1d9dc09f-8a8d-41cb-99f1-d0238231ff6d");
        Guid taskDataIdDelete = Guid.Parse("5ae5ef27-4f64-434a-8e75-bdfb6d047ce5");
        string onePlusJsonData = "{\"CurrentDateTime\":\"2019-07-29T18:17:33.155372Z\",\"Id\":\"PKQ1.180984.001\",\"Manufacturer\":\"OnePlus\",\"Model\":\"ONEPLUS A7000\",\"Device\":\"OnePlus7\",\"Display\":\"ONEPLUS A700\"}";
        string samsungJsonData = "{\"Id\":\"PKQ1.180716.001\",\"Manufacturer\":\"Samsung\",\"Model\":\"Galaxy S9\",\"Device\":\"Galaxy S9\",\"Display\":\"Samsung A5000_23_190527\"}";
        string mapData = "{\"Timestamp\":\"2019-08-30T18:31:06.679+00:00\",\"Latitude\":50.0859044,\"Longitude\":-8.7466984,\"Altitude\":188.80000305175781,\"Accuracy\":19.163999557495117,\"Speed\":null,\"Course\":null,\"IsFromMockProvider\":false}";
        [Test]
        public void Add()
        {
            User user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "test3@localhost.com",
                DateCreated = DateTime.UtcNow,
                Id = Guid.Parse("270e042a-baf6-11e9-a2a3-2a2ae2dbcce4")
            };

            TaskData taskData = new TaskData
            {
                Id = taskDataIdAdd,
                Data = onePlusJsonData,
                Tasks = new Tasks
                {
                    TaskType = Enums.TaskType.GetDeviceInfo,
                    Status = Enums.Status.Completed,
                    Device = new Devices
                    {
                        Id = Guid.NewGuid(),
                        Name = "TasksDataDeviceAdd#1",
                        Model = "TasksDataDeviceAdd#1",
                        Manufacturer = "TasksDataDeviceAdd",
                        User = user
                    },
                }
            };

            TaskData taskDataUpdate = new TaskData
            {
                Id = taskDataIdUpdate,
                Data = samsungJsonData,
                Tasks = new Tasks
                {
                    TaskType = Enums.TaskType.GetDeviceInfo,
                    Status = Enums.Status.Completed,
                    Device = new Devices
                    {
                        Id = Guid.NewGuid(),
                        Name = "TasksDataDeviceAdd#2",
                        Model = "TasksDataDeviceAdd#2",
                        Manufacturer = "TasksDataDeviceAdd#2",
                        User = user
                    },
                }
            };

            TaskData taskDataDelete = new TaskData
            {
                Id = taskDataIdDelete,
                Data = onePlusJsonData,
                Tasks = new Tasks
                {
                    TaskType = Enums.TaskType.GetDeviceInfo,
                    Status = Enums.Status.Completed,
                    Device = new Devices
                    {
                        Id = Guid.NewGuid(),
                        Name = "TasksDataDeviceAdd#3",
                        Model = "TasksDataDeviceAdd#3",
                        Manufacturer = "TasksDataDeviceAdd#3",
                        User = user
                    },
                }
            };
            TaskData mapTaskData = new TaskData
            {
                Id = Guid.NewGuid(),
                Data = mapData,
                Tasks = new Tasks
                {
                    TaskType = Enums.TaskType.GetLocation,
                    Status = Enums.Status.Completed,
                    Device = new Devices
                    {
                        Id = Guid.NewGuid(),
                        Name = "TasksDataDeviceMap#4",
                        Model = "TasksDataDeviceMap#4",
                        Manufacturer = "TasksDataDeviceMap#4",
                        User = user
                    },
                }
            };

            UserRepository.Add(user, "test");
            TasksDataRepository.Add(taskData);
            TasksDataRepository.Add(taskDataUpdate);
            TasksDataRepository.Add(taskDataDelete);
            TasksDataRepository.Add(mapTaskData);
            Assert.Greater(TasksDataRepository.SaveChanges(), 0);
        }

        [Test]
        public void AddToExistingTask()
        {

            TaskData taskData = new TaskData
            {
                Id = Guid.NewGuid(),
                Data = samsungJsonData,
                TasksId = Guid.Parse("09B15E45-8315-48D6-9EB2-21215295CECF")
            };
            
            TasksDataRepository.Add(taskData);
            Assert.Greater(TasksDataRepository.SaveChanges(), 0);
        }

        [Test]
        public void Update()
        {
            TaskData tasksData = TasksDataRepository.RetrieveByPK(taskDataIdUpdate);
            tasksData.DateModified = DateTime.Now;
            TasksDataRepository.Update(tasksData);
            Assert.Greater(TasksDataRepository.SaveChanges(), 0);
        }
        [Test]
        public void Delete()
        {
            TaskData tasksData = TasksDataRepository.RetrieveByPK(taskDataIdDelete);
            TasksDataRepository.Delete(tasksData);
            Assert.Greater(TasksDataRepository.SaveChanges(), 0);
        }
        [Test]
        public void RetrieveByPK()
        {
            TaskData tasks = TasksDataRepository.RetrieveByPK(taskDataIdAdd);
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.Write(JsonConvert.SerializeObject(tasks, jsonSerializerSettings));
            Assert.NotNull(tasks);
        }
        [Test]
        public void RetrieveTaskFromTaskData()
        {
            Tasks tasks = TasksDataRepository.RetrieveByPK(taskDataIdAdd).Tasks;
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.Write(JsonConvert.SerializeObject(tasks,jsonSerializerSettings));
            Assert.NotNull(tasks);
        }
        [Test]
        public void RetrieveAll()
        {
            IQueryable<TaskData> tasks = TasksDataRepository.RetrieveAll();
            Assert.Greater(tasks.Count(), 0);
        }
    }
}
