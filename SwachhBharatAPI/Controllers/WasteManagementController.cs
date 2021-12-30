using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.WasteManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class WasteManagementController : ApiController
    {
        IWMRepository objRep;

        [HttpGet]
        [Route("Get/GarbageCategory")]
        public List<GarbageCategoryVM> GetGarbageCategory(int appId)
        {
            List<GarbageCategoryVM> objDetail = new List<GarbageCategoryVM>();
            objRep = new WMRepository();
            objDetail = objRep.GetGarbageCategory(appId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/GarbageSubCategory")]
        public List<GarbageSubCategoryVM> GetGarbageSubCategory()
        {
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            IEnumerable<string> headerValue2 = Request.Headers.GetValues("CategoryID");
            var _CategoryID = headerValue2.FirstOrDefault();
            int CategoryID = int.Parse(_CategoryID);

            List<GarbageSubCategoryVM> objDetail = new List<GarbageSubCategoryVM>();
            objRep = new WMRepository();
            objDetail = objRep.GetGarbageSubCategory(AppId, CategoryID);
            return objDetail;
        }

        [HttpPost]
        [Route("Save/GarbageDetails")]
        public List<Result2> SaveGarbageDetails(List<GarbageDetailsVM> obj)
        {
            objRep = new WMRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            List<Result2> objDetail = new List<Result2>();
            objDetail = objRep.GarbageDetails(AppId, obj);
            return objDetail;
        }

        [HttpPost]
        [Route("Save/GarbageSales")]
        public List<Result2> SaveGarbageSales(List<GarbageSalesVM> obj)
        {
            objRep = new WMRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            List<Result2> objDetail = new List<Result2>();
            objDetail = objRep.GarbageSales(AppId, obj);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/GarbageHistory")]
        public List<GarbageHistoryVM> GetGarbageHistory()
        {
            objRep = new WMRepository();
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
            List<GarbageHistoryVM> objDetail = new List<GarbageHistoryVM>();
            objDetail = objRep.GetGarbageHistory(userId, year, month, AppId).OrderByDescending(c => c.Date).ToList();
            return objDetail;
        }

        [HttpGet]
        [Route("Get/GarbageHistory/Details")]
        public List<GarbageHistoryDetailsVM> GetGarbageDetails()
        {
            objRep = new WMRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("userId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("fdate");
            
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var date = headerValue3.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(date);
            var user = headerValue2.FirstOrDefault();
            int userId = int.Parse(user);
           
            List<GarbageHistoryDetailsVM> objDetail = new List<GarbageHistoryDetailsVM>();
            objDetail = objRep.GetUserGarbageDetails(fdate, AppId, userId);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/CombineCategorySubcategory")]
        public JObject CombineCategorySubcategory()
        {
            objRep = new WMRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            int CategoryID = 0;

            List<GarbageCategoryVM> objDetail = new List<GarbageCategoryVM>();
            objDetail = objRep.GetGarbageCategory(AppId);
            List<GarbageSubCategoryVM> objDetail1 = new List<GarbageSubCategoryVM>();
            objDetail1 = objRep.GetGarbageSubCategory(AppId, CategoryID);

            //List<GarbageHistoryDetailsVM> objDetail = new List<GarbageHistoryDetailsVM>();
            //objDetail = objRep.CombineCategorySubcategory(AppId);

            return JSonBuilder(objDetail, objDetail1);
        }

        private JObject JSonBuilder(List<GarbageCategoryVM> objDetail, List<GarbageSubCategoryVM> objDetail1)
        {

            string jsonString = string.Empty;

            jsonString += "{\n " + "status" + ": " + JsonConvert.SerializeObject("success", Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "message" + ": " + JsonConvert.SerializeObject("success", Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "Category" + ": " + JsonConvert.SerializeObject(objDetail, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "SubCategory" + ": " + JsonConvert.SerializeObject(objDetail1, Formatting.Indented) + "}";

            JObject json = JObject.Parse(jsonString);

            return json;
        }

    }
}
