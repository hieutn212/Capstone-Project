using CocShopData.Models.Entities;
using CocShopData.Models.Entities.Service;
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
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CustomerDatatable(JQueryDataTableParamModel param)
        {
            try
            {
                var customerService = this.Service<IAspNetUserService>();
                var listCustomers = customerService.GetAllCustomer();
                if (listCustomers == null)
                {
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = new List<AspNetUser>()
                    }, JsonRequestBehavior.AllowGet);
                }
                var customerList = listCustomers.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.UserName).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower()) ||
                                     StringConvert.EscapeName(a.Id).ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                int count = 1;
                var rp = customerList
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                    .Select(p => new IConvertible[]
                    {
                    count++,
                    p.UserName,
                    p.Point,
                    p.PhoneNumber,
                    p.Email,
                    p.FullName,
                    });
                var total = listCustomers.Count();
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
                    aaData = new List<Product>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetCustomerDetail(string username)
        {
            try
            {
                var customerService = this.Service<IAspNetUserService>();
                var customer = customerService.GetByUsername(username);
                if (customer != null)
                {
                    return Json(new
                    {
                        success = true,
                        username = customer.UserName,
                        fullname = customer.FullName,
                        phone = customer.PhoneNumber,
                        email = customer.Email,
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
        public async Task<ActionResult> BuyPoint(string username, int point)
        {
            try
            {
                var customerService = this.Service<IAspNetUserService>();
                var customer = customerService.GetByUsername(username);
                if(customer != null)
                {
                    customer.Point += point;
                    customerService.Update(customer);

                    var historyService = this.Service<IHistoryService>();
                    await historyService.CreateAsync(new History()
                    {
                        CreatedDate = DateTime.Now,
                        CustomerId = customer.Id,
                        Point = point,
                    });
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

        public async Task<ActionResult> History (string id)
        {
            ViewData["Username"] = id;
            return View();
        }
    }
}