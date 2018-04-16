
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wisky.Models;
using Wisky.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    public class HistoryController : Controller
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

        public JsonResult PositionDatatable(JQueryDataTableParamModel param, string IMEI, DateTime startDate, DateTime endDate)
        {
            try
            {
                var deviceService = this.Service<IDeviceService>();
                var device = deviceService.GetById(IMEI);

                if (device != null)
                {
                    IProduct_positionService positionService = this.Service<IProduct_positionService>();
                    IQueryable<Product_position> listPosition = positionService.getListById(IMEI, startDate, endDate);
                    if (listPosition == null)
                    {
                        return Json(new
                        {
                            sEcho = param.sEcho,
                            iTotalRecords = 0,
                            iTotalDisplayRecords = 0,
                            aaData = new List<Product_position>()
                        }, JsonRequestBehavior.AllowGet);
                    }
                    var positionList = listPosition.AsEnumerable()
                        .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.DeviceId).ToLower()
                                         .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                    int count = 1;
                    var rp = positionList
                        .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                        .Select(p => new IConvertible[]
                        {
                    count++,
                    p.CreatedDate.ToShortDateString() + " "+ p.CreatedDate.ToShortTimeString(),
                    p.Id,
                    p.Latitude,
                    p.Longitude,
                    p.Altitude,
                        });
                    var total = positionList.Count();
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
                    aaData = new List<Product_position>()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<Product_position>()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetPointMap(int id)
        {
            try
            {
                var positionService = this.Service<IProduct_positionService>();
                Product_position position = positionService.GetActive(q => q.Id == id).FirstOrDefault();
                if (position != null)
                {
                    return Json(new
                    {
                        success = true,
                        ID = position.Id,
                        Longitude = position.Longitude,
                        Latitude = position.Latitude,
                        Altitude = position.Latitude,
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

        public ActionResult GetAllCornerWithMap(int mapId)
        {
            try
            {
                ICornerService cornerService = this.Service<ICornerService>();
                List<Corner> corners = cornerService.GetListCornerWithMapId(mapId);

                if (corners != null)
                {
                    corners = corners.Select(q => new Corner()
                    {
                        Description = q.Description,
                        Floor = q.Floor,
                        Id = q.Id,
                        Latitude = q.Latitude,
                        Longitude = q.Longitude,
                        MapId = q.MapId,
                        Position = q.Position
                    }).ToList();

                        return Json(new
                        {
                            success = true,
                            result = corners,
                        });
                }

                return Json( new 
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
    }
}