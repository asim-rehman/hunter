using Hunter.API.Framework;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hunter.API.Controllers
{

    /// <summary>
    /// BaseAPI Controller, that every Controller inherits
    /// Has much of methods which return JSON data formatted in certain ways.
    /// Return a custom response
    /// </summary>
    [Area("API")]
    [Authorize]    
    public class BaseAPIController : ControllerBase
    {
        JsonSerializerSettings jsonSettings;
        public BaseAPIController()
        {
            jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        public JsonResult JsonResponse(ResponseType responseType, string message)
        {
            return new JsonResult(new { Status = Enum.GetName(typeof(ResponseType), responseType), Content = message }, jsonSettings);
        }

        public JsonResult JsonResponse<T>(ResponseType responseType, List<T> message)
        {
            return new JsonResult(new { Status = Enum.GetName(typeof(ResponseType), responseType), Content = message }, jsonSettings);
        }

        public JsonResult JsonResponse<T>(T entity, ResponseType responseType = ResponseType.success, string content = "") where T : class
        {
            return new JsonResult(new { Status = Enum.GetName(typeof(ResponseType), responseType), Content=content, Entity=entity }, jsonSettings);
        }

        protected JsonResult InternalException(Exception ex)
        {
            if(ex == null)
            {
                return JsonResponse(ResponseType.error, ResponseMessage.Http500);
            }
            else
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                    message = ex.InnerException.Message;

                    return JsonResponse(ResponseType.error, message);
            }
                
        }

        protected List<string> ValidationErrors()
        {
            List<string> validationMessage = new List<string>();
            foreach (KeyValuePair<string, ModelStateEntry> item in ModelState)
            {
                validationMessage.Add(item.Value.Errors.FirstOrDefault().ErrorMessage);                               
            }
            return validationMessage;
        }

        public User CurrentUser(IHunterDBContext hunterDBContext)
        {
            if (User != null)
            {
                UserRepository userRepository = new UserRepository(hunterDBContext);
                return userRepository.RetrieveByPK(Guid.Parse(User.Identity.Name));
            }
            return null;
        }
    }
}
