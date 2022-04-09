using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class QREmployeeController : ApiController
    {
        IRepository objRep;
        [HttpPost]
        [Route("Save/QrEmployeeAttendenceIn")]
        public Result SaveQrEmployeeAttendence(BigVQREmployeeAttendenceVM obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            Result objDetail = new Result();
            objDetail = objRep.SaveQrEmployeeAttendence(obj, AppId, 0);
            return objDetail;
        }

        [HttpPost]
        [Route("Save/QrEmployeeAttendenceOut")]
        public Result SaveQrEmployeeAttendenceOut(BigVQREmployeeAttendenceVM obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            Result objDetail = new Result();
            objDetail = objRep.SaveQrEmployeeAttendence(obj, AppId, 1);
            return objDetail;
        }


        [HttpPost]
        [Route("Save/QrEmpAttendenceOffline")]
        public List<SyncResult1> SaveQrEmployeeAttendenceOffline(List<BigVQREmployeeAttendenceVM> obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            List<SyncResult1> objDetail = new List<SyncResult1>();
            objDetail = objRep.SaveQrEmployeeAttendenceOffline(obj, AppId);
            return objDetail;
        }

        //Add by neha
        [HttpPost]
        [Route("Save/QrHPDCollections")]
        public Result SaveQrHPDCollections(BigVQRHPDVM obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("referanceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("gcType");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            var refid = headerValue2.FirstOrDefault().ToString();
            string referanceid = (refid == "" ? "" : refid);

            var gctype = headerValue3.FirstOrDefault();
            int Gctype = int.Parse(gctype);
            string houseid1 = obj.ReferanceId;
            string[] houseList = houseid1.Split(',');
           
            if (houseList.Length > 1)
            {
                obj.ReferanceId = houseList[0];
                obj.wastetype = houseList[1];
              
            }

            string[] referancList = refid.Split(',');

            if (referancList.Length > 1)
            {
                referanceid = referancList[0];

            }
            Result objDetail = new Result();
            objDetail = objRep.SaveQrHPDCollections(obj, AppId, referanceid, Gctype);
            return objDetail;
        }

        [HttpPost]
        [Route("Save/QrHPDCollectionsOffline")]
        public List<CollectionSyncResult> SaveQrHPDCollectionsOffline(List<BigVQRHPDVM> obj)
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            
            List<CollectionSyncResult> objDetail = new List<CollectionSyncResult>();
            objDetail = objRep.SaveQrHPDCollectionsOffline(obj, AppId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/QrWorkHistory")]
        //api/BookATable/GetBookAtableList
        public List<SBWorkDetails> GetWork()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("year");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("month");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var u = headerValue2.FirstOrDefault();
            int userId = int.Parse(u);
            var y = headerValue3.FirstOrDefault();
            int year = int.Parse(y);
            var m = headerValue4.FirstOrDefault();
            int month = int.Parse(m);
            List<SBWorkDetails> objDetail = new List<SBWorkDetails>();
            objDetail = objRep.GetQrWorkHistory(userId, year, month, AppId).OrderByDescending(c => c.date).ToList();
            return objDetail;
        }

        //Added by neha
        [HttpGet]
        [Route("Get/QrWorkHistoryDetails")]
        //api/BookATable/GetBookAtableList
        public List<BigVQrworkhistorydetails> GetQRWorkDetails()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("date");
            

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var date = headerValue3.FirstOrDefault();
            DateTime Date = Convert.ToDateTime(date);
            var user = headerValue2.FirstOrDefault();
            int userId = int.Parse(user);
                      

            List<BigVQrworkhistorydetails> objDetail = new List<BigVQrworkhistorydetails>();
            objDetail = objRep.GetQrWorkHistoryDetails(Date, AppId, userId);
            return objDetail;
        }

        //Added By Nishikant (06 May 2019)
        [HttpGet]
        [Route("Get/ScanifyHouse")]
        public BigVQRHPDVM2 GetScanifyHouseDetailsData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("ReferenceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("gcType");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var _ReferenceId = headerValue2.FirstOrDefault();
            string ReferenceId = Convert.ToString(_ReferenceId);
            var _gcType = headerValue3.FirstOrDefault();
            int gcType = Convert.ToInt32(_gcType);

            BigVQRHPDVM2 objDetail = new BigVQRHPDVM2();
            objDetail = objRep.GetScanifyHouseDetailsData(AppId, ReferenceId, gcType);
            return objDetail;

        }

    }
}
