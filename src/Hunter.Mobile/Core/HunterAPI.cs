using Android.Content;
using Hunter.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Mobile.Core
{
    public class HunterAPI : IHunterAPI
    {

        Context context;
        AppSettings appSettings;
        public HunterAPI()
        {
            appSettings = new AppSettings(context);
        }
        public HunterAPI(Context context)
        {
            this.context = context;
            appSettings = new AppSettings(context);
        }

        public async Task<ResponseModel<Tasks[]>> GetDeviceTasks()
        {
            ResponseModel<Tasks[]> tasks = new ResponseModel<Tasks[]>();
            using (HttpClient httpClient = new HttpClient())
            {                
                try
                {
                    if(!string.IsNullOrEmpty(appSettings.AuthToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appSettings.AuthToken);
                        var response = await httpClient.GetAsync(appSettings.BaseURL + "/tasks/" + appSettings.DeviceId);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                            tasks = JsonConvert.DeserializeObject<ResponseModel<Tasks[]>>(content);
                        }
                    }
                                                   
                }
                catch (Exception ex)
                {
                }
                return tasks;
            }
        }

        public async Task<ResponseModel<User>> Authenticate(LoginModel loginModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<User> response = new ResponseModel<User>();
                try
                {
                    var stringContent = JsonToHTTP(loginModel);
                    var httpresponse = await httpClient.PostAsync(appSettings.BaseURL + "/users/authenticate", stringContent);
                    if (httpresponse.IsSuccessStatusCode)
                    {
                        var content = await httpresponse.Content.ReadAsStringAsync();
                        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                        jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                        response=  JsonConvert.DeserializeObject<ResponseModel<User>>(content);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return response;
            }
        }
        public async Task PostTasksData(TaskData taskData)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appSettings.AuthToken);
                    var stringContent = JsonToHTTP(taskData);
                    var response = await httpClient.PostAsync(appSettings.BaseURL+"/tasksdata", stringContent);

                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(content);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
        }

        private StringContent JsonToHTTP(object item)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var data = JsonConvert.SerializeObject(item, jsonSerializerSettings);
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            return stringContent;
        }
    }
}