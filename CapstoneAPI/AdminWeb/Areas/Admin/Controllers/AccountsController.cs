using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
//using CocShopData.Models.Entities.Service;
using System.Linq;
//using CocShopData.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AccountsController : BaseController
    {
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            //var productService = this.Service<IProductService>();
            //var orderService = this.Service<IOrderService>();
            //var customerService = this.Service<IAspNetUserService>();

            //ViewBag.TotalCustomer = customerService.GetAllCustomer().ToList().Count();
            //ViewBag.TotalOrder = orderService.Get(o => o.Status == (int)OrderStatusEnum.Waiting).ToList().Count();
            //ViewBag.TotalProduct = productService.GetAll().Where(p => p.Status == (int)ProductStatusEnum.Stocking).ToList().Count();
            return View();
        }
    }
}