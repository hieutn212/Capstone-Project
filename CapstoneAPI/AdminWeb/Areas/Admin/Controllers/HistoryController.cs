
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
                    p.DeviceId,
                    p.Latitude,
                    p.Longitude,
                    p.Altitude,
                    p.CreatedDate.ToShortDateString() + " "+ p.CreatedDate.ToShortTimeString(),
                    p.Id,
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
    }
}