using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using SkyWeb.DatVM.Mvc.Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CapstoneAPI.Controllers
{
    public class DeviceController : BaseApiController
    {
        public HttpResponseMessage CreateProduct(string IMEI, string name, int userId)
        {
            IDeviceService deviceService = this.Service<IDeviceService>();
            IUserService userService = this.Service<IUserService>();
            User user = userService.GetById(userId);
            if (user != null)
            {
                Device model = deviceService.CheckProduct(IMEI);
                if (model == null)
                {
                    model = new Device();
                    model.Id = IMEI;
                    model.Name = name;
                    model.UserId = userId;
                    model.Active = true;
                    try
                    {
                        deviceService.CreateProduct(model);
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
                            Content = new JsonContent(e.Message)
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.Found,
                        Content = new JsonContent(model)
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }
}