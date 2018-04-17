using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{
    public partial interface IHistoryService
    {

        IQueryable<History> getListByUserId(int userId, DateTime startDate, DateTime endDate);
    }

    public partial class HistoryService
    {

        public IQueryable<History> getListByUserId(int userId, DateTime startDate, DateTime endDate)
        {
            return this.GetActive(q => (q.UserId == userId && q.CreatedDate >= startDate && q.CreatedDate <= endDate)).OrderByDescending(a => a.CreatedDate);
        }

        
    }
}
