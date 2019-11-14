using Android.Content;
using Hunter.Mobile.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Hunter.Mobile.Core
{
    public class HunterBLL
    {
        Context context;
        string path = Path.Combine(FileSystem.AppDataDirectory, AppSettings.TASKS_FILE);
        public HunterBLL(Context context)
        {
            this.context = context;
        }

        public Models.DeviceInfo GetDeviceInfo
        {
            get
            {
                return new Models.DeviceInfo(context);
            }
        }

        public async Task<ResponseModel<Tasks[]>> GetDeviceTasks()
        {
           
            ResponseModel<Tasks[]> responseModel = new ResponseModel<Tasks[]>();
            HunterAPI hunterAPI = new HunterAPI(context);
            responseModel = await hunterAPI.GetDeviceTasks();

            if (responseModel.Entity != null & responseModel.Entity.Length > 0)
                File.WriteAllText(path, JsonConvert.SerializeObject(responseModel.Entity));

            return responseModel;


        }

        public Tasks[] GetTasksFromFile()
        {
            if(File.Exists(path))
            {
                string content = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Tasks[]>(content);
            }
            return new Tasks[]{ };
        }

        public async Task<ResponseModel<User>> Login(LoginModel loginModel)
        {
            HunterAPI hunterAPI = new HunterAPI(context);
            var result = await hunterAPI.Authenticate(loginModel);
            return result;
        }

        public async Task PostTasksData(TaskData taskData)
        {
            HunterAPI hunterAPI = new HunterAPI(context);
            await hunterAPI.PostTasksData(taskData);
        }

        public Location GetDeviceLocationAsync()
        {
            //var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = Geolocation.GetLastKnownLocationAsync().Result;
            return location;
        }
    }
}