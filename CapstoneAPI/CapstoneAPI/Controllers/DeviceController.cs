using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using SkyWeb.DatVM.Mvc.Autofac;
using System.Web.Mvc;
using System.Net.Http;
using System.Collections.Generic;

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
                Device model = deviceService.GetById(IMEI);
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
                            Content = new JsonContent("Add device is success")
                        };
                    }
                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.BadRequest,
                            Content = new JsonContent("An error occurred. Please try again.")
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.Conflict,
                        Content = new JsonContent("Device is exist")
                    };
                }
            } else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new JsonContent("User is not exist")
                };
            }
        }

        public HttpResponseMessage getListProduct(int userId)
        {
            IDeviceService deviceService = this.Service<IDeviceService>();
            IUserService userService = this.Service<IUserService>();
            User user = userService.GetById(userId);
            if (user != null)
            {
                List<DeviceViewModel> list = deviceService.getByUserId(userId);
                if (list == null)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Content = new JsonContent("List device is empty")
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(list)
                    };
                }
            }
            else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new JsonContent("User is not exist")
                };
            }
        }
    }
    
}