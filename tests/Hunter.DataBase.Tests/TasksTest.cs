using Hunter.DataBase.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Hunter.DataBase.Tests
{
    public class TasksTest : BaseTest
    {
        Guid task1WithSchedule = Guid.Parse("33d4ed29-5d18-4219-b4f6-cf236ff52920");
        Guid task2Delete = Guid.Parse("d69d4609-518c-47e0-b396-634b47dd0744");
        Guid task3Update = Guid.Parse("09B15E45-8315-48D6-9EB2-21215295CECF");
        Guid intervalTask = Guid.Parse("e8f46df3-cbc4-45ce-a2d6-262b67b0a97e");
        [Test]
        public void Add()
        {
            User user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "test2@localhost.com",
                DateCreated = DateTime.UtcNow,
                Id = Guid.Parse("270dfcc8-baf6-11e9-a2a3-2a2ae2dbcce4")
            };

            Tasks tasks = new Tasks
            {
                Id = task1WithSchedule,
                TaskType = Enums.TaskType.GetDeviceInfo,
                Status = Enums.Status.Waiting,
                IntervalDays=60,
                IntervalSeconds=120,
                Device = new Devices
                {
                    Id = Guid.NewGuid(),
                    Name = "TaskDeviceAddTest",
                    Model = "TestMethodAddTaskDevice",
                    Manufacturer = "TestMethodAddTaskDevice",
                    User= user
                }
            };
            Tasks updateTask = new Tasks
            {
                Id = task3Update,
                TaskType = Enums.TaskType.GetDeviceInfo,
                Status = Enums.Status.Ready,
                IntervalDays = 90,
                IntervalSeconds = 180,
                Device = new Devices
                {
                    Id = Guid.NewGuid(),
                    Name = "TaskDeviceUpdateTest",
                    Model = "TaskDeviceUpdateTest",
                    Manufacturer = "TaskDeviceUpdateTest",
                    User = user
                },
            };
            Tasks deleteTask = new Tasks
            {
                Id = task2Delete,
                TaskType = Enums.TaskType.GetDeviceInfo,
                Status = Enums.Status.Ready,
                Device = new Devices
                {
                    Id = Guid.NewGuid(),
                    Name = "TaskDeviceDeleteTest",
                    Model = "TaskDeviceDeleteTest",
                    Manufacturer = "TaskDeviceDeleteTest",
                    User = user
                },
            };
            Tasks withInternval = new Tasks
            {
                Id = intervalTask,
                TaskType = Enums.TaskType.GetDeviceInfo,
                Status = Enums.Status.Ready,
                IntervalSeconds=60,
                IntervalDays=30,
                Device = new Devices
                {
                    Id = Guid.NewGuid(),
                    Name = "TaskDeviceIntervaleTest",
                    Model = "TaskDeviceIntervaleTest",
                    Manufacturer = "TaskDeviceIntervaleTest",
                    User = user
                },
            };

            UserRepository.Add(user, "test");
            TasksRepository.Add(updateTask);
            TasksRepository.Add(deleteTask);
            TasksRepository.Add(tasks);
            TasksRepository.Add(withInternval);

            int changes = TasksRepository.SaveChanges();
            Assert.Greater(changes, 0);
           
        }

        [Test]
        public void Update()
        {
            Tasks tasks = TasksRepository.RetrieveByPK(task3Update);
            tasks.TaskData.Add(new TaskData
            {
                Data = "{\"name\":\"OnePlus 52\"}"
            });
            tasks.DateModified = DateTime.Now;
            TasksRepository.Update(tasks);
            Assert.Greater(TasksRepository.SaveChanges(), 0);
        }
        [Test]
        public void Delete()
        {
            Tasks tasks = TasksRepository.RetrieveByPK(task2Delete);
            if (tasks != null)
                TasksRepository.Delete(tasks);

            Assert.Greater(TasksRepository.SaveChanges(), 0);
        }
        [Test]
        public void RetrieveByPK()
        {
            Tasks tasks = TasksRepository.RetrieveByPK(task3Update);
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.Write(JsonConvert.SerializeObject(tasks, jsonSerializerSettings));
            Assert.NotNull(tasks);
        }
        [Test]
        public void RetrieveDeviceFromTask()
        {
            Devices device = TasksRepository.RetrieveByPK(task1WithSchedule).Device;
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.Write(JsonConvert.SerializeObject(device, jsonSerializerSettings));
            Assert.NotNull(device);
        }

        [Test]
        public void RetrieveAll()
        {
            IQueryable<Tasks> device = TasksRepository.RetrieveAll();
            Assert.Greater(device.Count(), 1);
        }
    }
}
