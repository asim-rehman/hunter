using Hunter.DataBase.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Hunter.DataBase.Tests
{

    public class DevicesTest : BaseTest
    {
        Guid deleteId = Guid.Parse("fbe4fdd0-050c-43a3-b1a8-4a8ea64368c7");
        Guid updateId = Guid.Parse("a5890e3e-1ea9-44c7-8120-047be126bdd0");
        Guid retrieveId = Guid.Parse("1b2607be-1157-4912-beec-ce929b1a4790");
        Guid deviceWithTaskId = Guid.Parse("6362c64b-129f-4f42-9f6b-889a425b3758");
        Guid taskId = Guid.Parse("0addbb20-cefa-4f50-9f41-8e7c2f5b89d3");
        string retrieveDeviceName = "TestRetrieveDevice";
        [Test]
        public void Add()
        {

            User user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "test1@localhost.com",
                DateCreated = DateTime.UtcNow,
                Id = Guid.Parse("270e02a4-baf6-11e9-a2a3-2a2ae2dbcce4")
            };

            Devices retrieveDevice = new Devices
            {
                Id = retrieveId,
                Name = "TestRetrieveDevice",
                Manufacturer = "RetrieveDevice",
                Model = "#1-InitialCreation",
                User= user
            };
            Devices updateDevice = new Devices
            {
                Id = updateId,
                Name = "TestUpdateDevice",
                Manufacturer = "UpdateMethod",
                Model = "#1-InitialCreation",
                User = user
            };
            Devices deleteDevice = new Devices
            {
                Id = deleteId,
                Name = "TestDeleteDevice",
                Manufacturer = "DeleteMethod",
                Model = "#1-InitialCreation",
                User = user
            };
            List<Tasks> tasksList = new List<Tasks>();
            tasksList.Add(new Tasks
            {
                Id = taskId,
                Status = Enums.Status.Waiting,
                TaskType = Enums.TaskType.GetDeviceInfo
            });
            Devices deviceWithTask = new Devices
            {
                Id = deviceWithTaskId,
                Name = "TestDeviceWithTask",
                Manufacturer = "AddMetehod",
                Model = "#1-InitialCreation",
                Tasks = tasksList,
                User = user
            };
            UserRepository.Add(user, "test");
            DeviceRepository.Add(updateDevice);
            DeviceRepository.Add(retrieveDevice);
            DeviceRepository.Add(deleteDevice);
            DeviceRepository.Add(deviceWithTask);

            int changes = DeviceRepository.SaveChanges();
            Assert.Greater(changes, 0);
        }

        [Test]
        public void Update()
        {
            Devices device = DeviceRepository.RetrieveByPK(updateId);            
            if(device != null)
            {
                device.Model = "#2-UpdatedDevice";
                device.DateModified = DateTime.Now;
                DeviceRepository.Update(device);
            }
            Assert.Greater(DeviceRepository.SaveChanges(), 0);
        }


        [Test]
        public void RetrieveByPK()
        {
            Devices device = DeviceRepository.RetrieveByPK(retrieveId);
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.WriteLine(JsonConvert.SerializeObject(device,jsonSerializerSettings));
            Assert.NotNull(device);
        }
        [Test]
        public void Retrieve()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Devices device = DeviceRepository.RetrieveByQuery(p => p.Name == retrieveDeviceName);
            Console.WriteLine(JsonConvert.SerializeObject(device,jsonSerializerSettings));
            Assert.NotNull(device);            
        }
        [Test]
        public void RetrieveAll()
        {
            IQueryable<Devices> device = DeviceRepository.RetrieveAll();
            Assert.Greater(device.Count(), 1);
        }


        [Test]
        public void DeleteWithTask()
        {
            Devices device = DeviceRepository.RetrieveByPK(deviceWithTaskId);
            if (device != null)
                DeviceRepository.Delete(device);

            Assert.Greater(DeviceRepository.SaveChanges(), 0);
        }

        [Test]
        public void Delete()
        {
            Devices device = DeviceRepository.RetrieveByPK(deleteId);
            if (device != null)
                DeviceRepository.Delete(device);

            Assert.Greater(DeviceRepository.SaveChanges(), 0);
        }

    }
}
