//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using SkyWeb.DatVM.Mvc;
//using SkyWeb.DatVM.Mvc.Autofac;
//using Wisky.Models;
//using Wisky.Models.Entities;
//using Wisky.Models.Entities.Services;
//using Wisky.Models.ViewModels;

//namespace Wisky.Areas.Admin.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class LandingAppsController : BaseController
//    {
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult LoadLandingAppDatatable(JQueryDataTableParamModel param)
//        {
//            var landingAppService = this.Service<ILandingAppService>();
//            var listLandingApps = landingAppService.GetActive().AsEnumerable();
//            var count = 1;
//            var rp = listLandingApps.Where(a => (string.IsNullOrEmpty(param.sSearch) || a.AppName.ToLower().Contains(param.sSearch.ToLower())
//                || a.Description.ToLower().Contains(param.sSearch.ToLower())))
//                .Skip(param.iDisplayStart).Take(param.iDisplayLength)
//                .Select(a => new IConvertible[]
//                {
//                    count ++,
//                    a.AppName,
//                    a.Description,
//                    a.AppUrl,
//                    a.Active,
//                    a.AppId,
//                    a.AppId,
//                }).ToArray();

//            return Json(new
//            {
//                sEcho = param.sEcho,
//                iTotalRecords = listLandingApps.Count(),
//                iTotalDisplayRecords = listLandingApps.Count(),
//                aaData = rp
//            }, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult Create()
//        {
//            return PartialView("Create");
//        }


//        [HttpPost]
//        public async Task<JsonResult> AddLandingApp()
//        {
//            try
//            {
//                var landingAppService = this.Service<ILandingAppService>();
//                //get params
//                var appName = Request.Params["Name"];
//                var appDescription = Request.Params["Description"];
//                var appUrl = Request.Params["AppUrl"];

//                #region Add to Database

//                LandingApp newLandingApp = new LandingApp()
//                {
//                    AppName = appName,
//                    Description = appDescription,
//                    AppUrl = appUrl,
//                    Active = true,
//                };

//                await landingAppService.CreateAsync(newLandingApp);

//            }
//            catch (Exception)
//            {
//                return Json(new { success = false, message = Resources.Message_EN_Resources.ErrorOccured });
//            }
//            #endregion

//            return Json(new { success = true, message = Resources.Message_EN_Resources.AddSuccessfully });

//        }

//        public async Task<ActionResult> Edit(int? id)
//        {
//            var landingAppService = this.Service<ILandingAppService>();
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var landingApp = new LandingAppViewModel(await landingAppService.GetAsync(id.Value));

//            return PartialView("Edit", landingApp);
//        }


//        public async Task<JsonResult> EditLandingApp()
//        {
//            var landingAppService = this.Service<ILandingAppService>();
//            var appID = Request.Params["ID"];
//            var appName = Request.Params["Name"];
//            var appDescription = Request.Params["Description"];
//            var appUrl = Request.Params["AppUrl"];
//            var isActive = Request.Params["IsActive"];
//            int id;
//            bool active;
//            try
//            {
//                id = int.Parse(appID);
//                active = bool.Parse(isActive);


//                //get Landing app
//                LandingApp newLandingApp = await landingAppService.GetAsync(id);
//                if (newLandingApp == null)
//                {
//                    return Json(new { success = false, message = Resources.Message_EN_Resources.ErrorOccured });
//                }

//                #region Update to Database
//                newLandingApp.AppName = appName;
//                newLandingApp.Description = appDescription;
//                newLandingApp.AppUrl = appUrl;
//                newLandingApp.Active = active;

//                await landingAppService.UpdateAsync(newLandingApp);
//                #endregion

//            }
//            catch (Exception)
//            {
//                return Json(new { success = false, message = Resources.Message_EN_Resources.ErrorOccured });
//            }
//            return Json(new { success = true, message = Resources.Message_EN_Resources.EditSuccessfully });
//        }


//        public async Task<JsonResult> DeleteApp()
//        {
//            var landingAppService = this.Service<ILandingAppService>();
//            var appID = Request.Params["ID"];
//            int id;
//            try
//            {
//                id = int.Parse(appID);
//                var landingApp =await landingAppService.GetAsync(id);
//                await landingAppService.UpdateAsync(landingApp);
               
//            }
//            catch (Exception)
//            {
//                return Json(new { success = false, message = Resources.Message_EN_Resources.ErrorOccured });
//            }

//            return Json(new { success = true, message = Resources.Message_EN_Resources.DeleteSuccessfully });

//        }
//        public async Task<ActionResult> Delete(int? id)
//        {
//            var landingAppService = this.Service<ILandingAppService>();
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var landingApp =new LandingAppViewModel(await landingAppService.GetAsync(id.Value));
           
//            return PartialView(landingApp);
//        }
//    }
//}