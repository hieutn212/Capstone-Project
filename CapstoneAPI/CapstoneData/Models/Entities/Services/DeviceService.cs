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
        Task CreateProduct(Device model);
        Device CheckProduct(string IMEI);

    }
    public partial class DeviceService
    {
        public Device GetById(string id)
        {
            return this.GetActive(q => q.Id.Contains(id)).FirstOrDefault();
        }
        public async Task CreateProduct(Device model)
        {
            await this.CreateAsync(model);
        }
        public Device CheckProduct(string IMEI)
        {
            return this.GetActive(a => a.Id.ToUpper().Contains(IMEI.ToUpper())).FirstOrDefault();
        }

    }
}
