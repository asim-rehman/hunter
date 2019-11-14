using Hunter.API.Framework;
using Hunter.API.Models;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Hunter.API.Controllers
{
    /// <summary>
    /// API Controller for Users
    /// </summary>

    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseAPIController
    {
        private IUserRepository userRepository;
        private IOptions<HunterConfiguration> options;

        public UsersController(IHunterDBContext hunter, IOptions<HunterConfiguration> options)
        {
            userRepository = new UserRepository(hunter);
            this.options = options;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                IList<UserDTO> devices = userRepository.RetrieveAll().Select(x => new UserDTO {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Id = x.Id
                }).ToList();
                return JsonResponse(devices);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Post([Microsoft.AspNetCore.Mvc.FromBody]UserDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ValidationErrors);
                }

                if(userRepository.Exists(userDto.Username))
                {
                    return JsonResponse(ResponseType.error, "Username already taken");
                }

                User user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Username = userDto.Username,
                };

                userRepository.Add(user, userDto.Password);
                userRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public JsonResult Authenticate([Microsoft.AspNetCore.Mvc.FromBody]UserDTO userDto)
        {
            var user = userRepository.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return JsonResponse(userDto,responseType:ResponseType.error, content:"Username or password incorrect");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Value.SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            var data = new
            {
                user.Id,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = tokenString
            };
            return JsonResponse(data);
        }
        [HttpPut]
        public JsonResult Put(Guid id, [Microsoft.AspNetCore.Mvc.FromBody]UserDTO userDto)
        {

            try
            {
                User user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Username = userDto.Username,
                };
                userRepository.Update(user);
                userRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
        [HttpPut]
        [Route("changepassword")]
        public JsonResult ChangePassword([Microsoft.AspNetCore.Mvc.FromBody]ChangePasswordModel changePasswordModel)
        {
            try
            {
                bool success=userRepository.ChangePassword(changePasswordModel.User, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
                userRepository.SaveChanges();
                if(success)
                    return JsonResponse(ResponseType.success, "Password Changed");
                else
                    return JsonResponse(ResponseType.error, "Password could not be changed");
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            try
            {
                var user = userRepository.RetrieveByPK(id);
   
                if (user == null)
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }
                userRepository.Delete(user);
                userRepository.SaveChanges();

                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }
    }
}
