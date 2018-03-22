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
    public class CornerController : BaseApiController
    {
        public HttpResponseMessage GetAllCornerWithMap(int mapId)
        {
            try
            {
                ICornerService cornerService = this.Service<ICornerService>();
                List<Corner> corners = cornerService.GetListCornerWithMapId(mapId);

                if (corners != null)
                {
                    corners = corners.Select(q => new Corner()
                    {
                        Description = q.Description,
                        Floor = q.Floor,
                        Id = q.Id,
                        Latitude = q.Latitude,
                        Longitude = q.Longitude,
                        MapId = q.MapId,
                        Position = q.Position
                    }).ToList();
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(corners)
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
