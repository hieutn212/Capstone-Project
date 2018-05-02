using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using Wisky.Models;
using CapstoneData.Models.Entities.Services;
using CapstoneData.Models.Entities;
using System;
using System.Linq;

namespace Wisky.Areas.Admin.Controllers
{
    public class AdminDetailController : BaseController
    {
        // GET: Admin/Accounts
        public ActionResult Index(LoginViewModel model)
        {
            try
            {
                ViewBag.Username = Session["Username"];
                IUserService userService = this.Service<IUserService>();
                if (Session["Username"] == null)
                {
                    return this.Redirect("/");
                }
                User user = userService.GetByUsername(Session["Username"].ToString());
                ILicienseService licienseService = this.Service<ILicienseService>();
                Liciense liciense = licienseService.getIsUseLiciense(user.Id);
                ViewBag.licienseType = "Tài khoản quản lý";
                ViewBag.UserFullName = user.Fullname;
                IDeviceService deviceService = this.Service<IDeviceService>();
                IQueryable<User> userTotal = userService.GetAllUser();
                ViewBag.TotalAccount = userTotal.Count();
                ViewBag.TotalDevice = deviceService.GetActive().Count();
                DateTime bt = (DateTime)user.Birthday;
                //ViewBag.BirthDay = String.Format("{0:dd/MM/yyyy}", bt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return View(model);
        }
    }
}