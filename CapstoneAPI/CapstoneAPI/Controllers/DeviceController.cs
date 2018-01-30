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
            Device model = new Device();
            model = deviceService.CheckProduct(IMEI);
            if (model == null)
            {
                model.Id = IMEI;
                model.Name = name;
                model.UserId = userId;
                try
                {
                    deviceService.CreateProduct(model);
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.ExpectationFailed,
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
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };

        }
    }
}