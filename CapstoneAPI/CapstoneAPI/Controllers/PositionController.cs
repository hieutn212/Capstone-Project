using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Web.Http;

namespace CapstoneAPI.Controllers
{
    public class PositionController : BaseApiController
    {
        [HttpPost]
        public bool CreateProductPosition(float latitude, float longitude, float altitude, string deviceId)
        {
            IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
            IDeviceService deviceService = this.Service<IDeviceService>();

            Device device = deviceService.GetById(deviceId);

            if (device != null)
            {
                Product_position model = productPositionService.CheckProduct(latitude, longitude, altitude, deviceId);
                if (model == null)
                {
                    Product_position newPosition = new Product_position()
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Altitude = altitude,
                        DeviceId = deviceId,
                        Active = true,
                        CreatedDate = DateTime.Now,
                    };

                    try
                    {
                        productPositionService.CreateProduct(model);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    model.Active = true;
                    try
                    {
                        productPositionService.Update(model);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}