using CapstoneData.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wisky.Models;
using Wisky.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UserDatatable(JQueryDataTableParamModel param)
        {
            try
            {
                var userService = this.Service<IUserService>();
                var listUsers = userService.GetAllUser();
                if (listUsers == null)
                {
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = new List<UserViewModel>()
                    }, JsonRequestBehavior.AllowGet);
                }
                var userList = listUsers.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Username).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                int count = 1;

                var rp = userList
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                    .Select(p => new IConvertible[]
                    {
                    count++,
                    p.Username,
                    p.Fullname,
                    p.Devices.Count,
                    String.Format("{0:dd/ MM/ yyyy}", p.ExpireDate),
                    (p.Licienses.Where(q=> q.UserId == p.Id && q.IsUse).Count()) == 0 ? 0 : p.Licienses.Where(q=> q.UserId == p.Id && q.IsUse).FirstOrDefault().PackageId,
                    p.Active,
                    p.Id,
                    });
                var total = listUsers.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = total,
                    iTotalDisplayRecords = total,
                    aaData = rp
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<UserViewModel>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ActiveAccount(int userId)
        {
            try
            {
                var userService = this.Service<IUserService>();
                var user = userService.GetAllById(userId);
                if (user != null)
                {
                    user.Active = true;
                    userService.Update(user);
                    return Json(new
                    {
                        success = true,
                    });
                }
                return Json(new
                {
                    success = false,
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    success = true,
                });
            }
        }

        public async Task<ActionResult> DeActiveAccount(int userId)
        {
            try
            {
                var userService = this.Service<IUserService>();
                var user = userService.GetById(userId);
                if (user!= null)
                {
                    user.Active = false;
                    userService.Update(user);
                    return Json(new
                    {
                        success = true,
                    });
                }
                return Json(new
                {
                    success = false,
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    success = true,
                });
            }
        }
    }
}