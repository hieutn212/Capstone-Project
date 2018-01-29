using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IDeviceService
    {
        Device GetById(string id);
    }
    public partial class DeviceService
    {
        public Device GetById(string id)
        {
            return this.GetActive(q => q.Id.Contains(id)).FirstOrDefault();
        }
    }
}
