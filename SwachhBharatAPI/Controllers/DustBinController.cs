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
    public class DustBinController : ApiController
    {
        IRepository objRep;
        [HttpGet]
        [Route("Save/DustBinDetails")]
        public Result SaveDustBinDetails(string ID, string LT, string LO, string DIST, string TEMP, string S1, string S2)
        {
            objRep = new Repository();
            Result objDetail = new Result();
            objDetail = objRep.SaveDustBinDetails(ID, LT, LO, DIST, TEMP, S1, S2);
            return objDetail;
        }

      

    }
}
