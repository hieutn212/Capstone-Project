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
        User GetByUsername(string username, string password);
    }

    public partial class UserService
    {
        public User GetByUsername(string username, string password)
        {
            var md5 = new MD5Hasher(System.Web.Configuration.FormsAuthPasswordFormat.MD5);
            string passwordHash = md5.HashPassword(password);
            return this.GetActive(q => q.Username.Contains(username) && q.Password.Contains(passwordHash)).FirstOrDefault();
        }
    }
}
