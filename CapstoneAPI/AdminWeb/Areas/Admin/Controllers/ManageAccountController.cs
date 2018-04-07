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
        // GET: Admin/ManageAccount
        public ActionResult Index()
        {
            //ViewBag.Username = Session["Username"];
            return View();
        }

        [WebMethod]
        public ActionResult AddNewLicense (String username, int price)
        {
            try
            {
                int day = 0;
                if(price == 20)
                {
                    day = 30;
                }
                if(price == 30)
                {
                    day = 60;
                }
                if(price == 40)
                {
                    day = 90;
                }

                IUserService userService = this.Service<IUserService>() ;
                IHistoryService historyService = this.Service<IHistoryService>();
                userService.AddExpireDay(username, day);
                User user = userService.GetByUsername(username);
                History history = new History();
                history.Price = (float)price;
                history.UserId = user.Id;
                history.CreatedDate = DateTime.Now;
                history.BuyDate = day;
                historyService.Create(history);
                return this.RedirectToAction("Index", "ManageAccount", new { area = "Admin" });
                
            }
            catch (Exception)
            {
                
            }
            return this.RedirectToAction("Index", "ManageAccount", new { area = "Admin" });
        }
    }
}