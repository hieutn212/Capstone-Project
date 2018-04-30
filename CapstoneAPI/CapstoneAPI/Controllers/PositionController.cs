using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using CapstoneData.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneAPI.Controllers
{
    public class PositionController : BaseApiController
    {
        public HttpResponseMessage CreateProductPosition(float latitude, float longitude, float altitude,
            string deviceId, int buildingId, float width, float height)
        {
            IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
            IDeviceService deviceService = this.Service<IDeviceService>();
            ICornerService cornerService = this.Service<ICornerService>();
            IMapService mapService = this.Service<IMapService>();

            Device device = deviceService.GetById(deviceId);

            if (device != null)
            {
                int mapId = 0;
                List<Map> maps = mapService.searchMap(buildingId);
                int mapsSize = maps.Count;
                for (int i = 0; i < mapsSize; i++)
                {
                    double altitudeMap1 = maps[i].Altitude ?? 0;
                    double altitudeMap2 = 0.0;
                    string nameMap = maps[i].Name;
                    if (i < mapsSize - 1)
                    {
                        altitudeMap2 = maps[i + 1].Altitude ?? 0;
                        if (altitude == 0.0)
                        {
                            mapId = 1;
                            break;
                        }
                        else if (altitudeMap1 <= altitude && altitude < altitudeMap2)
                        {
                            mapId = maps[i].Id;
                            break;
                        }
                    }
                    else
                    {
                        if (altitudeMap1 <= altitude)
                        {
                            mapId = maps[i].Id;
                            break;
                        }
                    }
                }

                List<Corner> corners = cornerService.GetListCornerWithMapId(mapId);
                float posX = 0;
                float posY = 0;
                Utils.GetPointMap(latitude, longitude, corners, width, height, out posX, out posY);
                if (posX >= 0 && posX <= width && posY >= 0 && posY <= height)
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
                                Content = new JsonContent("Can not add your device position!!!")
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
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new JsonContent("Device is not exist")
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
        public HttpResponseMessage getPointMap(int id)
        {
                var positionService = this.Service<IProduct_positionService>();
                Product_position position = positionService.GetActive(q => q.Id == id).FirstOrDefault();
                if (position != null)
                {
                    Product_position positionModel = new Product_position()
                    {
                        Id = position.Id,
                        Longitude = position.Longitude,
                        Latitude = position.Latitude,
                        Altitude = position.Altitude,
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

        public HttpResponseMessage trackingProductWithTime(string deviceId, int timeSearch)
        {
            IDeviceService deviceService = this.Service<IDeviceService>();
            Device device = deviceService.GetById(deviceId);
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddMinutes(timeSearch*(-1));
            if (device != null)
            {
                IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
                Product_position positionAlt = productPositionService.trackingProduct(deviceId);
                double altitude = 0.0;
                if (positionAlt != null)
                {
                    altitude = positionAlt.Altitude.Value;
                }
                List<Product_position> positions = productPositionService.getListByTime(deviceId, endDate, startDate,altitude);
                if (positions != null)
                {
                    positions = positions.Select(q=> new Product_position()
                    {
                        Id = q.Id,
                        DeviceId = q.DeviceId,
                        Altitude = q.Altitude,
                        Latitude = q.Latitude,
                        Longitude = q.Longitude,
                        CreatedDate = q.CreatedDate,
                        Active = q.Active,
                    }).ToList();
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(positions)
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

        public HttpResponseMessage getAllPosition(string deviceId, DateTime startDate, DateTime endDate)
        {
            IDeviceService deviceService = this.Service<IDeviceService>();
            Device device = deviceService.GetById(deviceId);
            if (device != null)
            {
                IProduct_positionService productPositionService = this.Service<IProduct_positionService>();
                List<Product_position> positions = productPositionService.getListById(deviceId, startDate, endDate).ToList();
                if (positions != null)
                {
                    positions = positions.Select(q => new Product_position()
                    {
                        Id = q.Id,
                        DeviceId = q.DeviceId,
                        Altitude = q.Altitude,
                        Latitude = q.Latitude,
                        Longitude = q.Longitude,
                        CreatedDate = q.CreatedDate,
                        Active = q.Active,
                    }).ToList();
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(positions)
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
