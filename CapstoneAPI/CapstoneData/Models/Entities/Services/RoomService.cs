using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IRoomService
    {
        List<Room> GetListRoom(int mapId, int floor);
        IQueryable GetListRoom(int mapId, int floor);
        Room searchRoom(string name, int mapId);
    }

    public partial class RoomService
    {
        public List<Room> GetListRoom(int mapId, int floor)
        {
            return this.GetActive(q => q.MapId == mapId && q.Floor == floor).ToList();
        }
        public Room searchRoom(string name, int mapId)
        {
            return this.GetActive(q => q.Name.ToUpper().Equals(name) && q.MapId == mapId).FirstOrDefault();
        }

    }
}
