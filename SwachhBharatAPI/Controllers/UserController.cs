using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Timers;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        bool isexist = false;
        IRepository objRep;
        [HttpGet]
        [Route("Get/User")]
        public SBUserView GetUser()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headervalue2 = Request.Headers.GetValues("userId");
            //IEnumerable<string> headervalue3 = Request.Headers.GetValues("typeId");

            //var token= "";
            //if (System.Web.HttpContext.Current.Request.Headers["typeId"] == null)
            //{
            //     token = '0'.ToString();
            //}

            var token = Request.Headers.Contains("typeId") ? Request.Headers.GetValues("typeId").First() : null;
            if (token == null)
            {
                token = '0'.ToString();
            }
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var user = headervalue2.FirstOrDefault();
            int userId = int.Parse(user);
            var _TypeId = token;
            int typeId = int.Parse(_TypeId);
            SBUserView objDetail = new SBUserView();
            objDetail = objRep.GetUser(AppId, userId, typeId);
            return objDetail;
        }


        //[HttpPost]
        //[Route("Save/UserLocation")]
        //public List<SyncResult> SaveUserLocation(List<SBUserLocation> obj)
        //{
        //    objRep = new Repository();
        //    IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
        //    var batteryStatus = headervalue2.FirstOrDefault().ToString();
        //    var id = headerValue1.FirstOrDefault();
        //    int AppId = int.Parse(id);
        //    List<SyncResult> objDetail = new List<SyncResult>();
        //    objDetail = objRep.SaveUserLocation(obj, AppId, batteryStatus);
        //    return objDetail;
        //}


        [HttpPost]
        [Route("Save/UserLocation")]
        public List<SyncResult> SaveUserLocation(List<SBUserLocation> obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
            IEnumerable<string> headervalue3 = Request.Headers.GetValues("typeId");
            IEnumerable<string> headervalue4 = Request.Headers.GetValues("EmpType");
            var batteryStatus = headervalue2.FirstOrDefault().ToString();
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            var _typeId = headervalue3.FirstOrDefault();
            int typeId = int.Parse(_typeId);

            var EmpType = headervalue4.FirstOrDefault();

            List<SyncResult> objDetail = new List<SyncResult>();
            objDetail = objRep.SaveUserLocation(obj, AppId, batteryStatus, typeId,EmpType);
            return objDetail;
        }


        [HttpPost]
        [Route("Save/UserAttendenceIn")]
                
        public Result SaveUserAttendence(SBUserAttendence obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            var batteryStatus = headervalue2.FirstOrDefault().ToString();
            Result objDetail = new Result();
            objDetail = objRep.SaveUserAttendence(obj, AppId, 0, batteryStatus);
            return objDetail;
        }

        //public List<SyncResult> SaveUserAttendence(List<SBUserAttendence> obj)
        //{
        //    objRep = new Repository();
        //    IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
        //    var id = headerValue1.FirstOrDefault();
        //    int AppId = int.Parse(id);

        //    var batteryStatus = headervalue2.FirstOrDefault().ToString();
        //    List<SyncResult> objDetail = new List<SyncResult>();
        //    objDetail = objRep.SaveUserAttendence(obj, AppId, 0, batteryStatus);
        //    return objDetail;
        //}

        [HttpPost]
        [Route("Save/UserAttendenceOut")]

        public Result SaveUserAttendenceOut(SBUserAttendence obj)
        {
            
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var batteryStatus = headervalue2.FirstOrDefault().ToString();
            Result objDetail = new Result();

            objDetail = objRep.SaveUserAttendence(obj, AppId, 1, batteryStatus);
            return objDetail;
        }

        //public List<SyncResult> SaveUserAttendenceOut(List<SBUserAttendence> obj)
        //{
        //    objRep = new Repository();
        //    IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
        //    var id = headerValue1.FirstOrDefault();
        //    int AppId = int.Parse(id);
        //    var batteryStatus = headervalue2.FirstOrDefault().ToString();
        //    List<SyncResult> objDetail = new List<SyncResult>();
        //    objDetail = objRep.SaveUserAttendence(obj, AppId, 1, batteryStatus);
        //    return objDetail;
        //}
        [HttpPost]
        [Route("Save/AttendenceOffline")]
        public List<SyncResult1> SaveUserAttendenceOffline(List<SBUserAttendence> obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("cdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("EmpType");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var date = headerValue2.FirstOrDefault();
            var EmpType= headerValue3.FirstOrDefault();
            List<SyncResult1> objDetail = new List<SyncResult1>();
            objDetail = objRep.SaveUserAttendenceOffline(obj, AppId, date,EmpType);
            return objDetail;
        }

      

        [HttpGet]
        [Route("Get/UserAttendenc")]
        //api/BookATable/GetBookAtableList
        public List<SBUserAttendenceView> GetUserAttendenc()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("fdate");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var date = headerValue3.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(date);
            var user = headerValue2.FirstOrDefault();
            int userId = int.Parse(id);
            List<SBUserAttendenceView> objDetail = new List<SBUserAttendenceView>();
            objDetail = objRep.GetUserAttendence(fdate,AppId,userId);
            return objDetail;
        }

        //Added by shubham
        [HttpGet]
        [Route("Get/GetUserMobileIdentification")]
        public SyncResult2 GetUserMobileIdentification()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("isSync");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("batteryStatus");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("imeino");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var user = headerValue2.FirstOrDefault();
            int userId = int.Parse(user);
            var isSyn = headerValue3.FirstOrDefault();
            bool isSync = bool.Parse(isSyn);
            var batteryStatu = headerValue4.FirstOrDefault();
            int batteryStatus = int.Parse(batteryStatu);
            var imeino = headerValue5.FirstOrDefault();
            string imeinos = imeino;

            SyncResult2 objDetail = new SyncResult2();
         
            objDetail = objRep.GetUserMobileIdentification(AppId,userId,isSync,batteryStatus,imeinos);
            return objDetail;
        }


        [HttpGet]
        [Route("Get/WorkHistory")]
        //api/BookATable/GetBookAtableList
        public List<SBWorkDetails> GetWork()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("year");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("month");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("EmpType");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var u = headerValue2.FirstOrDefault();
            int userId = int.Parse(u);
            var y = headerValue3.FirstOrDefault();
            int year = int.Parse(y);
            var m = headerValue4.FirstOrDefault();
            int month= int.Parse(m);
            var EmpType = headerValue5.FirstOrDefault();
            List<SBWorkDetails> objDetail = new List<SBWorkDetails>();
            objDetail = objRep.GetUserWork(userId,year,month, AppId, EmpType).OrderByDescending(c=>c.date).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("Get/WorkHistory/Details")]
        //api/BookATable/GetBookAtableList
        public List<SBWorkDetailsHistory> GetWorkDetails()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("languageId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var date = headerValue3.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(date);
            var user = headerValue2.FirstOrDefault();
            int userId = int.Parse(user);
            var lang = headerValue4.FirstOrDefault();
            int languageId= int.Parse(lang);
            List<SBWorkDetailsHistory> objDetail = new List<SBWorkDetailsHistory>();
            objDetail = objRep.GetUserWorkDetails(fdate, AppId, userId,languageId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/VersionUpdate")]
        public Result GetVersionUpdate()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("version");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            string version = v.ToString();
            Result objDetail = new Result();
            objDetail = objRep.GetVersionUpdate(version, AppId);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/AdminVersionUpdate")]
        public Result GetAdminVersionUpdate()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("version");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            string version = v.ToString();
            Result objDetail = new Result();
            objDetail = objRep.GetAdminVersionUpdate(version, AppId);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/GameVersionUpdate")]
        public Result GetGameVersionUpdate()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("version");
            var v = headerValue1.FirstOrDefault();
            string version = v.ToString();
            Result objDetail = new Result();
            objDetail = objRep.GetGameVersionUpdate(version);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/Area")]
        public List<SBArea> GetArea(int AppId)
        {

            List<SBArea> objDetail = new List<SBArea>();
            objRep = new Repository();
            objDetail = objRep.GetArea(AppId);
            return objDetail;

        }

        //Added By Nishikant 10 May 2019
        [HttpGet]
        [Route("Get/Zone")]
        public List<CMSBZoneVM> GetZone(int AppId, string SearchString)
        {
            List<CMSBZoneVM> objDetail = new List<CMSBZoneVM>();
            objRep = new Repository();
            objDetail = objRep.GetZone(AppId, SearchString);
            return objDetail;
        }


        //Added By Nishikant 10 May 2019
        [HttpGet]
        [Route("Get/Ward")]
        public List<CMSBWardVM> GetWard(int AppId, string SearchString)
        {
            List<CMSBWardVM> objDetail = new List<CMSBWardVM>();
            objRep = new Repository();
            objDetail = objRep.GetWard(AppId, SearchString);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/Employee")]
        public List<CMSBEmployee> GetEmployee()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            List<CMSBEmployee> objDetail = new List<CMSBEmployee>();
            objDetail = objRep.GetEmployee(AppId);
            return objDetail;
        }


        [HttpGet]
        [Route("Get/AllCurrentUser")]
        public List<SBAUserlocation> GetAllUserLocation(int AppId)
        {

            List<SBAUserlocation> objDetail = new List<SBAUserlocation>();
            objRep = new Repository();
            objDetail = objRep.GetUserLocation(AppId);
            return objDetail;

        }


        [HttpGet]
        [Route("Get/AreaCurrentUser")]
        public List<SBAUserlocation> GetAreaUserLocation(int AppId,string area)
        {

            List<SBAUserlocation> objDetail = new List<SBAUserlocation>();
            objRep = new Repository();
            objDetail = objRep.GetUserLocation(AppId).Where(c=>c.area.Trim()==area).ToList();
            return objDetail;

        }
        [HttpGet]
        [Route("Get/CollectionArea")]
        public List<SBArea> GetColectionArea()
        {

            List<SBArea> objDetail = new List<SBArea>();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("type");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("EmpType");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            int type = int.Parse(v);
            var EmpType = headerValue3.FirstOrDefault();
            objDetail = objRep.GetCollectionArea(AppId,type, EmpType);
            return objDetail;

        }
        [HttpGet]
        [Route("Get/AreaHouse")]
        public List<HouseDetailsVM> GetHouseAreaWise()
        {

            List<HouseDetailsVM> objDetail = new List<HouseDetailsVM>();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("areaId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("EmpType");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            int areaId = int.Parse(v);
            var EmpType = headerValue3.FirstOrDefault();
            objDetail = objRep.GetAreaHouse(AppId, areaId, EmpType);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/AreaPoint")]
        public List<GarbagePointDetailsVM> GetPointAreaWise()
        {

            List<GarbagePointDetailsVM> objDetail = new List<GarbagePointDetailsVM>();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("areaId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            int areaId = int.Parse(v);
            objDetail = objRep.GetAreaPoint(AppId, areaId);
            return objDetail;

        }

        // Added By Saurabh (26 Apr 2019)

        [HttpGet]
        [Route("Get/DumpYardPoint")]
        public List<DumpYardPointDetailsVM> GetDumpYardAreaWise()
        {

            List<DumpYardPointDetailsVM> objDetail = new List<DumpYardPointDetailsVM>();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("areaId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            int areaId = int.Parse(v);
            objDetail = objRep.GetDumpYardArea(AppId, areaId);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/SendSms")]
        public Result SendNotificationt()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("areaId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
           int areaId = int.Parse(v);
            Result objDetail = new Result();
            objDetail = objRep.SendSMSToHOuse(areaId, AppId);
            return objDetail;

        }


        [HttpGet]
        [Route("Get/IsAttendence")]
        public Result CheckAttendence()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            //IEnumerable<string> headerValue3 = Request.Headers.GetValues("typeId");
            var token = Request.Headers.Contains("typeId") ? Request.Headers.GetValues("typeId").First() : null;
            if (token == null)
            {
                token = '0'.ToString();
            }
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var v = headerValue2.FirstOrDefault();
            int userId = int.Parse(v);
            var type = token;
            int typeId = int.Parse(type);
            Result objDetail = new Result();
            objDetail = objRep.CheckAttendence(userId, AppId, typeId);
            return objDetail;

        }

        [HttpGet]
        [Route("Get/States")]
        public List<CMSBStatesVM> GetStates(int AppId, string SearchString)
        {
            objRep = new Repository();
            List<CMSBStatesVM> objDetail = new List<CMSBStatesVM>();
            objDetail = objRep.GetStates(AppId, SearchString);
            return objDetail;
        }


        [HttpGet]
        [Route("Get/District")]
        public List<CMSBDistrictVM> GetDistrict(int AppId, string SearchString)
        {
            objRep = new Repository();
            List<CMSBDistrictVM> objDetail = new List<CMSBDistrictVM>();
            objDetail = objRep.GetDistrict(AppId, SearchString);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/Taluka")]
        public List<CMSBTalukaVM> GetTaluka(int AppId, string SearchString)
        {
            objRep = new Repository();
            List<CMSBTalukaVM> objDetail = new List<CMSBTalukaVM>();
            objDetail = objRep.GetTaluka(AppId, SearchString);
            return objDetail;
        }


        [HttpGet]
        [Route("Get/AreaList")]
        public List<CMSBAreaVM> GetAreaList(int AppId, string SearchString)
        {

            List<CMSBAreaVM> objDetail = new List<CMSBAreaVM>();
            objRep = new Repository();
            objDetail = objRep.GetAreaList(AppId, SearchString);
            return objDetail;

        }


        [HttpGet]
        [Route("Get/WardZoneList")]
        public List<CMSBWardZoneVM> GetWardZoneList(int AppId)
        {
            List<CMSBWardZoneVM> objDetail = new List<CMSBWardZoneVM>();
            objRep = new Repository();
            objDetail = objRep.GetWardZoneList(AppId);
            return objDetail;
        }

    }
}
