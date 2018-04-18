using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Net.Http;
using SkyWeb.DatVM.Mvc.Autofac;
using CapstoneAPI.Models;
using System.Web.Http;
using CapstoneData.Utility;

namespace CapstoneAPI.Controllers
{
    public class UserController : BaseApiController
    {
        public HttpResponseMessage Get(string username, string password)
        {
            try
            {
                IUserService userService = this.Service<IUserService>();
                ILicienseService licenseService = this.Service<ILicienseService>();
                User user = userService.GetByUsernameAndPassword(username, password);

                if (user != null)
                {
                    int checkDate = DateTime.Now.CompareTo(user.ExpireDate);
                    Liciense licenseModel = licenseService.getIsUseLiciense(user.Id);
                    if (checkDate <= 0 && licenseModel != null)
                    {
                        UserViewModel model = new UserViewModel()
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Password = user.Password,
                            Birthday = user.Birthday,
                            Fullname = user.Fullname,
                            RoleId = user.RoleId,
                            Active = user.Active,
                            ExpireDate = user.ExpireDate,
                            PackageId = licenseModel.PackageId,
                        };
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new JsonContent(model)
                        };
                    }
                    else
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.ExpectationFailed,
                            Content = new JsonContent("Expired ")
                        };
                    }

                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Content = new JsonContent("Unauthorized"),
                };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new JsonContent(e.Message)
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateAccount(string username, string password, int roleId, string fullname, DateTime birthday, bool active)
        {
            IUserService userService = this.Service<IUserService>();
            User newUser = userService.GetByUsername(username);
            if (newUser == null)
            {
                try
                {
                    var md5 = new MD5Hasher(System.Web.Configuration.FormsAuthPasswordFormat.MD5);
                    password = md5.HashPassword(password);
                    User user = new User();
                    user.Username = username;
                    user.Password = password;
                    user.RoleId = roleId;
                    user.Fullname = fullname;
                    user.Birthday = birthday;
                    user.Active = active;
                    userService.Create(user);
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                    };
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
            };
        }
    }
}
