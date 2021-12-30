using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading;
using SwachhBharatAPI.Dal.DataContexts;
using System.Xml;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Games;

namespace SwachBharat.API.Bll.Services
{
    public class ScreenService : AppService, IScreenService
    {
        private int AppID;
        public ScreenService(int AppId) : base(AppId)
            {
                AppID = AppId;
            }
       
        #region House Details
        public CMSBHouseDetailsVM GetHouseDetails(int HouseID)
            {
            try
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == AppID).FirstOrDefault();

                string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
                CMSBHouseDetailsVM house = new CMSBHouseDetailsVM();

                var Details = db.HouseMasters.Where(x => x.houseId == HouseID).FirstOrDefault();
                if (Details != null)
                {
                    house = FillHouseDetailsViewModel(Details);
                    if (house.houseQRCode != null && house.houseQRCode != "")
                    {
                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(ThumbnaiUrlCMS + house.houseQRCode.Trim());
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.NotFound)
                            {
                                house.houseQRCode = "/Images/default_not_upload.png";
                            }
                            else
                            {
                                house.houseQRCode = ThumbnaiUrlCMS + house.houseQRCode.Trim();
                            }
                        }
                        catch (Exception e) { house.houseQRCode = "/Images/default_not_upload.png"; }

                    }
                    else
                    {
                        house.houseQRCode = "/Images/default_not_upload.png";
                    }

