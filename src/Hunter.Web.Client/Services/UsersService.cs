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

    public class UsersService : BaseService
    {
        public UsersService(IOptions<HunterConfiguration> options) : base(options)
        {
        }

        public async Task<ResponseModel<User[]>> GetUsers()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<User[]> responseModel = new ResponseModel<User[]>();
                try
                {
                    SetAuthToken(httpClient);
                    string url = BASE_URL + "/users/";
                    HttpResponseMessage data = await httpClient.GetAsync(url);
                    if (data.IsSuccessStatusCode)
                    {
                        string json = await data.Content.ReadAsStringAsync();
                        responseModel = JsonConvert.DeserializeObject<ResponseModel<User[]>>(json);
                    }
                }
                catch (Exception ex)
                {
                    SetResponseValues(responseModel, ex);
                }
                return responseModel;
            }
        }
        public async Task<ResponseModel<User>> Create(User postModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<User> response = new ResponseModel<User>();
                try
                {
                    SetAuthToken(httpClient);
                    response = await httpClient.PostJsonAsync<ResponseModel<User>>(BASE_URL + "/users", postModel);

                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;
            }
        }
        public async Task<ResponseModel<ChangePasswordModel>> ChangePassword(ChangePasswordModel postModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<ChangePasswordModel> response = new ResponseModel<ChangePasswordModel>();
                try
                {
                    postModel.User = User;
                    SetAuthToken(httpClient);
                    response = await httpClient.PutJsonAsync<ResponseModel<ChangePasswordModel>>(BASE_URL + "/users/changepassword", postModel);

                }
                catch (Exception ex)
                {
                    SetResponseValues(response, ex);
                }
                return response;
            }
        }

        public async Task<ResponseModel<User>> Delete(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ResponseModel<User> response = new ResponseModel<User>();
                try
                {
                    SetAuthToken(httpClient);
                    var data = await httpClient.DeleteAsync(BASE_URL + "/users/" + id);
                    if (data.IsSuccessStatusCode)
                    {
                        string content = await data.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseModel<User>>(content);
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
