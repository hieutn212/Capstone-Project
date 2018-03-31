using SkyWeb.DatVM.Mvc;
using System.Web.Mvc;

namespace Wisky.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            bool isAdmin = HttpContext.User.IsInRole("Admin");
            bool isPublisher = HttpContext.User.IsInRole("Publisher");
            bool isBrandManager = HttpContext.User.IsInRole("BrandManager");
            if (isAdmin)
                return this.Redirect("~/admin/accounts");
            if (isPublisher)
                return this.RedirectToAction("Index","Publisher",new { area = "BrandManager"});
            if (isBrandManager)
                return this.RedirectToAction("Index","Home",new { area = "BrandManager", brandId = 0 });
            return View();
        }

    }
}