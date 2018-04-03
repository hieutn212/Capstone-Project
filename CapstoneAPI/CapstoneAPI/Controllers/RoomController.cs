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
    public class RoomController : BaseApiController
    {
        public HttpResponseMessage GetListRoom(int buildingId)
        {
            try
            {
                IRoomService roomService = this.Service<IRoomService>();
                List<Room> rooms = roomService.GetListRoom(buildingId);
                rooms = rooms.Select(q => new Room()
                {
                    Id = q.Id,
                    Floor = q.Floor,
                    Latitude = q.Latitude,
                    Longitude = q.Longitude,
                    Length = q.Length,
                    MapId = q.MapId,
                    Name = q.Name,
                    PosAX = q.PosAX,
                    PosAY = q.PosAY,
                    PosBX = q.PosBX,
                    PosBY = q.PosBY,
                    Width = q.Width
                }).ToList();
                if (rooms != null)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(rooms)
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
        public HttpResponseMessage searchRoom(string name, int buildingId)
        {
            try
            {
                IRoomService roomService = this.Service<IRoomService>();
                IMapService mapService = this.Service<IMapService>();
                List<Room> listRoom = roomService.searchRoom(name);
                List<Map> listMap = mapService.searchMap(buildingId);
                var model = from room in listRoom
                            join map in listMap
                                 on room.MapId equals map.Id
                            select new Room
                            {
                                Id = room.Id,
                                MapId = room.MapId,
                                Name = room.Name,
                                Floor = room.Floor,
                                Latitude = room.Latitude,
                                Length = room.Length,
                                Longitude = room.Longitude,
                                PosAX = room.PosAX,
                                PosAY = room.PosAY,
                                PosBX = room.PosBX,
                                PosBY = room.PosBY,
                                Width = room.Width
                            };
                Room roomModel = model.FirstOrDefault();
                if (roomModel != null)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new JsonContent(roomModel)
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
