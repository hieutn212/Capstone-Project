
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

        public async Task<JsonResult> HistoryDatatable(JQueryDataTableParamModel param, DateTime startDate, DateTime endDate)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseUser = await httpClient.GetAsync
                    (ShareDataConnection.IPconnection + "api/user/GetUserInfo?username=" + Session["Username"].ToString());
                var user = JsonConvert.DeserializeObject<User>(responseUser.Content.ReadAsStringAsync().Result);
                HttpResponseMessage response = await httpClient.GetAsync
                   (ShareDataConnection.IPconnection + "api/order/getAllHistory?userId=" + user.Id + "&startDate=" + startDate + "&endDate=" + endDate);
                if (response.StatusCode.ToString() == "OK")
                {
                    var listHistorys = JsonConvert.DeserializeObject<List<HistoryViewModel>>(response.Content.ReadAsStringAsync().Result);
                    var historyList = listHistorys.AsEnumerable()
                    .Where(a => (string.IsNullOrEmpty(param.sSearch) || StringConvert.EscapeName(a.LicenseType.Price + "").ToLower()
                                     .Contains(StringConvert.EscapeName(param.sSearch).ToLower())));
                    int count = 1;
                    var rp = historyList
                        .Skip(param.iDisplayStart).Take(param.iDisplayLength)
                        .Select(p => new IConvertible[]
                        {
                    count++,
                    p.username,
                    p.CreatedDate.ToShortDateString() + " " + p.CreatedDate.ToShortTimeString(),
                    p.BuyDate+ " ngày",
                    p.Price + " $",
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
                else
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

    }
}