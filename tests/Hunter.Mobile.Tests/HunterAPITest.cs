using System;
using System.Threading.Tasks;
using Hunter.Mobile.Core;
using Hunter.Mobile.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hunter.Mobile.Tests
{
    /// <summary>
    /// Old test class, used to test API against physical mobile device
    /// </summary>
    [TestClass]
    public class HunterAPITest
    {
        private const string BASE_URL = "http://localhost:5000/api/client/";
        private const string ID = "E696E066-6E17-4177-92FB-8B8A2722CA37";
        [TestMethod]
        public async Task GetAPIData()
        {
            HunterAPI hunterAPI = new HunterAPI();
            var data = await hunterAPI.GetDeviceTasks();

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public async Task PostAPIData()
        {
            HunterAPI hunterAPI = new HunterAPI();
            var data = await hunterAPI.GetDeviceTasks();

            var task = data.Entity[0];

            TaskData tasksData = new TaskData();
            tasksData.TasksId = task.Id;
            tasksData.Data = "{\"name\":\"OnePlus 39\"}";
            await hunterAPI.PostTasksData(tasksData);

            Assert.IsNotNull(data);

        }

    }
}
