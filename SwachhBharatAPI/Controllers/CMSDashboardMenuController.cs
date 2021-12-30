using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachhBharat.API.Bll.Repository.ChildRepository;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBharatAPI.Models;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.CMSB;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api/AdminMenuDashboard")]
    public class CMSDashboardMenuController : ApiController
    {
        IRepository objRep;
        IChildRepository objChild;

        [HttpGet]
        [Route("Get/AdminAttendence")]
        public List<AttendanceGridVM> GetAdminAttendence()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var Offset = headerValue5.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue6.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue7.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<AttendanceGridVM> objDetail = new List<AttendanceGridVM>();
            objDetail = objRep.GetAdminAttendence(AppId, fdate, tdate, _UserId, _Offset, _Fetch_Next , _SearchString);
            return objDetail;
        }

        [HttpGet]
        [Route("Get/HouseGarbageCollection")]
        public List<AHouseGarbageCollectionVM> GetHouseGarbageCollectionData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("gcType");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue8 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var gcTypeId = headerValue5.FirstOrDefault();
            int gcType = int.Parse(gcTypeId);
            var Offset = headerValue6.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue7.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue8.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<AHouseGarbageCollectionVM> objDetail = new List<AHouseGarbageCollectionVM>();
            objDetail = objRep.GetHouseGarbageCollectionData(AppId, _UserId, fdate, tdate, gcType, _Offset, _Fetch_Next, _SearchString);
            return objDetail;
        }

        //Added By Nishikant (04 May 2019)
        [HttpGet]
        [Route("Get/PointGarbageCollection")]
        public List<CMSBPointGarbageCollectionVM> GetPointGarbageCollectionData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("gcType");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue8 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var gcTypeId = headerValue5.FirstOrDefault();
            int gcType = int.Parse(gcTypeId);
            var Offset = headerValue6.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue7.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);

            var SearchString = headerValue8.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<CMSBPointGarbageCollectionVM> objDetail = new List<CMSBPointGarbageCollectionVM>();
            objDetail = objRep.GetPointGarbageCollectionData(AppId, _UserId, fdate, tdate, gcType, _Offset, _Fetch_Next, _SearchString);
            return objDetail;

        }

        //Added By Nishikant (11 May 2019)
        [HttpGet]
        [Route("Get/DumpYardCollection")]
        public List<CMSBGrabageCollectionVM> GetDumpYardCollectionData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("gcType");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue8 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var gcTypeId = headerValue5.FirstOrDefault();
            int gcType = int.Parse(gcTypeId);
            var Offset = headerValue6.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue7.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue8.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();


            List<CMSBGrabageCollectionVM> objDetail = new List<CMSBGrabageCollectionVM>();
            objDetail = objRep.GetDumpYardCollectionData(AppId, _UserId, fdate, tdate, gcType, _Offset, _Fetch_Next , _SearchString);
            return objDetail;

        }


        //Added By Nishikant (04 May 2019)
        [HttpGet]
        [Route("Get/LocationData")]
        public List<CMSBLocationVM> LocationData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var Offset =  headerValue5.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue6.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);

            var SearchString = headerValue7.FirstOrDefault();
            var IsSearchString = (SearchString == "" ? "" : SearchString);

            string _SearchString = IsSearchString.ToString();

            List<CMSBLocationVM> objDetail = new List<CMSBLocationVM>();
            objDetail = objRep.GetLocationData(AppId, _UserId, fdate, tdate , _Offset, _Fetch_Next , _SearchString);
            return objDetail;

        }

        //Added By Nishikant (04 May 2019)
        [HttpGet]
        [Route("Get/UserWiseLocation")]
        public List<CMSBUserLocationMapVM> GetUserWiseLocation()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("Type");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            string fdate = Convert.ToString(fromDate);
            var UserId = headerValue3.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var Type = headerValue4.FirstOrDefault();
            int _Type = int.Parse(Type);
            
            List<CMSBUserLocationMapVM> objDetail = new List<CMSBUserLocationMapVM>();
            objDetail = objRep.GetUserWiseLocation(AppId, _UserId, fdate , _Type);
            return objDetail;

        }

        //Added By Nishikant (06 May 2019)
        [HttpGet]
        [Route("Get/HouseRegistration")]
        public List<CMSBHouseDetailsVM> GetHouseDetailsData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var Offset = headerValue2.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue3.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);

            var SearchString = headerValue4.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<CMSBHouseDetailsVM> objDetail = new List<CMSBHouseDetailsVM>();
            objDetail = objRep.GetHouseDetailsData(AppId, _Offset, _Fetch_Next , _SearchString);
            return objDetail;

        }


        //Added By Nishikant (11 May 2019)
        [HttpPost]
        [Route("Save/HouseDetails")]
        public Result HouseDetails(CMSBHouseDetailsVM objRaw)
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("HouseId");

            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var HouseId = headerValue2.FirstOrDefault();
            var IsNullHouseId = (HouseId == "" ? "0" : HouseId);
            int _HouseId = int.Parse(IsNullHouseId);

            Result objres = new Result();
            
            CMSBHouseDetailsVM ViewData = new CMSBHouseDetailsVM();

            ViewData.houseNo = objRaw.houseNo;//formData["houseNo"];
            ViewData.Address = objRaw.Address;//formData["Address"];
            ViewData.Mobile = objRaw.Mobile;//formData["Mobile"];
            ViewData.houseOwner = objRaw.houseOwner; //formData["houseOwner"];
            ViewData.houseOwnerMar = objRaw.houseOwnerMar;//formData["houseOwnerMar"];
            ViewData.AreaId = objRaw.AreaId;//int.Parse(formData["AreaId"]);
            ViewData.WardNoId = objRaw.WardNoId;//int.Parse(formData["WardNoId"]);
            ViewData.zoneId = objRaw.zoneId;//int.Parse(formData["zoneId"]);
            //ViewData.houseQRCode = objRaw.houseQRCode;//formData["houseQRCode"];
            ViewData.houseLat = objRaw.houseLat;//formData["houseLat"];
            ViewData.houseLong = objRaw.houseLong;//formData["houseLong"];
            
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId);
            var AppDetails = dbMain.AppDetails.Where(x => x.AppId == _AppId).FirstOrDefault();
            var guid = Guid.NewGuid().ToString().Split('-');
            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

            if (_HouseId > 0)
            {
                var _ReferanceId = db.HouseMasters.Where(x => x.houseId == _HouseId).Select(x => x.ReferanceId).FirstOrDefault();
                ViewData.ReferanceId = _ReferanceId.ToString();
                var _houseQRCode = db.HouseMasters.Where(x => x.houseId == _HouseId).Select(x => x.houseQRCode).FirstOrDefault();
                ViewData.houseQRCode = _houseQRCode.ToString();
            }
            else
            {
                var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                int number = 1000;
                string refer = "HPSBA" + (number + id + 1);
                ViewData.ReferanceId = refer;
                ViewData.houseQRCode = image_Guid;
            }

            //Converting  Url to image 
            // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ house.ReferanceId);
            var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + ViewData.ReferanceId + "&chs=160x160&chld=L|0");
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            //Creating Path to save image in folder
            System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);

            //////////////////////////////////////////////////////////////////

            string ThumbnaiUrlCMS = AppDetails.baseImageUrlCMS + AppDetails.basePath + AppDetails.HouseQRCode + "/" ;
            //HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + ViewData.houseQRCode.Trim());
            //HttpWebResponse httpRes = null;


            //httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
            //if (httpRes.StatusCode == HttpStatusCode.NotFound)
            //{
            //    ViewData.houseQRCode = "/Images/default_not_upload.png";
            //}
            //else
            //{
            //    ViewData.houseQRCode = ThumbnaiUrlCMS + ViewData.houseQRCode.Trim();
            //}

            //////////////////////////////////////////////////////////////////


            //string imgpath = Path.Combine(HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.HouseQRCode), image_Guid);

            //string imgpath = Path.Combine(ThumbnaiUrlCMS, ViewData.houseQRCode);

            //var exists = System.IO.Directory.Exists(imgpath);
            
            //if (!exists)
            //{
            //    System.IO.Directory.CreateDirectory(imgpath);
            //}
            //img.Save(imgpath);

            //response.Close();
            //remoteStream.Close();
            //readStream.Close();
            //ViewData.houseQRCode = image_Guid;

            Result detail = objRep.SaveHouse(ViewData , _AppId, _HouseId);
          
            objres.status = detail.status;
            objres.messageMar = detail.messageMar;
            objres.message = detail.message;
            //objres.isAttendenceOff = detail.isAttendenceOff;
            return objres;

        }

        //Added By Nishikant (15 May 2019)
        [HttpPost]
        [Route("Save/PointDetails")]
        public Result AddPointDetails(CMSBGarbagePointDetailsVM objPointRaw)
        {

            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("gpId");

            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var gpId = headerValue2.FirstOrDefault();
            var IsNullgpId = (gpId == "" ? "0" : gpId);
            int _gpId = int.Parse(IsNullgpId);

            Result objres = new Result();


            CMSBGarbagePointDetailsVM ViewData = new CMSBGarbagePointDetailsVM();

            ViewData.zoneId = objPointRaw.zoneId;
            ViewData.wardId = objPointRaw.wardId;
            ViewData.areaId = objPointRaw.areaId;
            ViewData.Address = objPointRaw.Address;
            ViewData.gpLat = objPointRaw.gpLat;
            ViewData.gpLong = objPointRaw.gpLong;
            ViewData.gpName = objPointRaw.gpName;
            ViewData.gpNameMar = objPointRaw.gpNameMar;
            
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId);

            var AppDetails = dbMain.AppDetails.Where(x => x.AppId == _AppId).FirstOrDefault();
            var guid = Guid.NewGuid().ToString().Split('-');
            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

            if (_gpId > 0)
            {
                var _ReferanceId = db.GarbagePointDetails.Where(x => x.gpId == _gpId).Select(x => x.ReferanceId).FirstOrDefault();
                ViewData.ReferanceId = _ReferanceId.ToString();
                var _QrCode = db.GarbagePointDetails.Where(x => x.gpId == _gpId).Select(x => x.qrCode).FirstOrDefault();
                ViewData.QrCode = _QrCode.ToString();
            }
            else
            {
                var id = db.GarbagePointDetails.OrderByDescending(x => x.gpId).Select(x => x.gpId).FirstOrDefault();
                int number = 1000;
                string refer = "GPSBA" + (number + id + 1);
                ViewData.ReferanceId = refer;
                ViewData.QrCode = image_Guid; //"/Images/QRcode.png";
            }

            //Converting  Url to image 
            var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + ViewData.ReferanceId + "&chs=160x160&chld=L|0");

                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                //Creating Path to save image in folder
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);

                string imgpath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode), image_Guid);
                var exists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.PointQRCode));
                }
                img.Save(imgpath);
                response.Close();
                remoteStream.Close();
                readStream.Close();
            //objPointRaw.QrCode = image_Guid;

            Result pointDetails = objRep.SaveGarbagePoint(ViewData, _AppId , _gpId);
                
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
                //objres.isAttendenceOff = pointDetails.isAttendenceOff;
                return objres;
        }



        //Added By Nishikant (15 May 2019)
        [HttpPost]
        [Route("Save/DumpYardDetails")]
        public Result AddDumpYardDetails(CMSBDumpYardDetailsVM dumpYardRaw )
        {
            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("dyId");

            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var dyId = headerValue2.FirstOrDefault();
            var IsNulldyId = (dyId == "" ? "0" : dyId);
            int _dyId = int.Parse(IsNulldyId);

            Result objres = new Result();

            CMSBDumpYardDetailsVM ViewData = new CMSBDumpYardDetailsVM();
            
            ViewData.areaId = dumpYardRaw.areaId;
            ViewData.wardId = dumpYardRaw.wardId;
            ViewData.zoneId = dumpYardRaw.zoneId;
            //ViewData.dyId = dumpYardRaw.dyId;
            ViewData.Address = dumpYardRaw.Address;
            ViewData.dyLat = dumpYardRaw.dyLat;
            ViewData.dyLong = dumpYardRaw.dyLong;
            ViewData.Name = dumpYardRaw.Name;
            ViewData.NameMar = dumpYardRaw.NameMar;
            //ViewData.QrCode = dumpYardRaw.QrCode;
            //ViewData.ReferanceId = dumpYardRaw.ReferanceId;

            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId);
            
            var AppDetails = dbMain.AppDetails.Where(x => x.AppId == _AppId).FirstOrDefault();
            var guid = Guid.NewGuid().ToString().Split('-');
            string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";


            if (_dyId > 0)
            {
                var _ReferanceId = db.DumpYardDetails.Where(x => x.dyId == _dyId).Select(x => x.ReferanceId).FirstOrDefault();
                ViewData.ReferanceId = _ReferanceId.ToString();
                var _QrCode = db.DumpYardDetails.Where(x => x.dyId == _dyId).Select(x => x.dyQRCode).FirstOrDefault();
                ViewData.QrCode = _QrCode.ToString();
            }
            else
            {
                var id = db.DumpYardDetails.OrderByDescending(x => x.dyId).Select(x => x.dyId).FirstOrDefault();
                int number = 1000;
                string refer = "DYSBA" + (number + id + 1);
                ViewData.ReferanceId = refer;
                ViewData.QrCode = image_Guid;// "/Images/QRcode.png";
            }

            //Converting  Url to image 
            // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ point.ReferanceId);

            var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + ViewData.ReferanceId + "&chs=160x160&chld=L|0");

                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                //Creating Path to save image in folder
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);


            //string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.DumpYardQRCode), image_Guid);
            //var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.DumpYardQRCode));
            //if (!exists)
            //{
            //    System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.DumpYardQRCode));
            //}
            //img.Save(imgpath);
            //response.Close();
            //remoteStream.Close();
            //readStream.Close();
            //dumpYard.dyQRCode = image_Guid;


            Result pointDetails = objRep.SaveDumpYard(ViewData ,_AppId ,_dyId);

            
            objres.status = pointDetails.status;
            objres.messageMar = pointDetails.messageMar;
            objres.message = pointDetails.message;
            //objres.isAttendenceOff = pointDetails.isAttendenceOff;
            return objres;
        }


        [HttpPost]
        [Route("Save/EmployeeDetails")]
        public Result AddEmployeeDetails(CMSBEmployeeDetailsVM empRaw /*, HttpPostedFileBase filesUpload*/ )
        {

            objRep = new Repository();

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("UserId");

            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);
            var UserId = headerValue2.FirstOrDefault();
            var IsNullUserId = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserId);

            Result objres = new Result();

            CMSBEmployeeDetailsVM ViewData = new CMSBEmployeeDetailsVM();

            
            ViewData.userAddress = empRaw.userAddress;
            ViewData.userLoginId = empRaw.userLoginId;
            ViewData.userMobileNumber = empRaw.userMobileNumber;
            ViewData.userName = empRaw.userName;
            ViewData.userNameMar = empRaw.userNameMar;
            ViewData.userPassword = empRaw.userPassword;
            ViewData.userEmployeeNo = empRaw.userEmployeeNo;
            ViewData.imoNo = empRaw.imoNo;
            ViewData.isActive = empRaw.isActive;
            ViewData.bloodGroup = empRaw.bloodGroup;
            ViewData.gcTarget = empRaw.gcTarget;
            ViewData.Type = empRaw.Type;


            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId);

            //var AppDetails = dbMain.GetApplicationDetails(SessionHandler.Current.AppId);

            var AppDetails = dbMain.AppDetails.Where(x => x.AppId == _AppId).FirstOrDefault();

            //if (filesUpload != null)
            //{
            //    var guid = Guid.NewGuid().ToString().Split('-');
            //    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

            //    //Converting  Url to image 

            //    string imagePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.UserProfile), image_Guid);
            //    var exists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
            //    if (!exists)
            //    {
            //        System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
            //    }
            //    filesUpload.SaveAs(imagePath);
            //    //emp.userProfileImage = image_Guid;
            //}


            Result pointDetails = objRep.SaveEmployee(ViewData ,_AppId , _UserId ); // objRep.SaveDumpYard(ViewData, _AppId, _dyId);

                
                objres.status = pointDetails.status;
                objres.messageMar = pointDetails.messageMar;
                objres.message = pointDetails.message;
               
                //objres.isAttendenceOff = pointDetails.isAttendenceOff;
                return objres;
            
            
        }




        //Added By Nishikant (14 May 2019)
        [HttpGet]
        [Route("Get/GarbagePointRegistration")]
        public List<CMSBGarbagePointDetailsVM> GetGarbagePointData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("SearchString");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var Offset = headerValue2.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue3.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue4.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<CMSBGarbagePointDetailsVM> objDetail = new List<CMSBGarbagePointDetailsVM>();
            objDetail = objRep.GetGarbagePointData( _SearchString , AppId, _Offset, _Fetch_Next );
            return objDetail;

        }

        //Added By Nishikant (15 May 2019)
        [HttpGet]
        [Route("Get/DumpYardRegistration")]
        public List<CMSBDumpYardDetailsVM> GetDumpYardData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("SearchString");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var Offset = headerValue2.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue3.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue4.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<CMSBDumpYardDetailsVM> objDetail = new List<CMSBDumpYardDetailsVM>();
            objDetail = objRep.GetDumpYardData(_SearchString, AppId, _Offset, _Fetch_Next);
            return objDetail;

        }


        //Added By Nishikant (15 May 2019)
        [HttpGet]
        [Route("Get/EmployeeDetails")]
        public List<CMSBEmployeeDetailsVM> GetEmployeeDetailsData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("SearchString");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var Offset = headerValue2.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue3.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue4.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();

            List<CMSBEmployeeDetailsVM> objDetail = new List<CMSBEmployeeDetailsVM>();
            objDetail = objRep.GetEmployeeDetailsData(_SearchString, AppId, _Offset, _Fetch_Next);
            return objDetail;

        }

        //Added By Nishikant (25 May 2019)
        [HttpGet]
        [Route("Get/UserRouteData")]
        public List<CMSBUserLocationMapVM> UserRouteData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("daId");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var daId = headerValue2.FirstOrDefault();
            var IsNulldaId = (daId == "" ? "0" : daId);
            int _daId = int.Parse(IsNulldaId);

            List<CMSBUserLocationMapVM> obj = new List<CMSBUserLocationMapVM>();
            obj = objRep.GetUserAttenRoute(AppId, _daId);
            return obj;
        }

        #region GetAllDashBoardDetails
        //Added By Nishikant (4 June 2019)
        
        [HttpGet]
        [Route("Get/GetAllDashBoardDetails")]
        public JObject GetDashBoardDetails()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            objChild = new ChildRepository(AppId);

            CMSBDashBoardVM obj = new CMSBDashBoardVM();
            obj = objChild.GetDashBoardDetails();

            List<CMSBDashBoardVM> objList = new List<CMSBDashBoardVM>();
            objList = EmployeeTargetCount(AppId);
            
            return JSonBuilder(obj, objList); 
        }

        private List<CMSBDashBoardVM> EmployeeTargetCount(int AppId )
        {
            string dt = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime fdate = Convert.ToDateTime(dt + " " + "00:00:00");
            DateTime tdate = Convert.ToDateTime(dt + " " + "23:59:59");
            int userId = 0;

            List<CMSBDashBoardVM> obj;

            objChild = new ChildRepository(AppId);
            obj = objChild.getEmployeeTargetData(0, "", fdate, tdate, Convert.ToInt32(userId), AppId);
            return obj;
        }

        private JObject JSonBuilder(CMSBDashBoardVM obj , List<CMSBDashBoardVM> objList)
        {
            
            CMSBDashBoardVM objCount = new CMSBDashBoardVM();
            objCount.Attendance = obj.Attendance;
            objCount.HouseCollection = obj.HouseCollection;
            objCount.PointCollection = obj.PointCollection;
            objCount.DumpYardCount = obj.DumpYardCount;
            objCount.TodayAttandence = obj.TodayAttandence;

            CMSBDashBoardVM objBifurcation = new CMSBDashBoardVM();
            objBifurcation.MixedCount = obj.MixedCount;
            objBifurcation.BifurgatedCount = obj.BifurgatedCount;
            objBifurcation.NotCollected = obj.NotCollected;
            objBifurcation.NotSpecified = obj.NotSpecified;

            CMSBDashBoardVM objDumpYard = new CMSBDashBoardVM();
            objDumpYard.TotalWetWeightCount = obj.TotalWetWeightCount;
            objDumpYard.TotalDryWeightCount = obj.TotalDryWeightCount;
            objDumpYard.TotalGcWeightCount = obj.TotalGcWeightCount;
            objDumpYard.DryWeightCount = obj.DryWeightCount;
            objDumpYard.WetWeightCount = obj.WetWeightCount;
            objDumpYard.GcWeightCount = obj.GcWeightCount;

            string jsonString = string.Empty;

            jsonString += "{\n " + "Count" + ": " + JsonConvert.SerializeObject(objCount, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "Bifurcation" + ": " + JsonConvert.SerializeObject(objBifurcation, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "DumpYard" + ": " + JsonConvert.SerializeObject(objDumpYard, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            //jsonString += "\n " + "Target" + ": " + JsonConvert.SerializeObject(objList, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + "}";

            jsonString += "\n " + "Target" + ": " + JsonConvert.SerializeObject(objList, Formatting.Indented) + "}";

            JObject json = JObject.Parse(jsonString);

            return json;
        }

        #endregion

        //Added By neha(03 june 2019)
        [HttpGet]
        [Route("Get/EmployeeSummary")]
        public List<CMSBEmpolyeeSummaryGrid> GetEmployeeSummary()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var Offset = headerValue5.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue6.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue7.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();


            List<CMSBEmpolyeeSummaryGrid> objDetail = new List<CMSBEmpolyeeSummaryGrid>();
            objDetail = objRep.GetEmployeeSummary(AppId, fdate, tdate, _UserId, _Offset, _Fetch_Next, _SearchString);
            return objDetail;

        }

        //Added By neha(03 june 2019)
        [HttpGet]
        [Route("Get/EmployeeIdelTime")]
        public List<CMSBEmplyeeIdelGrid> GetEmployeeIdelTime()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("fdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("tdate");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("Offset");
            IEnumerable<string> headerValue6 = Request.Headers.GetValues("Fetch_Next");
            IEnumerable<string> headerValue7 = Request.Headers.GetValues("SearchString");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var fromDate = headerValue2.FirstOrDefault();
            DateTime fdate = Convert.ToDateTime(fromDate);
            var toDate = headerValue3.FirstOrDefault();
            DateTime tdate = Convert.ToDateTime(toDate);
            var UserId = headerValue4.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);
            var Offset = headerValue5.FirstOrDefault();
            int _Offset = int.Parse(Offset);
            var Fetch_Next = headerValue6.FirstOrDefault();
            int _Fetch_Next = int.Parse(Fetch_Next);
            var SearchString = headerValue7.FirstOrDefault();
            var IsNullSearchString = (SearchString == "" ? "" : SearchString);
            string _SearchString = IsNullSearchString.ToString();


            List<CMSBEmplyeeIdelGrid> objDetail = new List<CMSBEmplyeeIdelGrid>();
            objDetail = objRep.GetEmployeeIdelTime(AppId, fdate, tdate, _UserId, _Offset, _Fetch_Next, _SearchString);
            return objDetail;

        }


        #region HouseOnmap
        //Added By neha(11 june 2019)
        [HttpGet]
        [Route("Get/HouseLocationOnMap")]
        public List<CMSBHouseLocationOnMap> GetHouseLocationOnMap()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("gcdate");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("UserId");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("areaId");
            IEnumerable<string> headerValue5 = Request.Headers.GetValues("WardNo");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var gcDate = headerValue2.FirstOrDefault();
            DateTime _gcDate = Convert.ToDateTime(gcDate);
            var UserId = headerValue3.FirstOrDefault();
            var IsNullUserID = (UserId == "" ? "0" : UserId);
            int _UserId = int.Parse(IsNullUserID);

            var areaId = headerValue4.FirstOrDefault();
            var IsNullareaId = (areaId == "" ? "0" : areaId);
            int _areaId = int.Parse(IsNullareaId);

            var WardNo = headerValue5.FirstOrDefault();
            var IsNullWardNo = (WardNo == "" ? "0" : WardNo);
            int _WardNo = int.Parse(IsNullWardNo);

            List<CMSBHouseLocationOnMap> objDetail = new List<CMSBHouseLocationOnMap>();
            objDetail = objRep.GetHouseLocationOnMap(AppId, _gcDate, _UserId, _areaId, _WardNo);
            return objDetail;
            
        }
        #endregion

        //Added By Neha (25 july 2019)
        [HttpGet]
        [Route("Get/HouseAttRouteData")]
        public List<CMSBUserLocationMapVM> HouseAttRouteData()
        {
            objRep = new Repository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("daId");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);
            var daId = headerValue2.FirstOrDefault();
            var IsNulldaId = (daId == "" ? "0" : daId);
            int _daId = int.Parse(IsNulldaId);

            List<CMSBUserLocationMapVM> obj = new List<CMSBUserLocationMapVM>();
            obj = objRep.GetHouseAttenRoute(AppId, _daId);
            return obj;
        }
    }
}
