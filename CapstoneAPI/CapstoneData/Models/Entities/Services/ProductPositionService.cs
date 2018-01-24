using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IProduct_PositionService
    {
        Product_position CheckProduct(float latitude, float longitude, float altitude, string deviceId);
        Task CreateProduct(Product_position model);
    }

    public partial class Product_positionService
    {
        public Product_position CheckProduct(float latitude, float longitude, float altitude, string deviceId)
        {
            return this.GetActive(q => q.latitude == latitude && q.longitude == longitude && q.altitude == altitude && q.deviceId.Equals(deviceId)).FirstOrDefault();
        }
        public async Task CreateProduct(Product_position model)
        {
            await this.CreateAsync(model);
        }
    }
}
