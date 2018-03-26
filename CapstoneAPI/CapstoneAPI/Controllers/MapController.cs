using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneAPI.Controllers
{
    public class MapController : BaseApiController
    {
        public HttpResponseMessage GetListMap(int buildingId)
        {
            try
            {
                IMapService mapService = this.Service<IMapService>();
                List<Map> maps = mapService.searchMap(buildingId);

                if (maps != null)
                {
                    var result = from map in maps
                                 select new Map
                                 {
                                     Id = map.Id,
                                     Name = map.Name,
                                     MapUrl = map.MapUrl,
                                     Floor = map.Floor,
                                     BuildingId = map.BuildingId
                                 };
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(result)
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new JsonContent("Bad Request"),
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
