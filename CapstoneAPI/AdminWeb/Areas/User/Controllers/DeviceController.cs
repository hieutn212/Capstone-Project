
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
    public class DeviceController : Controller
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return this.Redirect("/");
            }
            return View();
        }

        public JsonResult DeviceDatatable(JQueryDataTableParamModel param)
        {
            try
            {
                var userService = this.Service<IUserService>();
                User user = userService.GetByUsername(Session["Username"].ToString());
                var deviceService = this.Service<IDeviceService>();
                var listDevices = deviceService.getByUserId(user.Id);
                if (listDevices == null)
                {
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = listDevices
                    }, JsonRequestBehavior.AllowGet);
                }
                var deviceList = listDevices.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Name).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower()) ||
                                     StringConvert.EscapeName(a.Id).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                int count = 1;
                var rp = deviceList
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                    .Select(p => new IConvertible[]
                    {
                    count++,
                    p.Id,
                    p.Name,
                    p.Active,
                    });
                var total = listDevices.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = total,
                    iTotalDisplayRecords = total,
                    aaData = rp
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<DeviceViewModel>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetDeviceDetail(string IMEI)
        {
            try
            {
                var deviceService = this.Service<IDeviceService>();
                var device = deviceService.GetById(IMEI);
                if (device != null)
                {
                    return Json(new
                    {
                        success = true,
                        IMEI = device.Id,
                        name = device.Name,
                        active = device.Active,
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

        [HttpPost]
        public async Task<ActionResult> DeleteDevice(string IMEI)
        {
            try
            {
                var deviceService = this.Service<IDeviceService>();
                var device = deviceService.GetById(IMEI);
                if (device != null)
                {
                    device.Active = false;
                    deviceService.Update(device);
                    return Json(new
                    {
                        success = true,
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

        [HttpPost]
        public async Task<ActionResult> UpdateDevice(string IMEI, string deviceName)
        {
            try
            {
                var deviceService = this.Service<IDeviceService>();
                var device = deviceService.GetById(IMEI);
                if (device != null)
                {
                    device.Name = deviceName;
                    deviceService.Update(device);
                    return Json(new
                    {
                        success = true,
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
        [HttpPost]
        public async Task<ActionResult> AddDevice(string IMEI, string deviceName)
        {
            try
            {
                String username = Session["Username"].ToString();
                int userId = 0;
                if (username != null)
                {
                    var userService = this.Service<IUserService>();
                    var user = userService.GetByUsername(username);
                    userId = user.Id;
                }
                var deviceService = this.Service<IDeviceService>();
                var device = deviceService.GetAllById(IMEI);                
                if (device == null)
                {
                    Device model = new Device();
                    model.Id = IMEI;
                    model.Name = deviceName;
                    model.Active = true;
                    model.UserId = userId;
                    deviceService.Create(model);
                    return Json(new
                    {
                        success = true,
                    });
                } else
                {
                    if (device.UserId == userId)
                    {
                        device.Active = true;
                        device.Name = deviceName;
                        deviceService.Update(device);
                        return Json(new
                        {
                            success = true,
                        });
                    }
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

        public async Task<ActionResult> History(string id)
        {
            var deviceService = this.Service<IDeviceService>();
            ViewData["Username"] = deviceService.GetById(id).Name;
            ViewData["IMEI"] = id;
            return View();
        }
    }
}