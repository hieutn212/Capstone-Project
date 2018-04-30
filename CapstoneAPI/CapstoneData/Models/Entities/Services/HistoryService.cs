using CapstoneData.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IHistoryService
    {

        List<HistoryViewModel> getListByUserId(int userId, DateTime startDate, DateTime endDate);
        List<HistoryViewModel> getAllList(DateTime startDate, DateTime endDate);
    }

    public partial class HistoryService
    {

        public List<HistoryViewModel> getListByUserId(int userId, DateTime startDate, DateTime endDate)
        {
            return this.GetActive(q => (q.UserId == userId && q.CreatedDate >= startDate && q.CreatedDate <= endDate))
                .OrderByDescending(a => a.CreatedDate).Select(a => new HistoryViewModel
                {
                    Id = a.Id,
                    CreatedDate = a.CreatedDate,
                    Price = a.LicenseType.Price.Value,
                    BuyDate = a.LicenseType.BuyDate.Value,
                    UserId = a.UserId,
                    TypeId = a.TypeId,
                    username = a.User.Username,
                }).ToList();
        }
        public List<HistoryViewModel> getAllList(DateTime startDate, DateTime endDate)
        {
            return this.GetActive(q => (q.CreatedDate >= startDate && q.CreatedDate <= endDate))
                .Select(a => new HistoryViewModel {
                    CreatedDate = a.CreatedDate,
                    Price = a.LicenseType.Price.Value,
                }).ToList();
        }
    }
    public partial class HistoryViewModel
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string username { get; set; }

        public int TypeId { get; set; }
        public double Price { get; set; }
        public int BuyDate { get; set; }
        public LicenseType LicenseType { get; set; }
    }
}
