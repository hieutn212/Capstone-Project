using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IMapService
    {
        List<Map> searchMap(int buildingId);
    }
    public partial class MapService
    {
        public List<Map> searchMap(int buildingId)
        {
            return this.GetActive(q => q.BuildingId == buildingId).ToList();
        }
    }
}
