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
    public class DevicesService : BaseService
    {

        public DevicesService(IOptions<HunterConfiguration> options) : base(options)
        {

        }

        public async Task<ResponseModel<Devices[]>> GetDevices()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Devices[]> response = new ResponseModel<Devices[]>();
                try
                {
                    SetAuthToken(httpClient);
                    HttpResponseMessage data = await httpClient.GetAsync(BASE_URL + "/devices");
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<Devices[]>>(json);
                    }
                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;

            }
        }
        public async Task<ResponseModel<Devices>> Create(Devices devicePostModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Devices> response = new ResponseModel<Devices>();
                try
                {
                    SetAuthToken(httpClient);
                    devicePostModel.DateModified = DateTime.UtcNow;
                    response = await httpClient.PostJsonAsync<ResponseModel<Devices>>(BASE_URL + "/devices", devicePostModel);
                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;
            }

        }
        public async Task<ResponseModel<Devices>> Edit(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Devices> response = new ResponseModel<Devices>();
                string json = string.Empty;
                try
                {
                    SetAuthToken(httpClient);
                    HttpResponseMessage data = await httpClient.GetAsync(BASE_URL + "/devices/" + id);
                    if (data.IsSuccessStatusCode)
                    {
                        json = await data.Content.ReadAsStringAsync();
                        //TODO:VALID GUID EXISTS
                        response = JsonConvert.DeserializeObject<ResponseModel<Devices>>(json);
                    }
                }

                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;

            }
        }
        public async Task<ResponseModel<Devices>> Edit(Devices devicePostModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Devices> response = new ResponseModel<Devices>();

                try
                {
                    SetAuthToken(httpClient);
                    devicePostModel.DateModified = DateTime.UtcNow;
                    response = await httpClient.PutJsonAsync<ResponseModel<Devices>>(BASE_URL + "/devices", devicePostModel);
                    response.Entity = new Devices();
                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);

                }
                return response;

            }
        }
        public async Task<ResponseModel<Devices>> Delete(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<Devices> response = new ResponseModel<Devices>();
                try
                {
                    SetAuthToken(httpClient);
                    var data = await httpClient.DeleteAsync(BASE_URL + "/devices/" + id);
                    if (data.IsSuccessStatusCode)
                    {
                        string content = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<Devices>>(content);
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
