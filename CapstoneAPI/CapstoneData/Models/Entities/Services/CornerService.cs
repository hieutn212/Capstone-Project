using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface ICornerService
    {
        List<Corner> GetListCornerWithMapId(int mapId);
    }
    public partial class CornerService
    {
        public List<Corner> GetListCornerWithMapId(int mapId)
        {
            return this.GetActive(q => q.MapId == mapId).ToList();
        }
    }
}
