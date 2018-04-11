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

        public int TYPE_1 = 1;
        public int TYPE_2 = 2;

        public IQueryable<Liciense> getListByUserId(int UserId)
        {
            DateTime today = DateTime.Now;
            return this.GetActive(q => (q.UserId == UserId && q.ExpireDate > today)).OrderByDescending(a => a.Type);
        }
        public Liciense getLicienseByUserIdAndType(int userId, int type)
        {
            return this.GetActive(q => (q.UserId == userId && q.Type == type)).FirstOrDefault();
        }

        public Liciense getIsUseLiciense(int userId)
        {
            return this.GetActive(q => q.UserId == userId && q.IsUse).FirstOrDefault();
        }

        public int getId()
        {
            int id = 1;
            try
            {
                var lastOrFirstLiciense = this.GetActive(q => q.Id >= 0).LastOrDefault();
                if (lastOrFirstLiciense != null)
                {
                    id = lastOrFirstLiciense.Id + 1;
                }
                return id;
            }
            catch (Exception)
            {
                return id; ;
            }
        }
        public Boolean AddNewLiciense(int userId, Liciense liciense)
        {
            int id = getId();
            if (liciense.Type == TYPE_1)
            {
                try
                {
                    if(getLicienseByUserIdAndType(userId, TYPE_1) != null)
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_2);
                        if (licienseCheck != null)
                        {
                            var availableDay = (liciense.ExpireDate - DateTime.Now).Days;
                            liciense.IsUse = false;
                            liciense.ExpireDate = licienseCheck.ExpireDate.AddDays(availableDay);
                            this.Update(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        //this.Activate(liciense);
                        this.Update(liciense);
                        return true;
                    } else
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_2);
                        if (licienseCheck != null)
                        {
                            var availableDay = (liciense.ExpireDate - DateTime.Now).Days;
                            liciense.IsUse = false;
                            liciense.ExpireDate = licienseCheck.ExpireDate.AddDays(availableDay);
                            this.Create(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        liciense.Id = id;
                        //this.Activate(liciense);
                        this.Create(liciense);
                        return true;
                    }                   
                }
                catch (Exception)
                {
                    return false;
                }
            }
            if (liciense.Type == TYPE_2)
            {
                try
                {
                    if(getLicienseByUserIdAndType(userId, TYPE_2) != null)
                    {
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_1);
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
                        var licienseCheck = getLicienseByUserIdAndType(userId, TYPE_1);
                        if (licienseCheck != null)
                        {
                            licienseCheck.IsUse = false;
                            var availableDay = (licienseCheck.ExpireDate - DateTime.Now).Days;
                            licienseCheck.ExpireDate = liciense.ExpireDate.AddDays(availableDay);
                            this.Create(liciense);
                            this.Update(licienseCheck);
                            return true;
                        }
                        liciense.Id = id;
                        liciense.IsUse = true;
                        this.Create(liciense);
                        return true;
                    }                  
                }
                catch (Exception e)
                {
                    System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", e);
                    throw argEx;                    
                }
            }
            return false;
        }
    }
}
