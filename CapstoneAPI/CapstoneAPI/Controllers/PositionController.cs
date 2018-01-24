using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneAPI.Controllers
{
    public class PositionController : BaseApiController
    {
        public bool createProductPosition(float latitude, float longitude, float altitude, string deviceId)
        {
            Product_position model = new Product_position();
            IProduct_PositionService productPositionService = this.Service<IProduct_PositionService>();

            model = productPositionService.CheckProduct(latitude,longitude,altitude,deviceId);
            if (model == null)
            {
                model.latitude = latitude;
                model.longitude = longitude;
                model.altitude = altitude;
                model.deviceId = deviceId;
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
                return false;
            }
            return true;
        }
    }
}