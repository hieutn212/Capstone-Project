using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneAPI.Controllers
{
    public class PositionController : BaseApiController
    {
        public HttpResponseMessage CreateProductPosition(float latitude, float longitude, float altitude, string deviceId)
        {
            IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
            IDeviceService deviceService = this.Service<IDeviceService>();

            Device device = deviceService.GetById(deviceId);

            if (device != null)
            {
                Product_position model = productPositionService.CheckProduct(latitude, longitude, altitude, deviceId);
                if (model == null)
                {
                    model = new Product_position();
                    model.Latitude = latitude;
                    model.Longitude = longitude;
                    model.Altitude = altitude;
                    model.DeviceId = deviceId;
                    model.Active = true;
                    model.CreatedDate = DateTime.Now;
                    try
                    {
                        productPositionService.Create(model);
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
                            Content = new JsonContent("An error occurred. Please try again later")
                        };
                    }
                }
                else
                {
                    model.Active = true;
                    model.CreatedDate = DateTime.Now;
                    try
                    {
                        productPositionService.Update(model);
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new JsonContent("Update device is success")
                        };
                    }
                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.BadRequest,
                            Content = new JsonContent("An error occurred. Please try again later")
                        };
                    }
                }
            }
            else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new JsonContent("Device is not exist")
                };
            }
        }
        public HttpResponseMessage trackingProduct(string deviceId)
        {
            IDeviceService deviceService = this.Service<IDeviceService>();
            Device device = deviceService.GetById(deviceId);
            if (device != null)
            {
                IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
                Product_position model = productPositionService.trackingProduct(deviceId);
                if (model != null)
                {
                    Product_position positionModel = new Product_position()
                    {
                        Id = model.Id,
                        DeviceId = model.DeviceId,
                        Altitude = model.Altitude,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        CreatedDate = model.CreatedDate,
                        Active = model.Active,
                    };
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(positionModel)
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Content = new JsonContent("Can not find device")
                    };
                }
            }
            else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new JsonContent("Device is not exist")
                };
            }
        }
    }
}
