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

namespace Wisky.Areas.Admin.Controllers
{
    public class AccountsController : BaseController
    {
        // GET: Admin/Accounts
        public ActionResult Index(LoginViewModel model)
        {
            try
            {
                ViewBag.Username = Session["Username"];
                IUserService userService = this.Service<IUserService>();
                User user = userService.GetByUsername(Session["Username"].ToString());
                ViewBag.UserFullName = user.Fullname;
                DateTime dt = (DateTime)user.ExpireDate;
                ViewBag.ExpireDay = String.Format("{0:dd/ MM/ yyyy}", dt);
                if (DateTime.Now.CompareTo(dt) <= 0)
                {
                    ViewBag.ExpireDay = String.Format("{0:dd/ MM/ yyyy}", dt);
                } else
                {
                    ViewBag.ExpireDay = "Hết hạn";
                }
                ViewBag.Status = "Đang hoạt động";
                if (!user.Active)
                {
                    ViewBag.Status = "Đang bị khóa";
                }
                DateTime bt = (DateTime)user.Birthday;
                ViewBag.BirthDay = String.Format("{0:dd/MM/yyyy}", bt);
            }
            catch (Exception)
            {
                
            }
           
            return View(model);
        }
    }
}