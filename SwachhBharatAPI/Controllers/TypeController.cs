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
    public class TypeController : ApiController
    {
        IRepository objRep;
        [HttpGet]
        [Route("Get/VehicleType")]
        //api/BookATable/GetBookAtableList
        public List<SBVehicleType> GetComplaintType()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
             int AppId = int.Parse(id);
             int a = Convert.ToInt32(AppId);
            List<SBVehicleType> objDetail = new List<SBVehicleType>();
            objDetail = objRep.GetVehicle(AppId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/ActiveEmployee")]
        //api/BookATable/GetBookAtableList
        public List<EmployeeVM> GetActiveEmployee()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            int a = Convert.ToInt32(AppId);
            List<EmployeeVM> objDetail = new List<EmployeeVM>();
            objDetail = objRep.GetActiveEmployee(AppId);
            return objDetail;
        }


        [HttpGet]
        [Route("Get/VehicleTypeList")]
        //api/BookATable/GetBookAtableList
        public List<SBVehicleType> GetVehicleTypeList(int AppId, string SearchString)
        {
            objRep = new Repository();
            List<SBVehicleType> objDetail = new List<SBVehicleType>();
            objDetail = objRep.GetVehicleTypeList(AppId, SearchString);
            return objDetail;
        }
        
    }
}
