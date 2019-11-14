using Hunter.Web.Client.Models;
using Hunter.Web.Client.Models.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hunter.Web.Client.Services
{

    public class TasksDataService : BaseService
    {
        public TasksDataService(IOptions<HunterConfiguration> options) : base(options)
        {
        }

        public async Task<ResponseModel<List<DeviceInfo>>> GetDeviceInfo(Guid id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<TaskData[]> response = new ResponseModel<TaskData[]>();
                ResponseModel<List<DeviceInfo>> responseModel = new ResponseModel<List<DeviceInfo>>();
                try
                {
                    SetAuthToken(httpClient);
                    string url = BASE_URL + "/tasksdata/" + id;
                    HttpResponseMessage data = await httpClient.GetAsync(url);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<TaskData[]>>(json);
                    }

                    responseModel.Status = response.Status;
                    responseModel.Exception = response.Exception;
                    responseModel.Content = response.Content;
                    responseModel.Entity = new List<DeviceInfo>();
                    for (int i=0; i < response.Entity.Length;i++)
                    {
                        var item = response.Entity[i];
                        responseModel.Entity.Add(JsonConvert.DeserializeObject<DeviceInfo>(item.Data));
                    }

                }
                catch (Exception ex)
                {
                    SetResponseValues(responseModel, ex);
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel<Location[]>> GetLocationData(Guid id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Location[]> response = new ResponseModel<Location[]>();
                try
                {
                    SetAuthToken(httpClient);
                    string url = BASE_URL + "/tasksdata/location/" + id;
                    HttpResponseMessage data = await httpClient.GetAsync(url);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<Location[]>>(json);
                    }

                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;

            }
        }

        public async Task<ResponseModel<TaskData>> Delete(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<TaskData> response = new ResponseModel<TaskData>();
                try
                {
                    SetAuthToken(httpClient);
                    var data = await httpClient.DeleteAsync(BASE_URL + "/tasksdata/" + id);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<TaskData>>(json);
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
