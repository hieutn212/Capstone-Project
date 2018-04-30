
using CapstoneData.Models.Entities;
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
    public class BuildingController : Controller
    {
        // GET: Admin/History
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return this.Redirect("/");
            }
            return View();
        }

        public JsonResult BuildingDatatable(JQueryDataTableParamModel param)
        {
            try
            {
                var buildingService = this.Service<IBuildingService>();
                var buildings = buildingService.GetActive();

                if (buildings != null)
                {
                    var buildingList = buildings.AsEnumerable()
                        .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Name).ToLower()
                                         .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                    int count = 1;
                    var rp = buildingList
                        .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                        .Select(p => new IConvertible[]
                        {
                    count++,
                    p.Name,
                    p.Address,
                    p.Id,
                        });
                    var total = buildings.Count();
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = total,
                        iTotalDisplayRecords = total,
                        aaData = rp
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<Building>()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<Building>()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> MapIndex(int id)
        {
            ViewData["BuildingId"] = id;
            return View();
        }
    }
}