
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

        //public JsonResult HistoryDatatable(JQueryDataTableParamModel param, string username, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var customerService = this.Service<IAspNetUserService>();
        //        var customer = customerService.GetByUsername(username);

        //        if (customer != null)
        //        {
        //            IHistoryService historyService = this.Service<IHistoryService>();
        //            IQueryable<History> listListory = historyService.GetByCustomerIdWithDateRange(customer.Id, startDate, endDate);
        //            if (listListory == null)
        //            {
        //                return Json(new
        //                {
        //                    sEcho = param.sEcho,
        //                    iTotalRecords = 0,
        //                    iTotalDisplayRecords = 0,
        //                    aaData = new List<History>()
        //                }, JsonRequestBehavior.AllowGet);
        //            }
        //            var historyList = listListory.AsEnumerable()
        //                .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.CustomerId).ToLower()
        //                                 .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
        //            int count = 1;
        //            var rp = historyList
        //                .Skip(param.iDisplayStart).Take(param.iDisplayLength)
        //                .Select(p => new IConvertible[]
        //                {
        //            count++,
        //            p.CustomerId,
        //            p.AspNetUser.UserName,
        //            p.Point,
        //            p.CreatedDate.ToShortDateString() + " "+ p.CreatedDate.ToShortTimeString(),
        //            p.Id,
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