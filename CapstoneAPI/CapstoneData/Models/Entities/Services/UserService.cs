using CapstoneData.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapstoneData.Models.Entities.Services
{
    public partial interface IUserService
    {
        User GetByUsernameAndPassword(string username, string password);
        User GetByUsername(string username);
        User GetById(int id);
        Boolean CheckExpireDay(String username);
        Boolean AddExpireDay(String username, Int64 totalDay);
    }

    public partial class UserService
    {
        public User GetByUsernameAndPassword(string username, string password)
        {
            var md5 = new MD5Hasher(System.Web.Configuration.FormsAuthPasswordFormat.MD5);
            string passwordHash = md5.HashPassword(password);
            return this.GetActive(q => q.Username.Equals(username) && q.Password.Equals(passwordHash)).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return this.GetActive(q => q.Username.Equals(username)).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return this.GetActive(q => q.Id == id).FirstOrDefault();
        }
        public Boolean CheckExpireDay(String username)
        {
            var user = GetByUsername(username);
            DateTime today = DateTime.Now;
            if (today.CompareTo((DateTime)user.ExpireDate) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean AddExpireDay(String username, Int64 totalDay)
        {
            try
            {
                var user = GetByUsername(username);
                DateTime currentDay = DateTime.Now.Date;
                if (currentDay.CompareTo((DateTime)user.ExpireDate) == -1)
                {
                    currentDay = (DateTime)user.ExpireDate;
                }
                //currentDay.AddDays((double)totalDay);
                currentDay=  currentDay.AddDays(totalDay);
                user.ExpireDate = currentDay;
                this.Update(user);
                return true;
            }
            catch (Exception)
            {
                
            }
            return false;
        }
    }
     public class UserViewModel {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Fullname { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public int TypeId { get; set; }
        public int PackageId { get; set; }
    }
}
