using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class RFIDController : ApiController
    {
        IRepository objRep;
        [HttpGet]
        [Route("Save/RfidDetails")]
        public Result SaveRfidDetails(string ReaderId, string TagId, string Lat, string Long, string Type, string DT)
        {
            objRep = new Repository();
            Result objDetail = new Result();
            objDetail = objRep.SaveRfidDetails(ReaderId, TagId, Lat, Long, Type, DT);
            return objDetail;
        }
    }
}
