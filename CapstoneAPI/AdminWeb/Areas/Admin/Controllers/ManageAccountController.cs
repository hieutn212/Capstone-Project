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
            return View();
        }

        [WebMethod]
        public ActionResult AddNewLicense(String username, int price, int type)
        {
            try
            {

                int day = getDay(price);

                IUserService userService = this.Service<IUserService>();
                IHistoryService historyService = this.Service<IHistoryService>();
                userService.AddExpireDay(username, day);
                User user = userService.GetByUsername(username);

                var licienseService = this.Service<ILicienseService>();
                //var listUserLiense = licienseService.getListByUserId(user.Id);

                //var dayToAdd = listUserLiense.FirstOrDefault(q => q.Type == type).ExpireDate;
                var flag = false;
                if (isCreated(user.Id, type))
                {
                    var liciense = licienseService.getLicienseByUserIdAndType(user.Id, type);
                    DateTime currentDay = DateTime.Now;
                    if (currentDay.CompareTo(liciense.ExpireDate) == -1)
                    {
                        currentDay = liciense.ExpireDate;
                    }
                    if (liciense.CreatedDate != null)
                    {
                        liciense.CreatedDate = currentDay;
                    }
                    liciense.DayOfPurchase = liciense.DayOfPurchase + day;
                    liciense.ExpireDate = currentDay.AddDays(day);
                    liciense.Active = true;
                    flag = licienseService.AddNewLiciense(user.Id, liciense);
                }
                else
                {
                    var liciense = new Liciense();
                    liciense.UserId = user.Id;
                    liciense.ExpireDate = DateTime.Now.AddDays(day);
                    liciense.CreatedDate = DateTime.Now;
                    liciense.Active = true;
                    liciense.DayOfPurchase = day;
                    liciense.IsUse = true;
                    liciense.Type = type;
                    flag = licienseService.AddNewLiciense(user.Id, liciense);
                }
                if (flag)
                {
                    Session["LicienseType"] = type;
                    //create history
                    History history = new History();
                    history.Price = (float)price;
                    history.UserId = user.Id;
                    history.CreatedDate = DateTime.Now;
                    history.BuyDate = day;
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

        private int getDay(int price)
        {
            int day = 0;
            if (price == 20 || price == 25)
            {
                day = 30;
            }
            if (price == 30 || price == 35)
            {
                day = 60;
            }
            if (price == 40 || price == 45)
            {
                day = 90;
            }
            return day;
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