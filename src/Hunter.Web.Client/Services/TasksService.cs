using Hunter.Web.Client.Models;
using Hunter.Web.Client.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hunter.Web.Client.Services
{

    public class TasksService : BaseService
    {
        public TasksService(IOptions<HunterConfiguration> options) : base(options)
        {
        }

        public async Task<ResponseModel<Tasks[]>> GetTasks(Guid id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Tasks[]> tasksListModel = new ResponseModel<Tasks[]>();
                try
                {
                    SetAuthToken(httpClient);
                    string url = BASE_URL + "/tasks/" + id + "?all=true";
                    HttpResponseMessage data = await httpClient.GetAsync(url);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        tasksListModel = JsonConvert.DeserializeObject<ResponseModel<Tasks[]>>(json);
                    }

            
                }
                catch (Exception ex)
                {
                    SetResponseValues(tasksListModel, ex);
                }

                return tasksListModel;

            }
        }
        public async Task<ResponseModel<Tasks>> Create(Tasks taskPostModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Tasks> response = new ResponseModel<Tasks>();
   
                try
                {
                    SetAuthToken(httpClient);
                    taskPostModel.DateModified = DateTime.UtcNow;
                    response = await httpClient.PostJsonAsync<ResponseModel<Tasks>>(BASE_URL + "/tasks", taskPostModel);

                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;
            }
        }
        public async Task<ResponseModel<Tasks>> Delete(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Tasks> response = new ResponseModel<Tasks>();
                try
                {
                    SetAuthToken(httpClient);
                    var data = await httpClient.DeleteAsync(BASE_URL + "/tasks/" + id);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<Tasks>>(json);
                    }
                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;
            }

        }
    }
}