                    //house.WardList = ListWardNo();
                    //house.AreaList = ListArea();
                    //house.ZoneList = ListZone();
                    return house;
                }
                else if (HouseID == -2)
                {
                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    //house.WardList = ListWardNo();
                    //house.AreaList = ListArea();
                    //house.ZoneList = ListZone();
                    house.houseId = 0;
                    return house;
                }
                else
                {
                    var id = db.HouseMasters.OrderByDescending(x => x.houseId).Select(x => x.houseId).FirstOrDefault();
                    int number = 1000;
                    string refer = "HPSBA" + (number + id + 1);
                    house.ReferanceId = refer;
                    house.houseQRCode = "/Images/QRcode.png";
                    //house.WardList = ListWardNo();
                    //house.AreaList = ListArea();
                    //house.ZoneList = ListZone();
                    house.houseId = id;
                    return house;
                }

            }
            catch (Exception)
            {
                throw;
            }
            }


        public Result SaveHouseDetails(CMSBHouseDetailsVM data, int _AppId, int _HouseId)
        {

            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == _AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId))
            {
                try
                {
                    CMSBHouseDetailsVM objdata = new CMSBHouseDetailsVM();

                    if (_HouseId > 0)     //Update
                    {

                        var model = db.HouseMasters.Where(x => x.houseId == _HouseId).FirstOrDefault();
                        if (model != null)
                        {
                            model.WardNo = data.WardNoId; //Convert.ToInt32(data.WardNo);
                            model.AreaId = data.AreaId;
                            model.houseOwner = data.houseOwner;
                            model.houseOwnerMar = data.houseOwnerMar;
                            model.houseAddress = data.Address;
                            model.houseOwnerMobile = data.Mobile;
                            model.houseNumber = data.houseNo;
                            model.houseQRCode = data.houseQRCode;
                            model.houseLat = data.houseLat;
                            model.houseLong = data.houseLong;
                            model.ZoneId = data.zoneId;
                            model.modified = DateTime.Now;
                            db.SaveChanges();
                        }

                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }
                    else                //Insert  
                    {

                        var type = FillHouseDetailsDataModel(data);
                        db.HouseMasters.Add(type);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    //result.name = "";
                    result.status = "error";
                    return result;
                }
            }
        }

        private HouseMaster FillHouseDetailsDataModel(CMSBHouseDetailsVM data)
        {
            HouseMaster model = new HouseMaster();
            model.houseId = data.houseId;
            model.WardNo = Convert.ToInt32(data.WardNo);
            model.AreaId = data.AreaId;
            model.houseOwner = data.houseOwner;
            model.houseOwnerMar = data.houseOwnerMar;
            model.houseAddress = data.Address;
            model.houseOwnerMobile = data.Mobile;
            model.houseNumber = data.houseNo;
            model.houseQRCode = data.houseQRCode;
            model.houseLat = data.houseLat;
            model.houseLong = data.houseLong;
            model.ZoneId = data.zoneId;
            model.ReferanceId = data.ReferanceId;
            model.modified = DateTime.Now;
            return model;
        }
        
        public void DeletHouseDetails(int teamId)
            {
                try
                {
                    using (var db = new DevSwachhBharatNagpurEntities(AppID))
                    {
                        var Details = db.HouseMasters.Where(x => x.houseId == teamId).FirstOrDefault();
                        if (Details != null)
                        {
                            db.HouseMasters.Remove(Details);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        #endregion
             

        #region ViewModel 
       
        private CMSBHouseDetailsVM FillHouseDetailsViewModel(HouseMaster data)
        {

            CMSBHouseDetailsVM model = new CMSBHouseDetailsVM();
            model.houseId = data.houseId;
            model.WardNo = data.WardNo.ToString();
            model.AreaId = data.AreaId;
            model.zoneId = data.ZoneId;
            model.houseOwner = data.houseOwner;
            model.houseOwnerMar = data.houseOwnerMar;
            model.Address = data.houseAddress;
            model.Mobile = data.houseOwnerMobile;
            model.houseNo = data.houseNumber;
            model.houseQRCode = data.houseQRCode;
            model.houseLat = data.houseLat;
            model.houseLong = data.houseLong; 
            model.ReferanceId = data.ReferanceId;
            using (var db = new DevSwachhBharatNagpurEntities(AppID))
            {
                if (data.AreaId > 0)
                {
                    model.Area = db.TeritoryMasters.Where(c => c.Id == data.AreaId).FirstOrDefault().Area;
                }
                else {
                    model.Area = "";
                }


                if (data.WardNo.ToString() != "0")
                {
                    model.WardName = db.WardNumbers.Where(c => c.Id == data.WardNo).FirstOrDefault().WardNo;
                }
                else
                {
                    model.WardName = "";
                }

            }

            return model;
        }

        #endregion

        #region Point Details

        public Result SaveGarbagePointDetails(CMSBGarbagePointDetailsVM data, int _AppId, int gpId)
        {

            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == _AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId))
            {
                try
                {
                    CMSBGarbagePointDetailsVM objdata = new CMSBGarbagePointDetailsVM();

                    if (gpId > 0)     //Update
                    {
                        var model = db.GarbagePointDetails.Where(x => x.gpId == gpId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.zoneId;
                            model.wardId = data.wardId;
                            model.areaId = data.areaId;
                            model.gpAddress = data.Address;
                            model.gpLat = data.gpLat;
                            model.gpLong = data.gpLong;
                            model.gpName = data.gpName;
                            model.gpNameMar = data.gpNameMar;
                            model.qrCode = data.QrCode;
                            model.ReferanceId = data.ReferanceId;
                            model.modified = DateTime.Now;
                            db.SaveChanges();
                        }

                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }
                    else                //Insert  
                    {

                        var type = FillGarbagePointDetailsDataModel(data);
                        db.GarbagePointDetails.Add(type);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    //result.name = "";
                    result.status = "error";
                    return result;
                }
            }
        }

        private GarbagePointDetail FillGarbagePointDetailsDataModel(CMSBGarbagePointDetailsVM data)
        {
            GarbagePointDetail model = new GarbagePointDetail();
            model.zoneId = data.zoneId;
            model.wardId = data.wardId;
            model.areaId = data.areaId;
            model.gpId = data.gpId;
            model.gpAddress = data.Address;
            model.gpLat = data.gpLat;
            model.gpLong = data.gpLong;
            model.gpName = data.gpName;
            model.gpNameMar = data.gpNameMar;
            model.qrCode = data.QrCode;
            model.ReferanceId = data.ReferanceId;
            model.modified = DateTime.Now;
            return model;
        }

        #endregion


        #region Dump Yard

        public Result SaveDumpYardtDetails(CMSBDumpYardDetailsVM data, int _AppId, int dyId)
        {

            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == _AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId))
            {
                try
                {
                    
                    if (dyId > 0)     //Update
                    {
                        var model = db.DumpYardDetails.Where(x => x.dyId == dyId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.zoneId;
                            model.wardId = data.wardId;
                            model.areaId = data.areaId;
                            model.dyAddress = data.Address;
                            model.dyLat = data.dyLat;
                            model.dyLong = data.dyLong;
                            model.dyName = data.Name;
                            model.dyNameMar = data.NameMar;
                            model.dyQRCode = data.QrCode;
                            model.ReferanceId = data.ReferanceId;
                            model.lastModifiedDate = DateTime.Now;
                            db.SaveChanges();
                        }

                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        
                    }
                    else                //Insert  
                    {

                        var type = FillDumpYardDetailsDataModel(data);
                        db.DumpYardDetails.Add(type);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.status = "error";
                    return result;
                }
            }

           
        }

        private DumpYardDetail FillDumpYardDetailsDataModel(CMSBDumpYardDetailsVM data)
        {
            DumpYardDetail model = new DumpYardDetail();
            model.areaId = data.areaId;
            model.wardId = data.wardId;
            model.zoneId = data.zoneId;
            model.dyId = data.dyId;
            model.dyAddress = data.Address;
            model.dyLat = data.dyLat;
            model.dyLong = data.dyLong;
            model.dyName = data.Name;
            model.dyNameMar = data.NameMar;
            model.dyQRCode = data.QrCode;
            model.ReferanceId = data.ReferanceId;
            model.lastModifiedDate = DateTime.Now;
            return model;
        }

        #endregion

        #region Employee 

        public Result SaveEmployeeDetails(CMSBEmployeeDetailsVM data, int _AppId, int UserId)
        {

            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == _AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId))
            {
                try
                {
                    if (UserId > 0)     //Update
                    {
                        var model = db.UserMasters.Where(x => x.userId == UserId).FirstOrDefault();
                        if (data.userProfileImage != null)
                        {
                            model.userProfileImage = data.userProfileImage;
                        }
                        //model.userId = data.userId;
                        model.userAddress = data.userAddress;
                        model.userLoginId = data.userLoginId;
                        model.userMobileNumber = data.userMobileNumber;
                        model.userName = data.userName;
                        model.userNameMar = data.userNameMar;
                        model.userPassword = data.userPassword;
                        model.userEmployeeNo = data.userEmployeeNo;
                        model.imoNo = data.imoNo;
                        model.isActive = bool.Parse(data.isActive);
                        model.bloodGroup = data.bloodGroup;
                        model.gcTarget = data.gcTarget;
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        
                    }
                    else                //Insert  
                    {

                        var type = FillEmployeeDataModel(data);
                        db.UserMasters.Add(type);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                       
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.status = "error";
                    return result;
                }
            }
        }

        private UserMaster FillEmployeeDataModel(CMSBEmployeeDetailsVM data)
        {
            UserMaster model = new UserMaster();
            //model.userId = data.userId;
            model.userAddress = data.userAddress;
            model.userLoginId = data.userLoginId;
            model.userMobileNumber = data.userMobileNumber;
            model.userName = data.userName;
            model.userNameMar = data.userNameMar;
            model.userPassword = data.userPassword;
            model.userProfileImage = data.userProfileImage;
            model.userEmployeeNo = data.userEmployeeNo;
            model.imoNo = data.imoNo;
            model.bloodGroup = data.bloodGroup;
            model.isActive = bool.Parse(data.isActive);
            model.gcTarget = data.gcTarget;
            model.Type = data.Type;
            return model;
        }

        #endregion

        #region Dashboard
        public CMSBDashBoardVM GetDashBoardDetailsData()
        {
            CMSBDashBoardVM model = new CMSBDashBoardVM();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(AppID))
                {

                    //DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    //var appdetails = dbm.AppDetails.Where(c => c.AppId == AppID).FirstOrDefault();
                    //List<CMSBComplaintVM> obj = new List<CMSBComplaintVM>();
                    //if (AppID == 1)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
                    //    obj = JsonConvert.DeserializeObject<List<CMSBComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}
                    
                    var data = db.SP_Dashboard_Details().First();

                    var date = DateTime.Today;
                    var houseCount = db.SP_TotalHouseCollection_Count(date).FirstOrDefault();
                    if (data != null)
                    {
                        model.TodayAttandence = data.TodayAttandence;
                        model.Attendance = data.TotalAttandence;
                        model.HouseCollection = data.TotalHouse;
                        model.PointCollection = data.TotalPoint;
                        //model.TotalComplaint = obj.Count();
                        model.TotalHouseCount = houseCount.TotalHouseCount;
                        model.MixedCount = houseCount.MixedCount;
                        model.BifurgatedCount = houseCount.BifurgatedCount;
                        model.NotCollected = houseCount.NotCollected;
                        model.DumpYardCount = data.TotalDump;
                        //model.TotalGcWeightCount = (houseCount.TotalGcWeightCount == null ? 0 : houseCount.TotalGcWeightCount);
                        //model.TotalDryWeightCount = (houseCount.TotalDryWeightCount == null ? 0 : houseCount.TotalDryWeightCount);
                        //model.TotalWetWeightCount = (houseCount.TotalWetWeightCount == null ? 0 : houseCount.TotalWetWeightCount);
                        model.NotSpecified = houseCount.NotSpecified;
                        //model.GcWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.GcWeightCount));
                        //model.DryWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.DryWeightCount));
                        //model.WetWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.WetWeightCount));
                        //model.TotalGcWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.TotalGcWeightCount));
                        //model.TotalDryWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.TotalDryWeightCount));
                        //model.TotalWetWeightCount = Convert.ToDouble(string.Format("{0:0.00}", houseCount.TotalWetWeightCount));

                        model.GcWeightCount = Convert.ToDouble(houseCount.GcWeightCount);
                        model.DryWeightCount = Convert.ToDouble(houseCount.DryWeightCount);
                        model.WetWeightCount = Convert.ToDouble(houseCount.WetWeightCount);
                        model.TotalGcWeightCount = Convert.ToDouble(houseCount.TotalGcWeightCount);
                        model.TotalDryWeightCount = Convert.ToDouble(houseCount.TotalDryWeightCount);
                        model.TotalWetWeightCount = Convert.ToDouble(houseCount.TotalWetWeightCount);

                        return model;
                    }
                    
                    else
                    {
                        return model;
                    }
                }
            }
            catch (Exception)
            {
                return model;
            }
        }
        #endregion

        #region Citizen

        public Result1 SaveDeviceDetails(int appId, string ReferanceId, string FCMID, string DeviceID, string Mobile)
        {

            bool IsExist = false;
            Result1 result = new Result1();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                try
                {
                    DeviceDetail details = new DeviceDetail();

                    IsExist = (from p in db.DeviceDetails where p.DeviceID == DeviceID select p).Count() > 0;

                    if(IsExist == true)
                    {
                        //var model = db.DeviceDetails.Where(x => x.ReferenceID == ReferanceId & x.DeviceID == DeviceID & x.FCMID == FCMID).FirstOrDefault();
                        var model = db.DeviceDetails.Where(x => x.ReferenceID == ReferanceId & x.DeviceID == DeviceID).FirstOrDefault();
                        if (model != null)
                        {
                            model.DeviceID = DeviceID;
                            model.FCMID = FCMID;
                            model.DeviceType = "Android";
                            model.ReferenceID = ReferanceId;
                            model.InstallDate = DateTime.Now;
                            db.SaveChanges();

                            result.Status = "Success";
                            result.Message = "Already registered";
                            result.MessageMar = "आधीच नोंदणी झाली आहे";
                        }
                        else
                        {
                            details.DeviceID = DeviceID;
                            details.FCMID = FCMID;
                            details.DeviceType = "Android";
                            details.ReferenceID = ReferanceId;
                            details.InstallDate = DateTime.Now;
                            db.DeviceDetails.Add(details);
                            db.SaveChanges();

                            result.Status = "Success";
                            result.Message = "Uploaded successfully";
                            result.MessageMar = "सबमिट यशस्वी";
                        }
                    }
                    else
                    {
                        details.DeviceID = DeviceID;
                        details.FCMID = FCMID;
                        details.DeviceType = "Android";
                        details.ReferenceID = ReferanceId;
                        details.InstallDate = DateTime.Now;
                        db.DeviceDetails.Add(details);
                        db.SaveChanges();

                        result.Status = "Success";
                        result.Message = "Uploaded successfully";
                        result.MessageMar = "सबमिट यशस्वी";
                    }

                    ///////////////////////////////////////////////////

                    SBGamePlayerVM data = new SBGamePlayerVM();

                    var GameMaster = dbMain.GameMasters.ToList();
                    if (GameMaster != null)
                    {
                        foreach (var item in GameMaster)
                        {
                            var IsExistPlayer = db.GamePlayerDetails.Where(x => x.GameId == item.GameId && x.Mobile == Mobile).FirstOrDefault();

                            //var IsExistPlayer = db.GamePlayerDetails.Where(x => x.DeviceId == DeviceID && x.GameId == item.GameId && x.Mobile == Mobile).FirstOrDefault();

                            if (IsExistPlayer == null)
                            {
                                data.GameId = item.GameId;
                                var houseDetails = db.HouseMasters.Where(x => x.ReferanceId == ReferanceId).FirstOrDefault();
                                if (houseDetails != null)
                                {
                                    data.PlayerId = ReferanceId;
                                    data.Name = houseDetails.houseOwner;
                                    data.Mobile = houseDetails.houseOwnerMobile;
                                    data.DeviceId = DeviceID;
                                }
                                var type = FillPlayerDataModel(data);
                                db.GamePlayerDetails.Add(type);
                                db.SaveChanges();
                            }
                            else
                            {
                                IsExistPlayer.GameId = item.GameId;
                                var houseDetails = db.HouseMasters.Where(x => x.ReferanceId == ReferanceId).FirstOrDefault();
                                if (houseDetails != null)
                                {
                                    //var DeviceIDDD = db.DeviceDetails.Where(x => x.ReferenceID == ReferanceId).FirstOrDefault();
                                    IsExistPlayer.PlayerId = ReferanceId;
                                    IsExistPlayer.Name = houseDetails.houseOwner;
                                    IsExistPlayer.Mobile = houseDetails.houseOwnerMobile;
                                    IsExistPlayer.DeviceId = DeviceID;
                                }
                                db.SaveChanges();
                            }
                        }
                    }
                    ///////////////////////////////////////////////////

                    return result;
                }
                catch (Exception ex)
                {
                    result.Message = "Something is wrong,Try Again.. ";
                    result.MessageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    //result.name = "";
                    result.Status = "error";
                    return result;
                }
            }
        }

        private GamePlayerDetail FillPlayerDataModel(SBGamePlayerVM data)
        {
            GamePlayerDetail model = new GamePlayerDetail();
            model.PlayerId = data.PlayerId;
            model.GameId = data.GameId;
            model.Name = data.Name;
            model.Mobile = data.Mobile;
            model.Score = data.Score;
            model.DeviceId = data.DeviceId;
            model.Created = DateTime.Now;
            return model;
        }

        public Result1 SaveDeviceDetailsClear(int appId, string DeviceID , string ReferenceID)
        {

            bool IsExist = false;
            Result1 result = new Result1();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                try
                {
                    DeviceDetail details = new DeviceDetail();

                    IsExist = (from p in db.DeviceDetails where p.DeviceID == DeviceID && p.ReferenceID == ReferenceID select p).Count() > 0;

                    if (IsExist == true)
                    {
                        var model = db.DeviceDetails.Where(x => x.DeviceID == DeviceID && x.ReferenceID == ReferenceID).FirstOrDefault();
                        if (model != null)
                        {
                            var Delete = db.DeviceDetails.Where(p => p.DeviceID == DeviceID && p.ReferenceID == ReferenceID).SingleOrDefault();
                            db.DeviceDetails.Remove(Delete);
                            db.SaveChanges();

                            ////////////////////////////////////////////////////////////

                            //(from p in db.GamePlayerDetails
                            // where p.DeviceId == DeviceID && p.PlayerId == ReferenceID
                            // select p).ToList()
                            //            .ForEach(x => x.PlayerId = "External");
                            //db.SaveChanges();

                            ////////////////////////////////////////////////////////////

                            result.Status = "Success";
                            result.Message = "Uploaded successfully";
                            result.MessageMar = "सबमिट यशस्वी";
                        }
                    }
                    else
                    {
                        result.Status = "error";
                        result.Message = "Device not available";
                        result.MessageMar = "साधन उपलब्ध नाही";
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    result.Message = "Something is wrong,Try Again.. ";
                    result.MessageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.Status = "error";
                    return result;
                }
            }
        }

        #endregion

        public List<CMSBUserLocationMapVM> GetUserAttenRoute(int daId)
        {
            List<CMSBUserLocationMapVM> userLocation = new List<CMSBUserLocationMapVM>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == null).ToList();
            foreach (var x in data)
            {
                string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                userLocation.Add(new CMSBUserLocationMapVM()
                {
                    userName = userName.userName,
                    date = dat,
                    time = tim,
                    lat = x.lat,
                    log = x.@long,
                    address = x.address,
                    vehicleNumber = att.vehicleNumber,
                    userMobile = userName.userMobileNumber,
                });
            }

            return userLocation;
        }


        // Added by Neha 24 july 2019
        public List<CMSBUserLocationMapVM> GetHouseAttenRoute(int daId)
        {
            List<CMSBUserLocationMapVM> userLocation = new List<CMSBUserLocationMapVM>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var att = db.Daily_Attendance.Where(c => c.daID == daId).FirstOrDefault();
            string Time = att.startTime;
            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
            string t = date.ToString("hh:mm:ss tt");
            string dt = Convert.ToDateTime(att.daDate).ToString("MM/dd/yyyy");
            DateTime? fdate = Convert.ToDateTime(dt + " " + t);
            DateTime? edate;
            if (att.endTime == "" | att.endTime == null)
            {
                edate = DateTime.Now;
            }
            else
            {
                string Time2 = att.endTime;
                DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                string t2 = date2.ToString("hh:mm:ss tt");
                string dt2 = Convert.ToDateTime(att.daEndDate).ToString("MM/dd/yyyy");
                edate = Convert.ToDateTime(dt2 + " " + t2);
            }
            var data = db.Locations.Where(c => c.userId == att.userId & c.datetime >= fdate & c.datetime <= edate & c.type == 1).ToList();

            foreach (var x in data)
            {
                if (x.type == 1)
                {

                    // string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                    //string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                    var userName = db.UserMasters.Where(c => c.userId == att.userId).FirstOrDefault();
                    //var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).FirstOrDefault();

                    var gcd = db.GarbageCollectionDetails.Where(c => (c.userId == x.userId & c.houseId != null) & EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(x.datetime)).ToList();

                    foreach (var d in gcd)
                    {

                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                        var house = db.HouseMasters.Where(c => c.houseId == d.houseId).FirstOrDefault();
                        userLocation.Add(new CMSBUserLocationMapVM()
                        {
                            userName = userName.userName,
                            date = dat,
                            time = tim,
                            lat = d.Lat,
                            log = d.Long,
                            address = x.address,
                            vehicleNumber = att.vehicleNumber,
                            userMobile = userName.userMobileNumber,
                            type = Convert.ToInt32(x.type),
                            HouseId = house.ReferanceId,
                            HouseAddress = house.houseAddress,
                            HouseOwnerName = house.houseOwner,
                            OwnerMobileNo = house.houseOwnerMobile
                        });

                    }
                    break;
                }

            }



            return userLocation;
        }
    }
}
