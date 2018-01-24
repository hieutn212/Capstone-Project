using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SkyWeb.DatVM.Mvc;
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

                return new HttpResponseMessage()
                {
                    Content = new JsonContent(user)
                };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    Content = new JsonContent(e.Message)
                };
            }
        }
    }
}
