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
                ILicienseService licienseService = this.Service<ILicienseService>();
                Liciense liciense = licienseService.getIsUseLiciense(user.Id);
                ViewBag.licienseType = "Tài khoản khách";
                ViewBag.UserFullName = user.Fullname;
                ViewBag.availableDay = 0;
                DateTime dt = DateTime.Now.AddDays(-1);
                if (liciense != null)
                {
                    if (liciense.Type == 1)
                    {
                        ViewBag.licienseType = "Tài khoản thường";
                    }
                    if (liciense.Type == 2)
                    {
                        ViewBag.licienseType = "Tài khoản VIP";
                    }
                    if (liciense.ExpireDate.CompareTo(DateTime.Now) == -1)
                    {
                        ViewBag.availableDay = 0;
                    }
                    else
                    {
                        ViewBag.availableDay = (liciense.ExpireDate - DateTime.Now).Days;
                    }
                    dt = (DateTime)liciense.ExpireDate;
                }
                
                //ViewBag.ExpireDay = String.Format("{0:dd/ MM/ yyyy}", dt);
                if (DateTime.Now.CompareTo(dt) <= 0)
                {
                    ViewBag.ExpireDay = String.Format("{0:dd/ MM/ yyyy}", dt);
                }
                else
                {
                    ViewBag.ExpireDay = "Hết hạn";
                }
                ViewBag.Status = "Đang hoạt động";
                if (!user.Active)
                {
                    ViewBag.Status = "Đã khóa";
                }
                DateTime bt = (DateTime)user.Birthday;
                ViewBag.BirthDay = String.Format("{0:dd/MM/yyyy}", bt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return View(model);
        }
    }
}