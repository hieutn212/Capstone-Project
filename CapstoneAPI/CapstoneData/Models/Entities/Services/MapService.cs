using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IMapService
    {
        List<Map> searchMap(int buildingId);
        Map getMapById(int mapId);

    }
    public partial class MapService
    {
        public List<Map> searchMap(int buildingId)
        {
            return this.GetActive(q => q.BuildingId == buildingId).ToList();
        }
        public Map getMapById(int mapId)
        {
            return this.GetActive(q => q.Id == mapId).FirstOrDefault();
        }
    }

    public class MapViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public string MapUrl { get; set; }
        public int Floor { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
