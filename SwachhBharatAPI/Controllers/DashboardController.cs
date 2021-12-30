using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        IRepository objRep;
        [HttpGet]
        [Route("Get/Dashboard")]
        public SBDashboardVM GetDashboard()
        {
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            objRep = new Repository();
            SBDashboardVM objDetail = new SBDashboardVM();
            objDetail = objRep.GetDashboard(AppId);

            return objDetail;
        }
    }
}
