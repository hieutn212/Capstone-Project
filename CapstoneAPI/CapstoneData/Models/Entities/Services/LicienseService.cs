using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneData.Models.Entities.Services
{

    public partial interface ILicienseService
    {
        IQueryable<Liciense> getListByUserId(int UserId);
        Liciense getLicienseByUserIdAndType(int userId, int type);
        Boolean AddNewLiciense(int userId, Liciense liciense);
        Liciense getIsUseLiciense(int userId);
    }

    public partial class LicienseService
    {

        public int TYPE_Normal = 1;
        public int TYPE_VIP = 2;

        public IQueryable<Liciense> getListByUserId(int UserId)
        {
            DateTime today = DateTime.Now;
            return this.GetActive(q => (q.UserId == UserId && q.ExpireDate > today)).OrderByDescending(a => a.PackageId);
        }
        public Liciense getLicienseByUserIdAndType(int userId, int type)
        {
            return this.GetActive(q => (q.UserId == userId && q.PackageId == type)).FirstOrDefault();
        }

        public Liciense getIsUseLiciense(int userId)
        {
            return this.GetActive(q => q.UserId == userId && q.IsUse).FirstOrDefault();
        }

       
        public Boolean AddNewLiciense(int userId, Liciense liciense)
        {
            if (liciense.PackageId == TYPE_Normal)
            {
                try
                {
                    if(getLicienseByUserIdAndType(userId, TYPE_Normal) != null)
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_VIP);
                        if (licienseCheck != null)
                        {
                            var availableDay = (liciense.ExpireDate - DateTime.Now).Days;
                            liciense.IsUse = false;
                            liciense.ExpireDate = licienseCheck.ExpireDate.AddDays(availableDay);
                            this.Update(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        this.Update(liciense);
                        return true;
                    } else
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_VIP);
                        if (licienseCheck != null)
                        {
                            var availableDay = (liciense.ExpireDate - DateTime.Now).Days;
                            liciense.IsUse = false;
                            liciense.ExpireDate = licienseCheck.ExpireDate.AddDays(availableDay);
                            this.Create(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        this.Create(liciense);
                        return true;
                    }                   
                }
                catch (Exception e)
                {
                    //return false;
                    throw new Exception(e.Message);
                }
            }
            if (liciense.PackageId == TYPE_VIP)
            {
                try
                {
                    if(getLicienseByUserIdAndType(userId, TYPE_VIP) != null)
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_Normal);
                        if (licienseCheck != null)
                        {
                            licienseCheck.IsUse = false;
                            var availableDay = (licienseCheck.ExpireDate - DateTime.Now).Days;
                            licienseCheck.ExpireDate = liciense.ExpireDate.AddDays(availableDay);
                            this.Update(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        liciense.IsUse = true;
                        this.Update(liciense);
                        return true;
                    }else
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_Normal);
                        if (licienseCheck != null)
                        {
                            licienseCheck.IsUse = false;
                            var availableDay = (licienseCheck.ExpireDate - DateTime.Now).Days;
                            licienseCheck.ExpireDate = liciense.ExpireDate.AddDays(availableDay);
                            this.Create(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }                        
                        liciense.IsUse = true;
                        this.Create(liciense);
                        return true;
                    }                  
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);                    
                }
            }
            return false;
        }
    }
}
