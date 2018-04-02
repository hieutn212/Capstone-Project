using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using Wisky.Models;

namespace Wisky.Areas.Admin.Controllers
{
    public class AccountsController : BaseController
    {
        // GET: Admin/Accounts
        public ActionResult Index(LoginViewModel model)
        {
            return View(model);
        }
    }
}