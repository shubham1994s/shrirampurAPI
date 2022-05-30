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
    [RoutePrefix("api/Supervisor")]
    public class SupervisorLoginController : ApiController
    {
        IRepository objRep;

        [Route("Login")]
        [HttpPost]
        public SBUser GetLogin(SBUser objlogin)
        {
            //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            //var id = headerValue1.FirstOrDefault();
            //int AppId = int.Parse(id);

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("EmpType");
            var EmpType = headerValue1.FirstOrDefault();

            objRep = new Repository();
            SBUser objresponse = objRep.CheckSupervisorUserLogin(objlogin.userLoginId, objlogin.userPassword, EmpType);
            return objresponse;
        }

        [HttpGet]
        [Route("AllUlb")]
        public List<NameULB> GetUlb()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("EmpType");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            var EmpType = headerValue1.FirstOrDefault();
            var u = headerValue2.FirstOrDefault();
            int userId = int.Parse(u);
            List<NameULB> objDetail = new List<NameULB>();
            objDetail = objRep.GetUlb(userId, EmpType).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("SelectedUlb")]
        public HSDashboard GetSelectedUlbData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("EmpType");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);
            var EmpType = headerValue1.FirstOrDefault();
            var u = headerValue2.FirstOrDefault();
            int userId = int.Parse(u);
            var objDetail = objRep.GetSelectedUlbData(userId, EmpType, AppId);
            return objDetail;
        }


        [HttpGet]
        [Route("QREmployeeList")]
        public List<HSEmployee> GetQREmployeeList()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("EmpType");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);
            var EmpType = headerValue1.FirstOrDefault();
            var u = headerValue2.FirstOrDefault();
            int userId = int.Parse(u);
            List<HSEmployee> objDetail = new List<HSEmployee>();
            objDetail = objRep.GetQREmployeeList(userId, EmpType, AppId).ToList();
            return objDetail;
        }

        [HttpGet]
        [Route("HouseScanifyDetailsGridRow")]
        public List<HouseScanifyDetailsGridRow> GetHouseScanifyDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HouseScanifyDetailsGridRow> objDetail = new List<HouseScanifyDetailsGridRow>();
            objDetail = objRep.GetHouseScanifyDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("AttendanceGridRow")]
        public List<HSAttendanceGrid> GetAttendanceDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HSAttendanceGrid> objDetail = new List<HSAttendanceGrid>();
            objDetail = objRep.GetAttendanceDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("HouseDetails")]
        public List<HSHouseDetailsGrid> GetHouseDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HSHouseDetailsGrid> objDetail = new List<HSHouseDetailsGrid>();
            objDetail = objRep.GetHouseDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("DumpYardDetails")]
        public List<HSDumpYardDetailsGrid> GetDumpYardDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HSDumpYardDetailsGrid> objDetail = new List<HSDumpYardDetailsGrid>();
            objDetail = objRep.GetDumpYardDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("LiquidDetails")]
        public List<HSLiquidDetailsGrid> GetLiquidDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HSLiquidDetailsGrid> objDetail = new List<HSLiquidDetailsGrid>();
            objDetail = objRep.GetLiquidDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [HttpGet]
        [Route("StreetDetails")]
        public List<HSStreetDetailsGrid> GetStreetDetails()
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("FromDate");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Todate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("userId");


            var Fdate = headerValue1.FirstOrDefault();
            DateTime FromDate = Convert.ToDateTime(Fdate);

            var Tdate = headerValue2.FirstOrDefault();
            DateTime Todate = Convert.ToDateTime(Tdate);

            var id = headerValue3.FirstOrDefault();
            int AppId = int.Parse(id);

            int userId;
            var u = headerValue4.FirstOrDefault();
            if (u == "null" || u == "" || u == null)
            {
                userId = 0;
            }
            else
            {
                userId = int.Parse(u);
            }

            List<HSStreetDetailsGrid> objDetail = new List<HSStreetDetailsGrid>();
            objDetail = objRep.GetStreetDetails(userId, FromDate, Todate, AppId).ToList();
            return objDetail;
        }


        [Route("AddHouseScanifyEmployee")]
        [HttpPost]
        public List<CollectionSyncResult> AddEmployee(List<HouseScanifyEmployeeDetails> objRaw)
        {

            objRep = new Repository();
            HouseScanifyEmployeeDetails gcDetail = new HouseScanifyEmployeeDetails();
            List<CollectionSyncResult> objres = new List<CollectionSyncResult>();
            try
            {

                IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
                var AppId = Convert.ToInt32(headerValue1.FirstOrDefault());


                foreach (var item in objRaw)
                {
                    gcDetail.qrEmpId = item.qrEmpId;
                    gcDetail.qrEmpName = item.qrEmpName;

                    CollectionSyncResult detail = objRep.SaveAddEmployee(gcDetail, AppId);
                    if (detail.message == "")
                    {
                        objres.Add(new CollectionSyncResult()
                        {
                            ID = detail.ID,
                            status = "error",
                            message = "Record not inserted",
                            messageMar = "रेकॉर्ड सबमिट केले नाही"
                        });
                    }

                    objres.Add(new CollectionSyncResult()
                    {
                        ID = detail.ID,
                        status = detail.status,
                        messageMar = detail.messageMar,
                        message = detail.message,
                        isAttendenceOff = detail.isAttendenceOff
                    });

                    return objres;

                }


            }
            catch (Exception ex)
            {

                objres.Add(new CollectionSyncResult()
                {
                    ID = 0,
                    status = "error",
                    message = "Something is wrong,Try Again.. ",
                    messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                });
                return objres;

            }

            objres.Add(new CollectionSyncResult()
            {
                ID = 0,
                status = "error",
                message = "Record not inserted",
                messageMar = "रेकॉर्ड सबमिट केले नाही",
            });

            return objres;

        }

    }
}
