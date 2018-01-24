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
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var userService = this.Service<IUserService>();
                return new HttpResponseMessage()
                {
                    Content = new JsonContent("")
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
