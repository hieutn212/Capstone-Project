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
    }

    public partial class UserService
    {
        public User GetByUsernameAndPassword(string username, string password)
        {
            //var md5 = new MD5Hasher(System.Web.Configuration.FormsAuthPasswordFormat.MD5);
            //string passwordHash = md5.HashPassword(password);
            return this.GetActive(q => q.Username.Equals(username) && q.Password.Contains(password)).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return this.GetActive(q => q.Username.Equals(username)).FirstOrDefault();
        }
    }
}
