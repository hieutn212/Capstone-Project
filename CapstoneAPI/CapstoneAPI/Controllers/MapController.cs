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
        public HttpResponseMessage GetListRoom(int buildingId)
        {
            try
            {
                IMapService mapService = this.Service<IMapService>();
                List<Map> maps = mapService.searchMap(buildingId);
                //maps = maps.Select(q => new Map()
                //{
                //    Id = q.Id,
                //    Floor = q.Floor,
                //    Latitude = q.Latitude,
                //    Longitude = q.Longitude,
                //    Length = q.Length,
                //    MapId = q.MapId,
                //    Name = q.Name,
                //    PosAX = q.PosAX,
                //    PosAY = q.PosAY,
                //    PosBX = q.PosBX,
                //    PosBY = q.PosBY,
                //    Width = q.Width
                //}).ToList();
                if (maps != null)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(maps)
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
