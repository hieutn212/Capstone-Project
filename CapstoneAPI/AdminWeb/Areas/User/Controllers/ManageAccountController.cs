using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using Newtonsoft.Json;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        //[WebMethod]
        public async System.Threading.Tasks.Task<ActionResult> AddNewLicense(String username, int LicenseTypeId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/License/getNewLicense?username=" + username + "&LicenseTypeId=" + LicenseTypeId);
                var result = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                if(response.StatusCode.ToString() == "OK")
                {
                    Session["LicienseType"] = result.ToString();
                }
                return this.RedirectToAction("Index", "ManageAccount", new { area = "User" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> GetListLicenseType()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/License/getAllListLicense");
                var licenseType = JsonConvert.DeserializeObject<List<LicenseType>>(response.Content.ReadAsStringAsync().Result);
                if (response.StatusCode.ToString() == "OK")
                {                
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

        //public Boolean isCreated(int userId, int type)
        //{
        //    try
        //    {
        //        var licienseService = this.Service<ILicienseService>();
        //        var liciense = licienseService.getLicienseByUserIdAndType(userId, type);
        //        if (liciense != null) return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return false;
        //}
    }
}