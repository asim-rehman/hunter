using Hunter.Web.Client.Models;
using Hunter.Web.Client.Models.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Hunter.Web.Client.Services
{
    public class BaseService
    {
        private IOptions<HunterConfiguration> options;
        public BaseService(IOptions<HunterConfiguration> options)
        {
            this.options = options;
            BASE_URL = options.Value.BASEURL;
        }
        protected void SetResponseValues<T>(ResponseModel<T> response, Exception ex) where T : class
        {
            response.Status = "error";
            response.Exception = ex;
            response.Content = ex.Message;
        }
        protected void SetAuthToken(HttpClient httpClient)
        {          
            if(User!=null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Token);
        }
        public string BASE_URL { get; private set; }
        public User User
        {
            get
            {
                return options.Value.User;
            }
        }
    }
}