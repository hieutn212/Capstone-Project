using CapstoneAPI.Models;
using CapstoneData.Models.Entities;
using CapstoneData.Models.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CapstoneAPI.Controllers
{
    public class OrderController : BaseApiController
    {
        public HttpResponseMessage getAllHistory(int userId, DateTime startDate, DateTime endDate)
        {
            var historyService = this.Service<IHistoryService>();
            var listHistorys = historyService.getListByUserId(userId, startDate, endDate);
            if (listHistorys != null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new JsonContent(listHistorys)
                };
            }
            else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new JsonContent("History null")
                };
            }
        }
    }
}