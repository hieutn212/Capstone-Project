using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Net.Http;
using SkyWeb.DatVM.Mvc.Autofac;
using CapstoneAPI.Models;

namespace CapstoneAPI.Controllers
{
    public class UserController : BaseApiController
    {
        public HttpResponseMessage Get(string username, string password)
        {
            try
            {
                IUserService userService = this.Service<IUserService>();
                User user = userService.GetByUsername(username, password);

                if(user != null)
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
    }
}
