using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IDeviceService
    {
        Task CreateProduct(Device model);
        Device CheckProduct(string macAddress);

    }
    public partial class DeviceService
    {
        public async Task CreateProduct(Device model)
        {
            await this.CreateAsync(model);
        }
        public Device CheckProduct(string macAddress)
        {
            return this.GetActive(a => a.MACAddress.ToUpper().Equals(macAddress.ToUpper())).FirstOrDefault();
        }
    }
}
