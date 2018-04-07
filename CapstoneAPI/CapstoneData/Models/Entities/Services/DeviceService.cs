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
        List<DeviceViewModel> getByUserId(int userId);
    }
    public partial class DeviceService
    {
        public Device GetById(string id)
        {
            return this.GetActive(q => q.Id.ToUpper().Equals(id.ToUpper())).FirstOrDefault();
        }
        public async Task CreateProduct(Device model)
        {
            await this.CreateAsync(model);
        }
        public Device CheckProduct(string IMEI)
        {
            return this.GetActive(a => a.Id.ToUpper().Contains(IMEI.ToUpper())).FirstOrDefault();
        }
        public List<DeviceViewModel> getByUserId(int userId)
        {
            List<DeviceViewModel> result = this.GetActive(a => a.UserId == userId)
                .Select(q => new DeviceViewModel
                {
                    Id = q.Id,
                    Name = q.Name,
                    UserId = q.UserId,
                    Active = q.Active
                }
                ).ToList();
            return result;
        }
       
    }
    public class DeviceViewModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
