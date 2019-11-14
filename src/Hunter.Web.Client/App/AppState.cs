using Hunter.Web.Client.Models;
using Hunter.Web.Client.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hunter.Web.Client
{
    public class AppState
    {
        private readonly HttpClient _httpClient;
        IJSRuntime jSRuntime;
        IOptions<HunterConfiguration> options;
        public AppState(IOptions<HunterConfiguration> options, IJSRuntime jSRuntime, IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this.options = options;
            this.jSRuntime = jSRuntime;
            BASE_URL = options.Value.BASEURL;
        }
        public async Task<ResponseModel<User>> Login(LoginModel loginDetails)
        {

            ResponseModel<User> response = new ResponseModel<User>();
            try
            {
                response = await _httpClient.PostJsonAsync<ResponseModel<User>>(BASE_URL + "/users/authenticate", loginDetails);
                if (response.Status == Enums.ResponseType.success.GetName())
                {
                    options.Value.User = response.Entity;
                    await SaveAccessToken(response.Entity.Token);
                }
            }
            catch (Exception ex)
            {
                response.Status = "error";
                response.Content = ex.Message;
            }

            return response;

        }
        public bool IsLoggedIn
        {
            get { return !string.IsNullOrEmpty(GetToken); }
        }
        public Task SaveAccessToken(string accessToken)
        {
            return jSRuntime.InvokeAsync<object>("wasmHelper.saveAccessToken", accessToken);
        }
        public Task<string> GetAccessToken()
        {
            return jSRuntime.InvokeAsync<string>("wasmHelper.getAccessToken");
        }
        public string GetToken
        {
            get
            {
                if(options.Value.User!=null)
                {
                    return options.Value.User.Token;
                }
                return string.Empty;     
            }
        }
        public void Logout()
        {
            options.Value.User.Token = null;
        }
        private string BASE_URL { get; set; }

    }
}
