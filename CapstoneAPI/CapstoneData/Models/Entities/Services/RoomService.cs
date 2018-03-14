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
        List<Room> searchRoom(string name);
    }

    public partial class RoomService
    {
        public List<Room> GetListRoom(int mapId, int floor)
        {
            return this.GetActive(q => q.MapId == mapId && q.Floor == floor).ToList();
        }
        public List<Room> searchRoom(string name)
        {
            return this.GetActive(q => q.Name.ToUpper().Equals(name)).ToList();
        }

    }
}
