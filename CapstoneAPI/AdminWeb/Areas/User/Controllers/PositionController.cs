
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using Newtonsoft.Json;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wisky.Models;
using Wisky.Utility;

namespace Wisky.Areas.Admin.Controllers
{
    public class PositionController : Controller
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

        public async Task<JsonResult> PositionDatatable(JQueryDataTableParamModel param, string IMEI, DateTime startDate, DateTime endDate)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/position/getAllPosition?deviceId=" + IMEI + "&startDate=" + startDate + "&endDate=" + endDate);
                if (response.StatusCode.ToString() == "OK")
                {
                    var listPosition = JsonConvert.DeserializeObject<List<Product_position>>(response.Content.ReadAsStringAsync().Result);
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
                else
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
        public async Task<ActionResult> GetPointMap(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/position/getPointMap?id=" + id);
                if (response.StatusCode.ToString() == "OK")
                {
                    var position = JsonConvert.DeserializeObject<Product_position>(response.Content.ReadAsStringAsync().Result);
                    return Json(new
                    {
                        success = true,
                        ID = position.Id,
                        Longitude = position.Longitude,
                        Latitude = position.Latitude,
                        Altitude = position.Altitude,
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

        public async Task<ActionResult> GetAllCornerWithMap(int mapId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/Corner/GetAllCornerWithMap?mapId=" + mapId);
                if (response.StatusCode.ToString() == "OK")
                {
                    var corners = JsonConvert.DeserializeObject<List<Corner>>(response.Content.ReadAsStringAsync().Result);

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
                else
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