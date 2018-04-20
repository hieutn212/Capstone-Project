
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
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return this.Redirect("/");
            }
            return View();
        }

        public JsonResult HistoryDatatable(JQueryDataTableParamModel param, DateTime startDate, DateTime endDate)
        {
            try
            {
                String username = Session["Username"].ToString();
                int userId = 0;
                if(username != null)
                {
                    var userService = this.Service<IUserService>();
                    var user = userService.GetByUsername(username);
                    userId = user.Id;
                }
                var historyService = this.Service<IHistoryService>();
                var listHistorys = historyService.getListByUserId(userId, startDate, endDate);
                if (listHistorys == null)
                {
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = new List<History>()
                    }, JsonRequestBehavior.AllowGet);
                }
                var historyList = listHistorys.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.LicenseType.Price + "").ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                int count = 1;
                var rp = historyList
                    .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                    .Select(p => new IConvertible[]
                    {
                    count++,
                    p.User.Username,
                    p.CreatedDate.ToShortDateString() + " " + p.CreatedDate.ToShortTimeString(),
                    p.LicenseType.BuyDate.ToString() + " ngày",
                    p.LicenseType.Price.ToString() + " $",
                    });
                var total = listHistorys.Count();
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
                    aaData = new List<History>()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult AcceptOrderDatatable(JQueryDataTableParamModel param, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var orderService = this.Service<IOrderService>();
        //        var listOrders = orderService.GetAllAcceptOrderWithDateRange(startDate, endDate);
        //        if (listOrders == null)
        //        {
        //            return Json(new
        //            {
        //                sEcho = param.sEcho,
        //                iTotalRecords = 0,
        //                iTotalDisplayRecords = 0,
        //                aaData = new List<Order>()
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        var OrderList = listOrders.AsEnumerable()
        //            .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.OrderId + "").ToLower()
        //                             .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
        //        int count = 1;
        //        var rp = OrderList
        //            .Skip(param.iDisplayStart).Take(param.iDisplayLength)
        //            .Select(p => new IConvertible[]
        //            {
        //            count++,
        //            p.OrderId,
        //            p.AspNetUser.UserName,
        //            p.Note,
        //            p.TotalPrice,
        //            p.PayDay.Value.ToShortDateString() + " " + p.PayDay.Value.ToShortTimeString(),
        //            });
        //        var total = listOrders.Count();
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = total,
        //            aaData = rp
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new List<Product>()
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult CancelledOrderDatatable(JQueryDataTableParamModel param, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var orderService = this.Service<IOrderService>();
        //        var listOrders = orderService.GetAllCancelledOrderWithDateRange(startDate, endDate);
        //        if (listOrders == null)
        //        {
        //            return Json(new
        //            {
        //                sEcho = param.sEcho,
        //                iTotalRecords = 0,
        //                iTotalDisplayRecords = 0,
        //                aaData = new List<Order>()
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        var OrderList = listOrders.AsEnumerable()
        //            .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.OrderId + "").ToLower()
        //                             .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
        //        int count = 1;
        //        var rp = OrderList
        //            .Skip(param.iDisplayStart).Take(param.iDisplayLength)
        //            .Select(p => new IConvertible[]
        //            {
        //            count++,
        //            p.OrderId,
        //            p.AspNetUser.UserName,
        //            p.Note,
        //            p.TotalPrice,
        //            p.CreateDate.ToShortDateString() + " " + p.CreateDate.ToShortTimeString(),
        //            });
        //        var total = listOrders.Count();
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = total,
        //            iTotalDisplayRecords = total,
        //            aaData = rp
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new List<Product>()
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult AcceptOrder(int orderId)
        //{
        //    try
        //    {
        //        var orderService = this.Service<IOrderService>();
        //        var order = orderService.GetById(orderId);
        //        if (order != null)
        //        {
        //            order.Status = (int)OrderStatusEnum.Accepted;
        //            order.PayDay = DateTime.Now;
        //            orderService.Update(order);
        //            return Json(new
        //            {
        //                success = true,
        //            });
        //        }
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}

        //[HttpPost]
        //public ActionResult CancelOrder(int orderId)
        //{
        //    try
        //    {
        //        var orderService = this.Service<IOrderService>();
        //        var order = orderService.GetById(orderId);
        //        if (order != null)
        //        {
        //            order.Status = (int)OrderStatusEnum.Cacancelled;
        //            orderService.Update(order);
        //            return Json(new
        //            {
        //                success = true,
        //            });
        //        }
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //        });
        //    }
        //}

        //public ActionResult Detail(int id)
        //{
        //    ViewData["OrderId"] = id;
        //    return View();
        //}

        //public ActionResult GetDetailOrder(JQueryDataTableParamModel param, int orderId)
        //{
        //    try
        //    {
        //        var orderService = this.Service<IOrderService>();
        //        var order = orderService.GetById(orderId);

        //        if (order != null)
        //        {
        //            IOrderedProductService orderedProductService = this.Service<IOrderedProductService>();
        //            IQueryable<OrderedProduct> listListory = orderedProductService.GetAllProductByOrderId(orderId);
        //            if (listListory == null)
        //            {
        //                return Json(new
        //                {
        //                    sEcho = param.sEcho,
        //                    iTotalRecords = 0,
        //                    iTotalDisplayRecords = 0,
        //                    aaData = new List<OrderedProduct>()
        //                }, JsonRequestBehavior.AllowGet);
        //            }
        //            var historyList = listListory.AsEnumerable()
        //                .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.Product.Name).ToLower()
        //                                 .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
        //            int count = 1;
        //            var rp = historyList
        //                .Skip(param.iDisplayStart).Take(param.iDisplayLength)
        //                .Select(p => new IConvertible[]
        //                {
        //            count++,
        //           p.Product.Name,
        //           p.Quantity,
        //           p.Price
        //                });
        //            var total = listListory.Count();
        //            return Json(new
        //            {
        //                sEcho = param.sEcho,
        //                iTotalRecords = total,
        //                iTotalDisplayRecords = total,
        //                aaData = rp
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new List<History>()
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = 0,
        //            iTotalDisplayRecords = 0,
        //            aaData = new List<History>()
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}