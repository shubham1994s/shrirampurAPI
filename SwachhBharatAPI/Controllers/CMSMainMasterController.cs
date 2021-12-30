using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBharatAPI.Models;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api/AdminMenuDashboard")]
    public class CMSMainMasterController : ApiController
    {
        IRepository objRep;

        DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

        #region State 

        [HttpPost]
        [Route("Save/AddStateDetails")]
        public Result AddStateDetails(CMSBStatesVM state)
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("stateId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var stateId = headerValue2.FirstOrDefault();
            var IsNullstateId = (stateId == "" ? "0" : stateId);
            int _stateId = int.Parse(IsNullstateId);

            state.id = _stateId;

            Result objres = new Result();
            if (_AppId != 0)
            {
                Result pointDetails = objRep.SaveState(state);
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
                objres.isAttendenceOff = pointDetails.isAttendenceOff;
            }
            return objres;

        }

        #endregion


        #region Zone

        [HttpPost]
        [Route("Save/AddZoneDetails")]
        public Result AddZoneDetails(CMSBZoneVM zoneRaw)
        {
            objRep = new Repository();
            Result objres = new Result();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("zoneId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var zoneId = headerValue2.FirstOrDefault();
            var IsNullzoneId = (zoneId == "" ? "0" : zoneId);
            int _zoneId = int.Parse(IsNullzoneId);

            zoneRaw.zoneId = _zoneId;
            CMSBZoneVM area = new CMSBZoneVM();
            area.zoneId = zoneRaw.zoneId;
            area.name = zoneRaw.name;
            if (_AppId != 0)
            {
                Result Details = objRep.SaveZone(area , _AppId);
                objres.status = Details.status;
                objres.messageMar = Details.messageMar;
                objres.message = Details.message;
                objres.isAttendenceOff = Details.isAttendenceOff;
            }
            return objres;
        }

        #endregion



        #region Ward 

        [HttpPost]
        [Route("Save/WardDetails")]
        public Result AddWardDetails(CMSBWardVM Ward)
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("WardNoId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var WardId = headerValue2.FirstOrDefault();
            var IsNullWardId = (WardId == "" ? "0" : WardId);
            int _WardId = int.Parse(IsNullWardId);

            Ward.Id = _WardId;

            Result objres = new Result();
            if (_AppId != 0)
            {
                Result pointDetails = objRep.SaveWard(Ward, _AppId);
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
                objres.isAttendenceOff = pointDetails.isAttendenceOff;
            }
            return objres;

        }

        #endregion


        #region Area

        [HttpPost]
        [Route("Save/AreaDetails")]
        public Result AddAreaDetails(CMSBAreaVM Area)
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("AreaId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var AreaId = headerValue2.FirstOrDefault();
            var IsNullAreaId = (AreaId == "" ? "0" : AreaId);
            int _AreaId = int.Parse(IsNullAreaId);

            Area.id = _AreaId;

            Result objres = new Result();
            if (_AppId != 0)
            {
                Result pointDetails = objRep.SaveArea(Area, _AppId);
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
                objres.isAttendenceOff = pointDetails.isAttendenceOff;
            }
            return objres;

        }

        #endregion


        #region VehicleType 

        [HttpPost]
        [Route("Save/VehicleTypeDetails")]
        public Result AddVehicleTypeDetails(SBVehicleType VehicleType)
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("vtId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var vtId = headerValue2.FirstOrDefault();
            var IsNullvtId = (vtId == "" ? "0" : vtId);
            int _vtIdId = int.Parse(IsNullvtId);

            VehicleType.vtId = _vtIdId;

            Result objres = new Result();
            if (_AppId != 0)
            {
                Result pointDetails = objRep.SaveVehicleType(VehicleType, _AppId);
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
                objres.isAttendenceOff = pointDetails.isAttendenceOff;
            }
            return objres;

        }


        #endregion
    }
}
