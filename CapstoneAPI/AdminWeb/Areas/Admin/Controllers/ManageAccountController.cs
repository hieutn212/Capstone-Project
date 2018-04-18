using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;


namespace Wisky.Areas.Admin.Controllers
{
    public class ManageAccountController : Controller
    {
        public int TYPE_1 = 1;
        public int TYPE_2 = 2;

        // GET: Admin/ManageAccount
        public ActionResult Index()
        {
            //ViewBag.Username = Session["Username"];
            if (Session["Username"] == null)
            {
                return this.Redirect("/");
            }
                return View();
        }

        [WebMethod]
        public ActionResult AddNewLicense(String username, int LicenseTypeId)
        {
            try
            {

                ILicenseTypeService licenseTypeService = this.Service<ILicenseTypeService>();
                var licenseType = licenseTypeService.getLicenseById(LicenseTypeId);

                IUserService userService = this.Service<IUserService>();
                IHistoryService historyService = this.Service<IHistoryService>();
                userService.AddExpireDay(username, (Int64) licenseType.BuyDate);
                User user = userService.GetByUsername(username);

                var licienseService = this.Service<ILicienseService>();
                //var listUserLiense = licienseService.getListByUserId(user.Id);

                //var dayToAdd = listUserLiense.FirstOrDefault(q => q.Type == type).ExpireDate;
                var flag = false;
                if (isCreated(user.Id, (int)licenseType.PackageId))
                {
                    var liciense = licienseService.getLicienseByUserIdAndType(user.Id, (int)licenseType.PackageId);
                    DateTime currentDay = DateTime.Now;
                    if (currentDay.CompareTo(liciense.ExpireDate) == -1)
                    {
                        currentDay = liciense.ExpireDate;
                    }
                    if (liciense.CreatedDate != null)
                    {
                        liciense.CreatedDate = currentDay;
                    }
                    liciense.DayOfPurchase = liciense.DayOfPurchase + (int)licenseType.BuyDate;
                    liciense.ExpireDate = currentDay.AddDays((int)licenseType.BuyDate);
                    liciense.Active = true;
                    flag = licienseService.AddNewLiciense(user.Id, liciense);
                }
                else
                {
                    var liciense = new Liciense();
                    liciense.UserId = user.Id;
                    liciense.ExpireDate = DateTime.Now.AddDays((Int64)licenseType.BuyDate);
                    liciense.CreatedDate = DateTime.Now;
                    liciense.Active = true;
                    liciense.DayOfPurchase = (int)licenseType.BuyDate;
                    liciense.IsUse = true;
                    liciense.PackageId = (int)licenseType.PackageId;
                    flag = licienseService.AddNewLiciense(user.Id, liciense);
                }
                if (flag)
                {
                    Session["LicienseType"] = licienseService.getIsUseLiciense(user.Id).PackageId.ToString();

                    //create history
                    History history = new History();
                    history.TypeId = licenseType.Id;
                    history.UserId = user.Id;
                    history.CreatedDate = DateTime.Now;
                    historyService.Create(history);
                    user.ExpireDate = licienseService.getIsUseLiciense(user.Id).ExpireDate;
                    userService.Update(user);
                }             
                return this.RedirectToAction("Index", "ManageAccount", new { area = "Admin" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public ActionResult GetListLicenseType()
        {
            try
            {
                var licenseTypeService = this.Service<ILicenseTypeService>();
                List<LicenseType> licenseType = licenseTypeService.getAllList();
                if (licenseType != null)
                {
                    licenseType = licenseType.Select(q => new LicenseType()
                    {
                        Id = q.Id,
                        TypeName = q.TypeName,
                        BuyDate = q.BuyDate,
                        Price = q.Price,
                        PackageId = q.PackageId,
                    }).ToList();
                    return Json(new
                    {
                        success = true,
                        result = licenseType
                    });
                }
                return Json(new
                {
                    success = false,
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        public Boolean isCreated(int userId, int type)
        {
            try
            {
                var licienseService = this.Service<ILicienseService>();
                var liciense = licienseService.getLicienseByUserIdAndType(userId, type);
                if (liciense != null) return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }
    }
}