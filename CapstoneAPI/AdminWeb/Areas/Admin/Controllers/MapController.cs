
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wisky.Models;
using Wisky.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    public class MapController : Controller
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

        public JsonResult MapDatatable(JQueryDataTableParamModel param)
        {
            try
            {
                var mapService = this.Service<IMapService>();
                var maps = mapService.GetActive();

                if (maps != null)
                {
                    var mapList = maps.AsEnumerable()
                        .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Name).ToLower()
                                         .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                    int count = 1;
                    var rp = mapList
                        .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                        .Select(p => new IConvertible[]
                        {
                    count++,
                    p.Name,
                    p.Floor,
                    p.MapUrl,
                    p.Altitude,
                    p.Id,
                        });
                    var total = maps.Count();
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

        [HttpPost]
        public async Task<ActionResult> AddMap()
        {
            try
            {
                var file = Request.Files[0];
                var mapName = Request.Params[0];
                var mapFloor = Request.Params[1];
                var mapAltitude = Request.Params[2];
                var buildingId = Request.Params[3];
                Map model = new Map();
                var mapService = this.Service<IMapService>();
                var maps = mapService.GetActive(a => a.Name.ToUpper().Equals(mapName.ToUpper()));
                if (maps.Count() == 0)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var pathWeb = Path.Combine(Server.MapPath("/Maps/"), mapName);
                        var pathApi = pathWeb.Replace("AdminWeb", "CapstoneAPI");
                        file.SaveAs(pathApi);
                        model.Altitude = Double.Parse(mapAltitude);
                        model.Name = mapName;
                        model.MapUrl = "maps/" + mapName + ".png";
                        model.BuildingId = int.Parse(buildingId);
                        mapService.Create(model);
                    }
                    return Json(new
                    {
                        success = true,
                    });
                } else
                {
                    return Json(new
                    {
                        success = false,
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                });
            }
        }
    }
}