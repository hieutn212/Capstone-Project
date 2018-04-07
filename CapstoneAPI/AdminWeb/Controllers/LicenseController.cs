using CapstoneData.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wisky.Controllers
{
    public class LicenseController : Controller
    {
        // GET: Lisense
        public ActionResult Index()
        {
            return View();
        }

        public Boolean AddNewLicense(String username, Int64 licenseDay)
        {
            IUserService userService = this.Service<IUserService>();
            var user = userService.GetByUsername(username);
            if(user != null)
            {

            }
            return false;
        }
    }
}