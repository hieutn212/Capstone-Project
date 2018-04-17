using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface ILicenseTypeService
    {
        LicenseType getLicenseById(int LicenseTypeId);

        List<LicenseType> getAllList();
    }

    public partial class LicenseTypeService
    {
        public LicenseType getLicenseById(int LicenseTypeId)
        {
            return this.GetActive(q => q.Id == LicenseTypeId).FirstOrDefault();
        }
        public List<LicenseType> getAllList()
        {
            return this.GetActive(q => q.PackageId == 1 || q.PackageId == 2).ToList();
        }
    }
}
