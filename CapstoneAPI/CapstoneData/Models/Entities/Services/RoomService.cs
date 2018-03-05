using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IRoomService
    {
        IQueryable GetListRoom(int mapId, int floor);
        Room searchRoom(string name, int mapId);
    }

    public partial class RoomService
    {
        public IQueryable GetListRoom(int mapId, int floor)
        {
            return this.GetActive(q => q.MapId == mapId && q.Floor == floor);
        }
        public Room searchRoom(string name, int mapId)
        {
            return this.GetActive(q => q.Name.ToUpper().Equals(name) && q.MapId == mapId).FirstOrDefault();
        }

    }
}
