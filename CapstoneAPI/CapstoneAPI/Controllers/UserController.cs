using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Net.Http;
using SkyWeb.DatVM.Mvc.Autofac;
using CapstoneAPI.Models;
using System.Web.Http;
using CapstoneData.Utility;

namespace CapstoneAPI.Controllers
{
    public class UserController : BaseApiController
    {
        public HttpResponseMessage Get(string username, string password)
        {
            try
            {
                IUserService userService = this.Service<IUserService>();
                User user = userService.GetByUsernameAndPassword(username, password);

                if (user != null)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(user)
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new JsonContent(e.Message)
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateAccount([FromBody] User user)
        {
            IUserService userService = this.Service<IUserService>();
            User newUser = userService.GetByUsername(user.Username);
            if (newUser == null)
            {
                try
                {
                    var md5 = new MD5Hasher(System.Web.Configuration.FormsAuthPasswordFormat.MD5);
                    user.Password = md5.HashPassword(user.Password);
                    userService.Create(user);
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                    };
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
            };
        }
    }
}
