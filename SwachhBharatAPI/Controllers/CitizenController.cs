using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Citizen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class CitizenController : ApiController
    {
        //Added By Nishikant (13 june 2019)

        IRepository objRep;

        [HttpGet]
        [Route("Get/MobileDetails")]
        public CitizenMobileDetails GetMobileDetails()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("ReferanceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("FCMID");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var ReferanceId = headerValue2.FirstOrDefault();
            string _ReferanceId = Convert.ToString(ReferanceId);
            var FCMID = headerValue3.FirstOrDefault();
            var IsNullUserID = (FCMID == "" ? "" : FCMID);
            string _FCMID = Convert.ToString(IsNullUserID);

            Result Result = new Result();

            CitizenMobileDetails List = new CitizenMobileDetails();

            List = objRep.GetMobileDetails(AppId, ReferanceId, FCMID);

            //string jsonString = string.Empty;

            //jsonString = JsonConvert.SerializeObject(List, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            //JObject json = JObject.Parse(jsonString);

            return List;

        }

        [HttpGet]
        [Route("Get/SendOTP")]
        public CitizenMobileOTP GetSendOTP()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("ReferanceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("Mobile");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var ReferanceId = headerValue2.FirstOrDefault();
            string _ReferanceId = Convert.ToString(ReferanceId);
            var Mobile = headerValue3.FirstOrDefault();
            string _Mobile = Convert.ToString(Mobile);
            
            CitizenMobileOTP List = new CitizenMobileOTP();
            List = objRep.GetSendOTP(AppId, ReferanceId, _Mobile);
            
            return List;

        }

        [HttpPost]
        [Route("Save/DeviceDetails")]
        public Result1 SaveDeviceDetails()
        {
            Result1 Result = new Result1();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("ReferanceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("FCMID");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("DeviceID");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("Mobile");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var ReferanceId = headerValue2.FirstOrDefault();
            string _ReferanceId = Convert.ToString(ReferanceId);
            var FCMID = headerValue3.FirstOrDefault();
            var IsNullUserID = (FCMID == "" ? "" : FCMID);
            string _FCMID = Convert.ToString(IsNullUserID);
            var DeviceID = headerValue4.FirstOrDefault();
            var IsNullDeviceID = (DeviceID == "" ? "" : DeviceID);
            string _DeviceID = Convert.ToString(IsNullDeviceID);

            var Mobile = headerValue5.FirstOrDefault();
            var IsNullMobile = Mobile;
            string _Mobile = Convert.ToString(IsNullMobile);

            Result = objRep.SaveDeviceDetails(AppId, _ReferanceId.ToUpper(), _FCMID, _DeviceID, _Mobile);

            return Result;
        }

        [HttpPost]
        [Route("Save/DeviceDetailsClear")]
        public Result1 SaveDeviceDetailsClear()
        {
            Result1 Result = new Result1();
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("DeviceID");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("ReferenceID");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var DeviceID = headerValue2.FirstOrDefault();
            var IsNullDeviceID = (DeviceID == "" ? "" : DeviceID);
            string _DeviceID = Convert.ToString(IsNullDeviceID);

            var ReferenceID = headerValue3.FirstOrDefault();
            var IsNullReferenceID = (ReferenceID == "" ? "" : ReferenceID);
            string _ReferenceID = Convert.ToString(IsNullReferenceID);

            Result = objRep.SaveDeviceDetailsClear(AppId, _DeviceID, _ReferenceID);

            return Result;
        }

        //[HttpGet]
        //[Route("Get/Questions")]
        //public List<CitizenQuestionMaster> GetQuestions()
        //{
        //    objRep = new Repository();
        //    IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    var id = headerValue1.FirstOrDefault();
        //    int AppId = int.Parse(id);

        //    List<CitizenQuestionMaster> Question = new List<CitizenQuestionMaster>();
        //    Question = objRep.GetQuestions(AppId);

        //    return Question;

        //}

        //[HttpPost]
        //[Route("Save/AnswerDetails")]
        //public Result GetAnswerDetails(List<CitizenAnswerDetails> obj)
        //{
        //    objRep = new Repository();
        //    IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    IEnumerable<string> headerValue2 = Request.Headers.GetValues("UserID");
        //    var id = headerValue1.FirstOrDefault();
        //    int AppId = int.Parse(id);
        //    var _UserID = headerValue2.FirstOrDefault();
        //    int UserID = int.Parse(_UserID);

        //    string jsonString = string.Empty;

        //    jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

        //    Result Result = new Result();
        //    Result = objRep.GetAnswerDetails(jsonString, AppId, UserID);
        //    return Result;
        //}


        [HttpGet]
        [Route("Get/GPHouseDetails")]

        public GPHousedetailsVM GetGPHouseDetails(int AppId, string ReferanceId)
        {
            objRep = new Repository();

            GPHousedetailsVM objDetail = new GPHousedetailsVM();
            objDetail = objRep.GetGPHouseDetails(AppId, ReferanceId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/CTPTAddress")]

        public List<CitizenCTPTAddress> GetCTPTAddress()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            List<CitizenCTPTAddress> objDetail = new List<CitizenCTPTAddress>();
            objDetail = objRep.GetCTPTAddress(AppId);
            return objDetail;
        }

    }
}
