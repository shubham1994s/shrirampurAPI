using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Spatial;
using System.IO;
using SwachBharat.API.Bll.Services;
using System.Web.Mvc;
using SwachhBhart.API.Bll.ViewModels.CMSB;
using SwachhBhart.API.Bll.ViewModels.Citizen;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Collections;
using System.Web;
using System.Drawing;

namespace SwachhBharat.API.Bll.Repository.Repository
{
    public class Repository : IRepository
    {
        #region Variables
        IScreenService screenService;
        IMainService MainService;
        public DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        #endregion

        #region CommonMethod
        public int checkIntNull(string str)
        {
            int result = 0;
            if (str == null || str == "")
            {
                result = 0;
                return result;
            }
            else
            {
                result = Convert.ToInt32(str);
                return result;
            }
        }
        public string checkNull(string str)
        {
            string result = "";
            if (str == null || str == "")
            {
                result = "";
                return result;
            }
            else
            {
                result = str;
                return result;
            }
        }
        public string ImagePath(string FolderName, string Image, AppDetail objmain)
        {
            string ImageUrl;
            if (Image == null || Image == "")
            {
                ImageUrl = "";
                return ImageUrl;
            }
            else
            {
                var AppDetailURL = objmain.baseImageUrl + objmain.basePath + FolderName + "/";
                ImageUrl = AppDetailURL + Image;
                return ImageUrl;
            }
        }
        public string ImagePathCMS(string FolderName, string Image, AppDetail objmain)
        {
            string ImageUrl;
            if (Image == null || Image == "")
            {
                ImageUrl = "";
                return ImageUrl;
            }
            else
            {
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + FolderName + "/";
                ImageUrl = AppDetailURL + Image;
                return ImageUrl;
            }
        }

        //public string Address1(string location)
        //{
        //    try
        //    {
        //        string API = "";
        //        GoogelHitDetail hitdetails = dbMain.GoogelHitDetails.Where(c => c.Id == 1).FirstOrDefault();
        //        if (Convert.ToDateTime(hitdetails.Date).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
        //        {
        //            if (hitdetails.hit <= 1000)
        //            {
        //                API = dbMain.GoogleAPIDetails.Where(c => c.Id == 1).FirstOrDefault().GoogleAPI;
        //                hitdetails.hit = hitdetails.hit + 1;
        //            }
        //            else if (hitdetails.hit > 1000 && hitdetails.hit <= 2000)
        //            {
        //                API = dbMain.GoogleAPIDetails.Where(c => c.Id == 2).FirstOrDefault().GoogleAPI;
        //                hitdetails.hit = hitdetails.hit + 1;
        //            }
        //            else if (hitdetails.hit > 2000)
        //            {
        //                API = dbMain.GoogleAPIDetails.Where(c => c.Id == 3).FirstOrDefault().GoogleAPI;
        //                hitdetails.hit = hitdetails.hit + 1;
        //            }
        //        }
        //        else {
        //            hitdetails.Date = DateTime.Now.Date;
        //            hitdetails.hit = 1;
        //            API = dbMain.GoogleAPIDetails.Where(c => c.Id == 1).FirstOrDefault().GoogleAPI;

        //        }

        //        dbMain.SaveChanges();

        //        if (location != string.Empty && location != null)
        //        {
        //            string lat = null, log = null;
        //            string[] arr = new string[2];
        //            arr = location.Split(',');
        //            lat = arr[0];
        //            log = arr[1];
        //            XmlDocument doc = new XmlDocument();
        //            string Address = "";

        //            //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "& sensor=false");
        //      //      doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "&sensor=false&key=" + API);

        //            doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "&sensor=false&key=AIzaSyBy6BUqH6o1r7JBS8s1Tk7cmllapL6xuMA");



        //            XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");

        //            if (element.InnerText == "OVER_QUERY_LIMIT")
        //            {
        //                sendSMS("API Cross limit", "7385888068,8055060248");
        //                return "";
        //            }

        //            if (element.InnerText == "REQUEST_DENIED")
        //            {
        //                sendSMS("API Not working", "7385888068,8055060248");
        //                return "";
        //            }


        //            if (element.InnerText == "ZERO_RESULTS")
        //            {
        //                Console.WriteLine("No data available for the specified location");
        //            }                 
        //            else
        //            {
        //                XmlNode xnList1;
        //                try {
        //                   xnList1 = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");
        //                }
        //                catch
        //                {

        //                    xnList1 = null;
        //                }
        //                if (xnList1 != null)
        //                {
        //                    Address = xnList1.InnerText;
        //                }
        //                else
        //                {
        //                    Address = "";
        //                }
        //            }
        //            return Address;
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch { return ""; }

        //}

        public string Address(string location)
        {
            //    try
            //    {
            //        string API = "";               
            //        var hitdetails = dbMain.GoogelHitDetails.Where(c => c.Date == EntityFunctions.TruncateTime(DateTime.Now) && c.hit < 1300).FirstOrDefault();
            //        if (hitdetails == null)
            //        {
            //            GoogelHitDetail data = dbMain.GoogelHitDetails.Where(c => c.Date != EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
            //            data.Date = DateTime.Now;
            //            data.hit = 1;
            //            dbMain.SaveChanges();

            //        }
            //        else {
            //            API = hitdetails.API;
            //            GoogelHitDetail data = dbMain.GoogelHitDetails.Where(c => c.Id == hitdetails.Id).FirstOrDefault();
            //            data.Date = DateTime.Now;
            //            data.hit = hitdetails.hit + 1;
            //            dbMain.SaveChanges();

            //        }                
            //      if (location != string.Empty && location != null)
            //    {
            //        string lat = null, log = null;
            //        string[] arr = new string[2];
            //        arr = location.Split(',');
            //        lat = arr[0];
            //        log = arr[1];
            //        XmlDocument doc = new XmlDocument();
            //        string Address = "";

            //        //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "& sensor=false");
            //        doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "&sensor=false&key=" + API);

            //        XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
            //            var d = dbMain.AdminContacts.ToList();
            //            string hmob = d.Where(c => c.Id == 1).FirstOrDefault().MobileNumber;
            //            if (element.InnerText == "OVER_QUERY_LIMIT")

            //        {                     
            //            sendSMS("API Cross limit", hmob);
            //            return "";
            //        }

            //        if (element.InnerText == "REQUEST_DENIED")
            //        {
            //            sendSMS("API Not working", hmob);
            //            return "";
            //        }


            //        if (element.InnerText == "ZERO_RESULTS")
            //        {
            //                return "";
            //        }
            //        else
            //        {
            //            XmlNode xnList1;
            //            try
            //            {
            //                xnList1 = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");
            //            }
            //            catch
            //            {

            //                xnList1 = null;
            //               return "";
            //                }
            //            if (xnList1 != null)
            //            {
            //                Address = xnList1.InnerText;
            //            }
            //            else
            //            {
            //                Address = "";
            //            }
            //        }
            //        return Address;
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //}
            //    catch  { return ""; }
            return "";
        }

        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        public string area(string address)
        {
            try
            {
                string[] ad = address.Split(',');
                int l = ad.Length - 4;
                return ad[l];
            }
            catch
            {

                return "";
            }


        }

        public void sendSMS(string sms, string MobilNumber)
        {
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message="+ sms + "%20&response=Y");

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");

                //  HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");

                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }

        public void sendSMSNgp(string sms, string MobilNumber)
        {
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message="+ sms + "%20&response=Y");

                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");

                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=ICTSBM&dest_mobileno=" + MobilNumber + "&message=" + sms + "%20&response=Y");
                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {


            }

        }

        public void sendSMSmar(string sms, string MobilNumber)
        {
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&message=" + sms + "&response=Y");
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=artiyocc&pass=123456&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message="+ sms + "%20&response=Y");
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(" https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=BIGVCL&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "%20&response=Y");
                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }

        public void PushNotificationMessage(string message, string FCMID, string type, string ApiKey)
        {
            // string serverKey = "AAAAx-2qbN4:APA91bHFXj5rDt-FjWekbG9rGi6qehxrz4q3-xFRqCzENEUg2y7RtGCqYTqKGPca3kCrE9gEohYSPx8Xpxw5eBfqJrd4j7TomomnYQMAddnMw9trAGyKuJsWH2YwgxR1g_Tqc5MdJlE1siimjMSWGnMIa7dSefEtgw";

            string authKey = ApiKey; //"AIzaSyDIBhuq26awLm3qQ8yuTYuoFx5WTPOuA7I";

            try
            {
                var result = "-1";
                var webAddr = ConfigurationManager.AppSettings["FCMUrl"];//"https://fcm.googleapis.com/fcm/send";
                string senderId = ConfigurationManager.AppSettings["senderId"];//"858685861086";
                //var regID = "epcs-3k3EiQ:APA91bG7QVcxxPHGMFvo1APvrIruP2vmo_QuRplJSJVCZnXfaN5x1hIgBNn-oLoZgZZeqgX_m17VJVZmZO53Z0cHa-Iw2YPf4TbDMweWNdi_HWrPwff6hzmkGCGVrX2j7lZD6F-kWS8J6ttjcGjgc-Tka4IBHtzW1Q";
                var regID = FCMID.Trim();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + authKey);
                httpWebRequest.Headers.Add(string.Format("Sender:id=" + senderId));
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"to\": \"" + regID + "\",\"data\": {\"title\": \"" + type + " \",\"message\": \"" + message + "\",\"body\": \"GG\"},\"priority\":10,\"timestamp\": \"" + DateTime.Now + "\"}";

                    //registration_ids, array of strings -  to, single recipient
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
            }

        }

        private class NotificationMessage
        {
            public string Title;
            public string Message;
            public long ItemId;
        }

        public void PushNotificationMessageBroadCast(string message, List<String> FCMID, string title, string ApiKey)
        {

            string authKey = ApiKey; //"AIzaSyDIBhuq26awLm3qQ8yuTYuoFx5WTPOuA7I";

            try
            {
                var result = "-1";
                var webAddr = ConfigurationManager.AppSettings["FCMUrl"];// "https://fcm.googleapis.com/fcm/send";
                string senderId = ConfigurationManager.AppSettings["senderId"];//"858685861086";

                string regID = (FCMID.Count > 1 ? (string.Join("\",\"", FCMID)) : (string.Join("\"\"", FCMID)));

                //string.Join("\",\"", FCMID);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + authKey);
                httpWebRequest.Headers.Add(string.Format("Sender:id=" + senderId));
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"registration_ids\": [\"" + regID + "\"],\"data\": {\"title\": \"" + title + " \",\"message\": \"" + message + "\",\"body\": \"GG\"},\"priority\":10,\"timestamp\": \"" + DateTime.Now + "\"}";

                    //registration_ids, array of strings -  to, single recipient
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
            }

        }

        #endregion
        public SBUser CheckUserLogin(string userName, string password, string imi, int AppId, string EmpType)
        {
            SBUser user = new SBUser();
            if (EmpType == "N")
            {
                user = CheckUserLoginForNormal(userName, password, imi, AppId, EmpType);
            }

            if (EmpType == "L")
            {
                user = CheckUserLoginForLiquid(userName, password, imi, AppId, EmpType);
            }

            if (EmpType == "S")
            {
                user = CheckUserLoginForStreet(userName, password, imi, AppId, EmpType);
            }

            if (EmpType == "D")
            {
                user = CheckUserLoginForDump(userName, password, imi, AppId, EmpType);
            }
            return user;
        }

        public SBUser CheckUserLoginForNormal(string userName, string password, string imi, int AppId, string EmpType)
        {
            SBUser user = new SBUser();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
                var obj = db.UserMasters.Where(c => c.userLoginId == userName & c.userPassword == password & c.isActive == true & c.EmployeeType == null).FirstOrDefault();
                var objEmpMst = db.QrEmployeeMasters.Where(c => c.qrEmpLoginId == userName & c.qrEmpPassword == password & c.isActive == true).FirstOrDefault();
                if (obj == null)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "Contact Your Authorized Person.";
                    user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                }


                if (obj != null && obj.userLoginId == userName && obj.userPassword == password)
                {
                    if (obj.imoNo != null)
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo2 = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.EmpType = "N";
                        user.imiNo = us.imoNo;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    if (obj.imoNo == null || obj.imoNo.Trim() == "")
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = us.imoNo2;
                        user.EmpType = "N";
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    //if (obj.imoNo != null && obj.imoNo2 !=null)
                    //{
                    //    UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                    //    us.imoNo = imi;                      
                    //    user.type = checkNull(obj.Type);
                    //    user.userId = obj.userId;
                    //    user.userLoginId = "";
                    //    user.userPassword = "";
                    //    user.imiNo = us.imoNo2;             
                    //    user.EmpType = "N";
                    //    user.gtFeatures = objmain.NewFeatures;
                    //    user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    //    us.imoNo2 = null;
                    //    db.SaveChanges();
                    //}
                    else
                    {
                        if (obj.imoNo == imi)
                        {
                            user.type = checkNull(obj.Type);
                            user.typeId = checkIntNull(obj.Type);
                            user.userId = obj.userId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = obj.imoNo2;
                            user.EmpType = "N";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        //if (obj.imoNo2 == imi)
                        //{
                        //    user.type = checkNull(obj.Type);
                        //    user.typeId = checkIntNull(obj.Type);
                        //    user.userId = obj.userId;
                        //    user.userLoginId = "";
                        //    user.userPassword = "";
                        //    user.imiNo = obj.imoNo;
                        //    user.EmpType = "N";
                        //    user.gtFeatures = objmain.NewFeatures;
                        //    user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        //}
                        //if (obj.imoNo2 == imi)
                        //{
                        //    user.type = checkNull(obj.Type);
                        //    user.typeId = checkIntNull(obj.Type);
                        //    user.userId = obj.userId;
                        //    user.userLoginId = "";
                        //    user.userPassword = "";
                        //    user.imiNo = "";
                        //    user.EmployeeType = obj.EmployeeType;
                        //    user.gtFeatures = objmain.NewFeatures;
                        //    user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        //}
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }
                    }

                }

                else if (obj != null && obj.userLoginId == userName && obj.userPassword != password)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "UserName or Passward not Match.";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
                //else if (objEmpMst == null)
                // {
                //     user.userId = 0;
                //     user.userLoginId = "";
                //     user.userPassword = "";
                //     user.status = "error";
                //     user.gtFeatures = false;
                //     user.imiNo = "";
                //     user.message = "Contact Your Authorized Person.";
                //     user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                // }
                else if (objEmpMst != null && objEmpMst.qrEmpLoginId == userName && objEmpMst.qrEmpPassword == password)
                {


                    if (objEmpMst.imoNo == null || objEmpMst.imoNo.Trim() == "")
                    {
                        QrEmployeeMaster us = db.QrEmployeeMasters.Where(c => c.qrEmpId == objEmpMst.qrEmpId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(objEmpMst.type);
                        user.typeId = Convert.ToInt32(objEmpMst.typeId);
                        user.userId = objEmpMst.qrEmpId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = "N";
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    else
                    {
                        if (objEmpMst.imoNo == imi || imi == null)
                        {
                            user.type = checkNull(objEmpMst.type);
                            user.typeId = Convert.ToInt32(objEmpMst.typeId);
                            user.userId = objEmpMst.qrEmpId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = "N";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }

                    }
                }

                else
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.message = "UserName or Passward not Match.";
                    user.EmpType = "";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
            }
            return user;
        }


        public SBUser CheckUserLoginForLiquid(string userName, string password, string imi, int AppId, string EmpType)
        {
            SBUser user = new SBUser();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
                var obj = db.UserMasters.Where(c => c.userLoginId == userName & c.isActive == true & c.EmployeeType == "L").FirstOrDefault();
                var objEmpMst = db.QrEmployeeMasters.Where(c => c.qrEmpLoginId == userName & c.isActive == true).FirstOrDefault();
                if (obj == null)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "Contact Your Authorized Person.";
                    user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                }


                if (obj != null && obj.userLoginId == userName && obj.userPassword == password)
                {
                    if (obj.imoNo != null)
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo2 = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.EmpType = "L";
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    if (obj.imoNo == null || obj.imoNo.Trim() == "")
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = "L";
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }

                    else
                    {
                        if (obj.imoNo == imi)
                        {
                            user.type = checkNull(obj.Type);
                            user.typeId = checkIntNull(obj.Type);
                            user.userId = obj.userId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = "L";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        //if (obj.imoNo2 == imi)
                        //{
                        //    user.type = checkNull(obj.Type);
                        //    user.typeId = checkIntNull(obj.Type);
                        //    user.userId = obj.userId;
                        //    user.userLoginId = "";
                        //    user.userPassword = "";
                        //    user.imiNo = "";
                        //    user.EmployeeType = obj.EmployeeType;
                        //    user.gtFeatures = objmain.NewFeatures;
                        //    user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        //}
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }
                    }

                }

                else if (obj != null && obj.userLoginId == userName && obj.userPassword != password)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "UserName or Passward not Match.";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
                //else if (objEmpMst == null)
                // {
                //     user.userId = 0;
                //     user.userLoginId = "";
                //     user.userPassword = "";
                //     user.status = "error";
                //     user.gtFeatures = false;
                //     user.imiNo = "";
                //     user.message = "Contact Your Authorized Person.";
                //     user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                // }
                else if (objEmpMst != null && objEmpMst.qrEmpLoginId == userName && objEmpMst.qrEmpPassword == password)
                {


                    if (objEmpMst.imoNo == null || objEmpMst.imoNo.Trim() == "")
                    {
                        QrEmployeeMaster us = db.QrEmployeeMasters.Where(c => c.qrEmpId == objEmpMst.qrEmpId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(objEmpMst.type);
                        user.typeId = Convert.ToInt32(objEmpMst.typeId);
                        user.userId = objEmpMst.qrEmpId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = obj.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    else
                    {
                        if (objEmpMst.imoNo == imi || imi == null)
                        {
                            user.type = checkNull(objEmpMst.type);
                            user.typeId = Convert.ToInt32(objEmpMst.typeId);
                            user.userId = objEmpMst.qrEmpId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = "L";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }

                    }
                }

                else
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "UserName or Passward not Match.";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
            }
            return user;
        }

        public SBUser CheckUserLoginForStreet(string userName, string password, string imi, int AppId, string EmpType)
        {
            SBUser user = new SBUser();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
                var obj = db.UserMasters.Where(c => c.userLoginId == userName & c.isActive == true & c.EmployeeType == "S").FirstOrDefault();
                var objEmpMst = db.QrEmployeeMasters.Where(c => c.qrEmpLoginId == userName & c.isActive == true).FirstOrDefault();
                if (obj == null)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "Contact Your Authorized Person.";
                    user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                }


                if (obj != null && obj.userLoginId == userName && obj.userPassword == password)
                {
                    if (obj.imoNo != null)
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo2 = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.EmpType = obj.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    if (obj.imoNo == null || obj.imoNo.Trim() == "")
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = us.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }

                    else
                    {
                        if (obj.imoNo == imi)
                        {
                            user.type = checkNull(obj.Type);
                            user.typeId = checkIntNull(obj.Type);
                            user.userId = obj.userId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = obj.EmployeeType;
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        //if (obj.imoNo2 == imi)
                        //{
                        //    user.type = checkNull(obj.Type);
                        //    user.typeId = checkIntNull(obj.Type);
                        //    user.userId = obj.userId;
                        //    user.userLoginId = "";
                        //    user.userPassword = "";
                        //    user.imiNo = "";
                        //    user.EmployeeType = obj.EmployeeType;
                        //    user.gtFeatures = objmain.NewFeatures;
                        //    user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        //}
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }
                    }

                }

                else if (obj != null && obj.userLoginId == userName && obj.userPassword != password)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "UserName or Passward not Match.";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
                //else if (objEmpMst == null)
                // {
                //     user.userId = 0;
                //     user.userLoginId = "";
                //     user.userPassword = "";
                //     user.status = "error";
                //     user.gtFeatures = false;
                //     user.imiNo = "";
                //     user.message = "Contact Your Authorized Person.";
                //     user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                // }
                else if (objEmpMst != null && objEmpMst.qrEmpLoginId == userName && objEmpMst.qrEmpPassword == password)
                {


                    if (objEmpMst.imoNo == null || objEmpMst.imoNo.Trim() == "")
                    {
                        QrEmployeeMaster us = db.QrEmployeeMasters.Where(c => c.qrEmpId == objEmpMst.qrEmpId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(objEmpMst.type);
                        user.typeId = Convert.ToInt32(objEmpMst.typeId);
                        user.userId = objEmpMst.qrEmpId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = obj.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    else
                    {
                        if (objEmpMst.imoNo == imi || imi == null)
                        {
                            user.type = checkNull(objEmpMst.type);
                            user.typeId = Convert.ToInt32(objEmpMst.typeId);
                            user.userId = objEmpMst.qrEmpId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = "S";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }

                    }
                }

                else
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.message = "UserName or Passward not Match.";
                    user.EmpType = "";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
            }
            return user;
        }

        public SBUser CheckUserLoginForDump(string userName, string password, string imi, int AppId, string EmpType)
        {
            SBUser user = new SBUser();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
                var obj = db.UserMasters.Where(c => c.userLoginId == userName & c.isActive == true & c.EmployeeType == "D").FirstOrDefault();
                var objEmpMst = db.QrEmployeeMasters.Where(c => c.qrEmpLoginId == userName & c.isActive == true).FirstOrDefault();
                if (obj == null)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "Contact Your Authorized Person.";
                    user.messageMar = "आपल्या अधिकृत व्यक्तीशी संपर्क साधा.";
                }


                if (obj != null && obj.userLoginId == userName && obj.userPassword == password)
                {
                    if (obj.imoNo != null)
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo2 = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.EmpType = obj.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    if (obj.imoNo == null || obj.imoNo.Trim() == "")
                    {
                        UserMaster us = db.UserMasters.Where(c => c.userId == obj.userId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(obj.Type);
                        user.userId = obj.userId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = us.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }

                    else
                    {
                        if (obj.imoNo == imi)
                        {
                            user.type = checkNull(obj.Type);
                            user.typeId = checkIntNull(obj.Type);
                            user.userId = obj.userId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = obj.EmployeeType;
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                    
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }
                    }

                }

                else if (obj != null && obj.userLoginId == userName && obj.userPassword != password)
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.EmpType = "";
                    user.message = "UserName or Passward not Match.";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
             
                else if (objEmpMst != null && objEmpMst.qrEmpLoginId == userName && objEmpMst.qrEmpPassword == password)
                {


                    if (objEmpMst.imoNo == null || objEmpMst.imoNo.Trim() == "")
                    {
                        QrEmployeeMaster us = db.QrEmployeeMasters.Where(c => c.qrEmpId == objEmpMst.qrEmpId).FirstOrDefault();
                        us.imoNo = imi;
                        db.SaveChanges();

                        user.type = checkNull(objEmpMst.type);
                        user.typeId = Convert.ToInt32(objEmpMst.typeId);
                        user.userId = objEmpMst.qrEmpId;
                        user.userLoginId = "";
                        user.userPassword = "";
                        user.imiNo = "";
                        user.EmpType = obj.EmployeeType;
                        user.gtFeatures = objmain.NewFeatures;
                        user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                    }
                    else
                    {
                        if (objEmpMst.imoNo == imi || imi == null)
                        {
                            user.type = checkNull(objEmpMst.type);
                            user.typeId = Convert.ToInt32(objEmpMst.typeId);
                            user.userId = objEmpMst.qrEmpId;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.imiNo = "";
                            user.EmpType = "D";
                            user.gtFeatures = objmain.NewFeatures;
                            user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
                        }
                        else
                        {
                            user.userId = 0;
                            user.userLoginId = "";
                            user.userPassword = "";
                            user.status = "error";
                            user.gtFeatures = false;
                            user.imiNo = "";
                            user.EmpType = "";
                            user.message = "User is already login with another mobile.";
                            user.messageMar = "वापरकर्ता दुसर्या मोबाइलवर आधीपासूनच लॉगिन आहे.";

                        }

                    }
                }

                else
                {
                    user.userId = 0;
                    user.userLoginId = "";
                    user.userPassword = "";
                    user.status = "error";
                    user.gtFeatures = false;
                    user.imiNo = "";
                    user.message = "UserName or Passward not Match.";
                    user.EmpType = "";
                    user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
                }
            }
            return user;
        }

        public SBAdmin CheckAdminLogin(string userName, string password)
        {
            SBAdmin admin = new SBAdmin();
            var obj = dbMain.AspNetUsers.Where(c => c.UserName == userName).FirstOrDefault();
            var userId = obj.Id;
            var appId = dbMain.UserInApps.Where(x => x.UserId == userId).Select(x => x.AppId).FirstOrDefault();
            var objMain = dbMain.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();
            if (obj.UserName == userName && (password == "Admin#123" || password == "Nazim#123") && objMain != null)
            {
                //var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
                admin.adminId = userId;
                admin.adminLoginId = obj.UserName;
                admin.adminPassword = password;
                admin.AppId = objMain.AppId;
                admin.AppName = objMain.AppName;
                admin.AppName_mar = objMain.AppName_mar;
                admin.Latitude = objMain.Latitude;
                admin.Logitude = objMain.Logitude;
                //admin.gtFeatures = false;
                admin.status = "success"; admin.message = "Login Successfully"; admin.messageMar = "लॉगिन यशस्वी";
                var DB_Name = dbMain.AppConnections.Where(x => x.AppId == appId).FirstOrDefault().InitialCatalog;
                admin.databaseName = DB_Name;
            }


            else
            {
                //admin.adminId = "";
                //admin.adminLoginId = "";
                //admin.adminPassword = "";
                admin.status = "error";
                admin.gtFeatures = false;
                admin.AppName = "";
                admin.AppName_mar = "";
                admin.Latitude = "";
                admin.Logitude = "";
                admin.message = "UserName or Passward not Match.";
                admin.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही..";

            }

            return admin;
        }
        public SBUserView GetUser(int AppId, int userId, int typeId)
        {
            SBUserView user = new SBUserView();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                AppDetail objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();

                if (typeId == 0 || typeId == 2)
                {
                    var obj = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                    if (obj != null)
                    {
                        user.type = obj.EmployeeType;
                        user.typeId = obj.Type == null ? 0 : int.Parse(obj.Type);
                        user.userId = obj.userEmployeeNo;
                        user.name = checkNull(obj.userName);
                        user.nameMar = checkNull(obj.userNameMar);
                        user.mobileNumber = obj.userMobileNumber;
                        user.address = obj.userAddress;
                        user.bloodGroup = checkNull(obj.bloodGroup);
                        user.profileImage = ImagePathCMS(objmain.UserProfile, obj.userProfileImage, objmain);
                    }
                }
                else if (typeId == 1)
                {
                    var obj = db.QrEmployeeMasters.Where(c => c.qrEmpId == userId).FirstOrDefault();
                    if (obj != null)
                    {
                        user.type = obj.type;
                        user.typeId = Convert.ToInt32(obj.typeId);
                        user.userId = obj.userEmployeeNo;
                        user.name = checkNull(obj.qrEmpName);
                        user.nameMar = checkNull(obj.qrEmpNameMar);
                        user.mobileNumber = obj.qrEmpMobileNumber;
                        user.address = obj.qrEmpAddress;
                        user.bloodGroup = checkNull(obj.bloodGroup);
                        user.profileImage = ""; //ImagePathCMS(objmain.UserProfile, obj., objmain);
                    }
                }

            }
            return user;
        }

        public List<SBVehicleType> GetVehicle(int appId)
        {
            List<SBVehicleType> obj = new List<SBVehicleType>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.VehicleTypes.Where(c => c.isActive == true).ToList();
                foreach (var x in data)
                {
                    string des = "", desmar = ""; ;
                    if (x.description != null)
                    {
                        des = checkNull(x.description.Trim());
                    }
                    if (x.descriptionMar != null)
                    {
                        desmar = checkNull(x.descriptionMar.Trim());
                    }
                    obj.Add(new SBVehicleType()
                    {

                        vtId = x.vtId,
                        description = des,
                        descriptionMar = desmar
                    });
                }

            }
            return obj;
        }

        public List<EmployeeVM> GetActiveEmployee(int appId)
        {
            List<EmployeeVM> obj = new List<EmployeeVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.UserMasters.Where(c => c.isActive == true).ToList();
                foreach (var x in data)
                {
                    string name = "", namemar = ""; ;
                    if (x.userName != null)
                    {
                        name = checkNull(x.userName.Trim());
                    }
                    if (x.userNameMar != null)
                    {
                        namemar = checkNull(x.userNameMar.Trim());
                    }
                    obj.Add(new EmployeeVM()
                    {

                        userId = x.userId,
                        userName = name,
                        userNameMar = namemar
                    });
                }

            }
            return obj;
        }

       
        public List<SyncResult> CheckHSUserName(int AppId, string userName)
        {
         
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var isrecord1 = db.QrEmployeeMasters.Where(x => x.qrEmpName == userName && x.isActive == true).FirstOrDefault();
                if (isrecord1 == null)
                {
                    List<SyncResult> objres = new List<SyncResult>();

                    objres.Add(new SyncResult()
                    {
                        status = "Success",
                        messageMar = "",
                        message = ""
                    });

                    return objres;
                    //return true;
                }
                else
                {
                    List<SyncResult> objres = new List<SyncResult>();

                    objres.Add(new SyncResult()
                    {
                        status = "Error",
                        messageMar = "नाव आधीपासून अस्तित्वात आहे..",
                        message = "Name Already Exist"
                    });

                    return objres;
                }
            }
        }

        public List<SyncResult> CheckHSUserLoginId(int AppId, string loginid)
        {

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var isrecord = db.QrEmployeeMasters.Where(x => x.qrEmpLoginId == loginid && x.isActive == true).FirstOrDefault();
                var isrecord1 = db.UserMasters.Where(x => x.userLoginId == loginid && x.isActive == true).FirstOrDefault();
                if (isrecord == null && isrecord1 == null)
                {
                    List<SyncResult> objres = new List<SyncResult>();

                    objres.Add(new SyncResult()
                    {
                        status = "Success",
                        messageMar = "",
                        message = ""
                    });

                    return objres;
                    //return true;
                }
                else
                {
                    List<SyncResult> objres = new List<SyncResult>();

                    objres.Add(new SyncResult()
                    {
                        status = "Error",
                        messageMar = "हे लॉगिनआयडी आधीच अस्तित्वात आहे !",
                        message = "This LoginId Is Already Exist !"
                    });

                    return objres;
                }
            }
        }

        //public List<SyncResult> SaveUserLocation(List<SBUserLocation> obj, int AppId, string batteryStatus)
        //{
        //    DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        try
        //        {
        //            List<SyncResult> result = new List<SyncResult>();
        //            var distCount = "";
        //            foreach (var x in obj)
        //            {
        //                var u = db.UserMasters.Where(c => c.userId == x.userId);

        //                if (x.OfflineId == 0)
        //                {

        //                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
        //                    if (atten == null)
        //                    {
        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = x.OfflineId,
        //                            isAttendenceOff = true,
        //                            status = "error",
        //                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
        //                            message = "Something is wrong,Try Again.. "
        //                        });

        //                        continue;
        //                    }
        //                    //else
        //                    //{
        //                    //    result.Add(new SyncResult()
        //                    //    {
        //                    //        isAttendenceOff = false,
        //                    //    });
        //                    //}

        //                }

        //                if (u != null & x.userId > 0)
        //                {
        //                    string addr = "", ar = "";
        //                    addr = Address(x.lat + "," + x.@long);
        //                    if (addr != "")
        //                    {
        //                        ar = area(addr);
        //                    }

        //                    //Location objdata = new Location();
        //                    //objdata.userId = obj.userId;
        //                    //objdata.gcDate = DateTime.Now;
        //                    //objdata.Lat = obj.Lat;
        //                    //    Location loc = new Location();
        //                    var locc = db.SP_UserLatLongDetail(x.userId).FirstOrDefault();

        //                    if (locc == null || locc.lat == "" || locc.@long == "")
        //                    {
        //                        //string a = objdata.Lat;
        //                        //string b = objdata.Long;

        //                        string a = x.lat;
        //                        string b = x.@long;

        //                        var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                        distCount = dist.Distance_in_KM.ToString();
        //                    }
        //                    else
        //                    {
        //                        var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                        distCount = dist.Distance_in_KM.ToString();
        //                    }

        //                    db.Locations.Add(new Location()
        //                    {
        //                        userId = x.userId,
        //                        lat = x.lat,
        //                        @long = x.@long,
        //                        datetime = x.datetime,
        //                        address = addr,
        //                        area = ar,
        //                        batteryStatus = batteryStatus,
        //                        Distnace = Convert.ToDecimal(distCount),

        //                    });
        //                    db.SaveChanges();

        //                    result.Add(new SyncResult()
        //                    {
        //                        ID = x.OfflineId,
        //                        isAttendenceOff = false,
        //                        status = "success",
        //                        message = "Uploaded successfully",
        //                        messageMar = "सबमिट यशस्वी"
        //                    });

        //                }

        //            }
        //            return result;
        //        }
        //        catch
        //        {
        //            List<SyncResult> objres = new List<SyncResult>();

        //            objres.Add(new SyncResult()
        //            {
        //                status = "error",
        //                messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
        //                message = "Something is wrong,Try Again.. "
        //            });

        //            return objres;
        //        }

        //    }region

        //}


        #region SaveUserLocation Old Code
        //public List<SyncResult> SaveUserLocation(List<SBUserLocation> obj, int AppId, string batteryStatus, int typeId)
        //{
        //    DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        try
        //        {
        //            List<SyncResult> result = new List<SyncResult>();
        //            var distCount = "";

        //            if (typeId == 0)
        //            {
        //                foreach (var x in obj)
        //                {
        //                    var u = db.UserMasters.Where(c => c.userId == x.userId);

        //                    DateTime Offlinedate = Convert.ToDateTime(x.datetime);

        //                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId & c.daDate == EntityFunctions.TruncateTime(Offlinedate)).FirstOrDefault();

        //                    if (atten == null)
        //                    {
        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = Convert.ToInt32(x.OfflineId),
        //                            isAttendenceOff = true,
        //                            status = "error",
        //                            message = "Your duty is currently off, please start again.. ",
        //                            messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..",
        //                        });

        //                        //return result;
        //                        continue;
        //                    }


        //                    if (u != null & x.userId > 0)
        //                    {
        //                        string addr = "", ar = "";
        //                        addr = Address(x.lat + "," + x.@long);
        //                        if (addr != "")
        //                        {
        //                            ar = area(addr);
        //                        }

        //                        var locc = db.SP_UserLatLongDetail(x.userId, typeId).FirstOrDefault();

        //                        if (locc == null || locc.lat == "" || locc.@long == "")
        //                        {
        //                            string a = x.lat;
        //                            string b = x.@long;

        //                            var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                            distCount = dist.Distance_in_KM.ToString();
        //                        }
        //                        else
        //                        {

        //                            var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                            distCount = dist.Distance_in_KM.ToString();
        //                        }

        //                        var IsSameRecordLocation = db.Locations.Where(c => c.userId == x.userId && c.datetime == x.datetime).FirstOrDefault();

        //                        if (IsSameRecordLocation == null)
        //                        {
        //                            db.Locations.Add(new Location()
        //                            {
        //                                userId = x.userId,
        //                                lat = x.lat,
        //                                @long = x.@long,
        //                                datetime = x.datetime,
        //                                address = addr,
        //                                area = ar,
        //                                batteryStatus = batteryStatus,
        //                                Distnace = Convert.ToDecimal(distCount),
        //                                CreatedDate = DateTime.Now,

        //                            });
        //                            db.SaveChanges();
        //                        }

        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = Convert.ToInt32(x.OfflineId),
        //                            status = "success",
        //                            message = "Uploaded successfully",
        //                            messageMar = "सबमिट यशस्वी",
        //                        });
        //                    }

        //                }

        //            }
        //            else if (typeId == 1)
        //            {
        //                foreach (var x in obj)
        //                {
        //                    var u = db.QrEmployeeMasters.Where(c => c.qrEmpId == x.userId);

        //                    DateTime Offlinedate = Convert.ToDateTime(x.datetime);
        //                    var atten = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == x.userId & c.endTime == "" & c.startDate == EntityFunctions.TruncateTime(x.OfflineId == 0 ? DateTime.Now : Offlinedate)).FirstOrDefault();
        //                    if (atten == null)
        //                    {
        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = Convert.ToInt32(x.OfflineId),
        //                            isAttendenceOff = true,
        //                            status = "error",
        //                            message = "Your duty is currently off, please start again.. ",
        //                            messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..",
        //                        });

        //                        continue;
        //                    }


        //                    if (u != null & x.userId > 0)
        //                    {
        //                        string addr = "", ar = "";
        //                        addr = Address(x.lat + "," + x.@long);
        //                        if (addr != "")
        //                        {
        //                            ar = area(addr);
        //                        }

        //                        var locc = db.SP_UserLatLongDetail(x.userId, typeId).FirstOrDefault();

        //                        if (locc == null || locc.lat == "" || locc.@long == "")
        //                        {

        //                            string a = x.lat;
        //                            string b = x.@long;

        //                            var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                            distCount = dist.Distance_in_KM.ToString();
        //                        }
        //                        else
        //                        {

        //                            var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
        //                            distCount = dist.Distance_in_KM.ToString();
        //                        }

        //                        var IsSameRecordQr_Location = db.Qr_Location.Where(c => c.empId == x.userId && c.datetime == x.datetime).FirstOrDefault();

        //                        if (IsSameRecordQr_Location == null)
        //                        {
        //                            db.Qr_Location.Add(new Qr_Location()
        //                            {
        //                                empId = x.userId,
        //                                lat = x.lat,
        //                                @long = x.@long,
        //                                datetime = x.datetime,
        //                                address = addr,
        //                                area = ar,
        //                                batteryStatus = batteryStatus,
        //                                Distnace = Convert.ToDecimal(distCount),
        //                                //CreatedDate = DateTime.Now,

        //                            });
        //                            db.SaveChanges();
        //                        }

        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = Convert.ToInt32(x.OfflineId),
        //                            status = "success",
        //                            message = "Uploaded successfully",
        //                            messageMar = "सबमिट यशस्वी",
        //                        });
        //                    }
        //                }
        //            }
        //            return result;
        //        }
        //        catch
        //        {
        //            throw ;
        //            List<SyncResult> objres = new List<SyncResult>();
        //            objres.Add(new SyncResult()
        //            {
        //                ID = 0 ,
        //                status = "error",
        //                messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
        //                message = "Something is wrong,Try Again.. ",
        //            });

        //            return objres;
        //        }

        //    }

        //}
        #endregion

        public List<SyncResult> SaveUserLocation(List<SBUserLocation> obj, int AppId, string batteryStatus, int typeId, string EmpType)
        {
            List<SyncResult> result = new List<SyncResult>();
            if(EmpType=="N" || EmpType=="S" || EmpType=="L")
            {
                result = SaveUserLocationNSL(obj,AppId,batteryStatus,typeId,EmpType);
            }

            if (EmpType == "SA")
            {
                result = SaveUserLocationSA(obj, AppId, batteryStatus, typeId, EmpType);
            }

            return result;
        }

        public List<SyncResult> SaveUserLocationNSL(List<SBUserLocation> obj, int AppId, string batteryStatus, int typeId, string EmpType)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    List<SyncResult> result = new List<SyncResult>();
                    var distCount = "";

                    if (typeId == 0)
                    {
                        foreach (var x in obj)
                        {
                            DateTime Dateeee = Convert.ToDateTime(x.datetime);
                            DateTime newTime = Dateeee;
                            DateTime oldTime;
                            TimeSpan span = TimeSpan.Zero;
                            var gcd = db.Locations.Where(c => c.userId == x.userId && c.type == null && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.locId).FirstOrDefault();
                            if (gcd != null)
                            {
                                oldTime = gcd.datetime.Value;
                                span = newTime.Subtract(oldTime);
                            }

                            if (gcd == null || span.Minutes >= 9)
                            {
                                //    var IsSameRecord_Location = db.Locations.Where(c => c.userId == x.userId && c.datetime == x.datetime).FirstOrDefault();

                                //if (IsSameRecord_Location == null)
                                //{
                                var u = db.UserMasters.Where(c => c.userId == x.userId);

                                DateTime Offlinedate = Convert.ToDateTime(x.datetime);
                                //var atten = db.Daily_Attendance.Where(c => c.userId == x.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(x.OfflineId == 0 ? DateTime.Now : Offlinedate)).FirstOrDefault();

                                var atten = db.Daily_Attendance.Where(c => c.userId == x.userId & c.daDate == EntityFunctions.TruncateTime(Offlinedate)).FirstOrDefault();

                                if (atten == null)
                                {
                                    result.Add(new SyncResult()
                                    {
                                        ID = Convert.ToInt32(x.OfflineId),
                                        isAttendenceOff = true,
                                        status = "error",
                                        message = "Your duty is currently off, please start again.. ",
                                        messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..",
                                    });

                                    //return result;
                                    continue;
                                }

                                if (u != null & x.userId > 0)
                                {
                                    string addr = "", ar = "";
                                    addr = Address(x.lat + "," + x.@long);
                                    if (addr != "")
                                    {
                                        ar = area(addr);
                                    }


                                    //Location objdata = new Location();
                                    //objdata.userId = obj.userId;
                                    //objdata.gcDate = DateTime.Now;
                                    //objdata.Lat = obj.Lat;
                                    //    Location loc = new Location();
                                    var locc = db.SP_UserLatLongDetail(x.userId, typeId).FirstOrDefault();

                                    if (locc == null || locc.lat == "" || locc.@long == "")
                                    {
                                        //string a = objdata.Lat;
                                        //string b = objdata.Long;

                                        string a = x.lat;
                                        string b = x.@long;

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }
                                    else
                                    {

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }

                                    if (EmpType == "N")
                                    {
                                        EmpType = null;
                                    }

                                    db.Locations.Add(new Location()
                                    {
                                        userId = x.userId,
                                        lat = x.lat,
                                        @long = x.@long,
                                        datetime = x.datetime,
                                        address = addr,
                                        area = ar,
                                        batteryStatus = batteryStatus,
                                        Distnace = Convert.ToDecimal(distCount),
                                        CreatedDate = DateTime.Now,
                                        EmployeeType = EmpType,
                                    });
                                    db.SaveChanges();
                                }
                            }

                            result.Add(new SyncResult()
                            {
                                ID = Convert.ToInt32(x.OfflineId),
                                status = "success",
                                message = "Uploaded successfully",
                                messageMar = "सबमिट यशस्वी",
                            });


                        }

                    }
                    else if (typeId == 1)
                    {
                        foreach (var x in obj)
                        {

                            DateTime newTime = x.datetime;
                            DateTime oldTime;
                            TimeSpan span = TimeSpan.Zero;
                            var IsSameRecordQr_Location = db.Qr_Location.Where(c => c.empId == x.userId && c.type == null && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(x.datetime)).OrderByDescending(c => c.locId).FirstOrDefault();
                            if (IsSameRecordQr_Location != null)
                            {
                                oldTime = IsSameRecordQr_Location.datetime.Value;
                                span = newTime.Subtract(oldTime);
                            }

                            if (IsSameRecordQr_Location == null || span.Minutes >= 9)

                            {
                                var u = db.QrEmployeeMasters.Where(c => c.qrEmpId == x.userId);

                                DateTime Offlinedate = Convert.ToDateTime(x.datetime);
                                var atten = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == x.userId & c.endTime == "" & c.startDate == EntityFunctions.TruncateTime(x.OfflineId == 0 ? DateTime.Now : Offlinedate)).FirstOrDefault();
                                if (atten == null)
                                {
                                    result.Add(new SyncResult()
                                    {
                                        ID = Convert.ToInt32(x.OfflineId),
                                        isAttendenceOff = true,
                                        status = "error",
                                        message = "Your duty is currently off, please start again.. ",
                                        messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..",
                                    });

                                    //return result;
                                    continue;
                                }

                                if (u != null & x.userId > 0)
                                {
                                    string addr = "", ar = "";
                                    addr = Address(x.lat + "," + x.@long);
                                    if (addr != "")
                                    {
                                        ar = area(addr);
                                    }

                                    //Location objdata = new Location();
                                    //objdata.userId = obj.userId;
                                    //objdata.gcDate = DateTime.Now;
                                    //objdata.Lat = obj.Lat;
                                    //    Location loc = new Location();

                                    var locc = db.SP_UserLatLongDetail(x.userId, typeId).FirstOrDefault();

                                    if (locc == null || locc.lat == "" || locc.@long == "")
                                    {
                                        //string a = objdata.Lat;
                                        //string b = objdata.Long;

                                        string a = x.lat;
                                        string b = x.@long;

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }
                                    else
                                    {

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }


                                    db.Qr_Location.Add(new Qr_Location()
                                    {
                                        empId = x.userId,
                                        lat = x.lat,
                                        @long = x.@long,
                                        datetime = x.datetime,
                                        address = addr,
                                        area = ar,
                                        batteryStatus = batteryStatus,
                                        Distnace = Convert.ToDecimal(distCount),

                                    });
                                    db.SaveChanges();

                                    DateTime newTimeh = DateTime.Now;
                                    DateTime oldTimeh;
                                    TimeSpan spanh = TimeSpan.Zero;
                                    var hd = db.HouseMasters.Where(c => c.houseLat != null && c.houseLong != null && EntityFunctions.TruncateTime(c.modified) == EntityFunctions.TruncateTime(newTimeh)).OrderByDescending(c => c.houseId).FirstOrDefault();
                                    if (hd != null)
                                    {
                                        oldTimeh = hd.modified.Value;
                                        spanh = newTimeh.Subtract(oldTimeh);
                                    }

                                    if (spanh.Minutes >= 9)
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "2";
                                    }


                                    DateTime newTimed = DateTime.Now;
                                    DateTime oldTimed;
                                    TimeSpan spand = TimeSpan.Zero;
                                    var dy = db.DumpYardDetails.Where(c => c.dyLat != null && c.dyLong != null && EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(newTimed)).OrderByDescending(c => c.dyId).FirstOrDefault();
                                    if (dy != null)
                                    {
                                        oldTimed = dy.lastModifiedDate.Value;
                                        spand = newTimed.Subtract(oldTimed);
                                    }

                                    if (spand.Minutes >= 9)
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "2";
                                    }


                                    DateTime newTimes = DateTime.Now;
                                    DateTime oldTimes;
                                    TimeSpan spans = TimeSpan.Zero;
                                    var st = db.StreetSweepingDetails.Where(c => c.SSLat != null && c.SSLong != null && EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(newTimes)).OrderByDescending(c => c.SSId).FirstOrDefault();
                                    if (st != null)
                                    {
                                        oldTimes = st.lastModifiedDate.Value;
                                        spans = newTimes.Subtract(oldTimes);
                                    }

                                    if (spans.Minutes >= 9)
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "2";
                                    }


                                    DateTime newTimel = DateTime.Now;
                                    DateTime oldTimel;
                                    TimeSpan spanl = TimeSpan.Zero;
                                    var lw = db.LiquidWasteDetails.Where(c => c.LWLat != null && c.LWLong != null && EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(newTimel)).OrderByDescending(c => c.LWId).FirstOrDefault();
                                    if (lw != null)
                                    {
                                        oldTimel = lw.lastModifiedDate.Value;
                                        spanl = newTimel.Subtract(oldTimel);
                                    }

                                    if (spanl.Minutes >= 9)
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "2";
                                    }

                                    if (hd == null && dy == null && st == null && lw == null)
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "0";
                                    }
                                    if ((spanl.Minutes <= 9 && lw != null) || (spans.Minutes <= 9 && st != null) || (spand.Minutes <= 9 && dy != null) || (spanh.Minutes <= 9 && hd != null))
                                    {
                                        var app = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
                                        app.FAQ = "1";
                                    }
                                    dbMain.SaveChanges();
                                    //List<AppDetail> AppDetailss = dbMain.Database.SqlQuery<AppDetail>("exec [Update_Trigger]").ToList();
                                }
                            }

                            result.Add(new SyncResult()
                            {
                                ID = Convert.ToInt32(x.OfflineId),
                                status = "success",
                                message = "Uploaded successfully",
                                messageMar = "सबमिट यशस्वी",
                            });
                        }
                    }
                    return result;
                }
                catch
                {
                    throw;
                    List<SyncResult> objres = new List<SyncResult>();
                    objres.Add(new SyncResult()
                    {
                        ID = 0,
                        status = "error",
                        messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                        message = "Something is wrong,Try Again.. ",
                    });


                    return objres;
                }

            }

        }

        public List<SyncResult> SaveUserLocationSA(List<SBUserLocation> obj, int AppId, string batteryStatus, int typeId, string EmpType)
        {
       
            using (DevSwachhBharatMainEntities db = new DevSwachhBharatMainEntities())
            {
                try
                {
                    List<SyncResult> result = new List<SyncResult>();
                    var distCount = "";

                
                        foreach (var x in obj)
                        {
                            DateTime Dateeee = Convert.ToDateTime(x.datetime);
                            DateTime newTime = Dateeee;
                            DateTime oldTime;
                            TimeSpan span = TimeSpan.Zero;
                            var gcd = db.UR_Location.Where(c => c.empId == x.userId && c.type == null && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.locId).FirstOrDefault();
                            if (gcd != null)
                            {
                                oldTime = gcd.datetime.Value;
                                span = newTime.Subtract(oldTime);
                            }

                            if (gcd == null || span.Minutes >= 9)
                        {

                            var u = db.EmployeeMasters.Where(c => c.EmpId == x.userId && c.isActive == true);
                            DateTime Offlinedate = Convert.ToDateTime(x.datetime);
                             

                                var atten = db.HSUR_Daily_Attendance.Where(c => c.userId == x.userId & c.daDate == EntityFunctions.TruncateTime(Offlinedate)).FirstOrDefault();

                                if (atten == null)
                                {
                                    result.Add(new SyncResult()
                                    {
                                        ID = Convert.ToInt32(x.OfflineId),
                                        isAttendenceOff = true,
                                        status = "error",
                                        message = "Your duty is currently off, please start again.. ",
                                        messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..",
                                    });

                                    //return result;
                                    continue;
                                }

                                if (u != null & x.userId > 0)
                                {
                                    string addr = "", ar = "";
                                    addr = Address(x.lat + "," + x.@long);
                                    if (addr != "")
                                    {
                                        ar = area(addr);
                                    }


                                  
                                    var locc = db.SP_UserLatLongDetail(x.userId, typeId).FirstOrDefault();

                                    if (locc == null || locc.lat == "" || locc.@long == "")
                                    {
                                        

                                        string a = x.lat;
                                        string b = x.@long;

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }
                                    else
                                    {

                                        var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(x.lat), Convert.ToDouble(x.@long)).FirstOrDefault();
                                        distCount = dist.Distance_in_KM.ToString();
                                    }

                                

                                    db.UR_Location.Add(new UR_Location()
                                    {
                                        empId = x.userId,
                                        lat = x.lat,
                                        @long = x.@long,
                                        datetime = x.datetime,
                                        address = addr,
                                        area = ar,
                                        batteryStatus = batteryStatus,
                                        Distnace = Convert.ToDecimal(distCount),
                                        CreatedDate = DateTime.Now,
                                        type = null,
                                    });
                                    db.SaveChanges();
                                }
                            }

                            result.Add(new SyncResult()
                            {
                                ID = Convert.ToInt32(x.OfflineId),
                                status = "success",
                                message = "Uploaded successfully",
                                messageMar = "सबमिट यशस्वी",
                            });


                        }

               
                   
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                    List<SyncResult> objres = new List<SyncResult>();
                    objres.Add(new SyncResult()
                    {
                        ID = 0,
                        status = "error",
                        messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                        message = "Something is wrong,Try Again.. ",
                    });


                    return objres;
                }

            }

        }

        public Result SaveUserAttendence(SBUserAttendence obj, int AppId, int type, string batteryStatus)
        {
            Result result = new Result();
            if (obj.EmpType == "N")
            {
                result = SaveUserAttendenceForNormal(obj, AppId, type, batteryStatus);
            }
            if (obj.EmpType == "L")
            {
                result = SaveUserAttendenceForLiquid(obj, AppId, type, batteryStatus);
            }
            if (obj.EmpType == "S")
            {
                result = SaveUserAttendenceForStreet(obj, AppId, type, batteryStatus);
            }
            if (obj.EmpType == "D")
            {
                result = SaveUserAttendenceForDump(obj, AppId, type, batteryStatus);
            }
           
            return result;

        }


        public Result SaveUserAttendenceForNormal(SBUserAttendence obj, int AppId, int type, string batteryStatus)
        {
            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var user = db.UserMasters.Where(c => c.userId == obj.userId && c.EmployeeType == null).FirstOrDefault();

                if (type == 0)
                {
                    if (user.isActive == true)
                    {
                        //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        Daily_Attendance data = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == null || c.endTime == "") && c.EmployeeType == null).FirstOrDefault();
                        if (data != null)
                        {
                            data.endTime = obj.startTime;
                            data.daEndDate = obj.daDate;
                            data.endLat = obj.startLat;
                            data.endLong = obj.startLong;
                            data.batteryStatus = batteryStatus;
                            data.totalKm = obj.totalKm;
                            data.EmployeeType = null;
                            db.SaveChanges();
                        }
                        try
                        {
                            Daily_Attendance objdata = new Daily_Attendance();

                            objdata.userId = obj.userId;
                            objdata.daDate = obj.daDate;
                            objdata.endLat = "";
                            objdata.startLat = obj.startLat;
                            objdata.startLong = obj.startLong;
                            objdata.startTime = obj.startTime;
                            objdata.endTime = "";
                            objdata.vtId = obj.vtId;
                            obj.vehicleNumber = obj.vehicleNumber;
                            objdata.daStartNote = obj.daStartNote;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.vehicleNumber = obj.vehicleNumber;
                            //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = null;
                            db.Daily_Attendance.Add(objdata);
                            string Time2 = obj.startTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = sdate;
                            loc.lat = obj.startLat;
                            loc.@long = obj.startLong;
                            loc.batteryStatus = batteryStatus;
                            loc.EmployeeType = null;
                            loc.address = Address(obj.startLat + "," + obj.startLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift started Successfully";
                            result.messageMar = "शिफ्ट सुरू";
                            result.emptype = "N";
                            return result;
                        }

                        catch
                        {

                            result.status = "error";
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            result.emptype = "N";
                            return result;
                        }
                    }

                    result.status = "error";
                    result.message = "Please contact your administrator.. ";
                    result.messageMar = "कृपया आपल्या ऍडमिनिस्ट्रेटरशी संपर्क साधा..";
                    result.emptype = "N";
                    return result;

                }
                else
                {

                    try
                    {
                        Daily_Attendance objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == null).FirstOrDefault();
                        if (objdata != null)
                        {

                            objdata.userId = obj.userId;
                            objdata.daEndDate = obj.daDate;
                            objdata.endLat = obj.endLat;
                            objdata.endLong = obj.endLong;
                            objdata.endTime = obj.endTime;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = null;
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            string Time2 = obj.endTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = edate;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.batteryStatus = batteryStatus;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            loc.EmployeeType = null;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "N";
                            return result;
                        }
                        else
                        {
                            Daily_Attendance objdata2 = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == null).OrderByDescending(c => c.daID).FirstOrDefault();
                            objdata2.userId = obj.userId;
                            objdata2.daEndDate = DateTime.Now;
                            objdata2.endLat = obj.endLat;
                            objdata2.endLong = obj.endLong;
                            objdata2.endTime = obj.endTime;
                            objdata2.daEndNote = obj.daEndNote;
                            objdata2.batteryStatus = batteryStatus;
                            objdata2.EmployeeType = null;
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.EmployeeType = null;
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "N";
                            return result;
                        }
                    }
                    catch
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        result.emptype = "N";
                        return result;
                    }
                }
            }


        }

        public Result SaveUserAttendenceForLiquid(SBUserAttendence obj, int AppId, int type, string batteryStatus)
        {
            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var user = db.UserMasters.Where(c => c.userId == obj.userId && c.EmployeeType == "L").FirstOrDefault();

                if (type == 0)
                {
                    if (user.isActive == true)
                    {
                        //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        Daily_Attendance data = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == null || c.endTime == "") && c.EmployeeType == "L").FirstOrDefault();
                        if (data != null)
                        {
                            data.endTime = obj.startTime;
                            data.daEndDate = obj.daDate;
                            data.endLat = obj.startLat;
                            data.endLong = obj.startLong;
                            data.batteryStatus = batteryStatus;
                            data.totalKm = obj.totalKm;
                            data.EmployeeType = "L";
                            db.SaveChanges();
                        }
                        try
                        {
                            Daily_Attendance objdata = new Daily_Attendance();

                            objdata.userId = obj.userId;
                            objdata.daDate = obj.daDate;
                            objdata.endLat = "";
                            objdata.startLat = obj.startLat;
                            objdata.startLong = obj.startLong;
                            objdata.startTime = obj.startTime;
                            objdata.endTime = "";
                            objdata.vtId = obj.vtId;
                            obj.vehicleNumber = obj.vehicleNumber;
                            objdata.daStartNote = obj.daStartNote;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.vehicleNumber = obj.vehicleNumber;
                            //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "L";
                            db.Daily_Attendance.Add(objdata);
                            string Time2 = obj.startTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = sdate;
                            loc.lat = obj.startLat;
                            loc.@long = obj.startLong;
                            loc.batteryStatus = batteryStatus;
                            loc.EmployeeType = "L";
                            loc.address = Address(obj.startLat + "," + obj.startLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift started Successfully";
                            result.messageMar = "शिफ्ट सुरू";
                            result.emptype = "L";
                            return result;
                        }

                        catch (Exception ex)
                        {

                            result.status = "error";
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            result.emptype = "L";
                            return result;
                        }
                    }

                    result.status = "error";
                    result.message = "Please contact your administrator.. ";
                    result.messageMar = "कृपया आपल्या ऍडमिनिस्ट्रेटरशी संपर्क साधा..";
                    result.emptype = "L";
                    return result;

                }
                else
                {

                    try
                    {
                        Daily_Attendance objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "L").FirstOrDefault();
                        if (objdata != null)
                        {

                            objdata.userId = obj.userId;
                            objdata.daEndDate = obj.daDate;
                            objdata.endLat = obj.endLat;
                            objdata.endLong = obj.endLong;
                            objdata.endTime = obj.endTime;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "L";
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            string Time2 = obj.endTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = edate;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.batteryStatus = batteryStatus;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            loc.EmployeeType = "L";
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "L";
                            return result;
                        }
                        else
                        {
                            Daily_Attendance objdata2 = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "L").OrderByDescending(c => c.daID).FirstOrDefault();
                            objdata2.userId = obj.userId;
                            objdata2.daEndDate = DateTime.Now;
                            objdata2.endLat = obj.endLat;
                            objdata2.endLong = obj.endLong;
                            objdata2.endTime = obj.endTime;
                            objdata2.daEndNote = obj.daEndNote;
                            objdata2.batteryStatus = batteryStatus;
                            objdata2.EmployeeType = "L";
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.EmployeeType = "L";
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "L";
                            return result;
                        }
                    }
                    catch
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        result.emptype = "L";
                        return result;
                    }
                }
            }

        }

        public Result SaveUserAttendenceForStreet(SBUserAttendence obj, int AppId, int type, string batteryStatus)
        {
            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var user = db.UserMasters.Where(c => c.userId == obj.userId && c.EmployeeType == "S").FirstOrDefault();

                if (type == 0)
                {
                    if (user.isActive == true)
                    {
                        //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        Daily_Attendance data = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == null || c.endTime == "") && c.EmployeeType == "S").FirstOrDefault();
                        if (data != null)
                        {
                            data.endTime = obj.startTime;
                            data.daEndDate = obj.daDate;
                            data.endLat = obj.startLat;
                            data.endLong = obj.startLong;
                            data.batteryStatus = batteryStatus;
                            data.totalKm = obj.totalKm;
                            data.EmployeeType = "S";
                            db.SaveChanges();
                        }
                        try
                        {
                            Daily_Attendance objdata = new Daily_Attendance();

                            objdata.userId = obj.userId;
                            objdata.daDate = obj.daDate;
                            objdata.endLat = "";
                            objdata.startLat = obj.startLat;
                            objdata.startLong = obj.startLong;
                            objdata.startTime = obj.startTime;
                            objdata.endTime = "";
                            objdata.vtId = obj.vtId;
                            obj.vehicleNumber = obj.vehicleNumber;
                            objdata.daStartNote = obj.daStartNote;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.vehicleNumber = obj.vehicleNumber;
                            //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "S";
                            db.Daily_Attendance.Add(objdata);
                            string Time2 = obj.startTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = sdate;
                            loc.lat = obj.startLat;
                            loc.@long = obj.startLong;
                            loc.batteryStatus = batteryStatus;
                            loc.EmployeeType = "S";
                            loc.address = Address(obj.startLat + "," + obj.startLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift started Successfully";
                            result.messageMar = "शिफ्ट सुरू";
                            result.emptype = "S";
                            return result;
                        }

                        catch
                        {

                            result.status = "error";
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            result.emptype = "S";
                            return result;
                        }
                    }

                    result.status = "error";
                    result.message = "Please contact your administrator.. ";
                    result.messageMar = "कृपया आपल्या ऍडमिनिस्ट्रेटरशी संपर्क साधा..";
                    result.emptype = "S";
                    return result;

                }
                else
                {

                    try
                    {
                        Daily_Attendance objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "S").FirstOrDefault();
                        if (objdata != null)
                        {

                            objdata.userId = obj.userId;
                            objdata.daEndDate = obj.daDate;
                            objdata.endLat = obj.endLat;
                            objdata.endLong = obj.endLong;
                            objdata.endTime = obj.endTime;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "S";
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            string Time2 = obj.endTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = edate;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.batteryStatus = batteryStatus;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            loc.EmployeeType = "L";
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "S";
                            return result;
                        }
                        else
                        {
                            Daily_Attendance objdata2 = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "S").OrderByDescending(c => c.daID).FirstOrDefault();
                            objdata2.userId = obj.userId;
                            objdata2.daEndDate = DateTime.Now;
                            objdata2.endLat = obj.endLat;
                            objdata2.endLong = obj.endLong;
                            objdata2.endTime = obj.endTime;
                            objdata2.daEndNote = obj.daEndNote;
                            objdata2.batteryStatus = batteryStatus;
                            objdata2.EmployeeType = "S";
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.EmployeeType = "S";
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "S";
                            return result;
                        }
                    }
                    catch
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        result.emptype = "S";
                        return result;
                    }
                }
            }

        }

        public Result SaveUserAttendenceForDump(SBUserAttendence obj, int AppId, int type, string batteryStatus)
        {
            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var user = db.UserMasters.Where(c => c.userId == obj.userId && c.EmployeeType == "D").FirstOrDefault();
                var dy= db.DumpYardDetails.Where(x => x.ReferanceId == obj.ReferanceId).FirstOrDefault();
              
                if (type == 0)
                {
                    if (user.isActive == true)
                    {
                        //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        Daily_Attendance data = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == null || c.endTime == "") && c.EmployeeType == "D").FirstOrDefault();
                        if (data != null)
                        {
                            data.endTime = obj.startTime;
                            data.daEndDate = obj.daDate;
                            data.endLat = obj.startLat;
                            data.endLong = obj.startLong;
                            data.batteryStatus = batteryStatus;
                            data.totalKm = obj.totalKm;
                            data.EmployeeType = "D";
                            if(dy!=null)
                            {
                                data.dyid = dy.dyId;
                            }
                            db.SaveChanges();
                        }
                        try
                        {
                            Daily_Attendance objdata = new Daily_Attendance();

                            objdata.userId = obj.userId;
                            objdata.daDate = obj.daDate;
                            objdata.endLat = "";
                            objdata.startLat = obj.startLat;
                            objdata.startLong = obj.startLong;
                            objdata.startTime = obj.startTime;
                            objdata.endTime = "";
                            objdata.vtId = obj.vtId;
                            obj.vehicleNumber = obj.vehicleNumber;
                            objdata.daStartNote = obj.daStartNote;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.vehicleNumber = obj.vehicleNumber;
                            //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "D";
                            if (dy != null)
                            {
                                objdata.dyid = dy.dyId;
                            }
                            db.Daily_Attendance.Add(objdata);
                            string Time2 = obj.startTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = sdate;
                            loc.lat = obj.startLat;
                            loc.@long = obj.startLong;
                            loc.batteryStatus = batteryStatus;
                            loc.EmployeeType = "D";
                            loc.address = Address(obj.startLat + "," + obj.startLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift started Successfully";
                            result.messageMar = "शिफ्ट सुरू";
                            result.emptype = "D";
                            return result;
                        }

                        catch
                        {

                            result.status = "error";
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            result.emptype = "D";
                            return result;
                        }
                    }

                    result.status = "error";
                    result.message = "Please contact your administrator.. ";
                    result.messageMar = "कृपया आपल्या ऍडमिनिस्ट्रेटरशी संपर्क साधा..";
                    result.emptype = "D";
                    return result;

                }
                else
                {

                    try
                    {
                        Daily_Attendance objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "D").FirstOrDefault();
                        if (objdata != null)
                        {

                            objdata.userId = obj.userId;
                            objdata.daEndDate = obj.daDate;
                            objdata.endLat = obj.endLat;
                            objdata.endLong = obj.endLong;
                            objdata.endTime = obj.endTime;
                            objdata.daEndNote = obj.daEndNote;
                            objdata.batteryStatus = batteryStatus;
                            objdata.totalKm = obj.totalKm;
                            objdata.EmployeeType = "D";
                            if (dy != null)
                            {
                                objdata.dyid = dy.dyId;
                            }
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            string Time2 = obj.endTime;
                            DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                            string t2 = date2.ToString("hh:mm:ss tt");
                            string dt2 = Convert.ToDateTime(obj.daDate).ToString("MM/dd/yyyy");
                            DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = edate;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.batteryStatus = batteryStatus;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.CreatedDate = DateTime.Now;
                            loc.EmployeeType = "D";
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "D";
                            return result;
                        }
                        else
                        {
                            Daily_Attendance objdata2 = db.Daily_Attendance.Where(c => c.userId == obj.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "D").OrderByDescending(c => c.daID).FirstOrDefault();
                            objdata2.userId = obj.userId;
                            objdata2.daEndDate = DateTime.Now;
                            objdata2.endLat = obj.endLat;
                            objdata2.endLong = obj.endLong;
                            objdata2.endTime = obj.endTime;
                            objdata2.daEndNote = obj.daEndNote;
                            objdata2.batteryStatus = batteryStatus;
                            objdata2.EmployeeType = "D";
                            if (dy != null)
                            {
                                objdata2.dyid = dy.dyId;
                            }
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);
                            Location loc = new Location();
                            loc.userId = obj.userId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.EmployeeType = "D";
                            loc.CreatedDate = DateTime.Now;
                            db.Locations.Add(loc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            result.isAttendenceOff = true;
                            result.emptype = "D";
                            return result;
                        }
                    }
                    catch
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        result.emptype = "D";
                        return result;
                    }
                }
            }


        }

      

        //public List<SyncResult> SaveUserAttendence(List<SBUserAttendence> obj, int AppId, int type, string batteryStatus)
        //{
        //    List<SyncResult> result = new List<SyncResult>();
        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        foreach (var x in obj)
        //        {
        //            if (type == 0)
        //            {
        //                var user = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault();

        //                if (user.isActive == true)
        //                {

        //                    Daily_Attendance data = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
        //                    if (data != null)
        //                    {
        //                        data.endTime = x.startTime;
        //                        data.daEndDate = x.daDate;
        //                        data.endLat = x.startLat;
        //                        data.endLong = x.startLong;
        //                        data.batteryStatus = batteryStatus;
        //                        data.totalKm = x.totalKm;
        //                        db.SaveChanges();
        //                    }
        //                    try
        //                    {
        //                        Daily_Attendance objdata = new Daily_Attendance();

        //                        objdata.userId = x.userId;
        //                        objdata.daDate = x.daDate;
        //                        objdata.endLat = "";
        //                        objdata.startLat = x.startLat;
        //                        objdata.startLong = x.startLong;
        //                        objdata.startTime = x.startTime;
        //                        objdata.endTime = "";
        //                        objdata.vtId = x.vtId;
        //                        x.vehicleNumber = x.vehicleNumber;
        //                        objdata.daStartNote = x.daStartNote;
        //                        objdata.daEndNote = x.daEndNote;
        //                        objdata.vehicleNumber = x.vehicleNumber;
        //                        //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
        //                        objdata.batteryStatus = batteryStatus;
        //                        objdata.totalKm = x.totalKm;
        //                        db.Daily_Attendance.Add(objdata);
        //                        db.SaveChanges();

        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = x.OfflineID,
        //                            status = "success",
        //                            message = "Shift started Successfully",
        //                            messageMar = "शिफ्ट सुरू",
        //                        });

        //                    }

        //                    catch
        //                    {
        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = x.OfflineID,
        //                            status = "error",
        //                            message = "Something is wrong,Try Again.. ",
        //                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
        //                        });

        //                    }
        //                }
        //                else {

        //                    result.Add(new SyncResult()
        //                    {
        //                        ID = x.OfflineID,
        //                        status = "error",
        //                        message = "Something went wrong, Please Contact Your Administrator ",
        //                        messageMar = "काहीतरी चुकीचे आहे",
        //                    });

        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    Daily_Attendance objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(x.daDate) && c.userId == x.userId && (c.endTime == "" || c.endTime == null)).FirstOrDefault();
        //                    if (objdata != null)
        //                    {
        //                        objdata.userId = x.userId;
        //                        objdata.daEndDate = x.daDate;
        //                        objdata.endLat = x.endLat;
        //                        objdata.endLong = x.endLong;
        //                        objdata.endTime = x.endTime;
        //                        objdata.daEndNote = x.daEndNote;
        //                        objdata.batteryStatus = batteryStatus;
        //                        objdata.totalKm = x.totalKm;

        //                        string Time2 = x.endTime;
        //                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
        //                        string t2 = date2.ToString("hh:mm:ss tt");
        //                        string dt2 = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy");
        //                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);
        //                        Location loc = new Location();
        //                        loc.userId = x.userId;
        //                        loc.datetime = edate;
        //                        loc.lat = x.endLat;
        //                        loc.@long = x.endLong;
        //                        loc.batteryStatus = batteryStatus;
        //                        loc.address = Address(x.endLat + "," + x.endLong);
        //                        if (loc.address != "")
        //                        { loc.area = area(loc.address); }
        //                        else
        //                        {
        //                            loc.area = "";
        //                        }
        //                        db.Locations.Add(loc);
        //                        db.SaveChanges();

        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = x.OfflineID,
        //                            status = "success",
        //                            message = "Shift ended successfully",
        //                            messageMar = "शिफ्ट संपले",
        //                        });

        //                    }
        //                    else
        //                    {
        //                        Daily_Attendance objdata2 = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.endTime == "" || c.endTime == null)).OrderByDescending(c => c.daID).FirstOrDefault();
        //                        objdata2.userId = x.userId;
        //                        objdata2.daEndDate = DateTime.Now;
        //                        objdata2.endLat = x.endLat;
        //                        objdata2.endLong = x.endLong;
        //                        objdata2.endTime = x.endTime;
        //                        objdata2.daEndNote = x.daEndNote;
        //                        objdata2.batteryStatus = batteryStatus;
        //                        //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);
        //                        Location loc = new Location();
        //                        loc.userId = x.userId;
        //                        loc.datetime = DateTime.Now;
        //                        loc.lat = x.endLat;
        //                        loc.@long = x.endLong;
        //                        loc.address = Address(x.endLat + "," + x.endLong);
        //                        if (loc.address != "")
        //                        { loc.area = area(loc.address); }
        //                        else
        //                        {
        //                            loc.area = "";
        //                        }
        //                        db.Locations.Add(loc);
        //                        db.SaveChanges();

        //                        result.Add(new SyncResult()
        //                        {
        //                            ID = x.OfflineID,
        //                            status = "success",
        //                            message = "Shift ended successfully",
        //                            messageMar = "शिफ्ट संपले",
        //                        });

        //                    }
        //                    return result;
        //                }
        //                catch
        //                {
        //                    result.Add(new SyncResult()
        //                    {
        //                        ID = x.OfflineID,
        //                        status = "error",
        //                        message = "Something is wrong,Try Again.. ",
        //                        messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
        //                    });
        //                    return result;
        //                }

        //            }

        //        }
        //    }

        //    return result;
        //}

        public List<SyncResult1> SaveUserAttendenceOffline(List<SBUserAttendence> obj, int AppId, string cdate, string EmpType)
        {

            List<SyncResult1> result = new List<SyncResult1>();
            double aaaa = distance(21.142180, 79.067540, 21.138420, 79.069210, 'k');

            if (EmpType == "N")
            {
                result = SaveUserAttendenceOfflineForNormal(obj, AppId, cdate, EmpType);
            }
            if (EmpType == "L")
            {
                result = SaveUserAttendenceOfflineForLiquid(obj, AppId, cdate, EmpType);
            }
            if (EmpType == "S")
            {
                result = SaveUserAttendenceOfflineForStreet(obj, AppId, cdate, EmpType);
            }

            if (EmpType == "D")
            {
                result = SaveUserAttendenceOfflineForDump(obj, AppId, cdate, EmpType);
            }
            return result;


        }

        public List<SyncResult1> SaveUserAttendenceOfflineForNormal(List<SBUserAttendence> obj, int AppId, string cdate, string EmpType)
        {
            List<SyncResult1> result = new List<SyncResult1>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                Daily_Attendance attendance = new Daily_Attendance();

                foreach (var x in obj)
                {

                    DateTime Datee = Convert.ToDateTime(cdate);
                    var IsSameRecordLocation = db.Locations.Where(c => c.userId == x.userId && c.datetime == Datee && c.type == null && c.EmployeeType == null).FirstOrDefault();
                    try
                    {
                        bool _IsInSync = false, _IsOutSync = false;
                        var user = db.UserMasters.Where(c => c.userId == x.userId && c.EmployeeType == null).FirstOrDefault();

                        if (user.isActive == true)
                        {

                            var IsSameRecord = db.Daily_Attendance.Where(
                                   c => c.userId == x.userId &&
                                   //c.startLat == x.startLat &&
                                   //c.startLong == x.startLong &&
                                   //c.endLat == x.endLat &&
                                   //c.endLong == x.endLong &&
                                   c.startTime == x.startTime &&
                                   c.endTime == x.endTime &&
                                   c.daDate == EntityFunctions.TruncateTime(x.daDate) &&
                                   c.daEndDate == EntityFunctions.TruncateTime(x.daEndDate) && c.EmployeeType == null
                                 ).FirstOrDefault();

                            if (IsSameRecord == null)
                            {

                                var objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(x.daDate) && c.userId == x.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == null).FirstOrDefault();
                                if (objdata != null && x.endTime == null)
                                {
                                    objdata.endTime = x.startTime;
                                    objdata.daEndDate = x.daDate;
                                    objdata.endLat = x.startLat;
                                    objdata.endLong = x.startLong;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    objdata.totalKm = x.totalKm;
                                    objdata.EmployeeType = null;
                                    db.SaveChanges();
                                }
                                if (objdata != null)
                                {

                                    objdata.userId = x.userId;
                                    objdata.startLat = x.startLat;
                                    objdata.startLong = x.startLong;
                                    objdata.startTime = x.startTime;
                                    objdata.daDate = x.daDate;
                                    objdata.vehicleNumber = x.vehicleNumber;
                                    objdata.vtId = x.vtId;
                                    objdata.EmployeeType = null;
                                    //objdata.daEndDate = x.daEndDate;

                                    if (x.daEndDate.Equals(DateTime.MinValue))
                                    {
                                        objdata.daEndDate = null;
                                        objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                    }
                                    else
                                    {
                                        //objdata.daEndDate = x.daEndDate;
                                        if (x.daEndDate == x.daDate)
                                        {
                                            objdata.daEndDate = x.daEndDate;
                                            objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            objdata.daEndDate = x.daDate;
                                            objdata.endTime = "11:50 PM";
                                        }
                                    }

                                    objdata.endLat = x.endLat;
                                    objdata.endLong = x.endLong;
                                    //objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                    objdata.daStartNote = x.daStartNote;
                                    objdata.daEndNote = x.daEndNote;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    //  objdata.batteryStatus = x.batteryStatus;
                                    if (objdata != null && x.endTime == null)
                                    {
                                        db.Daily_Attendance.Add(objdata);
                                    }
                                    _IsInSync = true;

                                    if (x.endLat != null && x.endLong != null && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = null;
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                        _IsInSync = false;
                                    }

                                    db.SaveChanges();
                                }
                                else
                                {
                                    var OutTime = db.Daily_Attendance.Where(a => a.userId == x.userId && a.startTime == x.startTime && a.daDate == x.daDate && a.EmployeeType == null).OrderByDescending(m => m.daID).FirstOrDefault();

                                    if (OutTime != null && OutTime.endTime == "11:50 PM")
                                    {
                                        OutTime.userId = x.userId;
                                        OutTime.startLat = x.startLat;
                                        OutTime.startLong = x.startLong;
                                        OutTime.startTime = x.startTime;
                                        OutTime.daDate = x.daDate;
                                        OutTime.vehicleNumber = x.vehicleNumber;
                                        OutTime.vtId = x.vtId;
                                        OutTime.EmployeeType = null;
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            OutTime.daEndDate = null;
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                OutTime.daEndDate = x.daEndDate;
                                                OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                            }
                                            else
                                            {
                                                OutTime.daEndDate = x.daDate;
                                                OutTime.endTime = "11:50 PM";
                                            }

                                        }

                                        OutTime.endLat = x.endLat;
                                        OutTime.endLong = x.endLong;
                                        //OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        OutTime.daStartNote = x.daStartNote;
                                        OutTime.daEndNote = x.daEndNote;
                                        OutTime.batteryStatus = x.batteryStatus;

                                        //db.Daily_Attendance.Add(attendance);
                                        _IsInSync = true;

                                    }
                                    else
                                    {
                                        attendance.userId = x.userId;
                                        attendance.startLat = x.startLat;
                                        attendance.startLong = x.startLong;
                                        attendance.startTime = x.startTime;
                                        attendance.daDate = x.daDate;
                                        attendance.vehicleNumber = x.vehicleNumber;
                                        attendance.vtId = x.vtId;
                                        attendance.EmployeeType = null;
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            attendance.daEndDate = null;
                                            attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                attendance.daEndDate = x.daEndDate;
                                                attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);

                                            }
                                            else
                                            {
                                                attendance.daEndDate = x.daDate;
                                                attendance.endTime = "11:50 PM";
                                            }
                                        }

                                        attendance.endLat = x.endLat;
                                        attendance.endLong = x.endLong;
                                        //attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        attendance.daStartNote = x.daStartNote;
                                        attendance.daEndNote = x.daEndNote;
                                        attendance.batteryStatus = x.batteryStatus;
                                        if (OutTime != null)
                                        {
                                            if (OutTime.endTime == "" || OutTime.endTime == null)
                                            {
                                                db.Daily_Attendance.Add(attendance);
                                            }
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        if (OutTime == null)
                                        {
                                            db.Daily_Attendance.Add(attendance);
                                        }
                                        _IsInSync = true;
                                        //  db.SaveChanges();
                                        //if ((!string.IsNullOrEmpty(x.endLat))  && (!string.IsNullOrEmpty(x.endLong)))
                                        //{
                                        //    string Time2 = x.endTime;
                                        //    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        //    string t2 = date2.ToString("hh:mm:ss tt");
                                        //    string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        //    DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        //    Location loc = new Location();
                                        //    loc.userId = x.userId;
                                        //    loc.datetime = edate;
                                        //    loc.lat = x.endLat;
                                        //    loc.@long = x.endLong;
                                        //    loc.batteryStatus = x.batteryStatus;
                                        //    loc.address = Address(x.endLat + "," + x.endLong);
                                        //    if (loc.address != "")
                                        //    {
                                        //        loc.area = area(loc.address);
                                        //    }
                                        //    else
                                        //    {
                                        //        loc.area = "";
                                        //    }

                                        //    loc.IsOffline = true;
                                        //    loc.CreatedDate = DateTime.Now;

                                        //    db.Locations.Add(loc);
                                        //    _IsOutSync = true;
                                        //}

                                        //db.SaveChanges();

                                    }
                                    if ((!string.IsNullOrEmpty(x.endLat)) && (!string.IsNullOrEmpty(x.endLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = null;
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                    }

                                    if ((!string.IsNullOrEmpty(x.startLat)) && (!string.IsNullOrEmpty(x.startLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.startTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy");
                                        DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = sdate;
                                        loc.lat = x.startLat;
                                        loc.@long = x.startLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.startLat + "," + x.startLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = null;
                                        db.Locations.Add(loc);
                                        _IsOutSync = false;
                                    }
                                    db.SaveChanges();
                                }

                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = _IsInSync,
                                    IsOutSync = _IsOutSync,
                                    EmpType = "N",

                                });
                            }
                            else
                            {
                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = true,
                                    IsOutSync = true,
                                    EmpType = "N",
                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Add(new SyncResult1()
                        {
                            ID = x.OfflineID,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                            IsInSync = false,
                            IsOutSync = false,
                            EmpType = "N",
                        });
                        return result;
                    }
                }

            }
            return result;
        }

        public List<SyncResult1> SaveUserAttendenceOfflineForLiquid(List<SBUserAttendence> obj, int AppId, string cdate, string EmpType)
        {
            List<SyncResult1> result = new List<SyncResult1>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                Daily_Attendance attendance = new Daily_Attendance();

                foreach (var x in obj)
                {

                    DateTime Datee = Convert.ToDateTime(cdate);
                    var IsSameRecordLocation = db.Locations.Where(c => c.userId == x.userId && c.datetime == Datee && c.type == null && c.EmployeeType == "L").FirstOrDefault();
                    try
                    {
                        bool _IsInSync = false, _IsOutSync = false;
                        var user = db.UserMasters.Where(c => c.userId == x.userId && c.EmployeeType == "L").FirstOrDefault();

                        if (user.isActive == true)
                        {

                            var IsSameRecord = db.Daily_Attendance.Where(
                                   c => c.userId == x.userId &&
                                   //c.startLat == x.startLat &&
                                   //c.startLong == x.startLong &&
                                   //c.endLat == x.endLat &&
                                   //c.endLong == x.endLong &&
                                   c.startTime == x.startTime &&
                                   c.endTime == x.endTime &&
                                   c.daDate == EntityFunctions.TruncateTime(x.daDate) &&
                                   c.daEndDate == EntityFunctions.TruncateTime(x.daEndDate) && c.EmployeeType == "L"
                                 ).FirstOrDefault();

                            if (IsSameRecord == null)
                            {

                                var objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(x.daDate) && c.userId == x.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "L").FirstOrDefault();
                                if (objdata != null && x.endTime == null)
                                {
                                    objdata.endTime = x.startTime;
                                    objdata.daEndDate = x.daDate;
                                    objdata.endLat = x.startLat;
                                    objdata.endLong = x.startLong;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    objdata.totalKm = x.totalKm;
                                    objdata.EmployeeType = "L";
                                    db.SaveChanges();
                                }
                                if (objdata != null)
                                {

                                    objdata.userId = x.userId;
                                    objdata.startLat = x.startLat;
                                    objdata.startLong = x.startLong;
                                    objdata.startTime = x.startTime;
                                    objdata.daDate = x.daDate;
                                    objdata.vehicleNumber = x.vehicleNumber;
                                    objdata.vtId = x.vtId;
                                    objdata.EmployeeType = "L";
                                    //objdata.daEndDate = x.daEndDate;

                                    if (x.daEndDate.Equals(DateTime.MinValue))
                                    {
                                        objdata.daEndDate = null;
                                        objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                    }
                                    else
                                    {
                                        //objdata.daEndDate = x.daEndDate;
                                        if (x.daEndDate == x.daDate)
                                        {
                                            objdata.daEndDate = x.daEndDate;
                                            objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            objdata.daEndDate = x.daDate;
                                            objdata.endTime = "11:50 PM";
                                        }
                                    }

                                    objdata.endLat = x.endLat;
                                    objdata.endLong = x.endLong;
                                    //objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                    objdata.daStartNote = x.daStartNote;
                                    objdata.daEndNote = x.daEndNote;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    //  objdata.batteryStatus = x.batteryStatus;
                                    if (objdata != null && x.endTime == null)
                                    {
                                        db.Daily_Attendance.Add(objdata);
                                    }
                                    _IsInSync = true;

                                    if (x.endLat != null && x.endLong != null && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "L";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                        _IsInSync = false;
                                    }

                                    db.SaveChanges();
                                }
                                else
                                {
                                    var OutTime = db.Daily_Attendance.Where(a => a.userId == x.userId && a.startTime == x.startTime && a.daDate == x.daDate && a.EmployeeType == "L").OrderByDescending(m => m.daID).FirstOrDefault();

                                    if (OutTime != null && OutTime.endTime == "11:50 PM")
                                    {
                                        OutTime.userId = x.userId;
                                        OutTime.startLat = x.startLat;
                                        OutTime.startLong = x.startLong;
                                        OutTime.startTime = x.startTime;
                                        OutTime.daDate = x.daDate;
                                        OutTime.vehicleNumber = x.vehicleNumber;
                                        OutTime.vtId = x.vtId;
                                        OutTime.EmployeeType = "L";
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            OutTime.daEndDate = null;
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                OutTime.daEndDate = x.daEndDate;
                                                OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                            }
                                            else
                                            {
                                                OutTime.daEndDate = x.daDate;
                                                OutTime.endTime = "11:50 PM";
                                            }

                                        }

                                        OutTime.endLat = x.endLat;
                                        OutTime.endLong = x.endLong;
                                        //OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        OutTime.daStartNote = x.daStartNote;
                                        OutTime.daEndNote = x.daEndNote;
                                        OutTime.batteryStatus = x.batteryStatus;

                                        //db.Daily_Attendance.Add(attendance);
                                        _IsInSync = true;

                                    }
                                    else
                                    {
                                        attendance.userId = x.userId;
                                        attendance.startLat = x.startLat;
                                        attendance.startLong = x.startLong;
                                        attendance.startTime = x.startTime;
                                        attendance.daDate = x.daDate;
                                        attendance.vehicleNumber = x.vehicleNumber;
                                        attendance.vtId = x.vtId;
                                        attendance.EmployeeType = "L";
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            attendance.daEndDate = null;
                                            attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                attendance.daEndDate = x.daEndDate;
                                                attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);

                                            }
                                            else
                                            {
                                                attendance.daEndDate = x.daDate;
                                                attendance.endTime = "11:50 PM";
                                            }
                                        }

                                        attendance.endLat = x.endLat;
                                        attendance.endLong = x.endLong;
                                        //attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        attendance.daStartNote = x.daStartNote;
                                        attendance.daEndNote = x.daEndNote;
                                        attendance.batteryStatus = x.batteryStatus;
                                        if (OutTime != null)
                                        {
                                            if (OutTime.endTime == "" || OutTime.endTime == null)
                                            {
                                                db.Daily_Attendance.Add(attendance);
                                            }
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        if (OutTime == null)
                                        {
                                            db.Daily_Attendance.Add(attendance);
                                        }
                                        _IsInSync = true;
                                        //  db.SaveChanges();
                                        //if ((!string.IsNullOrEmpty(x.endLat))  && (!string.IsNullOrEmpty(x.endLong)))
                                        //{
                                        //    string Time2 = x.endTime;
                                        //    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        //    string t2 = date2.ToString("hh:mm:ss tt");
                                        //    string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        //    DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        //    Location loc = new Location();
                                        //    loc.userId = x.userId;
                                        //    loc.datetime = edate;
                                        //    loc.lat = x.endLat;
                                        //    loc.@long = x.endLong;
                                        //    loc.batteryStatus = x.batteryStatus;
                                        //    loc.address = Address(x.endLat + "," + x.endLong);
                                        //    if (loc.address != "")
                                        //    {
                                        //        loc.area = area(loc.address);
                                        //    }
                                        //    else
                                        //    {
                                        //        loc.area = "";
                                        //    }

                                        //    loc.IsOffline = true;
                                        //    loc.CreatedDate = DateTime.Now;

                                        //    db.Locations.Add(loc);
                                        //    _IsOutSync = true;
                                        //}

                                        //db.SaveChanges();

                                    }
                                    if ((!string.IsNullOrEmpty(x.endLat)) && (!string.IsNullOrEmpty(x.endLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "L";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                    }

                                    if ((!string.IsNullOrEmpty(x.startLat)) && (!string.IsNullOrEmpty(x.startLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.startTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy");
                                        DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = sdate;
                                        loc.lat = x.startLat;
                                        loc.@long = x.startLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.startLat + "," + x.startLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "L";
                                        db.Locations.Add(loc);
                                        _IsOutSync = false;
                                    }
                                    db.SaveChanges();
                                }

                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = _IsInSync,
                                    IsOutSync = _IsOutSync,
                                    EmpType = "L",

                                });
                            }
                            else
                            {
                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = true,
                                    IsOutSync = true,
                                    EmpType = "L",
                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Add(new SyncResult1()
                        {
                            ID = x.OfflineID,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                            IsInSync = false,
                            IsOutSync = false,
                            EmpType = "L",
                        });
                        return result;
                    }
                }

            }
            return result;
        }

        public List<SyncResult1> SaveUserAttendenceOfflineForStreet(List<SBUserAttendence> obj, int AppId, string cdate, string EmpType)
        {
            List<SyncResult1> result = new List<SyncResult1>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                Daily_Attendance attendance = new Daily_Attendance();

                foreach (var x in obj)
                {

                    DateTime Datee = Convert.ToDateTime(cdate);
                    var IsSameRecordLocation = db.Locations.Where(c => c.userId == x.userId && c.datetime == Datee && c.type == null && c.EmployeeType == "S").FirstOrDefault();
                    try
                    {
                        bool _IsInSync = false, _IsOutSync = false;
                        var user = db.UserMasters.Where(c => c.userId == x.userId && c.EmployeeType == "S").FirstOrDefault();

                        if (user.isActive == true)
                        {

                            var IsSameRecord = db.Daily_Attendance.Where(
                                   c => c.userId == x.userId &&
                                   //c.startLat == x.startLat &&
                                   //c.startLong == x.startLong &&
                                   //c.endLat == x.endLat &&
                                   //c.endLong == x.endLong &&
                                   c.startTime == x.startTime &&
                                   c.endTime == x.endTime &&
                                   c.daDate == EntityFunctions.TruncateTime(x.daDate) &&
                                   c.daEndDate == EntityFunctions.TruncateTime(x.daEndDate) && c.EmployeeType == "S"
                                 ).FirstOrDefault();

                            if (IsSameRecord == null)
                            {

                                var objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(x.daDate) && c.userId == x.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "S").FirstOrDefault();
                                if (objdata != null && x.endTime == null)
                                {
                                    objdata.endTime = x.startTime;
                                    objdata.daEndDate = x.daDate;
                                    objdata.endLat = x.startLat;
                                    objdata.endLong = x.startLong;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    objdata.totalKm = x.totalKm;
                                    objdata.EmployeeType = "S";
                                    db.SaveChanges();
                                }
                                if (objdata != null)
                                {

                                    objdata.userId = x.userId;
                                    objdata.startLat = x.startLat;
                                    objdata.startLong = x.startLong;
                                    objdata.startTime = x.startTime;
                                    objdata.daDate = x.daDate;
                                    objdata.vehicleNumber = x.vehicleNumber;
                                    objdata.vtId = x.vtId;
                                    objdata.EmployeeType = "S";
                                    //objdata.daEndDate = x.daEndDate;

                                    if (x.daEndDate.Equals(DateTime.MinValue))
                                    {
                                        objdata.daEndDate = null;
                                        objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                    }
                                    else
                                    {
                                        //objdata.daEndDate = x.daEndDate;
                                        if (x.daEndDate == x.daDate)
                                        {
                                            objdata.daEndDate = x.daEndDate;
                                            objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            objdata.daEndDate = x.daDate;
                                            objdata.endTime = "11:50 PM";
                                        }
                                    }

                                    objdata.endLat = x.endLat;
                                    objdata.endLong = x.endLong;
                                    //objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                    objdata.daStartNote = x.daStartNote;
                                    objdata.daEndNote = x.daEndNote;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    //  objdata.batteryStatus = x.batteryStatus;
                                    if (objdata != null && x.endTime == null)
                                    {
                                        db.Daily_Attendance.Add(objdata);
                                    }
                                    _IsInSync = true;

                                    if (x.endLat != null && x.endLong != null && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "S";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                        _IsInSync = false;
                                    }

                                    db.SaveChanges();
                                }
                                else
                                {
                                    var OutTime = db.Daily_Attendance.Where(a => a.userId == x.userId && a.startTime == x.startTime && a.daDate == x.daDate && a.EmployeeType == "S").OrderByDescending(m => m.daID).FirstOrDefault();

                                    if (OutTime != null && OutTime.endTime == "11:50 PM")
                                    {
                                        OutTime.userId = x.userId;
                                        OutTime.startLat = x.startLat;
                                        OutTime.startLong = x.startLong;
                                        OutTime.startTime = x.startTime;
                                        OutTime.daDate = x.daDate;
                                        OutTime.vehicleNumber = x.vehicleNumber;
                                        OutTime.vtId = x.vtId;
                                        OutTime.EmployeeType = "S";
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            OutTime.daEndDate = null;
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                OutTime.daEndDate = x.daEndDate;
                                                OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                            }
                                            else
                                            {
                                                OutTime.daEndDate = x.daDate;
                                                OutTime.endTime = "11:50 PM";
                                            }

                                        }

                                        OutTime.endLat = x.endLat;
                                        OutTime.endLong = x.endLong;
                                        //OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        OutTime.daStartNote = x.daStartNote;
                                        OutTime.daEndNote = x.daEndNote;
                                        OutTime.batteryStatus = x.batteryStatus;

                                        //db.Daily_Attendance.Add(attendance);
                                        _IsInSync = true;

                                    }
                                    else
                                    {
                                        attendance.userId = x.userId;
                                        attendance.startLat = x.startLat;
                                        attendance.startLong = x.startLong;
                                        attendance.startTime = x.startTime;
                                        attendance.daDate = x.daDate;
                                        attendance.vehicleNumber = x.vehicleNumber;
                                        attendance.vtId = x.vtId;
                                        attendance.EmployeeType = "S";
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            attendance.daEndDate = null;
                                            attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                attendance.daEndDate = x.daEndDate;
                                                attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);

                                            }
                                            else
                                            {
                                                attendance.daEndDate = x.daDate;
                                                attendance.endTime = "11:50 PM";
                                            }
                                        }

                                        attendance.endLat = x.endLat;
                                        attendance.endLong = x.endLong;
                                        //attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        attendance.daStartNote = x.daStartNote;
                                        attendance.daEndNote = x.daEndNote;
                                        attendance.batteryStatus = x.batteryStatus;
                                        if (OutTime != null)
                                        {
                                            if (OutTime.endTime == "" || OutTime.endTime == null)
                                            {
                                                db.Daily_Attendance.Add(attendance);
                                            }
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        if (OutTime == null)
                                        {
                                            db.Daily_Attendance.Add(attendance);
                                        }
                                        _IsInSync = true;
                                        //  db.SaveChanges();
                                        //if ((!string.IsNullOrEmpty(x.endLat))  && (!string.IsNullOrEmpty(x.endLong)))
                                        //{
                                        //    string Time2 = x.endTime;
                                        //    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        //    string t2 = date2.ToString("hh:mm:ss tt");
                                        //    string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        //    DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        //    Location loc = new Location();
                                        //    loc.userId = x.userId;
                                        //    loc.datetime = edate;
                                        //    loc.lat = x.endLat;
                                        //    loc.@long = x.endLong;
                                        //    loc.batteryStatus = x.batteryStatus;
                                        //    loc.address = Address(x.endLat + "," + x.endLong);
                                        //    if (loc.address != "")
                                        //    {
                                        //        loc.area = area(loc.address);
                                        //    }
                                        //    else
                                        //    {
                                        //        loc.area = "";
                                        //    }

                                        //    loc.IsOffline = true;
                                        //    loc.CreatedDate = DateTime.Now;

                                        //    db.Locations.Add(loc);
                                        //    _IsOutSync = true;
                                        //}

                                        //db.SaveChanges();

                                    }
                                    if ((!string.IsNullOrEmpty(x.endLat)) && (!string.IsNullOrEmpty(x.endLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "S";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                    }

                                    if ((!string.IsNullOrEmpty(x.startLat)) && (!string.IsNullOrEmpty(x.startLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.startTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy");
                                        DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = sdate;
                                        loc.lat = x.startLat;
                                        loc.@long = x.startLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.startLat + "," + x.startLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "S";
                                        db.Locations.Add(loc);
                                        _IsOutSync = false;
                                    }
                                    db.SaveChanges();
                                }

                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = _IsInSync,
                                    IsOutSync = _IsOutSync,
                                    EmpType = "S",

                                });
                            }
                            else
                            {
                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = true,
                                    IsOutSync = true,
                                    EmpType = "S",
                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Add(new SyncResult1()
                        {
                            ID = x.OfflineID,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                            IsInSync = false,
                            IsOutSync = false,
                            EmpType = "S",
                        });
                        return result;
                    }
                }

            }
            return result;
        }

        public List<SyncResult1> SaveUserAttendenceOfflineForDump(List<SBUserAttendence> obj, int AppId, string cdate, string EmpType)
        {
            List<SyncResult1> result = new List<SyncResult1>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                Daily_Attendance attendance = new Daily_Attendance();
            
                foreach (var x in obj)
                {
                    var dy = db.DumpYardDetails.Where(c => c.ReferanceId == x.ReferanceId).FirstOrDefault();
                    DateTime Datee = Convert.ToDateTime(cdate);
                    var IsSameRecordLocation = db.Locations.Where(c => c.userId == x.userId && c.datetime == Datee && c.type == null && c.EmployeeType == "D").FirstOrDefault();
                    try
                    {
                        bool _IsInSync = false, _IsOutSync = false;
                        var user = db.UserMasters.Where(c => c.userId == x.userId && c.EmployeeType == "D").FirstOrDefault();

                        if (user.isActive == true)
                        {

                            var IsSameRecord = db.Daily_Attendance.Where(
                                   c => c.userId == x.userId &&
                                   //c.startLat == x.startLat &&
                                   //c.startLong == x.startLong &&
                                   //c.endLat == x.endLat &&
                                   //c.endLong == x.endLong &&
                                   c.startTime == x.startTime &&
                                   c.endTime == x.endTime &&
                                   c.daDate == EntityFunctions.TruncateTime(x.daDate) &&
                                   c.daEndDate == EntityFunctions.TruncateTime(x.daEndDate) && c.EmployeeType == "D"
                                 ).FirstOrDefault();

                            if (IsSameRecord == null)
                            {

                                var objdata = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(x.daDate) && c.userId == x.userId && (c.endTime == "" || c.endTime == null) && c.EmployeeType == "D").FirstOrDefault();
                                if (objdata != null && x.endTime == null)
                                {
                                    objdata.endTime = x.startTime;
                                    objdata.daEndDate = x.daDate;
                                    objdata.endLat = x.startLat;
                                    objdata.endLong = x.startLong;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    objdata.totalKm = x.totalKm;
                                    if(dy!=null)
                                    { 
                                    objdata.dyid = dy.dyId;
                                    }
                                    objdata.EmployeeType = "D";
                                    db.SaveChanges();
                                }
                                if (objdata != null)
                                {

                                    objdata.userId = x.userId;
                                    objdata.startLat = x.startLat;
                                    objdata.startLong = x.startLong;
                                    objdata.startTime = x.startTime;
                                    objdata.daDate = x.daDate;
                                    objdata.vehicleNumber = x.vehicleNumber;
                                    objdata.vtId = x.vtId;
                                    if (dy != null)
                                    {
                                        objdata.dyid = dy.dyId;
                                    }
                                    objdata.EmployeeType = "D";
                                    //objdata.daEndDate = x.daEndDate;

                                    if (x.daEndDate.Equals(DateTime.MinValue))
                                    {
                                        objdata.daEndDate = null;
                                        objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                    }
                                    else
                                    {
                                        //objdata.daEndDate = x.daEndDate;
                                        if (x.daEndDate == x.daDate)
                                        {
                                            objdata.daEndDate = x.daEndDate;
                                            objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            objdata.daEndDate = x.daDate;
                                            objdata.endTime = "11:50 PM";
                                        }
                                    }

                                    objdata.endLat = x.endLat;
                                    objdata.endLong = x.endLong;
                                    //objdata.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                    objdata.daStartNote = x.daStartNote;
                                    objdata.daEndNote = x.daEndNote;
                                    objdata.OutbatteryStatus = x.batteryStatus;
                                    //  objdata.batteryStatus = x.batteryStatus;
                                    if (objdata != null && x.endTime == null)
                                    {
                                        db.Daily_Attendance.Add(objdata);
                                    }
                                    _IsInSync = true;

                                    if (x.endLat != null && x.endLong != null && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "D";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                        _IsInSync = false;
                                    }

                                    db.SaveChanges();
                                }
                                else
                                {
                                    var OutTime = db.Daily_Attendance.Where(a => a.userId == x.userId && a.startTime == x.startTime && a.daDate == x.daDate && a.EmployeeType == "D").OrderByDescending(m => m.daID).FirstOrDefault();

                                    if (OutTime != null && OutTime.endTime == "11:50 PM")
                                    {
                                        OutTime.userId = x.userId;
                                        OutTime.startLat = x.startLat;
                                        OutTime.startLong = x.startLong;
                                        OutTime.startTime = x.startTime;
                                        OutTime.daDate = x.daDate;
                                        OutTime.vehicleNumber = x.vehicleNumber;
                                        OutTime.vtId = x.vtId;
                                        OutTime.EmployeeType = "D";
                                        if (dy != null)
                                        {
                                            OutTime.dyid = dy.dyId;
                                        }
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            OutTime.daEndDate = null;
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                OutTime.daEndDate = x.daEndDate;
                                                OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                            }
                                            else
                                            {
                                                OutTime.daEndDate = x.daDate;
                                                OutTime.endTime = "11:50 PM";
                                            }

                                        }

                                        OutTime.endLat = x.endLat;
                                        OutTime.endLong = x.endLong;
                                        //OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        OutTime.daStartNote = x.daStartNote;
                                        OutTime.daEndNote = x.daEndNote;
                                        OutTime.batteryStatus = x.batteryStatus;

                                        //db.Daily_Attendance.Add(attendance);
                                        _IsInSync = true;

                                    }
                                    else
                                    {
                                        attendance.userId = x.userId;
                                        attendance.startLat = x.startLat;
                                        attendance.startLong = x.startLong;
                                        attendance.startTime = x.startTime;
                                        attendance.daDate = x.daDate;
                                        attendance.vehicleNumber = x.vehicleNumber;
                                        attendance.vtId = x.vtId;
                                        attendance.EmployeeType = "D";
                                        if (x.daEndDate.Equals(DateTime.MinValue))
                                        {
                                            attendance.daEndDate = null;
                                            attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        else
                                        {
                                            if (x.daEndDate == x.daDate)
                                            {
                                                attendance.daEndDate = x.daEndDate;
                                                attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);

                                            }
                                            else
                                            {
                                                attendance.daEndDate = x.daDate;
                                                attendance.endTime = "11:50 PM";
                                            }
                                        }

                                        attendance.endLat = x.endLat;
                                        attendance.endLong = x.endLong;
                                        //attendance.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime); //x.endTime;
                                        attendance.daStartNote = x.daStartNote;
                                        attendance.daEndNote = x.daEndNote;
                                        attendance.batteryStatus = x.batteryStatus;
                                        if (OutTime != null)
                                        {
                                            if (OutTime.endTime == "" || OutTime.endTime == null)
                                            {
                                                db.Daily_Attendance.Add(attendance);
                                            }
                                            OutTime.endTime = (string.IsNullOrEmpty(x.endTime) ? "" : x.endTime);
                                        }
                                        if (dy != null)
                                        {
                                            attendance.dyid = dy.dyId;
                                        }
                                        if (OutTime == null)
                                        {
                                            db.Daily_Attendance.Add(attendance);
                                        }
                                        _IsInSync = true;
                                      
                                    }
                                    if ((!string.IsNullOrEmpty(x.endLat)) && (!string.IsNullOrEmpty(x.endLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.endTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daEndDate).ToString("MM/dd/yyyy");
                                        DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = edate;
                                        loc.lat = x.endLat;
                                        loc.@long = x.endLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.endLat + "," + x.endLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "D";
                                        db.Locations.Add(loc);
                                        _IsOutSync = true;
                                    }

                                    if ((!string.IsNullOrEmpty(x.startLat)) && (!string.IsNullOrEmpty(x.startLong)) && IsSameRecordLocation == null)
                                    {
                                        string Time2 = x.startTime;
                                        DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                        string t2 = date2.ToString("hh:mm:ss tt");
                                        string dt2 = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy");
                                        DateTime? sdate = Convert.ToDateTime(dt2 + " " + t2);

                                        Location loc = new Location();
                                        loc.userId = x.userId;
                                        loc.datetime = sdate;
                                        loc.lat = x.startLat;
                                        loc.@long = x.startLong;
                                        loc.batteryStatus = x.batteryStatus;
                                        loc.address = Address(x.startLat + "," + x.startLong);
                                        if (loc.address != "")
                                        {
                                            loc.area = area(loc.address);
                                        }
                                        else
                                        {
                                            loc.area = "";
                                        }

                                        loc.IsOffline = true;
                                        loc.CreatedDate = DateTime.Now;
                                        loc.EmployeeType = "D";
                                        db.Locations.Add(loc);
                                        _IsOutSync = false;
                                    }
                                    db.SaveChanges();
                                }

                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = _IsInSync,
                                    IsOutSync = _IsOutSync,
                                    EmpType = "D",

                                });
                            }
                            else
                            {
                                result.Add(new SyncResult1()
                                {
                                    ID = x.OfflineID,
                                    status = "success",
                                    message = "Shift started Successfully",
                                    messageMar = "शिफ्ट सुरू",
                                    IsInSync = true,
                                    IsOutSync = true,
                                    EmpType = "D",
                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Add(new SyncResult1()
                        {
                            ID = x.OfflineID,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                            IsInSync = false,
                            IsOutSync = false,
                            EmpType = "D",
                        });
                        return result;
                    }
                }

            }
            return result;
        }

        //public void SaveAttendenceSettingsDetail(int AppID, string hour)
        //{

        //        using (var db = new DevSwachhBharatNagpurEntities(AppID))
        //        {
        //        List<SBAAttendenceSettingsGridRow> datalist = new List<SBAAttendenceSettingsGridRow>();


        //        var data1 = db.sp_MsgNotification("01:07 PM").ToList();    
        //        foreach (var data in data1)
        //        {

        //            string mes = "कचरा संकलन कर्मचारी ''" + data.userNameMar + "'' आज ड्युटी वर गैरहजर आहे";
        //            string housemob = "8830635095";
        //            sendSMSmar(mes, housemob);

        //        }
        //        data1.Clear();
        //    }
        //}


        public List<SBAAttendenceSettingsGridRow> SaveAttendenceSettingsDetail(int AppId, string hour)
        {


            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                List<SBAAttendenceSettingsGridRow> datalist = new List<SBAAttendenceSettingsGridRow>();


                var data1 = db.sp_MsgNotification(hour).ToList();
                foreach (var data in data1)
                {
                    string mes = "कचरा संकलन कर्मचारी ''" + data.userNameMar + "'' आज ड्युटी वर गैरहजर आहे";
                    string housemob = "9422783030,8830635095,7385888068,9420870617,8605551059,9423684600";
                    sendSMSmar(mes, housemob);
                }
                return datalist;
            }



        }


        public CollectionResult SaveGarbageCollection(SBGarbageCollectionView obj, int AppId, int type, string batteryStatus)
        {

            int locType = 0;
            string mes = string.Empty;
            CollectionResult result = new CollectionResult();
            HouseMaster dbHouse = new HouseMaster();

            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                string name = "", housemob = "", nameMar = "", addre = "";

                var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId).FirstOrDefault();
                bool IsExist = false;
                DateTime startDateTime = DateTime.Today; //Today at 00:00:00
                DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
                var distCount = "";
                try
                {
                    GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                    objdata.userId = obj.userId;
                    objdata.gcDate = DateTime.Now;
                    objdata.Lat = obj.Lat;
                    objdata.Long = obj.Long;
                    var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

                    Location loc = new Location();
                    var locc = db.SP_UserLatLongDetail(objdata.userId, 0).FirstOrDefault();

                    if (locc == null || locc.lat == "" || locc.@long == "")
                    {
                        string a = objdata.Lat;
                        string b = objdata.Long;

                        //string a = loc.lat;
                        //string b = loc.@long;

                        var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
                        distCount = dist.Distance_in_KM.ToString();
                    }
                    else
                    {

                        var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
                        distCount = dist.Distance_in_KM.ToString();
                    }



                    if (atten == null)
                    {
                        result.isAttendenceOff = true;
                        result.message = "Your duty is currently off, please start again.. ";
                        result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                        result.name = "";
                        result.status = "success";
                        return result;
                    }
                    else { result.isAttendenceOff = false; }
                    if (obj.houseId != null && obj.houseId != "")
                    {
                        try
                        {
                            locType = 1;
                            //var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId).FirstOrDefault();
                            objdata.houseId = house.houseId;
                            // objdata.gpId = 0;
                            name = house.houseOwner;
                            nameMar = checkNull(house.houseOwnerMar);
                            addre = checkNull(house.houseAddress);
                            housemob = house.houseOwnerMobile;

                            IsExist = (from p in db.GarbageCollectionDetails where p.houseId == objdata.houseId && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;

                        }
                        catch (Exception ex)
                        {
                            result.message = "Invalid houseId"; result.messageMar = "अवैध घर आयडी"; result.name = "";
                            result.status = "error";
                            return result;
                        }

                    }
                    if (obj.gpId != null && obj.gpId != "")
                    {
                        try
                        {
                            locType = 2;
                            var gpdetails = db.GarbagePointDetails.Where(c => c.ReferanceId == obj.gpId).FirstOrDefault();
                            objdata.gpId = gpdetails.gpId;
                            // objdata.houseId = 0;
                            name = gpdetails.gpName;
                            nameMar = checkNull(gpdetails.gpNameMar);
                            housemob = "";
                            // housemob = house.houseOwnerMobile;
                            addre = checkNull(gpdetails.gpAddress);

                            //IsExist = (from p in db.GarbageCollectionDetails where p.gpId == objdata.gpId && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;
                        }
                        catch
                        {
                            result.message = "Invalid gpId"; result.messageMar = "अवैध जीपी आयडी"; result.name = "";
                            result.status = "error";
                            return result;
                        }

                    }


                    if (IsExist == true)
                    {
                        ///////////////Temperary Added By Nishikant on dated 12-06-2019 ////////////
                        //#region Temperary Added
                        var dbLocalHouse = db.HouseMasters.Where(c => c.houseId == house.houseId).FirstOrDefault();
                        if (dbLocalHouse.houseLong == null && dbLocalHouse.houseLong == null)
                        {
                            dbLocalHouse.houseLat = obj.Lat;
                            dbLocalHouse.houseLong = obj.Long;
                        }
                        //#endregion
                        /////////////////////////////////////////////////////////

                        //var gcd = db.GarbageCollectionDetails.Where(c => c.houseId == house.houseId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                        //gcd.gcType = obj.gcType;
                        //gcd.gpBeforImage = obj.gpBeforImage;
                        //gcd.gpAfterImage = obj.gpAfterImage;
                        //gcd.note = checkNull(obj.note);
                        //gcd.garbageType = checkIntNull(obj.garbageType.ToString());
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        //gcd.vehicleNumber = checkNull(obj.vehicleNumber);
                        //loc.Distnace = Convert.ToDecimal(distCount);
                        //gcd.batteryStatus = batteryStatus;
                        //gcd.userId = obj.userId;
                        //gcd.gcDate = DateTime.Now;
                        //gcd.Lat = obj.Lat;
                        //gcd.Long = obj.Long;

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}
                        //db.Entry(gcd).State = System.Data.Entity.EntityState.Modified;
                        //db.SaveChanges();
                        //db.GarbageCollectionDetails.Add(objdata);

                        objdata.locAddresss = addre;

                        // Location loc = new Location();
                        loc.datetime = DateTime.Now;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;

                        if (!string.IsNullOrEmpty(obj.houseId))
                        {
                            loc.ReferanceID = obj.houseId;
                        }
                        else if (!string.IsNullOrEmpty(obj.gpId))
                        {
                            loc.ReferanceID = obj.gpId;
                        }

                        loc.CreatedDate = DateTime.Now;

                        db.Locations.Add(loc);
                        // db.Entry(objdata).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        result.name = name;
                        result.nameMar = nameMar;
                        result.mobile = housemob;
                    }
                    else
                    {
                        ///////////////Temperary Added By Nishikant on dated 12-06-2019 ////////////
                        #region Temperary Added

                        //var dbLocalHouse = db.HouseMasters.Where(c => c.houseId == house.houseId).FirstOrDefault();
                        //dbLocalHouse.houseLat = obj.Lat;
                        //dbLocalHouse.houseLong = obj.Long;

                        #endregion
                        /////////////////////////////////////////////////////////


                        //  Temperary Added

                        if (house != null)
                        {
                            if (house.houseLat == null && house.houseLong == null)
                            {
                                house.houseLat = obj.Lat;
                                house.houseLong = obj.Long;
                            }
                        }

                        objdata.gcType = obj.gcType;
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        loc.Distnace = Convert.ToDecimal(distCount);
                        objdata.batteryStatus = batteryStatus;
                        objdata.userId = obj.userId;


                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        //db.HouseMasters.Add(house);
                        db.GarbageCollectionDetails.Add(objdata);

                        // Location loc = new Location();
                        loc.datetime = DateTime.Now;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;

                        if (!string.IsNullOrEmpty(obj.houseId))
                        {
                            loc.ReferanceID = obj.houseId;
                        }
                        else if (!string.IsNullOrEmpty(obj.gpId))
                        {
                            loc.ReferanceID = obj.gpId;
                        }

                        loc.CreatedDate = DateTime.Now;

                        db.Locations.Add(loc);
                        db.SaveChanges();
                        result.name = name;
                        result.nameMar = nameMar;
                        result.mobile = housemob;
                    }

                    result.status = "success";
                    result.message = "Uploaded successfully";
                    result.messageMar = "सबमिट यशस्वी";
                    if (appdetails.AppId == 1003 || appdetails.AppId == 1006)
                    {
                        result.messageMar = "सबमिट यशस्वी";
                    }

                    else
                    {
                        //   objdata.garbageType = 0;
                        if (objdata.garbageType == 3 && objdata.houseId != null)
                        {
                            switch (appdetails.LanguageId)
                            {
                                case 1:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;

                                case 2:
                                    mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    break;

                                case 3:
                                    mes = "नमस्कार! आपके घर से कचरा एकत्र किया जाता है। गीले और सूखे के रूप में वर्गीकृत कचरा सफाई कर्मचारियों को सौंपने में सहायता के लिए धन्यवाद। आपकी सेवा में " + appdetails.AppName_mar + " " + appdetails.yoccContact;
                                    break;

                                default:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;
                            }




                            if (house != null)
                            {
                                List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
                                {
                                    //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                }
                                else if (housemob != "")
                                {
                                    sendSMS(mes, housemob);
                                }
                            }

                        }

                        if (objdata.garbageType == 0)
                        {

                            switch (appdetails.LanguageId)
                            {
                                case 1:
                                    mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";
                                    break;

                                case 2:
                                    mes = "नमस्कार! आपल्या घरातून आज ओला व सुका कचरा मिश्र स्वरूपात संकलित करण्यात आलेला आहे. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    break;

                                case 3:
                                    mes = "नमस्कार! आज, हमारे घरों से मिश्रित रूप में नम और सूखा कचरा एकत्र किया गया है। आपसे अनुरोध है कि प्रतिदिन नम कचरे की सफाई और निपटान में सहायता करें और सफाई कर्मचारियों को सौंप दें। " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                    break;

                                default:
                                    mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";
                                    break;
                            }



                            if (house != null)
                            {
                                List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
                                {
                                    //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                }
                                else if (housemob != "")
                                {
                                    sendSMS(mes, housemob);
                                }
                            }

                        }

                        if (objdata.garbageType == 1)
                        {

                            switch (appdetails.LanguageId)
                            {
                                case 1:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;

                                case 2:
                                    mes = "नमस्कार! आपल्या घरातून आज ओला व सुका असा विघटित केलेला कचरा संकलित करण्यात आलेला आहे. आपण केलेल्या सहयोगाबद्दल धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    break;

                                case 3:
                                    mes = "नमस्कार! आज, हमारे घर से कचरा एकत्र किया जाता है, जो गीला और सूखा होता है। आपके सहयोग के लिए धन्यवाद।" + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                    break;

                                default:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;
                            }


                            if (house != null)
                            {
                                List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
                                {
                                    //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                }
                                else if (housemob != "")
                                {
                                    sendSMS(mes, housemob);
                                }
                            }

                        }

                        if (objdata.garbageType == 2)
                        {
                            switch (appdetails.LanguageId)
                            {
                                case 1:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Received Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;

                                case 2:
                                    mes = "नमस्कार! आपल्या घरातून आज कोणत्याही प्रकारचा कचरा सफाई कर्मचाऱ्यास देण्यात आलेला नाही. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    break;

                                case 3:
                                    mes = "नमस्कार! आज आपके घर में कोई भी कचरा उपलब्ध नहीं कराया गया है। आपसे अनुरोध है कि प्रतिदिन गीला और सूखा कचरे की सफाई और निपटान में सहायता करें और सफाई कर्मचारियों को सौंप दें।" + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                    break;

                                default:
                                    mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    break;
                            }

                            if (house != null)
                            {
                                List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                if (ArrayList.Count > 0)
                                {
                                    PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                }
                                else if (housemob != "")
                                {
                                    sendSMS(mes, housemob);
                                }
                            }

                        }
                    }
                    return result;
                }

                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.name = "";
                    result.status = "error";
                    return result;
                }
            }


        }

        private List<String> DeviceDetailsFCM(string houseId, int AppId)
        {
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var FCM = db.DeviceDetails.Where(x => x.ReferenceID == houseId & x.FCMID != null).ToList();

                List<String> ArrayList = new List<String>();

                foreach (var x in FCM)
                {
                    ArrayList.Add(x.FCMID);
                }

                return ArrayList;
            }

        }

        private CollectionSyncResult SaveHouseCollectionSync(SBGarbageCollectionView obj, int AppId, int type)
        {
            int locType = 0;
            string mes = string.Empty;
            CollectionSyncResult result = new CollectionSyncResult();
            HouseMaster dbHouse = new HouseMaster();



            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                string name = "", housemob = "", nameMar = "", addre = "";

                var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId).FirstOrDefault();
                bool IsExist = false;
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime startDateTime = new DateTime(Dateeee.Year, Dateeee.Month, Dateeee.Day, 00, 00, 00, 000);
                DateTime endDateTime = new DateTime(Dateeee.Year, Dateeee.Month, Dateeee.Day, 23, 59, 59, 999);
                var IsSameHouseRecord = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.houseId == house.houseId && c.gcDate == Dateeee).FirstOrDefault();
                if (IsSameHouseRecord == null)
                {

                    try
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;
                        //    objdata.garbageType = obj.garbageType;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee) ).FirstOrDefault();

                        Location loc = new Location();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.ID = obj.OfflineID;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }
                        if (obj.houseId != null && obj.houseId != "")
                        {
                            try
                            {
                                locType = 1;
                                objdata.houseId = house.houseId;
                                name = house.houseOwner;
                                nameMar = checkNull(house.houseOwnerMar);
                                addre = checkNull(house.houseAddress);
                                housemob = house.houseOwnerMobile;


                                IsExist = (from p in db.GarbageCollectionDetails where p.houseId == objdata.houseId && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;
                                //if (obj.wastetype == "DW")
                                //{
                                //   
                                //    IsExist = (from p in db.GarbageCollectionDetails where p.houseId == objdata.houseId && p.WasteType == "DW" && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;
                                //}
                                //if (obj.wastetype=="WW")
                                //{
                                //    
                                //    IsExist = (from p in db.GarbageCollectionDetails where p.houseId == objdata.houseId && p.WasteType=="WW" && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;
                                //}                            

                            }
                            catch (Exception ex)
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid houseId"; result.messageMar = "अवैध घर आयडी";
                                result.status = "error";
                                return result;
                            }

                        }


                        if (IsExist == true)
                        {

                            var gcd = db.GarbageCollectionDetails.Where(c => c.houseId == house.houseId && c.userId == obj.userId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();
                            if (gcd == null)
                            {
                                result.ID = obj.OfflineID;
                                result.message = "This house id already scanned."; result.messageMar = "हे घर आयडी आधीच स्कॅन केले आहे.";
                                result.status = "error";
                                return result;
                            }
                            if (gcd != null)
                            {
                                if (Dateeee > gcd.gcDate)
                                {
                                    gcd.gcType = obj.gcType;
                                    gcd.gpBeforImage = obj.gpBeforImage;
                                    gcd.gpAfterImage = obj.gpAfterImage;
                                    gcd.note = checkNull(obj.note);
                                    gcd.garbageType = checkIntNull(obj.garbageType.ToString());
                                    objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                                    gcd.vehicleNumber = checkNull(obj.vehicleNumber);

                                    gcd.batteryStatus = obj.batteryStatus;
                                    gcd.userId = obj.userId;
                                    gcd.gcDate = Dateeee;
                                    gcd.Lat = obj.Lat;
                                    gcd.Long = obj.Long;

                                    //gcd.Lat = house.houseLat;
                                    //gcd.Long = house.houseLong;
                                }


                                //if (AppId == 1003 || AppId == 1010)
                                //{
                                //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                                //}
                                //else
                                //{
                                //    objdata.locAddresss = addre;
                                //}

                                gcd.locAddresss = addre;
                                gcd.CreatedDate = DateTime.Now;

                                //var LocationContext = db.Locations.Where(c => c.datetime == Dateeee && c.userId == obj.userId).FirstOrDefault();

                                //LocationContext.datetime = Dateeee;
                                //LocationContext.lat = objdata.Lat;
                                //LocationContext.@long = objdata.Long;
                                //LocationContext.address = objdata.locAddresss;
                                //LocationContext.batteryStatus = obj.batteryStatus; 
                                //if (objdata.locAddresss != "")
                                //{ LocationContext.area = area(objdata.locAddresss); }
                                //else
                                //{
                                //    LocationContext.area = "";
                                //}
                                //LocationContext.userId = objdata.userId;
                                //LocationContext.type = 1;
                                //LocationContext.Distnace = obj.Distance; //Convert.ToDecimal(distCount);
                                //LocationContext.IsOffline = true;
                                //LocationContext.ReferanceID = obj.houseId;
                                //LocationContext.CreatedDate = DateTime.Now;

                                loc.datetime = Dateeee;
                                loc.lat = objdata.Lat;
                                loc.@long = objdata.Long;
                                loc.address = objdata.locAddresss; //Address(objdata.Lat + "," + objdata.Long);
                                loc.batteryStatus = obj.batteryStatus;
                                if (objdata.locAddresss != "")
                                { loc.area = area(loc.address); }
                                else
                                {
                                    loc.area = "";
                                }
                                loc.userId = objdata.userId;
                                loc.type = 1;
                                loc.Distnace = obj.Distance;
                                loc.IsOffline = obj.IsOffline;

                                if (!string.IsNullOrEmpty(obj.houseId))
                                {
                                    loc.ReferanceID = obj.houseId;
                                }
                                loc.CreatedDate = DateTime.Now;

                                db.Locations.Add(loc);
                                db.SaveChanges();

                            }
                        }
                        else
                        {
                            if (house != null)
                            {
                                if (house.houseLat == null && house.houseLong == null)
                                {
                                    house.houseLat = obj.Lat;
                                    house.houseLong = obj.Long;
                                }
                            }

                            objdata.gcType = obj.gcType;
                            objdata.gpBeforImage = obj.gpBeforImage;
                            objdata.gpAfterImage = obj.gpAfterImage;
                            objdata.note = checkNull(obj.note);
                            objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                            objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                            loc.Distnace = obj.Distance; // Convert.ToDecimal(distCount);
                            objdata.batteryStatus = obj.batteryStatus;
                            objdata.userId = obj.userId;

                            //if (AppId == 1010)
                            //{
                            //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                            //}
                            //else
                            //{
                            //    objdata.locAddresss = addre;
                            //}

                            objdata.locAddresss = addre;
                            objdata.CreatedDate = DateTime.Now;
                            objdata.WasteType = obj.wastetype;
                            db.GarbageCollectionDetails.Add(objdata);

                            loc.datetime = Dateeee;
                            loc.lat = objdata.Lat;
                            loc.@long = objdata.Long;
                            loc.address = objdata.locAddresss;
                            loc.batteryStatus = obj.batteryStatus;
                            if (objdata.locAddresss != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            loc.userId = objdata.userId;
                            loc.type = 1;
                            loc.Distnace = obj.Distance;
                            loc.IsOffline = obj.IsOffline;
                            if (!string.IsNullOrEmpty(obj.houseId))
                            {
                                loc.ReferanceID = obj.houseId;
                            }
                            loc.CreatedDate = DateTime.Now;

                            db.Locations.Add(loc);
                            db.SaveChanges();

                        }

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        if (appdetails.AppId == 1003 || appdetails.AppId == 1006)
                        {
                            result.messageMar = "सबमिट यशस्वी";
                        }

                        else
                        {
                            if (objdata.garbageType == 3 && objdata.houseId != null)
                            {
                                //string mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

                                //string mesMar = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

                                switch (appdetails.LanguageId)
                                {
                                    //case 1:
                                    //    mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    //    break;

                                    //case 2:
                                    //    mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    //    break;

                                    case 3:
                                        //  mes = "नमस्कार! आपके घर से कचरा एकत्र किया जाता है। गीले और सूखे के रूप में वर्गीकृत कचरा सफाई कर्मचारियों को सौंपने में सहायता के लिए धन्यवाद। आपकी सेवा में " + appdetails.AppName_mar + " " + appdetails.yoccContact;
                                        mes = "" + appdetails.MsgForNotSpecified + " " + appdetails.AppName_mar + " " + appdetails.yoccContact;
                                        break;
                                    case 4:
                                        //  mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForNotSpecified + " " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        break;

                                    default:
                                        //  mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                        mes = "" + appdetails.MsgForNotSpecified + " " + appdetails.AppName + ".";
                                        break;
                                }


                                if (house != null)
                                {
                                    List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                    if (ArrayList.Count > 0)
                                    {
                                        PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    }
                                    else if (housemob != "")
                                    {
                                        if (appdetails.LanguageId == 4)
                                        {
                                            sendSMSmar(mes, housemob);
                                        }
                                        if (appdetails.LanguageId == 1)
                                        {

                                        }

                                        else
                                        {
                                            if (appdetails.LanguageId != 4)
                                            {
                                                sendSMS(mes, housemob);
                                            }

                                            else
                                            {
                                                sendSMS(mes, housemob);
                                            }
                                        }
                                    }
                                }

                            }

                            if (objdata.garbageType == 0)
                            {
                                //string mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";

                                //string mesMar = "नमस्कार! आपल्या घरातून आज ओला व सुका कचरा मिश्र स्वरूपात संकलित करण्यात आलेला आहे. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

                                switch (appdetails.LanguageId)
                                {
                                    //case 1:
                                    //    mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";
                                    //    break;

                                    //case 2:
                                    //    mes = "नमस्कार! आपल्या घरातून आज ओला व सुका कचरा मिश्र स्वरूपात संकलित करण्यात आलेला आहे. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    //    break;

                                    case 3:
                                        //  mes = "नमस्कार! आज, हमारे घरों से मिश्रित रूप में नम और सूखा कचरा एकत्र किया गया है। आपसे अनुरोध है कि प्रतिदिन नम कचरे की सफाई और निपटान में सहायता करें और सफाई कर्मचारियों को सौंप दें। " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForMixed + " " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        break;

                                    case 4:
                                        //  mes = "प्रिय नागरिक आपणाद्वारे आज मिश्र स्वरूपाचा कचरा देण्यात आला आहे. कृपया दररोज ओला व सुका असा वर्गीकृत कचरा देण्यात यावा.आपली सौ स्वातीताई संतोषभाऊ कोल्हे " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForMixed + " " + appdetails.yoccContact + "  आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        break;
                                    default:
                                        //   mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";
                                        mes = "" + appdetails.MsgForMixed + " " + appdetails.AppName + ".";
                                        break;
                                }

                                if (house != null)
                                {
                                    List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                    if (ArrayList.Count > 0)
                                    {
                                        PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    }
                                    else if (housemob != "")
                                    {
                                        if (appdetails.LanguageId == 4)
                                        {
                                            sendSMSmar(mes, housemob);
                                        }
                                        if (appdetails.LanguageId == 1)
                                        {

                                        }

                                        else
                                        {
                                            if (appdetails.LanguageId != 4)
                                            {
                                                sendSMS(mes, housemob);
                                            }

                                            else
                                            {
                                                sendSMS(mes, housemob);
                                            }
                                        }
                                    }
                                }

                            }

                            if (objdata.garbageType == 1)
                            {
                                //string mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

                                //string mesMar = "नमस्कार! आपल्या घरातून आज ओला व सुका असा विघटित केलेला कचरा संकलित करण्यात आलेला आहे. आपण केलेल्या सहयोगाबद्दल धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

                                switch (appdetails.LanguageId)
                                {
                                    //case 1:
                                    //    mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    //    break;

                                    //case 2:
                                    //    mes = "नमस्कार! आपल्या घरातून आज ओला व सुका असा विघटित केलेला कचरा संकलित करण्यात आलेला आहे. आपण केलेल्या सहयोगाबद्दल धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    //    break;

                                    case 3:
                                        //mes = "नमस्कार! आज, हमारे घर से कचरा एकत्र किया जाता है, जो गीला और सूखा होता है। आपके सहयोग के लिए धन्यवाद।" + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForSegregated + " " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        break;
                                    case 4:
                                        //  mes = "प्रिय नागरिक, आपण घंटागाडीमध्ये ओला व सुका असा वर्गीकृत कचरा दिल्याबद्दल धन्यवाद. आपली सौ स्वातीताई संतोषभाऊ कोल्हे" + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForSegregated + " " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        break;

                                    default:
                                        //  mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

                                        mes = "" + appdetails.MsgForSegregated + " " + appdetails.AppName + ".";
                                        break;
                                }

                                if (house != null)
                                {
                                    List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                    if (ArrayList.Count > 0)
                                    {
                                        PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    }
                                    else if (housemob != "")
                                    {
                                        if (appdetails.LanguageId == 4)
                                        {
                                            sendSMSmar(mes, housemob);
                                        }
                                        if (appdetails.LanguageId == 1)
                                        {

                                        }

                                        else
                                        {
                                            if (appdetails.LanguageId != 4)
                                            {
                                                sendSMS(mes, housemob);
                                            }

                                            else
                                            {
                                                sendSMS(mes, housemob);
                                            }
                                        }
                                    }
                                }

                            }

                            if (objdata.garbageType == 2)
                            {
                                //string mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Received Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

                                //string mesMar = "नमस्कार! आपल्या घरातून आज कोणत्याही प्रकारचा कचरा सफाई कर्मचाऱ्यास देण्यात आलेला नाही. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

                                switch (appdetails.LanguageId)
                                {
                                    //case 1:
                                    //    mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Received Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";
                                    //    break;

                                    //case 2:
                                    //    mes = "नमस्कार! आपल्या घरातून आज कोणत्याही प्रकारचा कचरा सफाई कर्मचाऱ्यास देण्यात आलेला नाही. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                    //    break;

                                    case 3:
                                        //  mes = "नमस्कार! आज आपके घर में कोई भी कचरा उपलब्ध नहीं कराया गया है। आपसे अनुरोध है कि प्रतिदिन गीला और सूखा कचरे की सफाई और निपटान में सहायता करें और सफाई कर्मचारियों को सौंप दें।" + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForNotReceived + " " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "";
                                        break;

                                    case 4:
                                        //  mes = "प्रिय नागरिक आपणाद्वारे आज कचरा देण्यात आला नाही. कृपया दररोज ओला व सुका असा वर्गीकृत कचरा देण्यात यावा. आपली सौ स्वातीताई संतोषभाऊ कोल्हे" + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        mes = "" + appdetails.MsgForNotReceived + " " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                                        break;

                                    default:
                                        //  mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

                                        mes = "" + appdetails.MsgForNotReceived + " " + appdetails.AppName + ".";
                                        break;
                                }

                                if (house != null)
                                {
                                    List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

                                    if (ArrayList.Count > 0)
                                    {
                                        PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
                                    }
                                    else if (housemob != "")
                                    {

                                        if (appdetails.LanguageId == 4)
                                        {
                                            sendSMSmar(mes, housemob);
                                        }
                                        if (appdetails.LanguageId == 1)
                                        {

                                        }

                                        else
                                        {
                                            if (appdetails.LanguageId != 4)
                                            {
                                                sendSMS(mes, housemob);
                                            }

                                            else
                                            {
                                                sendSMS(mes, housemob);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        return result;
                    }

                    catch (Exception ex)
                    {
                        result.ID = obj.OfflineID;
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        result.status = "error";
                        return result;
                    }

                }

                else
                {
                    result.ID = obj.OfflineID;
                    result.status = "success";
                    result.message = "Uploaded successfully";
                    result.messageMar = "सबमिट यशस्वी";
                    return result;
                }
            }
        }




        private CollectionSyncResult SavePointCollectionSync(SBGarbageCollectionView obj, int AppId, int type)
        {
            int locType = 0;
            CollectionSyncResult result = new CollectionSyncResult();
            HouseMaster dbHouse = new HouseMaster();

            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                string name = "", housemob = "", nameMar = "", addre = "";

                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime startDateTime = new DateTime(Dateeee.Year, Dateeee.Month, Dateeee.Day, 00, 00, 00, 000);  //Today at 00:00:00
                DateTime endDateTime = new DateTime(Dateeee.Year, Dateeee.Month, Dateeee.Day, 23, 59, 59, 999); // Dateeee.AddDays(1).AddTicks
                try
                {
                    GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                    objdata.userId = obj.userId;
                    objdata.gcDate = Convert.ToDateTime(obj.gcDate);
                    objdata.Lat = obj.Lat;
                    objdata.Long = obj.Long;

                    //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                    var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                    Location loc = new Location();

                    if (atten == null)
                    {
                        result.ID = obj.OfflineID;
                        result.isAttendenceOff = true;
                        result.message = "Your duty is currently off, please start again.. ";
                        result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                        result.status = "success";
                        return result;
                    }
                    else { result.isAttendenceOff = false; }


                    var gpdetails = db.GarbagePointDetails.Where(c => c.ReferanceId == obj.gpId).FirstOrDefault();

                    //var start = Dateeee;
                    //var gpCollectionDetails = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.gpId == gpdetails.gpId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    //DateTime oldDte = Convert.ToDateTime(gpCollectionDetails.gcDate);

                    //if ((start - oldDte).TotalMinutes >= 10)

                    DateTime oldTime;
                    TimeSpan span = TimeSpan.Zero;

                    var gpCollectionDetails = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.gpId == gpdetails.gpId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gpCollectionDetails != null)
                    {
                        oldTime = gpCollectionDetails.gcDate.Value;
                        span = Dateeee.Subtract(oldTime);
                    }

                    if (gpCollectionDetails == null || span.Minutes >= 10)
                    {
                        if (obj.gpId != null && obj.gpId != "")
                        {
                            try
                            {
                                locType = 2;
                                //var gpdetails = db.GarbagePointDetails.Where(c => c.ReferanceId == obj.gpId).FirstOrDefault();
                                objdata.gpId = gpdetails.gpId;
                                name = gpdetails.gpName;
                                nameMar = checkNull(gpdetails.gpNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.gpAddress);

                                var IsSamePointRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.gpId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSamePointRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid gpId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }

                        objdata.gcType = obj.gcType;
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.userId = obj.userId;

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        db.GarbageCollectionDetails.Add(objdata);

                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;
                        loc.batteryStatus = obj.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance; //Convert.ToDecimal(distCount);
                        //loc.IsOffline = obj.IsOffline;
                        loc.ReferanceID = obj.gpId;
                        loc.CreatedDate = DateTime.Now;
                        db.Locations.Add(loc);
                        db.SaveChanges();

                    }
                    else
                    {
                        if (obj.gpId != null && obj.gpId != "")
                        {
                            try
                            {
                                locType = 2;
                                //var gpdetails = db.GarbagePointDetails.Where(c => c.ReferanceId == obj.gpId).FirstOrDefault();
                                gpCollectionDetails.gpId = gpdetails.gpId;
                                name = gpdetails.gpName;
                                nameMar = checkNull(gpdetails.gpNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.gpAddress);

                                var IsSamePointRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.gpId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSamePointRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid gpId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }

                        gpCollectionDetails.gcType = obj.gcType;
                        gpCollectionDetails.gpBeforImage = obj.gpBeforImage;
                        gpCollectionDetails.gpAfterImage = obj.gpAfterImage;
                        gpCollectionDetails.note = checkNull(obj.note);
                        gpCollectionDetails.garbageType = checkIntNull(obj.garbageType.ToString());
                        gpCollectionDetails.vehicleNumber = checkNull(obj.vehicleNumber);
                        gpCollectionDetails.batteryStatus = obj.batteryStatus;
                        gpCollectionDetails.userId = obj.userId;

                        if (AppId == 1010)
                        {
                            gpCollectionDetails.locAddresss = Address(gpCollectionDetails.Lat + "," + gpCollectionDetails.Long);
                        }
                        else
                        {
                            gpCollectionDetails.locAddresss = addre;
                        }

                        //gpCollectionDetails.locAddresss = addre;
                        gpCollectionDetails.CreatedDate = DateTime.Now;
                        //db.GarbageCollectionDetails.Add(objdata);


                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;
                        loc.batteryStatus = obj.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.gpId))
                        {
                            loc.ReferanceID = obj.gpId;
                        }

                        loc.CreatedDate = DateTime.Now;

                        db.Locations.Add(loc);

                        db.SaveChanges();

                    }

                    result.ID = obj.OfflineID;
                    result.status = "success";
                    result.message = "Uploaded successfully";
                    result.messageMar = "सबमिट यशस्वी";

                    return result;
                }

                catch (Exception ex)
                {
                    result.ID = obj.OfflineID;
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.status = "error";
                    return result;
                }
            }

        }


        private CollectionSyncResult SaveDumpCollectionSyncForNormal(SBGarbageCollectionView obj, int AppId, int type)
        {

            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.dyId == dydetails.dyId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null || span.Minutes >= 10)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                objdata.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);

                                var IsSameDumpRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.dyId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameDumpRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                gcd.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध डीवाय आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);


                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
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


        private CollectionSyncResult SaveDumpCollectionSyncForLiquid(SBGarbageCollectionView obj, int AppId, int type)
        {

            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.dyId == dydetails.dyId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null || span.Minutes >= 10)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                objdata.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);

                                var IsSameDumpRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.dyId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameDumpRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        objdata.EmployeeType = "L";
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "L";
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                gcd.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध डीवाय आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);
                        gcd.EmployeeType = "L";

                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "L";
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
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

        private CollectionSyncResult SaveDumpCollectionSyncForStreet(SBGarbageCollectionView obj, int AppId, int type)
        {

            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.dyId == dydetails.dyId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null || span.Minutes >= 10)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                objdata.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);

                                var IsSameDumpRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.dyId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameDumpRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        objdata.EmployeeType = "S";
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "S";
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                gcd.dyId = gpdetails.dyId;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid dyId"; result.messageMar = "अवैध डीवाय आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);
                        gcd.EmployeeType = "S";

                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "S";
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
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

        private CollectionSyncResult SaveDumpCollectionSyncForDump(SBGarbageCollectionView obj, int AppId, int type)
        {

            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var vrdetails = db.Vehical_QR_Master.Where(c => c.ReferanceId == obj.vqrId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.vqrid == vrdetails.vqrId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null || span.Minutes >= 10)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.vqrId != null && obj.vqrId != "")
                        {
                            try
                            {
                                var gpdetails = db.Vehical_QR_Master.Where(c => c.ReferanceId == obj.vqrId).FirstOrDefault();
                                objdata.vqrid = gpdetails.vqrId;
                                name = gpdetails.VehicalNumber;
                                nameMar = checkNull(gpdetails.VehicalType);
                                housemob = "";
                             //   addre = checkNull(gpdetails.VehicalNumber);

                                var IsSameDumpRecord = db.GarbageCollectionDetails.Where(a => a.vqrid == gpdetails.vqrId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameDumpRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid Vehicle Id"; result.messageMar = "अवैध वाहन आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        objdata.EmployeeType = "D";
                        objdata.dyId = atten.dyid;
                        objdata.vqrid = vrdetails.vqrId;
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.vqrId))
                        {
                            loc.ReferanceID = obj.vqrId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "D";
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.vqrId != null && obj.vqrId != "")
                        {
                            try
                            {
                                var gpdetails = db.Vehical_QR_Master.Where(c => c.ReferanceId == obj.vqrId).FirstOrDefault();
                                gcd.vqrid = gpdetails.vqrId;
                                name = gpdetails.VehicalNumber;
                                nameMar = checkNull(gpdetails.VehicalType);
                                housemob = "";
                                addre = checkNull(gpdetails.VehicalNumber);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid Vehicle Id"; result.messageMar = "अवैध वाहन आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        if (obj.gpBeforImage == null)
                        {
                            obj.gpBeforImage = "";
                        }
                        if (obj.gpAfterImage == null)
                        {
                            obj.gpAfterImage = "";
                        }
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);
                        gcd.dyId = atten.dyid;
                        gcd.vqrid = vrdetails.vqrId;
                        gcd.EmployeeType = "D";

                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.vqrId))
                        {
                            loc.ReferanceID = obj.vqrId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "D";
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
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

        private CollectionSyncResult SaveLiquidCollectionSync(SBGarbageCollectionView obj, int AppId, int type)
        {

            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.LiquidWasteDetails.Where(c => c.ReferanceId == obj.LWId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.LWId == dydetails.LWId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee) & c.EmployeeType == "L").FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.LWId != null && obj.LWId != "")
                        {
                            try
                            {
                                var gpdetails = db.LiquidWasteDetails.Where(c => c.ReferanceId == obj.LWId).FirstOrDefault();
                                objdata.LWId = gpdetails.LWId;
                                name = gpdetails.LWName;
                                nameMar = checkNull(gpdetails.LWNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.LWAddreLW);

                                var IsSameLiquidRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.LWId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameLiquidRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid LWId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        objdata.EmployeeType = "L";
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "L";
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee) & c.EmployeeType == "L").FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.LWId != null && obj.LWId != "")
                        {
                            try
                            {
                                var gpdetails = db.LiquidWasteDetails.Where(c => c.ReferanceId == obj.LWId).FirstOrDefault();
                                gcd.LWId = gpdetails.LWId;
                                name = gpdetails.LWName;
                                nameMar = checkNull(gpdetails.LWNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.LWAddreLW);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid LWId"; result.messageMar = "अवैध डीवाय आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);


                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.LWId))
                        {
                            loc.ReferanceID = obj.LWId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "L";
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
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

        private CollectionSyncResult SaveStreetCollectionSync(SBGarbageCollectionView obj, int AppId, int type)
        {
            int i = 0;
            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                // var distCount = "";
                DateTime Dateeee = Convert.ToDateTime(obj.gcDate);
                DateTime newTime = Dateeee;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.StreetSweepingDetails.Where(c => c.ReferanceId == obj.SSId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.SSId == dydetails.SSId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }

                    if (gcd == null)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = Dateeee;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;

                        //var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee) & c.EmployeeType == "S").FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            result.ID = obj.OfflineID;
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.SSId != null && obj.SSId != "")
                        {
                            try
                            {
                                var gpdetails = db.StreetSweepingDetails.Where(c => c.ReferanceId == obj.SSId).FirstOrDefault();
                                objdata.SSId = gpdetails.SSId;
                                name = gpdetails.SSName;
                                nameMar = checkNull(gpdetails.SSNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.SSAddress);

                                var IsSameStreetRecord = db.GarbageCollectionDetails.Where(a => a.gpId == gpdetails.SSId && a.userId == obj.userId && a.gcDate == Dateeee).FirstOrDefault();

                                if (IsSameStreetRecord != null)
                                {
                                    result.ID = obj.OfflineID;
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid SSId"; result.messageMar = "अवैध जीपी आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        objdata.gcType = obj.gcType;
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus;
                        objdata.Distance = Convert.ToDouble(obj.Distance);  //Convert.ToDouble(distCount);

                        //if (AppId == 1010)
                        //{
                        //    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        //}
                        //else
                        //{
                        //    objdata.locAddresss = addre;
                        //}

                        objdata.locAddresss = addre;
                        objdata.CreatedDate = DateTime.Now;
                        objdata.EmployeeType = "S";
                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;
                        loc.Distnace = obj.Distance;
                        //loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "S";
                        db.Locations.Add(loc);
                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        //if (housemob != "")
                        //{
                        //    sendSMS(mes, housemob);
                        //}
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = Dateeee;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee) & c.EmployeeType == "S").FirstOrDefault();

                        if (atten == null)
                        {
                            result.ID = obj.OfflineID;
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.SSId != null && obj.SSId != "")
                        {
                            try
                            {
                                var gpdetails = db.StreetSweepingDetails.Where(c => c.ReferanceId == obj.SSId).FirstOrDefault();
                                gcd.SSId = gpdetails.SSId;
                                name = gpdetails.SSName;
                                nameMar = checkNull(gpdetails.SSNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.SSAddress);
                            }
                            catch
                            {
                                result.ID = obj.OfflineID;
                                result.message = "Invalid SSId"; result.messageMar = "अवैध डीवाय आयडी";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus;
                        gcd.Distance = Convert.ToDouble(obj.Distance); //Convert.ToDouble(distCount);


                        //if (AppId == 1010)
                        //{
                        //    gcd.locAddresss = Address(obj.Lat + "," + obj.Long);
                        //}
                        //else
                        //{
                        //    gcd.locAddresss = addre;
                        //}

                        gcd.locAddresss = addre;
                        gcd.CreatedDate = DateTime.Now;

                        /////////////////////////////////////////////////////////////
                        //GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        Location loc = new Location();
                        loc.datetime = Dateeee;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre; //Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;

                        if (addre != "")
                        {
                            loc.area = area(loc.address);
                        }
                        else
                        {
                            loc.area = "";
                        }

                        loc.userId = obj.userId;
                        loc.type = 1;
                        //loc.IsOffline = obj.IsOffline;
                        loc.Distnace = obj.Distance;

                        if (!string.IsNullOrEmpty(obj.LWId))
                        {
                            loc.ReferanceID = obj.LWId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.EmployeeType = "S";
                        db.Locations.Add(loc);

                        /////////////////////////////////////////////////////////////

                        db.SaveChanges();

                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";


                    }

                    var gc = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.SSId == dydetails.SSId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    var sd = db.StreetSweepingDetails.Where(x => x.SSId == gc.SSId).FirstOrDefault();
                    var sbeatcount = db.StreetSweepingBeats.Where(x => x.ReferanceId1 == sd.ReferanceId || x.ReferanceId2 == sd.ReferanceId || x.ReferanceId3 == sd.ReferanceId || x.ReferanceId4 == sd.ReferanceId || x.ReferanceId5 == sd.ReferanceId).FirstOrDefault();
                    var beatcount = db.Vw_BitCount.Where(x => x.BeatId == sbeatcount.BeatId).FirstOrDefault();
                    var sd1 = db.StreetSweepingDetails.Where(z => z.ReferanceId == sbeatcount.ReferanceId1 || z.ReferanceId == sbeatcount.ReferanceId2 || z.ReferanceId == sbeatcount.ReferanceId3 || z.ReferanceId == sbeatcount.ReferanceId4 || z.ReferanceId == sbeatcount.ReferanceId5).ToList();
                    foreach (var x in sd1)
                    {
                        var sgcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.SSId == x.SSId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                        if (sgcd != null)
                        {
                            i++;
                        }

                    }

                    if (beatcount.BitCount == i)
                    {
                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Street Sweeping Completed Successfully";
                        result.messageMar = "सबमिट यशस्वी";
                    }
                    else
                    {
                        result.ID = obj.OfflineID;
                        result.status = "success";
                        result.message = "Street Sweeping Partially Completed";
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
        public CollectionSyncResult SaveUserLocationOfflineSync(SBGarbageCollectionView obj, int AppId, int typeId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    CollectionSyncResult result = new CollectionSyncResult();
                    DateTime Dateeee = Convert.ToDateTime(obj.gcDate);

                    if (typeId == 0 || typeId == 2)
                    {

                        DateTime newTime = Dateeee;
                        DateTime oldTime;
                        TimeSpan span = TimeSpan.Zero;
                        var gcd = db.Locations.Where(c => c.userId == obj.userId && c.type == null && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.locId).FirstOrDefault();
                        if (gcd != null)
                        {
                            oldTime = gcd.datetime.Value;
                            span = newTime.Subtract(oldTime);
                        }

                        if (gcd == null || span.Minutes >= 9)
                        //  var IsSameRecordLocation = db.Locations.Where(c => c.userId == obj.userId && c.datetime == Dateeee).FirstOrDefault();

                        //if (IsSameRecordLocation == null)
                        {
                            var u = db.UserMasters.Where(c => c.userId == obj.userId);

                            var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.daDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                            if (atten == null)
                            {
                                result.isAttendenceOff = true;
                                result.ID = obj.OfflineID;
                                result.message = "Your duty is currently off, please start again.. ";
                                result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                                result.status = "error";
                                return result;
                            }
                            else { result.isAttendenceOff = false; }


                            if (u != null & obj.userId > 0)
                            {
                                string addr = "", ar = "";
                                addr = Address(obj.Lat + "," + obj.Long);
                                if (addr != "")
                                {
                                    ar = area(addr);
                                }

                                db.Locations.Add(new Location()
                                {
                                    userId = obj.userId,
                                    lat = obj.Lat,
                                    @long = obj.Long,
                                    datetime = Convert.ToDateTime(obj.gcDate),
                                    address = addr,
                                    area = ar,
                                    batteryStatus = obj.batteryStatus,
                                    Distnace = obj.Distance,
                                    //IsOffline = true,
                                    ReferanceID = obj.ReferenceID,
                                    CreatedDate = DateTime.Now,
                                });
                                db.SaveChanges();
                            }
                        }
                        result.ID = Convert.ToInt32(obj.OfflineID);
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        return result;


                    }


                    else if (typeId == 1)
                    {
                        DateTime newTime = Dateeee;
                        DateTime oldTime;
                        TimeSpan span = TimeSpan.Zero;
                        var IsSameRecordQr_Location = db.Qr_Location.Where(c => c.empId == obj.userId && c.type == null && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(Dateeee)).OrderByDescending(c => c.locId).FirstOrDefault();
                        if (IsSameRecordQr_Location != null)
                        {
                            oldTime = IsSameRecordQr_Location.datetime.Value;
                            span = newTime.Subtract(oldTime);
                        }

                        if (IsSameRecordQr_Location == null || span.Minutes >= 9)

                        //    var IsSameRecordQr_Location = db.Locations.Where(c => c.userId == obj.userId && c.datetime == Dateeee).FirstOrDefault();

                        //   if (IsSameRecordQr_Location == null)
                        {
                            var u = db.QrEmployeeMasters.Where(c => c.qrEmpId == obj.userId);

                            if (obj.OfflineID == 0)
                            {
                                var atten = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.userId & c.startDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();
                                if (atten == null)
                                {
                                    result.ID = Convert.ToInt32(obj.OfflineID);
                                    result.isAttendenceOff = false;
                                    result.status = "error";
                                    result.message = "Your duty is currently off, please start again.. ";
                                    result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                                    return result;
                                }
                            }


                            if (u != null & obj.userId > 0)
                            {
                                string addr = "", ar = "";
                                addr = Address(obj.Lat + "," + obj.Long);
                                if (addr != "")
                                {
                                    ar = area(addr);
                                }

                                db.Qr_Location.Add(new Qr_Location()
                                {
                                    empId = obj.userId,
                                    lat = obj.Lat,
                                    @long = obj.Long,
                                    datetime = Convert.ToDateTime(obj.gcDate),
                                    address = addr,
                                    area = ar,
                                    batteryStatus = obj.batteryStatus,
                                    Distnace = obj.Distance, //Convert.ToDecimal(distCount),
                                });
                                db.SaveChanges();

                            }
                        }
                        result.ID = Convert.ToInt32(obj.OfflineID);
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";

                    }
                    return result;
                }
                catch
                {

                    CollectionSyncResult result = new CollectionSyncResult();
                    result.ID = 0;
                    result.status = "error";
                    result.message = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.messageMar = "Something is wrong,Try Again.. ";

                    return result;
                }

            }

        }

        public CollectionSyncResult SaveGarbageCollectionOffline(SBGarbageCollectionView obj, int AppId, int type)
        {
            CollectionSyncResult result = new CollectionSyncResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId);

            string House_Lat = obj.Lat;
            string House_Long = obj.Long;
            string HouseLat = House_Lat.Substring(0, 5);
            string HouseLong = House_Long.Substring(0, 5);
            var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId && c.houseLat.Contains(HouseLat) && c.houseLong.Contains(HouseLong)).FirstOrDefault();
            coordinates p = new coordinates()
            {
                lat = Convert.ToDouble(obj.Lat),
                lng = Convert.ToDouble(obj.Long)
            };
            List<List<coordinates>> lstPoly = new List<List<coordinates>>();
            List<coordinates> poly = new List<coordinates>();
            AppAreaMapVM ebm = GetEmpBeatMapByUserId(AppId);
            lstPoly = ebm.AppAreaLatLong;
            int polyId = 0;
            if (lstPoly != null && lstPoly.Count > polyId)
            {
                poly = lstPoly[polyId];
            }


            obj.IsIn = IsPointInPolygon(poly, p);


            if ((obj.IsIn == true && appdetails.IsAreaActive == true) || (appdetails.IsAreaActive == false))
            {
                if (obj.IsLocation == false && house != null && appdetails.IsScanNear == true)
                {
                    switch (obj.gcType)
                    {
                        case 1:
                            result = SaveHouseCollectionSync(obj, AppId, type);
                            break;
                        case 2:
                            result = SavePointCollectionSync(obj, AppId, type);
                            break;
                        case 3:
                            if (obj.EmpType == "N")
                            {
                                result = SaveDumpCollectionSyncForNormal(obj, AppId, type);
                            }
                            if (obj.EmpType == "L")
                            {
                                result = SaveDumpCollectionSyncForLiquid(obj, AppId, type);
                            }

                            if (obj.EmpType == "S")
                            {
                                result = SaveDumpCollectionSyncForStreet(obj, AppId, type);
                            }                   

                            break;
                        case 4:
                            result = SaveLiquidCollectionSync(obj, AppId, type);
                            break;
                        case 5:
                            result = SaveStreetCollectionSync(obj, AppId, type);
                            break;
                        case 6:
                            if (obj.EmpType == "D")
                            {
                                result = SaveDumpCollectionSyncForDump(obj, AppId, type);
                            }
                            break;
                    }
                }

                else if (obj.IsLocation == false && appdetails.IsScanNear == null)
                {
                    switch (obj.gcType)
                    {
                        case 1:
                            result = SaveHouseCollectionSync(obj, AppId, type);
                            break;
                        case 2:
                            result = SavePointCollectionSync(obj, AppId, type);
                            break;
                        case 3:
                            if (obj.EmpType == "N")
                            {
                                result = SaveDumpCollectionSyncForNormal(obj, AppId, type);
                            }
                            if (obj.EmpType == "L")
                            {
                                result = SaveDumpCollectionSyncForLiquid(obj, AppId, type);
                            }

                            if (obj.EmpType == "S")
                            {
                                result = SaveDumpCollectionSyncForStreet(obj, AppId, type);
                            }
                          
                            break;

                        case 4:
                            result = SaveLiquidCollectionSync(obj, AppId, type);
                            break;
                        case 5:
                            result = SaveStreetCollectionSync(obj, AppId, type);
                            break;
                        case 6:
                            if (obj.EmpType == "D")
                            {
                                result = SaveDumpCollectionSyncForDump(obj, AppId, type);
                            }
                            break;
                    }
                }
                else if (obj.IsLocation == true)
                {
                    result = SaveUserLocationOfflineSync(obj, AppId, type);
                }
                if (obj.IsLocation == false && house == null && appdetails.IsScanNear == true)
                {
                    result.message = "You Are Not In Nearby.";
                    result.messageMar = "आपण जवळपास नाही.";
                }

                if (obj.IsLocation == false && obj.EmpType == "N" && result.status == "success")
                {
                    appdetails.Today_Waste_Status = true;
                }
                if (obj.IsLocation == false && obj.EmpType == "L" && result.status == "success")
                {
                    appdetails.Today_Liquid_Status = true;
                }
                if (obj.IsLocation == false && obj.EmpType == "S" && result.status == "success")
                {
                    appdetails.Today_Street_Status = true;

                }
                dbMain.SaveChanges();

            }

            else
            {
                result.message = "Your outside the area,please go to inside the area.. ";
                result.messageMar = "तुम्ही क्षेत्राबाहेर आहात.कृपया परिसरात जा..";
                result.status = "error";
                return result;
            }

            return result;
        }

        // }
        //}


        //private CollectionSyncResult SaveGarbageCollectionHouse(SBGarbageCollectionView obj, int AppId, int type, string batteryStatus)
        //{
        //    int locType = 0;
        //    CollectionSyncResult result = new CollectionSyncResult();
        //    HouseMaster dbHouse = new HouseMaster();

        //    var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        string name = "", housemob = "", nameMar = "", addre = "";

        //        var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId).FirstOrDefault();
        //        bool IsExist = false;
        //        DateTime startDateTime = DateTime.Today; //Today at 00:00:00
        //        DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
        //        var distCount = "";
        //        try
        //        {
        //            GarbageCollectionDetail objdata = new GarbageCollectionDetail();
        //            objdata.userId = obj.userId;
        //            objdata.gcDate = DateTime.Now;
        //            objdata.Lat = obj.Lat;
        //            objdata.Long = obj.Long;
        //            var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

        //            Location loc = new Location();
        //            var locc = db.SP_UserLatLongDetail(objdata.userId, 0).FirstOrDefault();

        //            if (locc == null || locc.lat == "" || locc.@long == "")
        //            {
        //                string a = objdata.Lat;
        //                string b = objdata.Long;

        //                //string a = loc.lat;
        //                //string b = loc.@long;

        //                var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
        //                distCount = dist.Distance_in_KM.ToString();
        //            }
        //            else
        //            {

        //                var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
        //                distCount = dist.Distance_in_KM.ToString();
        //            }


        //            if (atten == null)
        //            {
        //                result.isAttendenceOff = true;
        //                result.message = "Your duty is currently off, please start again.. ";
        //                result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
        //                result.name = "";
        //                result.status = "success";
        //                return result;
        //            }
        //            else { result.isAttendenceOff = false; }
        //            if (obj.houseId != null && obj.houseId != "")
        //            {
        //                try
        //                {
        //                    locType = 1;
        //                    //var house = db.HouseMasters.Where(c => c.ReferanceId == obj.houseId).FirstOrDefault();
        //                    objdata.houseId = house.houseId;
        //                    // objdata.gpId = 0;
        //                    name = house.houseOwner;
        //                    nameMar = checkNull(house.houseOwnerMar);
        //                    addre = checkNull(house.houseAddress);
        //                    housemob = house.houseOwnerMobile;

        //                    IsExist = (from p in db.GarbageCollectionDetails where p.houseId == objdata.houseId && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;

        //                }
        //                catch (Exception ex)
        //                {
        //                    result.message = "Invalid houseId"; result.messageMar = "अवैध घर आयडी"; result.name = "";
        //                    result.status = "error";
        //                    return result;
        //                }

        //            }
        //            if (obj.gpId != null && obj.gpId != "")
        //            {
        //                try
        //                {
        //                    locType = 2;
        //                    var gpdetails = db.GarbagePointDetails.Where(c => c.ReferanceId == obj.gpId).FirstOrDefault();
        //                    objdata.gpId = gpdetails.gpId;
        //                    // objdata.houseId = 0;
        //                    name = gpdetails.gpName;
        //                    nameMar = checkNull(gpdetails.gpNameMar);
        //                    housemob = "";
        //                    addre = checkNull(gpdetails.gpAddress);

        //                    //IsExist = (from p in db.GarbageCollectionDetails where p.gpId == objdata.gpId && p.gcDate >= startDateTime && p.gcDate <= endDateTime select p).Count() > 0;
        //                }
        //                catch
        //                {
        //                    result.message = "Invalid gpId"; result.messageMar = "अवैध जीपी आयडी"; result.name = "";
        //                    result.status = "error";
        //                    return result;
        //                }

        //            }


        //            if (IsExist == true)
        //            {
        //                ///////////////Temperary Added By Nishikant on dated 12-06-2019 ////////////
        //                //#region Temperary Added
        //                var dbLocalHouse = db.HouseMasters.Where(c => c.houseId == house.houseId).FirstOrDefault();
        //                if (dbLocalHouse.houseLong == null && dbLocalHouse.houseLong == null)
        //                {
        //                    dbLocalHouse.houseLat = obj.Lat;
        //                    dbLocalHouse.houseLong = obj.Long;
        //                }
        //                //#endregion
        //                /////////////////////////////////////////////////////////

        //                var gcd = db.GarbageCollectionDetails.Where(c => c.houseId == house.houseId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
        //                gcd.gcType = obj.gcType;
        //                gcd.gpBeforImage = obj.gpBeforImage;
        //                gcd.gpAfterImage = obj.gpAfterImage;
        //                gcd.note = checkNull(obj.note);
        //                gcd.garbageType = checkIntNull(obj.garbageType.ToString());
        //                objdata.garbageType = checkIntNull(obj.garbageType.ToString());
        //                gcd.vehicleNumber = checkNull(obj.vehicleNumber);
        //                loc.Distnace = Convert.ToDecimal(distCount);
        //                gcd.batteryStatus = batteryStatus;
        //                gcd.userId = obj.userId;
        //                gcd.gcDate = DateTime.Now;
        //                gcd.Lat = obj.Lat;
        //                gcd.Long = obj.Long;

        //                if (AppId == 1003 || AppId == 1010)
        //                {
        //                    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
        //                }
        //                else
        //                {
        //                    objdata.locAddresss = addre;
        //                }
        //                //db.Entry(gcd).State = System.Data.Entity.EntityState.Modified;
        //                //db.SaveChanges();

        //                //db.GarbageCollectionDetails.Add(objdata);

        //                // Location loc = new Location();
        //                loc.datetime = DateTime.Now;
        //                loc.lat = objdata.Lat;
        //                loc.@long = objdata.Long;
        //                loc.address = objdata.locAddresss; //Address(objdata.Lat + "," + objdata.Long);
        //                loc.batteryStatus = batteryStatus;
        //                if (objdata.locAddresss != "")
        //                { loc.area = area(loc.address); }
        //                else
        //                {
        //                    loc.area = "";
        //                }
        //                loc.userId = objdata.userId;
        //                loc.type = 1;
        //                db.Locations.Add(loc);
        //                // db.Entry(objdata).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();
        //                result.name = name;
        //                result.nameMar = nameMar;
        //                result.mobile = housemob;
        //            }
        //            else
        //            {
        //                ///////////////Temperary Added By Nishikant on dated 12-06-2019 ////////////
        //                #region Temperary Added

        //                //var dbLocalHouse = db.HouseMasters.Where(c => c.houseId == house.houseId).FirstOrDefault();
        //                //dbLocalHouse.houseLat = obj.Lat;
        //                //dbLocalHouse.houseLong = obj.Long;

        //                #endregion
        //                /////////////////////////////////////////////////////////


        //                //  Temperary Added

        //                if (house != null)
        //                {
        //                    if (house.houseLat == null && house.houseLong == null)
        //                    {
        //                        house.houseLat = obj.Lat;
        //                        house.houseLong = obj.Long;
        //                    }
        //                }

        //                objdata.gcType = obj.gcType;
        //                objdata.gpBeforImage = obj.gpBeforImage;
        //                objdata.gpAfterImage = obj.gpAfterImage;
        //                objdata.note = checkNull(obj.note);
        //                objdata.garbageType = checkIntNull(obj.garbageType.ToString());
        //                objdata.vehicleNumber = checkNull(obj.vehicleNumber);
        //                loc.Distnace = Convert.ToDecimal(distCount);
        //                objdata.batteryStatus = batteryStatus;
        //                objdata.userId = obj.userId;


        //                if (AppId == 1003 || AppId == 1010)
        //                {
        //                    objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
        //                }
        //                else
        //                {
        //                    objdata.locAddresss = addre;
        //                }
        //                //db.HouseMasters.Add(house);
        //                db.GarbageCollectionDetails.Add(objdata);

        //                // Location loc = new Location();
        //                loc.datetime = DateTime.Now;
        //                loc.lat = objdata.Lat;
        //                loc.@long = objdata.Long;
        //                loc.address = objdata.locAddresss; //Address(objdata.Lat + "," + objdata.Long);
        //                loc.batteryStatus = batteryStatus;
        //                if (objdata.locAddresss != "")
        //                { loc.area = area(loc.address); }
        //                else
        //                {
        //                    loc.area = "";
        //                }
        //                loc.userId = objdata.userId;
        //                loc.type = 1;
        //                db.Locations.Add(loc);
        //                db.SaveChanges();
        //                result.name = name;
        //                result.nameMar = nameMar;
        //                result.mobile = housemob;
        //            }

        //            result.status = "success";
        //            result.message = "Uploaded successfully";
        //            result.messageMar = "सबमिट यशस्वी";
        //            if (appdetails.AppId == 1003 || appdetails.AppId == 1006)
        //            {
        //                result.messageMar = "सबमिट यशस्वी";
        //            }

        //            else
        //            {
        //                if (objdata.garbageType == 3 && objdata.houseId != null)
        //                {
        //                    string mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Specified Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + "."; /*"नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";*/

        //                    string mesMar = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

        //                    if (house != null)
        //                    {
        //                        List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

        //                        if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
        //                        {
        //                            //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                            PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                        }
        //                        else if (housemob != "")
        //                        {
        //                            sendSMS(mes, housemob);
        //                        }
        //                    }

        //                }

        //                if (objdata.garbageType == 0)
        //                {
        //                    string mes = "Dear Citizen Waste Pattern collected today from your house- mixed Suggested Waste Pattern - Dry and Wet Segregated. Thank You! " + appdetails.AppName + ".";

        //                    string mesMar = "नमस्कार! आपल्या घरातून आज ओला व सुका कचरा मिश्र स्वरूपात संकलित करण्यात आलेला आहे. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

        //                    if (house != null)
        //                    {
        //                        List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

        //                        if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
        //                        {
        //                            //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                            PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                        }
        //                        else if (housemob != "")
        //                        {
        //                            sendSMS(mes, housemob);
        //                        }
        //                    }

        //                }

        //                if (objdata.garbageType == 1)
        //                {
        //                    string mes = "Dear Citizen Waste Pattern collected today from your house-Segregated Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

        //                    string mesMar = "नमस्कार! आपल्या घरातून आज ओला व सुका असा विघटित केलेला कचरा संकलित करण्यात आलेला आहे. आपण केलेल्या सहयोगाबद्दल धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

        //                    if (house != null)
        //                    {
        //                        List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

        //                        if (ArrayList.Count > 0) //if (!string.IsNullOrEmpty(house.FCMID))
        //                        {
        //                            //PushNotificationMessage(mes, house.FCMID, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                            PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                        }
        //                        else if (housemob != "")
        //                        {
        //                            sendSMS(mes, housemob);
        //                        }
        //                    }

        //                }

        //                if (objdata.garbageType == 2)
        //                {
        //                    string mes = "Dear Citizen Waste Pattern collected today from your house-Waste Not Received Suggested Waste Pattern - Dry and Wet Segregated.Thank You! " + appdetails.AppName + ".";

        //                    string mesMar = "नमस्कार! आपल्या घरातून आज कोणत्याही प्रकारचा कचरा सफाई कर्मचाऱ्यास देण्यात आलेला नाही. आपणास विनंती आहे कि दररोज ओला व सुका कचरा विघटित करून सफाई कर्मचाऱ्यास सुपूर्द करून सहयोग करावा धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";

        //                    if (house != null)
        //                    {
        //                        List<String> ArrayList = DeviceDetailsFCM(obj.houseId, AppId);

        //                        if (ArrayList.Count > 0)
        //                        {
        //                            PushNotificationMessageBroadCast(mes, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);
        //                        }
        //                        else if (housemob != "")
        //                        {
        //                            sendSMS(mes, housemob);
        //                        }
        //                    }

        //                }
        //            }
        //            return result;
        //        }

        //        catch (Exception ex)
        //        {
        //            result.message = "Something is wrong,Try Again.. ";
        //            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
        //            result.name = "";
        //            result.status = "error";
        //            return result;
        //        }
        //    }


        //}



        public List<SBGarbageCollectionView> GetGarbageCollection(DateTime fdate, int AppId)
        {
            AppDetail objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
            List<SBGarbageCollectionView> obj = new List<SBGarbageCollectionView>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.GarbageCollectionDetails.Where(c => c.gcDate >= fdate).ToList();
                foreach (var x in data)
                {
                    obj.Add(new SBGarbageCollectionView()
                    {
                        gcId = x.gcId,
                        gcDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy"),
                        Lat = x.Lat,
                        Long = x.Long,
                        houseId = (x.houseId).ToString(),
                        gpId = (x.gpId).ToString(),
                        gpBeforImage = ImagePath(objmain.Collection, x.gpBeforImage, objmain),
                        gcType = Convert.ToInt32(x.gcType),
                        gpAfterImage = ImagePath(objmain.Collection, x.gpAfterImage, objmain),
                    });
                }

            }
            return obj;

        }

        public List<SBUserAttendenceView> GetUserAttendence(DateTime fDate, int appId, int userId)
        {
            List<SBUserAttendenceView> obj = new List<SBUserAttendenceView>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.Daily_Attendance.Where(c => c.userId == userId && c.daDate >= fDate).ToList();
                foreach (var x in data)
                {

                    obj.Add(new SBUserAttendenceView()
                    {
                        daID = x.daID,
                        userId = Convert.ToInt32(x.userId),
                        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                        startTime = checkNull(x.startTime),
                        endTime = checkNull(x.endTime),
                        vtId = x.vtId,
                    });
                }

            }
            return obj;
        }


        public SyncResult2 GetUserMobileIdentification(int appId, int userId, bool isSync, int batteryStatus, string imeinos)
        {
            SyncResult2 result = new SyncResult2();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                Daily_Attendance attendance = new Daily_Attendance();
                var mob = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                string imei1 = mob.imoNo;
                string imei2 = mob.imoNo2;
                if (mob.imoNo != null && mob.imoNo2 != null)
                {
                    if (mob.imoNo == imeinos)
                    {
                        mob.imoNo = null;
                        result.IsInSync = true;
                        result.UserId = userId;
                        result.batterystatus = batteryStatus;
                        result.imei = imei1;
                        mob.imoNo = mob.imoNo2;
                        mob.imoNo2 = null;
                        db.SaveChanges();
                    }
                    else
                    {
                        result.IsInSync = false;
                        result.UserId = userId;
                        result.batterystatus = batteryStatus;
                        result.imei = imei2;
                    }
                }
                else
                {
                    result.IsInSync = false;
                    result.UserId = userId;
                    result.batterystatus = batteryStatus;
                    result.imei = imei1;
                }

            }
            return result;
        }


        public List<SBWorkDetails> GetUserWork(int userId, int year, int month, int appId, string EmpType)
        {
            List<SBWorkDetails> obj = new List<SBWorkDetails>();
            if (EmpType == "N")
            {
                obj = GetUserWorkForNormal(userId, year, month, appId);
            }
            if (EmpType == "L")
            {
                obj = GetUserWorkForLiquid(userId, year, month, appId);
            }
            if (EmpType == "S")
            {
                obj = GetUserWorkForStreet(userId, year, month, appId);
            }
            return obj;
        }


        public List<SBWorkDetails> GetUserWorkForNormal(int userId, int year, int month, int appId)
        {
            List<SBWorkDetails> obj = new List<SBWorkDetails>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetAttendenceDetailsTotal(userId, year, month).ToList();
                foreach (var x in data)
                {

                    obj.Add(new SBWorkDetails()
                    {
                        date = Convert.ToDateTime(x.day).ToString("MM-dd-yyy"),
                        houseCollection = checkIntNull(x.HouseCollection.ToString()),
                        pointCollection = checkIntNull(x.PointCollection.ToString()),
                        DumpYardCollection = checkIntNull(x.DumpYardCollection.ToString()),
                    });
                }

            }
            return obj;
        }

        public List<SBWorkDetails> GetUserWorkForLiquid(int userId, int year, int month, int appId)
        {
            List<SBWorkDetails> obj = new List<SBWorkDetails>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetAttendenceDetailsTotalLiquid(userId, year, month).ToList();
                foreach (var x in data)
                {

                    obj.Add(new SBWorkDetails()
                    {
                        date = Convert.ToDateTime(x.day).ToString("MM-dd-yyy"),
                        LiquidCollection = checkIntNull(x.LiquidCollection.ToString()),
                        DumpYardCollection = checkIntNull(x.DumpYardCollection.ToString()),

                    });
                }

            }
            return obj;
        }


        public List<SBWorkDetails> GetUserWorkForStreet(int userId, int year, int month, int appId)
        {
            List<SBWorkDetails> obj = new List<SBWorkDetails>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetAttendenceDetailsTotalStreet(userId, year, month).ToList();
                foreach (var x in data)
                {

                    obj.Add(new SBWorkDetails()
                    {
                        date = Convert.ToDateTime(x.day).ToString("MM-dd-yyy"),
                        StreetCollection = checkIntNull(x.StreetCollection.ToString()),
                        DumpYardCollection = checkIntNull(x.DumpYardCollection.ToString()),
                    });
                }

            }
            return obj;
        }

        public List<SBWorkDetailsHistory> GetUserWorkDetails(DateTime date, int appId, int userId, int languageId)
        {

            List<SBWorkDetailsHistory> obj = new List<SBWorkDetailsHistory>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GarbageCollectionDetails.Where(c => EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(date) && c.userId == userId).OrderByDescending(c => c.gcDate).ToList();
                foreach (var x in data)
                {
                    string housnum = "", area = "", Name = "";
                    var att = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(date) && c.userId == userId).FirstOrDefault();
                    if (x.gcType == 1)
                    {
                        try
                        {
                            var house = db.HouseMasters.Where(c => c.houseId == x.houseId).FirstOrDefault();
                            housnum = checkNull(house.ReferanceId);

                            if (languageId == 1)
                            {
                                Name = checkNull(house.houseOwner);
                                area = db.TeritoryMasters.Where(c => c.Id == house.AreaId).FirstOrDefault().Area;
                            }
                            else
                            {
                                Name = checkNull(house.houseOwnerMar);
                                area = db.TeritoryMasters.Where(c => c.Id == house.AreaId).FirstOrDefault().AreaMar;
                            }

                        }
                        catch
                        {
                            //housnum = "";
                            //area = "";
                        }


                    }
                    if (x.gcType == 2)
                    {
                        try
                        {
                            var point = db.GarbagePointDetails.Where(c => c.gpId == x.gpId).FirstOrDefault();
                            housnum = point.ReferanceId;
                            if (languageId == 1)
                            {
                                Name = checkNull(point.gpName);
                                area = db.TeritoryMasters.Where(c => c.Id == point.areaId).FirstOrDefault().Area;
                                // housnum = point.gpId.ToString();
                            }
                            else
                            {
                                Name = checkNull(point.gpNameMar);
                                area = db.TeritoryMasters.Where(c => c.Id == point.areaId).FirstOrDefault().AreaMar;
                                //       housnum = point.gpId.ToString();
                            }
                        }
                        catch
                        {
                            //housnum = "";
                            //area = "";
                        }

                    }

                    if (x.gcType == 3)
                    {
                        try
                        {
                            var dump = db.DumpYardDetails.Where(c => c.dyId == x.dyId).FirstOrDefault();
                            housnum = dump.ReferanceId;
                            if (languageId == 1)
                            {
                                Name = checkNull(dump.dyName);
                                area = db.TeritoryMasters.Where(c => c.Id == dump.areaId).FirstOrDefault().Area;
                                // housnum = point.gpId.ToString();
                            }
                            else
                            {
                                Name = checkNull(dump.dyNameMar);
                                area = db.TeritoryMasters.Where(c => c.Id == dump.areaId).FirstOrDefault().AreaMar;
                                //       housnum = point.gpId.ToString();
                            }
                        }
                        catch
                        {
                            //housnum = "";
                            //area = "";
                        }

                    }

                    if (x.gcType == 4)
                    {
                        try
                        {
                            var Liquid = db.LiquidWasteDetails.Where(c => c.LWId == x.LWId).FirstOrDefault();
                            housnum = Liquid.ReferanceId;
                            if (languageId == 1)
                            {
                                Name = checkNull(Liquid.LWName);
                                area = db.TeritoryMasters.Where(c => c.Id == Liquid.areaId).FirstOrDefault().Area;
                                // housnum = point.gpId.ToString();
                            }
                            else
                            {
                                Name = checkNull(Liquid.LWNameMar);
                                area = db.TeritoryMasters.Where(c => c.Id == Liquid.areaId).FirstOrDefault().AreaMar;
                                //       housnum = point.gpId.ToString();
                            }
                        }
                        catch
                        {
                            //housnum = "";
                            //area = "";
                        }

                    }

                    if (x.gcType == 5)
                    {
                        try
                        {
                            var Street = db.StreetSweepingDetails.Where(c => c.SSId == x.SSId).FirstOrDefault();
                            housnum = Street.ReferanceId;
                            if (languageId == 1)
                            {
                                Name = checkNull(Street.SSName);
                                area = db.TeritoryMasters.Where(c => c.Id == Street.areaId).FirstOrDefault().Area;
                                // housnum = point.gpId.ToString();
                            }
                            else
                            {
                                Name = checkNull(Street.SSNameMar);
                                area = db.TeritoryMasters.Where(c => c.Id == Street.areaId).FirstOrDefault().AreaMar;
                                //       housnum = point.gpId.ToString();
                            }
                        }
                        catch
                        {
                            //housnum = "";
                            //area = "";
                        }

                    }

                    obj.Add(new SBWorkDetailsHistory()
                    {
                        time = Convert.ToDateTime(x.gcDate).ToString("hh:mm tt"),
                        Refid = housnum,
                        name = Name,
                        vehicleNumber = checkNull(x.vehicleNumber),
                        areaName = area,
                        type = Convert.ToInt32(x.gcType),
                    });
                }

            }
            //return obj.OrderByDescending(c => c.time).ToList(); 
            return obj.ToList();
        }

        public Result GetVersionUpdate(string version, int AppId)
        {
            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();

            if (appdetails.AppVersion != version && appdetails.ForceUpdate == true)
            {
                if (Convert.ToInt32(appdetails.AppVersion) <= Convert.ToInt32(version))
                {
                    result.status = "false";
                    result.message = "";
                    result.messageMar = "";
                    return result;
                }
                else
                {
                    result.status = "true";
                    result.message = "";
                    result.messageMar = "";
                    return result;
                }
            }

            else
            {

                result.status = "false";
                result.message = "";
                result.messageMar = "";
                return result;
            }


        }

        public Result GetAdminVersionUpdate(string version, int AppId)
        {
            Result result = new Result();
            //var appdetails = dbMain.AppVersions.Where(c => c.AppID == AppId).FirstOrDefault();

            //if (appdetails.AppVersionAdmin != version && appdetails.ForceUpdateAdmin == true)
            //{
            //    if (Convert.ToInt32(appdetails.AppVersionAdmin) <= Convert.ToInt32(version))
            //    {
            //        result.status = "false";
            //        result.message = "";
            //        result.messageMar = "";
            //        return result;
            //    }
            //    else
            //    {
            //        result.status = "true";
            //        result.message = "";
            //        result.messageMar = "";
            //        return result;
            //    }
            //}

            //else
            //{

            result.status = "false";
            result.message = "";
            result.messageMar = "";
            return result;
            //}


        }


        public Result GetGameVersionUpdate(string version)
        {
            Result result = new Result();
            //var appdetails = dbMain.AppVersions.FirstOrDefault();

            //if (appdetails.AppVersionGame != version && appdetails.ForceUpdateGame == true)
            //{
            //    if (Convert.ToInt32(appdetails.AppVersionGame) <= Convert.ToInt32(version))
            //    {
            //        result.status = "false";
            //        result.message = "";
            //        result.messageMar = "";
            //        return result;
            //    }
            //    else
            //    {
            //        result.status = "true";
            //        result.message = "";
            //        result.messageMar = "";
            //        return result;
            //    }
            //}
            //else
            //{
            result.status = "false";
            result.message = "";
            result.messageMar = "";
            return result;
            // }


        }
        public List<SBArea> GetArea(int AppId)
        {

            List<SBArea> obj = new List<SBArea>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.sp_area().ToList();

                foreach (var x in data)
                {
                    obj.Add(new SBArea()
                    {
                        area = x.area.Trim(),
                    });
                }

            }
            return obj;

        }


        public List<CMSBZoneVM> GetZone(int AppId, string SearchString)
        {

            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.ZoneMasters.Select(x => new CMSBZoneVM
                {
                    zoneId = x.zoneId,
                    name = x.name
                }).ToList();

                foreach (var item in data)
                {

                    if (item.name == null && item.name == "")
                        item.name = "";
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.name.ToUpper().ToString().Contains(SearchString) || c.name.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.name.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.zoneId).ToList();
            }

        }
        //public List<CMSBCommonVM> GetZone(int AppId, string SearchString)
        //{

        //    using (var db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        var data = db.ZoneMasters.Select(x => new CMSBCommonVM
        //        {
        //            ZoneID = x.zoneId,
        //            Zone = x.name
        //        }).ToList();

        //        foreach (var item in data)
        //        {

        //            if (item.Zone == null && item.Zone == "")
        //                item.Zone = "";
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            var model = data.Where(c => c.Zone.ToUpper().ToString().Contains(SearchString) || c.Zone.ToString().ToLower().ToString().Contains(SearchString) ||
        //             c.Zone.ToString().Contains(SearchString)).ToList();

        //            data = model.ToList();
        //        }
        //        return data.OrderByDescending(c => c.ZoneID).ToList();

        //    }
        //}

        //public List<CMSBCommonVM> GetWard(int AppId, string SearchString)
        //{

        //    using (var db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        var data = db.WardNumbers.Select(x => new CMSBCommonVM
        //        {
        //            WardID = x.Id,
        //            WardNo = x.WardNo,
        //            ZoneID = x.zoneId,
        //            Zonename = db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name,
        //        }).ToList();
        //        foreach (var item in data)
        //        {
        //            item.WardNo = checkNull(item.WardNo);
        //            item.Zonename = checkNull(item.Zonename);
        //        }
        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString) || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.WardNo.ToString().Contains(SearchString) || c.Zonename.ToString().Contains(SearchString) || c.Zonename.ToUpper().ToString().Contains(SearchString) || c.Zonename.ToLower().ToString().Contains(SearchString)).ToList();

        //            data = model.ToList();
        //        }
        //        return data.OrderByDescending(c => c.WardID).ToList();

        //        //List<CMSBCommonVM> obj = new List<CMSBCommonVM>();
        //        //using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //        //{
        //        //    var data = db.WardNumbers.ToList();

        //        //    foreach (var x in data)
        //        //    {
        //        //        obj.Add(new CMSBCommonVM()
        //        //        {
        //        //            WardID = x.Id,
        //        //            WardNo = x.WardNo,
        //        //            ZoneID = Convert.ToUInt16(x.zoneId),
        //        //            Zonename = db.ZoneMasters.Where(v => v.zoneId == x.zoneId).FirstOrDefault().name
        //        //        });
        //        //    }

        //        //}
        //        //return obj;

        //    }
        //}

        public List<CMSBWardVM> GetWard(int AppId, string SearchString)
        {

            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.WardNumbers.Select(x => new CMSBWardVM
                {
                    Id = x.Id,
                    WardNo = x.WardNo,
                    zoneId = x.zoneId,
                    zonename = db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name,
                }).ToList();
                foreach (var item in data)
                {
                    item.WardNo = checkNull(item.WardNo);
                    item.zonename = checkNull(item.zonename);
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString) || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.WardNo.ToString().Contains(SearchString) || c.zonename.ToString().Contains(SearchString) || c.zonename.ToUpper().ToString().Contains(SearchString) || c.zonename.ToLower().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id).ToList();
            }
        }

        public List<SBAUserlocation> GetUserLocation(int AppId)
        {

            List<SBAUserlocation> obj = new List<SBAUserlocation>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.CurrentAllUserLocation().ToList();

                foreach (var x in data)
                {
                    var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && c.daDate == System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(x.datetime) && (c.endTime == null || c.endTime == "")).FirstOrDefault();

                    string v = "";
                    if (atten != null)
                    {
                        v = atten.vehicleNumber;
                    }
                    else
                    {
                        v = "";
                    }

                    obj.Add(new SBAUserlocation()
                    {
                        userName = x.userName,
                        address = x.address,
                        area = x.area,
                        latitude = x.lat,
                        longitude = x.@long,
                        date = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy"),
                        time = Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                        vehcileNumber = v,
                        userMobile = x.userMobileNumber,

                    });
                }

            }
            return obj;

        }

        public List<SBArea> GetCollectionArea(int AppId, int type, string EmpType)
        {
            List<SBArea> obj = new List<SBArea>();

            if (EmpType == "N")
            {
                obj = GetCollectionAreaForNormal(AppId, type);
            }
            if (EmpType == "L")
            {
                obj = GetCollectionAreaForLiquid(AppId, type);
            }
            if (EmpType == "S")
            {
                obj = GetCollectionAreaForStreet(AppId, type);
            }
            return obj;

        }


        public List<SBArea> GetCollectionAreaForNormal(int AppId, int type)
        {
            List<SBArea> obj = new List<SBArea>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.CollecctionArea(type).ToList();

                foreach (var x in data)
                {

                    obj.Add(new SBArea()
                    {
                        id = x.Id,
                        area = checkNull(x.Area).Trim(),
                        areaMar = checkNull(x.AreaMar).Trim()
                    });
                }

            }
            return obj;
        }

        public List<SBArea> GetCollectionAreaForLiquid(int AppId, int type)
        {
            List<SBArea> obj = new List<SBArea>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.CollecctionAreaForLiquid(type).ToList();

                foreach (var x in data)
                {

                    obj.Add(new SBArea()
                    {
                        id = x.Id,
                        area = checkNull(x.Area).Trim(),
                        areaMar = checkNull(x.AreaMar).Trim()
                    });
                }

            }
            return obj;
        }

        public List<SBArea> GetCollectionAreaForStreet(int AppId, int type)
        {
            List<SBArea> obj = new List<SBArea>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.CollecctionAreaForStreet(type).ToList();

                foreach (var x in data)
                {

                    obj.Add(new SBArea()
                    {
                        id = x.Id,
                        area = checkNull(x.Area).Trim(),
                        areaMar = checkNull(x.AreaMar).Trim()
                    });
                }

            }
            return obj;
        }



        public List<HouseDetailsVM> GetAreaHouse(int AppId, int areaId, string EmpType)
        {

            List<HouseDetailsVM> obj = new List<HouseDetailsVM>();
            if (EmpType == "N")
            {
                obj = GetAreaHouseForNormal(AppId, areaId);
            }
            if (EmpType == "L")
            {
                obj = GetAreaHouseForLiquid(AppId, areaId);
            }
            if (EmpType == "S")
            {
                obj = GetAreaHousForStreet(AppId, areaId);
            }

            return obj;

        }

        public List<HouseDetailsVM> GetAreaHouseForNormal(int AppId, int areaId)
        {
            List<HouseDetailsVM> obj = new List<HouseDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.Vw_GetHouseNumber.Where(c => c.AreaId == areaId).ToList();
                if (AppId == 1003)
                {
                    foreach (var x in data)
                    {

                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = x.ReferanceId,
                        });
                    }
                }
                else
                {

                    foreach (var x in data)
                    {
                        string HouseN = "";
                        if (x.houseNumber == null || x.houseNumber == "")
                        {
                            HouseN = x.ReferanceId;
                        }
                        else { HouseN = x.houseNumber; }
                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = HouseN,

                        });
                    }
                }

            }
            return obj;
        }

        public List<HouseDetailsVM> GetAreaHouseForLiquid(int AppId, int areaId)
        {
            List<HouseDetailsVM> obj = new List<HouseDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.Vw_GetLiquidNumber.Where(c => c.AreaId == areaId).ToList();
                if (AppId == 1003)
                {
                    foreach (var x in data)
                    {

                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = x.ReferanceId,
                        });
                    }
                }
                else
                {

                    foreach (var x in data)
                    {
                        string HouseN = "";
                        //if (x.houseNumber == null || x.houseNumber == "")
                        //{
                        //    HouseN = x.ReferanceId;
                        //}
                        //else { HouseN = x.houseNumber; }
                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = x.ReferanceId,

                        });
                    }
                }

            }
            return obj;
        }

        public List<HouseDetailsVM> GetAreaHousForStreet(int AppId, int areaId)
        {
            List<HouseDetailsVM> obj = new List<HouseDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.Vw_GetStreetNumber.Where(c => c.AreaId == areaId).ToList();
                if (AppId == 1003)
                {
                    foreach (var x in data)
                    {

                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = x.ReferanceId,
                        });
                    }
                }
                else
                {

                    foreach (var x in data)
                    {
                        string HouseN = "";
                        //if (x.houseNumber == null || x.houseNumber == "")
                        //{
                        //    HouseN = x.ReferanceId;
                        //}
                        //else { HouseN = x.houseNumber; }
                        obj.Add(new HouseDetailsVM()
                        {
                            houseid = x.ReferanceId,
                            houseNumber = x.ReferanceId,

                        });
                    }
                }

            }
            return obj;
        }


        public List<GarbagePointDetailsVM> GetAreaPoint(int AppId, int areaId)
        {

            List<GarbagePointDetailsVM> obj = new List<GarbagePointDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.GarbagePointDetails.Where(c => c.areaId == areaId).ToList();

                foreach (var x in data)
                {
                    string HouseN = "";
                    if (x.gpName == null || x.gpName == "")
                    {
                        HouseN = x.ReferanceId;
                    }
                    else { HouseN = x.gpName; }
                    obj.Add(new GarbagePointDetailsVM()
                    {
                        gpId = x.ReferanceId,
                        gpName = HouseN,

                    });
                }

            }
            return obj;

        }

        //Added By Saurabh (26 Apr 2019)
        public List<DumpYardPointDetailsVM> GetDumpYardArea(int AppId, int areaId)
        {

            List<DumpYardPointDetailsVM> obj = new List<DumpYardPointDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.DumpYardDetails.Where(c => c.areaId == areaId).ToList();

                foreach (var x in data)
                {
                    string HouseN = "";
                    if (x.dyName == null || x.dyName == "")
                    {
                        HouseN = x.ReferanceId;
                    }
                    else { HouseN = x.dyName; }
                    obj.Add(new DumpYardPointDetailsVM()
                    {
                        dyId = x.ReferanceId,
                        dyName = HouseN,

                    });
                }

            }
            return obj;

        }
        public Result SendSMSToHOuse(int area, int AppId)
        {

            Result res = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {

                //var data = db.HouseMasters.Where(c => c.AreaId == area).ToList();
                //foreach (var x in data)
                //{
                //    string msg;
                //    if (AppId == 1)
                //    {
                //        msg = "नमस्कार! घंटागाडीचे आगमन आपल्या क्षेत्रात झालेले आहे. आपल्या घरातून लवकरच कचरा संकलित करण्यात येईल. आपणास विनंती आहे कि कृपया आपल्या घरातील ओला व सुका असा वर्गीकृत कचरा आमच्या सफाई सेवकास सुपूर्द करावा. कचरा संकलन न झाल्यास कृपया खालील दिलेल्या क्रमांकावर संपर्क करून तक्रार/सूचना नोंदवावी. धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";
                //    }
                //    else {
                //        msg = "नमस्कार! घंटागाडीचे आगमन आपल्या क्षेत्रात झालेले आहे. आपल्या घरातून लवकरच कचरा संकलित करण्यात येईल. आपणास विनंती आहे कि कृपया आपल्या घरातील ओला व सुका असा वर्गीकृत कचरा आमच्या सफाई सेवकास सुपूर्द करावा. धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";
                //    }
                //    sendSMS(msg, x.houseOwnerMobile);

                //}

                string mob = db.GetMobile(Convert.ToInt32(area)).FirstOrDefault().mob;

                string msg, msgMar;



                if (appdetails.LanguageId == 3)
                {
                    //  msg = "प्रिय नागरिक, कन्नड नगरपरिषदेची घंटागाडी १५ मिनिटाच्या आत आपल्या घरासमोर कचरा संकलनासाठी येत आहे. तरी आपण ओला व सुका कचरा वेगवेगळा करून गाडीत टाकावा. आपली सौ स्वातीताई संतोषभाऊ कोल्हे" + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";
                    msg = "" + appdetails.MsgForBroadcast + " " + appdetails.yoccContact + " आपकी सेवा में " + appdetails.AppName_mar + "|";
                    sendSMSmar(msg, mob);
                }
                if (appdetails.LanguageId == 4)
                {
                    //  msg = "प्रिय नागरिक, कन्नड नगरपरिषदेची घंटागाडी १५ मिनिटाच्या आत आपल्या घरासमोर कचरा संकलनासाठी येत आहे. तरी आपण ओला व सुका कचरा वेगवेगळा करून गाडीत टाकावा. आपली सौ स्वातीताई संतोषभाऊ कोल्हे" + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";
                    msg = "" + appdetails.MsgForBroadcast + " " + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";
                    sendSMSmar(msg, mob);
                }
                else if (AppId == 1 && appdetails.LanguageId == 4)
                {
                    // msgMar = "नमस्कार! घंटागाडीचे आगमन आपल्या क्षेत्रात झालेले आहे. आपल्या घरातून लवकरच कचरा संकलित करण्यात येईल. आपणास विनंती आहे कि कृपया आपल्या घरातील ओला व सुका असा वर्गीकृत कचरा आमच्या सफाई सेवकास सुपूर्द करावा. कचरा संकलन न झाल्यास कृपया खालील दिलेल्या क्रमांकावर संपर्क करून तक्रार/सूचना नोंदवावी. धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";

                    //msg = "Dear citizen, garbage collection van will arrive in your area very shortly. Kindly keep the waste in dry and wet segregated form ready and submit it to the sanitary worker. Thank you " + appdetails.AppName + ".";

                    //   msg= "Dear citizen, garbage collection van will arrive in your area very shortly.Kindly keep the dry and wet segregated waste ready & submit it to the sanitary worker.Thank you " + appdetails.AppName + ".";

                    msg = "" + appdetails.MsgForBroadcast + " " + appdetails.AppName + ".";
                    sendSMS(msg, mob);
                }


                else
                {
                    // msgMar = "नमस्कार! घंटागाडीचे आगमन आपल्या क्षेत्रात झालेले आहे. आपल्या घरातून लवकरच कचरा संकलित करण्यात येईल. आपणास विनंती आहे कि कृपया आपल्या घरातील ओला व सुका असा वर्गीकृत कचरा आमच्या सफाई सेवकास सुपूर्द करावा. धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी" + appdetails.AppName_mar + ".";

                    //  msg = "Dear citizen, garbage collection van will arrive in your area very shortly.Kindly keep the dry and wet segregated waste ready & submit it to the sanitary worker.Thank you " + appdetails.AppName + ".";
                    if (appdetails.LanguageId == 1)
                    {
                        msg = "" + appdetails.MsgForBroadcast + " " + appdetails.AppName + ".";
                    }
                    else
                    {
                        msg = "" + appdetails.MsgForBroadcast + " " + appdetails.AppName + ".";
                        sendSMS(msg, mob);
                    }
                }


                var FCM = from h in db.HouseMasters
                          join d in db.DeviceDetails on h.ReferanceId equals d.ReferenceID
                          select new { FCMID = d };

                //var FCM = db.HouseMasters
                //  .Where(y => y.AreaId == area & y.FCMID != null)
                //  .ToList();

                List<String> ArrayList = new List<String>();

                foreach (var x in FCM)
                {
                    ArrayList.Add(x.FCMID.FCMID);
                }

                PushNotificationMessageBroadCast(msg, ArrayList, appdetails.AppName, appdetails.Android_GCM_pushNotification_Key);

            }
            res.status = "success";
            return res;


        }

        public Result CheckAttendence(int userId, int AppId, int typeId)
        {

            Daily_Attendance objatten = new Daily_Attendance();
            Qr_Employee_Daily_Attendance objqratten = new Qr_Employee_Daily_Attendance();
            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {

                if (typeId == 0)
                {
                    objatten = db.Daily_Attendance.Where(c => c.userId == userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                }

                else
                {
                    objqratten = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == userId & c.endTime == "" & c.startDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                }
                if (objatten == null || objqratten == null)
                {
                    result.isAttendenceOff = true;
                    result.message = "Your duty is currently off, please start again.. ";
                    result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                    result.status = "success";
                    return result;
                }
                else
                {
                    result.isAttendenceOff = false;
                    result.message = "";
                    result.messageMar = "";
                    result.status = "success";
                    return result;

                }
            }

            // return result;


        }

        //Added By Saurabh (CMS)
        public SBDashboardVM GetDashboard(int appId)
        {
            SBDashboardVM model = new SBDashboardVM();
            try
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
                {

                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();
                    //List<ComplaintVM> obj = new List<ComplaintVM>();
                    //if (appId == 1)
                    //{
                    //    string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=1");
                    //    obj = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).Where(c => Convert.ToDateTime(c.createdDate2).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
                    //}

                    var data = db.SP_Dashboard_Details().First();
                    if (data != null)
                    {

                        model.Attendance = data.TotalAttandence;
                        model.HouseCollection = data.TotalHouse;
                        model.PointCollection = data.TotalPoint;
                        model.DumpingYardCollection = data.TotalDump;
                        // model.TotalComplaint = obj.Count;
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

        public List<AttendanceGridVM> GetAdminAttendence(int appId, DateTime fdate, DateTime tdate, int UserId, int Offset, int Fetch_Next, string SearchString)
        {

            List<AttendanceGridVM> obj = new List<AttendanceGridVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.Daily_Attendance.OrderByDescending(c => c.daID).ToList();

                if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                {
                    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();
                    //data = data.Where(c => (c.daDate == fdate || c.daEndDate == tdate || c.endTime == "")).ToList();
                }
                else
                {
                    data = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                }
                foreach (var x in data)
                {
                    int a = Convert.ToInt32(x.vtId.Trim());
                    string vt = "";
                    try { vt = db.VehicleTypes.Where(c => c.vtId == a).FirstOrDefault().description; }
                    catch { vt = ""; }
                    ///x.daDate = checkNull(x.daDate.tp);
                    x.endLat = checkNull(x.endLat);
                    x.endLong = checkNull(x.endLong);
                    x.endTime = checkNull(x.endTime);
                    x.startLat = checkNull(x.startLat);
                    x.startLong = checkNull(x.startLong);
                    x.startTime = checkNull(x.startTime);
                    x.vehicleNumber = checkNull(x.vehicleNumber);
                    x.daEndNote = checkNull(x.daEndNote);
                    x.daStartNote = checkNull(x.daStartNote);
                    string endate = "", daDate = "";
                    if (x.daEndDate == null)
                    {
                        endate = "";
                    }
                    else
                    {
                        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    }

                    obj.Add(new AttendanceGridVM()
                    {
                        daID = x.daID,
                        userId = Convert.ToInt32(x.userId),
                        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                        daEndDate = endate,
                        startTime = x.startTime,
                        endTime = x.endTime,
                        startLat = x.startLat,
                        startLong = x.startLong,
                        endLat = x.startLong,
                        endLong = x.endLong,
                        vtId = vt,
                        vehicleNumber = x.vehicleNumber,
                        CompareDate = x.daDate,
                    });
                }


                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    obj = model.ToList();
                }



                if (UserId > 0)
                {
                    var model = obj.Where(c => c.userId == UserId).ToList();

                    obj = model.ToList();

                }
                return obj.OrderByDescending(c => c.daID).Skip(Fetch_Next).Take(Offset).ToList();


            }
            return obj;

        }

        public List<AHouseGarbageCollectionVM> GetHouseGarbageCollectionData(int appId, int userId, DateTime fdate, DateTime tdate, int gcType, int Offset, int Fetch_Next, string SearchString)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

            List<AHouseGarbageCollectionVM> obj = new List<AHouseGarbageCollectionVM>();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
            string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_GarbageCollection(appId, userId, fdate, tdate, null, null, null).Select(x => new AHouseGarbageCollectionVM
                {
                    Id = x.gcId,
                    userId = x.userId,
                    houseId = x.houseId,
                    UserName = x.houseOwner,
                    HouseNumber = db.HouseMasters.Where(c => c.houseId == x.houseId).Select(c => c.houseNumber).FirstOrDefault(),//
                    gcDate = x.gcDate,
                    gcType = 1,
                    type1 = x.garbageType.ToString(),
                    Address = x.locAddresss,
                    gpBeforImage = x.gpBeforImage,
                    gpAfterImage = x.gpAfterImage,
                    VehicleNumber = x.vehicleNumber,
                    Note = x.note,
                    ReferanceId = x.ReferanceId,
                    Employee = x.userName,
                    attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                    gpIdfk = x.gcId,
                    gpIdpk = x.gcId,
                    batterystatus = x.batteryStatus,


                }).OrderByDescending(c => c.gcDate).ToList().ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

                    //   || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

                    //   || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
                    //   ).ToList();

                    var model = data.Where(c => ((c.UserName == null ? " " : c.UserName) + " " + (c.HouseNumber == null ? " " : c.HouseNumber) + " " + (c.VehicleNumber == null ? " " : c.VehicleNumber) + " " + (c.ReferanceId == null ? "" : c.ReferanceId) + " " + (c.Address == null ? " " : c.Address) + " " + (c.Employee == null ? " " : c.Employee) + " " + (c.attandDate == null ? " " : c.attandDate) + " " + (c.Note == null ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())
                       ).ToList();

                    data = model.ToList();
                }


                if (userId > 0)
                {
                    var model = data.Where(c => c.userId == userId).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.gcDate).Skip(Fetch_Next).Take(Offset).ToList(); ;
            }
        }
        //public IEnumerable<SBAGrabageCollectionGridRow> GetHouseGarbageCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        //{
        //    DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        //    var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
        //    string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
        //    {

        //        var data = db.SP_GarbageCollection(appId, userId, fdate, tdate).Select(x => new SBAGrabageCollectionGridRow
        //        {
        //            Id = x.gcId,
        //            userId = x.userId,
        //            houseId = x.houseId,
        //            UserName = x.houseOwner,
        //            HouseNumber = x.houseOwner,
        //            gcDate = x.gcDate,
        //            gcType = 1,
        //            type1 = x.garbageType.ToString(),
        //            Address = x.locAddresss,
        //            gpBeforImage = x.gpBeforImage,
        //            gpAfterImage = x.gpAfterImage,
        //            VehicleNumber = x.vehicleNumber,
        //            Note = x.note,
        //            ReferanceId = x.ReferanceId,
        //            Employee = x.userName,
        //            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
        //            gpIdfk = x.gcId,
        //            gpIdpk = x.gcId,


        //        }).OrderByDescending(c => c.gcDate).ToList().ToList();

        //        if (!string.IsNullOrEmpty(SearchString))
        //        {
        //            var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

        //               || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

        //               || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
        //               ).ToList();



        //            data = model.ToList();
        //        }

        //        if (userId > 0)
        //        {
        //            var model = data.Where(c => c.userId == userId).ToList();

        //            data = model.ToList();
        //        }
        //        return data;
        //    }
        //}


        // Added By Saurabh (26 Apr 2019)

        public CollectionResult SaveDumpYardCollection(SBGarbageCollectionView obj, int AppId, int type, string batteryStatus)
        {
            CollectionResult result = new CollectionResult();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                // GarbageCollectionDetail gcd = new GarbageCollectionDetail();
                string name = "", housemob = "", nameMar = "", addre = "";
                var distCount = "";
                DateTime newTime = DateTime.Now;
                DateTime oldTime;
                TimeSpan span = TimeSpan.Zero;
                var dydetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                //var dyId = dydetails.dyId; || tdate.AddMinutes(15) >= gcd.gcDate

                try
                {
                    var gcd = db.GarbageCollectionDetails.Where(c => c.userId == obj.userId && c.dyId == dydetails.dyId && EntityFunctions.TruncateTime(c.gcDate) == EntityFunctions.TruncateTime(DateTime.Now)).OrderByDescending(c => c.gcDate).FirstOrDefault();
                    if (gcd != null)
                    {
                        oldTime = gcd.gcDate.Value;
                        span = newTime.Subtract(oldTime);
                    }
                    // UserId == 0 ? null : UserId
                    if (gcd == null || span.Minutes >= 10)
                    {
                        GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        objdata.userId = obj.userId;
                        objdata.gcDate = DateTime.Now;
                        objdata.Lat = obj.Lat;
                        objdata.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

                        var locc = db.SP_UserLatLongDetail(objdata.userId, 0).FirstOrDefault();
                        if (locc == null || locc.lat == "" || locc.@long == "")
                        {
                            string a = objdata.Lat;
                            string b = objdata.Long;

                            var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
                            distCount = dist.Distance_in_KM.ToString();
                        }
                        else
                        {

                            var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(objdata.Lat), Convert.ToDouble(objdata.Long)).FirstOrDefault();
                            distCount = dist.Distance_in_KM.ToString();
                        }

                        // var abc = dist;
                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.name = "";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                objdata.dyId = gpdetails.dyId;
                                //objdata.houseId = 0;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);
                            }
                            catch
                            {
                                result.message = "Invalid dyId"; result.messageMar = "अवैध जीपी आयडी"; result.name = "";
                                result.status = "error";
                                return result;
                            }

                        }

                        objdata.gcType = obj.gcType;
                        objdata.gpBeforImage = obj.gpBeforImage;
                        objdata.gpAfterImage = obj.gpAfterImage;
                        objdata.note = checkNull(obj.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        objdata.vehicleNumber = checkNull(obj.vehicleNumber);
                        objdata.totalGcWeight = obj.totalGcWeight;
                        objdata.totalDryWeight = obj.totalDryWeight;
                        objdata.totalWetWeight = obj.totalWetWeight;
                        objdata.batteryStatus = obj.batteryStatus; ;
                        objdata.Distance = Convert.ToDouble(distCount);


                        if (AppId == 1010)
                        {
                            objdata.locAddresss = Address(objdata.Lat + "," + objdata.Long);
                        }
                        else
                        {
                            objdata.locAddresss = addre;
                        }

                        objdata.CreatedDate = DateTime.Now;

                        db.GarbageCollectionDetails.Add(objdata);

                        Location loc = new Location();
                        loc.datetime = DateTime.Now;
                        loc.lat = objdata.Lat;
                        loc.@long = objdata.Long;
                        loc.address = objdata.locAddresss;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = objdata.batteryStatus;
                        if (objdata.locAddresss != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = objdata.userId;
                        loc.type = 1;

                        loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        loc.Distnace = Convert.ToDecimal(distCount);

                        db.Locations.Add(loc);

                        db.SaveChanges();
                        result.name = name;
                        result.nameMar = nameMar;
                        result.mobile = housemob;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        if (housemob != "")
                        {
                            sendSMS(mes, housemob);
                        }
                    }
                    else
                    {
                        // GarbageCollectionDetail objdata = new GarbageCollectionDetail();
                        gcd.userId = obj.userId;
                        gcd.gcDate = DateTime.Now;
                        gcd.Lat = obj.Lat;
                        gcd.Long = obj.Long;
                        var atten = db.Daily_Attendance.Where(c => c.userId == obj.userId & c.endTime == "" & c.daDate == EntityFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

                        if (atten == null)
                        {
                            result.isAttendenceOff = true;
                            result.message = "Your duty is currently off, please start again.. ";
                            result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                            result.name = "";
                            result.status = "success";
                            return result;
                        }
                        else { result.isAttendenceOff = false; }

                        if (obj.dyId != null && obj.dyId != "")
                        {
                            try
                            {
                                var gpdetails = db.DumpYardDetails.Where(c => c.ReferanceId == obj.dyId).FirstOrDefault();
                                gcd.dyId = gpdetails.dyId;
                                //objdata.houseId = 0;
                                name = gpdetails.dyName;
                                nameMar = checkNull(gpdetails.dyNameMar);
                                housemob = "";
                                addre = checkNull(gpdetails.dyAddress);
                            }
                            catch
                            {
                                result.message = "Invalid dyId"; result.messageMar = "अवैध डीवाय आयडी"; result.name = "";
                                result.status = "error";
                                return result;
                            }

                        }
                        gcd.gcType = obj.gcType;
                        gcd.gpBeforImage = obj.gpBeforImage;
                        gcd.gpAfterImage = obj.gpAfterImage;
                        gcd.note = checkNull(gcd.note);
                        //objdata.garbageType = checkIntNull(obj.garbageType.ToString());
                        gcd.vehicleNumber = checkNull(gcd.vehicleNumber);
                        gcd.totalGcWeight = obj.totalGcWeight;
                        gcd.totalDryWeight = obj.totalDryWeight;
                        gcd.totalWetWeight = obj.totalWetWeight;
                        gcd.batteryStatus = obj.batteryStatus; ;
                        // objdata.Distance = Convert.ToDouble(distCount);

                        Location loc = new Location();
                        loc.datetime = DateTime.Now;
                        loc.lat = obj.Lat;
                        loc.@long = obj.Long;
                        loc.address = addre;//Address(objdata.Lat + "," + objdata.Long);
                        loc.batteryStatus = obj.batteryStatus;
                        if (addre != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        loc.userId = obj.userId;
                        loc.type = 1;

                        loc.IsOffline = obj.IsOffline;

                        if (!string.IsNullOrEmpty(obj.dyId))
                        {
                            loc.ReferanceID = obj.dyId;
                        }

                        loc.CreatedDate = DateTime.Now;
                        db.Locations.Add(loc);

                        db.SaveChanges();
                        result.name = name;
                        result.nameMar = nameMar;
                        result.mobile = housemob;
                        result.status = "success";
                        result.message = "Uploaded successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        string mes = "नमस्कार! आपल्या घरून कचरा संकलित करण्यात आलेला आहे. कृपया ओला व सुका असा वर्गीकृत केलेला कचरा सफाई कर्मचाऱ्यास सुपूर्द करून सहकार्य करावे धन्यवाद. " + appdetails.yoccContact + " आपल्या सेवेशी " + appdetails.AppName_mar + "";
                        if (housemob != "")
                        {
                            sendSMS(mes, housemob);
                        }
                    }
                    return result;

                }

                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.name = "";
                    result.status = "error";
                    return result;
                }
            }
        }

        //Added By Nishikant (03 May 2019)
        public List<CMSBPointGarbageCollectionVM> GetPointGarbageCollectionData(int appId, int userId, DateTime fdate, DateTime tdate, int gcType, int Offset, int Fetch_Next, string SearchString)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
                {
                    var data = db.GarbageCollectionDetails.Where(x => x.gcType == gcType & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new CMSBPointGarbageCollectionVM
                    {
                        Id = x.gcId,
                        Note = x.note,
                        gpAfterImage = x.gpAfterImage,
                        gpBeforImage = x.gpBeforImage,
                        gcType = x.gcType,
                        houseId = x.houseId,
                        gpIdfk = x.gpId,
                        userId = x.userId,
                        gcDate = x.gcDate,
                        VehicleNumber = x.vehicleNumber,
                    }).ToList();

                    foreach (var item in data)
                    {
                        var gcdata = db.GarbageCollectionDetails.Where(c => c.gcId == item.Id).FirstOrDefault();

                        item.ReferanceId = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().ReferanceId;

                        item.Employee = db.UserMasters.Where(c => c.userId == item.userId).FirstOrDefault().userName;
                        item.attandDate = Convert.ToDateTime(item.gcDate).ToString("dd/MM/yyyy hh:mm tt");

                        item.UserName = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().gpName;

                        item.HouseNumber = checkNull(item.HouseNumber);
                        item.VehicleNumber = checkNull(item.VehicleNumber);
                        item.Note = checkNull(item.Note);
                        if (gcdata.Lat != null && gcdata.Long != "" && gcdata.Lat != "" && gcdata.Long != null)
                        { item.Address = gcdata.locAddresss; }
                        else { item.Address = ""; }
                        item.Employee = checkNull(item.Employee);
                        item.UserName = checkNull(item.UserName);


                        if (item.gpAfterImage == "")
                        //{ item.gpAfterImage = "/Images/default_not_upload.png"; }
                        { item.gpAfterImage = ""; }
                        else
                        {
                            item.gpAfterImage = ThumbnaiUrlAPI + item.gpAfterImage.Trim();
                        }
                        if (item.gpBeforImage == "")
                        { item.gpBeforImage = ""; }
                        //{ item.gpBeforImage = "/Images/default_not_upload.png"; }
                        else
                        { item.gpBeforImage = ThumbnaiUrlAPI + item.gpBeforImage.Trim(); }

                        item.UserName = checkNull(item.UserName);
                        item.HouseNumber = checkNull(item.HouseNumber);
                        item.VehicleNumber = checkNull(item.VehicleNumber);
                        item.Employee = checkNull(item.Employee);
                        item.Address = checkNull(item.Address);
                        item.Note = checkNull(item.Note);
                        item.ReferanceId = checkNull(item.ReferanceId);


                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

                        || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

                        || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
                        ).ToList();

                        data = model.ToList();
                    }

                    if (userId > 0)
                    {
                        var model = data.Where(c => c.userId == userId).ToList();

                        data = model.ToList();
                    }
                    //var datatest = db.GarbageCollectionDetails.OrderBy(g => g.gcId).Skip(10).Take(15).ToList();

                    return data.OrderByDescending(g => g.gcDate).Skip(Fetch_Next).Take(Offset).ToList();
                }
            }
        }


        //Added By Nishikant (11 May 2019)
        public List<CMSBGrabageCollectionVM> GetDumpYardCollectionData(int appId, int empId, DateTime fdate, DateTime tdate, int gcType, int Offset, int Fetch_Next, string SearchString)
        {
            {

                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
                {
                    var data = db.GarbageCollectionDetails.Where(x => x.gcType == 3 & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new CMSBGrabageCollectionVM
                    {
                        Id = x.gcId,
                        Note = x.note,
                        gpAfterImage = x.gpAfterImage,
                        gpBeforImage = x.gpBeforImage,
                        gcType = x.gcType,
                        houseId = x.houseId,
                        dyIdfk = x.dyId,
                        userId = x.userId,
                        gcDate = x.gcDate,
                        VehicleNumber = x.vehicleNumber,
                        totalGcWeight = x.totalGcWeight,
                        totalDryWeight = x.totalDryWeight,
                        totalWetWeight = x.totalWetWeight,
                        batteryStatus = x.batteryStatus

                    }).ToList();

                    foreach (var item in data)
                    {
                        var gcdata = db.GarbageCollectionDetails.Where(c => c.gcId == item.Id).FirstOrDefault();
                        item.ReferanceId = db.DumpYardDetails.Where(c => c.dyId == item.dyIdfk).FirstOrDefault().ReferanceId;

                        item.Employee = db.UserMasters.Where(c => c.userId == item.userId).FirstOrDefault().userName;
                        item.attandDate = Convert.ToDateTime(item.gcDate).ToString("dd/MM/yyyy hh:mm tt");
                        item.UserName = db.DumpYardDetails.Where(c => c.dyId == item.dyIdfk).FirstOrDefault().dyName;

                        item.HouseNumber = checkNull(item.HouseNumber);
                        item.VehicleNumber = checkNull(item.VehicleNumber);
                        item.Note = checkNull(item.Note);
                        if (gcdata.Lat != null && gcdata.Long != "" && gcdata.Lat != "" && gcdata.Long != null)
                        { item.Address = gcdata.locAddresss; }
                        else { item.Address = ""; }
                        item.Employee = checkNull(item.Employee);
                        item.UserName = checkNull(item.UserName);


                        if (item.gpAfterImage == "")
                        { item.gpAfterImage = ""; }
                        else
                        {
                            item.gpAfterImage = ThumbnaiUrlAPI + item.gpAfterImage.Trim();
                        }
                        if (item.gpBeforImage == "")
                        { item.gpBeforImage = ""; }
                        else
                        { item.gpBeforImage = ThumbnaiUrlAPI + item.gpBeforImage.Trim(); }
                        item.UserName = checkNull(item.UserName);
                        item.HouseNumber = checkNull(item.HouseNumber);
                        item.VehicleNumber = checkNull(item.VehicleNumber);
                        item.Employee = checkNull(item.Employee);
                        item.Address = checkNull(item.Address);
                        item.Note = checkNull(item.Note);
                        item.ReferanceId = checkNull(item.ReferanceId);

                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

                        || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

                        || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
                        ).ToList();

                        data = model.ToList();
                    }


                    if (empId > 0)
                    {
                        var model = data.Where(c => c.userId == empId).ToList();

                        data = model.ToList();
                    }
                    return data.OrderByDescending(g => g.gcDate).Skip(Fetch_Next).Take(Offset).ToList();

                }
            }
        }


        //Added By Nishikant (04 May 2019)
        public List<CMSBLocationVM> GetLocationData(int appId, int userId, DateTime fdate, DateTime tdate, int Offset, int Fetch_Next, string SearchString)
        {

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                List<CMSBLocationVM> data = new List<CMSBLocationVM>();

                //var data1 = db.Locations.Where(c => c.datetime >= fdate && c.datetime <= tdate).OrderByDescending(c => c.locId).ToList();

                //foreach (var x in data1.Where(c => c.datetime >= fdate && c.datetime <= tdate))
                //{
                //    string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");

                //    string t = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");

                //    string Lat = x.lat;

                //    string Long = x.@long;

                //    var atten = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.daDate == EntityFunctions.TruncateTime(x.datetime) && (c.endTime == "" || c.endTime == null))).FirstOrDefault();
                //    string v = "";
                //    if (atten != null)
                //    {
                //        v = atten.vehicleNumber;
                //    }
                //    else { v = ""; }

                //    //v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                //    var _UserMasters = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault();

                //    data.Add(new CMSBLocationVM
                //    {
                //        locId = x.locId,

                //        userName = _UserMasters.userName,//db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                //        userId = Convert.ToInt32(x.userId),
                //        date = dat,
                //        time = t,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                //        lat = Lat,
                //        longe = Long,
                //        Address = x.address,
                //        CompareDate = x.datetime,
                //        userMobile = _UserMasters.userMobileNumber,//db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userMobileNumber,
                //        vehicleNumber = v,
                //    });


                //}


                var data1 = db.sp_Locationdata(fdate, tdate).ToList();
                foreach (var x in data1)
                {
                    data.Add(new CMSBLocationVM
                    {
                        locId = x.locId,

                        userName = x.userName,//db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                        userId = Convert.ToInt32(x.userId),
                        date = x.date,
                        time = x.times,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                        lat = x.lat,
                        longe = x.@long,
                        Address = x.address,
                        CompareDate = x.CompareDate,
                        userMobile = x.userMobile,//db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userMobileNumber,
                        vehicleNumber = x.vehicleNumber
                    });
                }
                foreach (var item in data)
                {
                    if (item.userName != null && item.userName == "")
                        item.userName = "";
                    item.lat = checkNull(item.lat);
                    item.longe = checkNull(item.longe);
                    item.date = checkNull(item.date);
                    item.time = checkNull(item.time);
                    item.Address = checkNull(item.Address);
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.userName.Contains(SearchString) || c.date.Contains(SearchString) || c.time.Contains(SearchString) || c.Address.Contains(SearchString) || c.lat.Contains(SearchString) || c.longe.Contains(SearchString)

                    || c.userName.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.time.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.lat.ToLower().Contains(SearchString) || c.longe.ToLower().Contains(SearchString)

                    || c.userName.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.time.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.lat.ToUpper().Contains(SearchString) || c.longe.ToUpper().Contains(SearchString)
                    ).ToList();

                    data = model.ToList();
                }

                if (userId > 0)
                {
                    var model = data.Where(c => c.userId == userId).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(l => l.date).Skip(Fetch_Next).Take(Offset).ToList();
                //return data;
            }

        }


        //Added By Nishikant (04 May 2019)
        public List<CMSBUserLocationMapVM> GetUserWiseLocation(int appId, int userId, string date, int _Type)
        {
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                List<CMSBUserLocationMapVM> userLocation = new List<CMSBUserLocationMapVM>();

                if (_Type == 0 && userId == 0)
                {
                    var data = db.CurrentAllUserLocation().ToList();

                    foreach (var x in data)
                    {
                        var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && c.daDate == System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(x.datetime) && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        string v = "";
                        //if (atten != null)
                        //{
                        //    v = atten.vehicleNumber;
                        //}

                        v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                        userLocation.Add(new CMSBUserLocationMapVM()
                        {
                            userId = Convert.ToInt32(x.userid),
                            userName = x.userName,
                            address = x.address,
                            //area = x.area,
                            lat = x.lat,//latitude = x.lat,
                            log = x.@long,//longitude = x.@long,
                            date = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy"),
                            time = Convert.ToDateTime(x.datetime).ToString("hh:mm tt"),
                            vehicleNumber = v,//vehcileNumber = v,
                            userMobile = x.userMobileNumber,
                        });
                    }

                }
                else if (_Type == 0 && userId > 0)
                {
                    var data = db.CurrentAllUserLocationUserIDWise(userId).ToList();

                    foreach (var x in data)
                    {
                        var atten = db.Daily_Attendance.Where(c => c.userId == x.userid && c.daDate == System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(x.datetime) && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                        string v = "";
                        //if (atten != null)
                        //{
                        //    v = atten.vehicleNumber;
                        //}

                        v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                        userLocation.Add(new CMSBUserLocationMapVM()
                        {
                            userId = userId,
                            userName = x.userName,
                            address = x.address,
                            //area = x.area,
                            lat = x.lat,//latitude = x.lat,
                            log = x.@long,//longitude = x.@long,
                            date = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy"),
                            time = Convert.ToDateTime(x.datetime).ToString("hh:mm tt"),
                            vehicleNumber = v,//vehcileNumber = v,
                            userMobile = x.userMobileNumber,
                        });
                    }


                }
                else if (_Type == 1 && userId > 0)
                {

                    DateTime newdate = DateTime.Now.Date;
                    var datt = newdate;

                    //var data = db.Locations.Where(c => c.userId == userId || c.userId > 0).ToList();
                    var data = db.Locations.Where(c => c.userId == userId && EntityFunctions.TruncateTime(c.datetime) == EntityFunctions.TruncateTime(datt)).ToList();

                    //foreach (var x in data.Where(c => Convert.ToDateTime(c.datetime).Date == Convert.ToDateTime(date).Date))
                    foreach (var x in data)
                    {
                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");
                        string tim = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");
                        var userName = db.UserMasters.Where(c => c.userId == userId).FirstOrDefault();
                        var atten = db.Daily_Attendance.Where(c => c.userId == x.userId && (c.daDate == EntityFunctions.TruncateTime(x.datetime) && (c.endTime == "" || c.endTime == null))).FirstOrDefault();
                        string v = "";
                        //if (atten != null)
                        //{
                        //    v = atten.vehicleNumber;
                        //}
                        //else { v = ""; }

                        v = (string.IsNullOrEmpty(atten.vehicleNumber) ? "" : atten.vehicleNumber);

                        userLocation.Add(new CMSBUserLocationMapVM()
                        {
                            userId = userId,
                            userName = userName.userName,
                            date = dat,
                            time = tim,
                            lat = x.lat,
                            log = x.@long,
                            address = x.address,
                            vehicleNumber = v,
                            userMobile = userName.userMobileNumber,
                        });
                    }

                    if (userId > 0)
                    {
                        var model = userLocation.Where(c => c.userId == userId).ToList();
                        userLocation = model.ToList();
                    }

                }
                return userLocation.OrderByDescending(ul => ul.date).ToList();
            }

        }

        //Added By Nishikant (06 May 2019)
        public List<CMSBHouseDetailsVM> GetHouseDetailsData(int appId, int Offset, int Fetch_Next, string SearchString)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.HouseDetails().Select(x => new CMSBHouseDetailsVM
                {
                    houseId = x.houseId,
                    WardNo = x.Ward,
                    Area = x.Area,
                    zone = x.Zone,
                    Address = x.Address,
                    houseNo = x.HouseNumber,
                    Mobile = x.MobileNumber,
                    Name = x.Name,
                    QRCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    ReferanceId = x.ReferanceId,
                    houseOwner = x.Name,
                    houseOwnerMar = x.NameMar,
                    houseLat = x.houseLat,
                    houseLong = x.houseLong

                }).ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString)
                    || c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString) || c.houseNo.ToUpper().ToString().Contains(SearchString) || c.Mobile.ToUpper().ToString().Contains(SearchString) || c.zone.ToString().ToUpper().ToString().Contains(SearchString) || c.Address.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)
                     || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.zone.ToString().ToLower().ToString().Contains(SearchString)
                    || c.Area.ToString().ToLower().ToString().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) || c.houseNo.ToString().ToLower().ToString().Contains(SearchString) || c.Mobile.ToString().ToLower().ToString().Contains(SearchString) || c.Address.ToString().ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)
                    || c.WardNo.ToString().Contains(SearchString) || c.zone.ToString().Contains(SearchString)
                    || c.Area.ToString().Contains(SearchString) || c.Name.ToString().Contains(SearchString)
                    || c.houseNo.ToString().Contains(SearchString) || c.Mobile.ToString().Contains(SearchString)
                    || c.Address.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString) || c.QRCode.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }

                return data.OrderByDescending(h => h.houseId).Skip(Fetch_Next).Take(Offset).ToList();
            }
        }

        //Added By Nishikant (11 May 2019)

        public List<CMSBEmployee> GetEmployee(int AppId)
        {

            List<CMSBEmployee> obj = new List<CMSBEmployee>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.UserMasters.Where(x => x.isActive == true).ToList();

                foreach (var x in data)
                {
                    obj.Add(new CMSBEmployee()
                    {
                        userId = x.userId,
                        userName = x.userName,
                    });
                }

            }
            return obj.OrderBy(c => c.userName).ToList();

        }

        //Added By Nishikant (10 May 2019)
        public Result SaveHouse(CMSBHouseDetailsVM data, int _AppId, int _HouseId)
        {
            screenService = new ScreenService(_AppId);
            Result result = new Result();
            if (_HouseId == 0 || string.IsNullOrEmpty(_HouseId.ToString()))
            {
                data.houseId = 0;
            }
            Result dd = screenService.SaveHouseDetails(data, _AppId, _HouseId);

            return dd;
        }


        public Result SaveGarbagePoint(CMSBGarbagePointDetailsVM data, int _AppId, int gpId)
        {
            screenService = new ScreenService(_AppId);
            Result result = new Result();
            if (gpId == 0 || string.IsNullOrEmpty(gpId.ToString()))
            {
                data.gpId = 0;
            }
            Result dd = screenService.SaveGarbagePointDetails(data, _AppId, gpId);
            return dd;
        }

        public Result SaveEmployee(CMSBEmployeeDetailsVM employee, int _AppId, int UserId)
        {
            screenService = new ScreenService(_AppId);
            Result result = new Result();
            if (UserId == 0 || string.IsNullOrEmpty(UserId.ToString()))
            {
                employee.userId = 0;
            }
            Result dd = screenService.SaveEmployeeDetails(employee, _AppId, UserId);
            return dd;
        }

        public Result SaveDumpYard(CMSBDumpYardDetailsVM data, int _AppId, int dyId)
        {
            screenService = new ScreenService(_AppId);
            Result result = new Result();
            if (dyId == 0 || string.IsNullOrEmpty(dyId.ToString()))
            {
                data.dyId = 0;
            }
            Result dd = screenService.SaveDumpYardtDetails(data, _AppId, dyId);
            return dd;
        }


        public List<CMSBStatesVM> GetStates(int AppId, string SearchString)

        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {
                var data = dbMain.country_states.Select(x => new CMSBStatesVM
                {
                    id = x.id,
                    state_name = x.state_name,
                    state_name_mar = x.state_name_mar,
                    country_name = x.country_name,
                }).ToList();

                foreach (var item in data)
                {

                    item.state_name_mar = checkNull(item.state_name_mar);
                    item.state_name = checkNull(item.state_name);
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.state_name.ToUpper().ToString().Contains(SearchString) || c.state_name_mar.ToString().ToUpper().ToString().Contains(SearchString) ||
                     c.state_name_mar.ToString().ToLower().Contains(SearchString) || c.state_name.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.state_name.ToString().Contains(SearchString) || c.state_name_mar.ToString().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.id).ToList();
            }
        }

        public List<CMSBDistrictVM> GetDistrict(int AppId, string SearchString)
        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {

                var data = dbMain.state_districts.Select(x => new CMSBDistrictVM
                {
                    id = x.id,
                    district_name = x.district_name,
                    district_name_mar = x.district_name_mar,
                    state_id = x.state_id
                }).ToList();

                foreach (var item in data)
                {
                    item.district_name_mar = checkNull(item.district_name_mar);
                    item.district_name = checkNull(item.district_name);

                    if (item.state_id > 0)
                    {
                        item.statename = dbMain.country_states.Where(x => x.id == item.state_id).Select(x => x.state_name).FirstOrDefault();
                    }
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.district_name.ToUpper().ToString().Contains(SearchString) || c.district_name_mar.ToUpper().ToString().Contains(SearchString) || c.statename.ToUpper().ToString().Contains(SearchString) ||

                    c.district_name_mar.ToString().ToLower().Contains(SearchString) || c.district_name.ToString().ToLower().ToString().Contains(SearchString) || c.statename.ToUpper().ToString().Contains(SearchString) ||

                    c.district_name.ToString().Contains(SearchString) || c.district_name_mar.ToString().Contains(SearchString) || c.statename.ToUpper().ToString().Contains(SearchString)).ToList();
                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.id).ToList();
            }
        }


        public List<CMSBTalukaVM> GetTaluka(int AppId, string SearchString)
        {

            using (var dbMain = new DevSwachhBharatMainEntities())
            {
                var data = dbMain.tehsils.Select(x => new CMSBTalukaVM
                {
                    id = x.id,
                    name = x.name,
                    name_mar = x.name_mar,
                    longitude = x.longitude,
                    latitude = x.latitude,
                    stateid = x.state,
                    districtid = x.district
                }).ToList();

                foreach (var item in data)
                {
                    if (item.name_mar == "" && item.name_mar == null)
                        item.name_mar = "";

                    if (item.name == null && item.name == "")
                        item.name = "";
                    if (item.stateid > 0)
                    {
                        item.statename = dbMain.country_states.Where(x => x.id == item.stateid).Select(x => x.state_name).FirstOrDefault();
                    }
                    if (item.districtid > 0)
                    {
                        item.districtname = dbMain.state_districts.Where(x => x.id == item.districtid).Select(x => x.district_name).FirstOrDefault();
                    }
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.name.ToUpper().ToString().Contains(SearchString) || c.name_mar.ToString().ToUpper().ToString().Contains(SearchString) || c.name_mar.ToString().ToLower().ToString().Contains(SearchString) ||

                     c.name_mar.ToString().ToLower().Contains(SearchString) || c.name.ToString().ToLower().ToString().Contains(SearchString) ||

                     c.name.ToString().Contains(SearchString) || c.name_mar.ToString().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.id).ToList();
            }
        }

        public List<CMSBGarbagePointDetailsVM> GetGarbagePointData(string SearchString, int appId, int Offset, int Fetch_Next)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.PointQRCode + "/";
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.PointDetails().Select(x => new CMSBGarbagePointDetailsVM
                {
                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    gpName = x.Name,
                    gpNameMar = x.NameMar,
                    gpId = x.gpId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = x.Address,
                    ReferanceId = x.ReferanceId

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    || c.gpName.ToString().Contains(SearchString) || c.gpNameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    || c.Area.Contains(SearchString) || c.gpNameMar.ToLower().ToString().Contains(SearchString) || c.gpName.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    || c.Area.ToUpper().ToString().Contains(SearchString) || c.gpName.ToUpper().ToString().Contains(SearchString)
                    || c.gpNameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();

                }
                return data.OrderByDescending(c => c.gpId).Skip(Fetch_Next).Take(Offset).ToList();
            }
        }

        public List<CMSBDumpYardDetailsVM> GetDumpYardData(string SearchString, int appId, int Offset, int Fetch_Next)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.DumpYardQRCode + "/";
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_DumpYardDetails().Select(x => new CMSBDumpYardDetailsVM
                {

                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    Name = x.Name,
                    NameMar = x.NameMar,
                    dyId = x.dyId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = x.Address,
                    ReferanceId = x.ReferanceId,
                    dyLat = x.dyLat,
                    dyLong = x.dyLong

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    || c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    || c.Area.Contains(SearchString) || c.NameMar.ToLower().ToString().Contains(SearchString) || c.Name.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    || c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString)
                    || c.NameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.dyId).Skip(Fetch_Next).Take(Offset).ToList();
            }
        }

        public List<CMSBEmployeeDetailsVM> GetEmployeeDetailsData(string SearchString, int appId, int Offset, int Fetch_Next)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.UserMasters.Select(x => new CMSBEmployeeDetailsVM
                {
                    userId = x.userId,
                    userAddress = x.userAddress,
                    userLoginId = x.userLoginId,
                    userPassword = x.userPassword,
                    userMobileNumber = x.userMobileNumber,
                    userName = x.userName,
                    userNameMar = x.userNameMar,
                    userProfileImage = x.userProfileImage,
                    userEmployeeNo = x.userEmployeeNo,
                    isActive = x.isActive.ToString(),
                    bloodGroup = x.bloodGroup,
                    gcTarget = x.gcTarget,
                    imoNo = x.imoNo
                }).ToList();
                foreach (var item in data)
                {
                    item.isActive = checkNull(item.isActive);
                    if (item.bloodGroup == "0")
                    {
                        item.bloodGroup = "";
                    }
                    if (item.isActive == "True")
                    {

                        item.isActive = "Active";
                    }
                    else { item.isActive = "Not Active"; }
                    item.userNameMar = checkNull(item.userNameMar);
                    item.bloodGroup = checkNull(item.bloodGroup);
                    if (item.userAddress == null && item.userAddress == "")
                        item.userAddress = "";
                    if (item.userMobileNumber == null && item.userMobileNumber == "")
                        item.userMobileNumber = "";
                    if (item.userName == null && item.userName == "")
                        item.userName = "";
                    if (item.userNameMar == null && item.userNameMar == "")
                        item.userNameMar = "";
                    if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                        item.userEmployeeNo = "";
                    if (item.imoNo == null && item.imoNo == "")
                        item.imoNo = "";

                    if (item.userProfileImage == null || item.userProfileImage == "")
                    { item.userProfileImage = ""; }
                    else
                    {
                        item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                    }
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.userMobileNumber.ToString().Contains(SearchString) || c.userEmployeeNo.ToString().Contains(SearchString)
                    || c.userAddress.ToString().Contains(SearchString) || c.userName.ToString().Contains(SearchString) || c.userNameMar.ToString().Contains(SearchString) || c.bloodGroup.ToString().Contains(SearchString)

                    || c.userMobileNumber.Contains(SearchString) || c.userAddress.ToLower().ToString().Contains(SearchString) || c.userName.ToLower().ToString().Contains(SearchString) || c.userNameMar.ToLower().ToString().Contains(SearchString)
                    || c.userEmployeeNo.ToLower().ToString().Contains(SearchString) || c.bloodGroup.ToLower().ToString().Contains(SearchString)

                    || c.userMobileNumber.ToUpper().ToString().Contains(SearchString) || c.userNameMar.ToUpper().ToString().Contains(SearchString) || c.userName.ToUpper().ToString().Contains(SearchString) || c.bloodGroup.ToUpper().ToString().Contains(SearchString) || c.userAddress.ToUpper().ToString().Contains(SearchString) || c.userEmployeeNo.ToUpper().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.userId).Skip(Fetch_Next).Take(Offset).ToList();
            }
        }


        public List<SBVehicleType> GetVehicleTypeList(int AppId, string SearchString)
        {


            List<SBVehicleType> obj = new List<SBVehicleType>();
            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {

                var data = db.VehicleTypes.Select(x => new SBVehicleType
                {
                    vtId = x.vtId,
                    description = x.description,
                    descriptionMar = x.descriptionMar,
                    isActive = x.isActive
                }).ToList();


                foreach (var item in data)
                {
                    if (item.description == null && item.description == "")
                        item.description = "";


                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.description.ToUpper().ToString().Contains(SearchString) || c.description.ToString().ToLower().ToString().Contains(SearchString) || c.description.ToString().Contains(SearchString) || c.isActive.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.vtId).ToList();
            }
        }

        public List<CMSBAreaVM> GetAreaList(int AppId, string SearchString)
        {

            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.TeritoryMasters.Select(x => new CMSBAreaVM
                {
                    id = x.Id,
                    area = x.Area,
                    areaMar = x.AreaMar,
                    wardId = x.wardId,
                    Wardno = db.WardNumbers.Where(v => v.Id == x.wardId).FirstOrDefault().WardNo
                }).ToList();

                foreach (var item in data)
                {
                    item.area = checkNull(item.area);
                    item.areaMar = checkNull(item.areaMar);
                    item.Wardno = checkNull(item.Wardno);
                    string zone = string.Empty;
                    if (!string.IsNullOrEmpty(item.Wardno))
                    {
                        int wa = Convert.ToInt32(db.WardNumbers.Where(c => c.WardNo == item.Wardno).FirstOrDefault().zoneId);
                        zone = db.ZoneMasters.Where(c => c.zoneId == wa).FirstOrDefault().name;
                        item.Wardno = item.Wardno + " (" + zone + ")";
                    }
                    else
                    {
                        item.Wardno = item.Wardno + " (" + zone + ")";
                    }

                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.area.ToUpper().ToString().Contains(SearchString) || c.areaMar.ToString().ToUpper().ToString().Contains(SearchString) ||
                     c.areaMar.ToString().ToLower().Contains(SearchString) || c.area.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.area.ToString().Contains(SearchString) || c.areaMar.ToString().ToString().Contains(SearchString) || c.Wardno.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.id).ToList();
            }

        }

        public List<CMSBWardZoneVM> GetWardZoneList(int AppId)
        {

            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.WardNumbers.Select(x => new CMSBWardZoneVM
                {
                    WardID = x.Id,
                    WardNo = x.WardNo,
                    zoneId = x.zoneId,
                    Zone = db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name
                }).ToList();

                return data.OrderByDescending(c => c.WardID).ToList();
            }

        }


        // Added By Saurabh (16 May 2019)

        //public BigVQrEmployeeVM CheckQrEmployeeLogin(string userName, string password)
        //{
        //    BigVQrEmployeeVM employee = new BigVQrEmployeeVM();
        //    //var obj = dbMain.AspNetUsers.Where(c => c.UserName == userName).FirstOrDefault();
        //    //var userId = obj.Id;
        //    var obj = dbMain.QrEmployeeMasters.Where(x => x.qrEmpLoginId == userName && x.qrEmpPassword == password).FirstOrDefault();
        //    //var objMain = dbMain.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();



        //    if (obj != null && obj.qrEmpLoginId == userName && obj.qrEmpPassword == password)
        //    {
        //        //var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + objmain.UserProfile + "/";
        //        employee.qrEmpId = obj.qrEmpId;
        //        employee.qrEmpLoginId = obj.qrEmpLoginId;
        //        employee.qrEmpPassword = obj.qrEmpPassword;
        //        employee.appId = obj.appId;
        //        employee.qrEmpName = obj.qrEmpName;
        //        employee.qrEmpNameMar = obj.qrEmpNameMar;
        //        employee.isActive = obj.isActive;
        //        //employee.Latitude = objMain.Latitude;
        //        //admin.Logitude = objMain.Logitude;
        //        //admin.gtFeatures = false;
        //        employee.status = "success"; employee.message = "Login Successfully"; employee.messageMar = "लॉगिन यशस्वी";

        //    }

        //    else
        //    {
        //        //admin.adminId = "";
        //        //admin.adminLoginId = "";
        //        //admin.adminPassword = "";
        //        employee.status = "error";
        //        employee.message = "UserName or Passward not Match.";
        //        employee.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही..";
        //    }

        //    return employee;
        //}


        public Result SaveState(CMSBStatesVM state)
        {
            MainService = new MainService();
            Result result = new Result();
            result = MainService.SaveStateDetail(state);
            return result;
        }

        public Result SaveZone(CMSBZoneVM type, int AppID)
        {
            MainService = new MainService();
            Result result = new Result();
            if (type.zoneId <= 0)
            {
                type.zoneId = 0;
            }
            result = MainService.SaveZone(type, AppID);
            return result;
        }


        //Added By Saurabh(16 May 2019)
        public Result SaveQrEmployeeAttendence(BigVQREmployeeAttendenceVM obj, int AppId, int type)
        {
            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                if (type == 0)
                {
                    //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                    Qr_Employee_Daily_Attendance data = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.qrEmpId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                    if (data != null)
                    {
                        data.endTime = obj.startTime;
                        data.endDate = obj.startDate;
                        data.endLat = obj.startLat;
                        data.endLong = obj.startLong;
                        db.SaveChanges();
                    }
                    try
                    {
                        Qr_Employee_Daily_Attendance objdata = new Qr_Employee_Daily_Attendance();

                        var isActive=db.QrEmployeeMasters.Where(c => c.qrEmpId == obj.qrEmpId && c.isActive==true).FirstOrDefault();
                        if (isActive != null) { 

                            objdata.qrEmpId = obj.qrEmpId;
                            objdata.startDate = obj.startDate;
                            objdata.endLat = "";
                            objdata.startLat = obj.startLat;
                            objdata.startLong = obj.startLong;
                            objdata.startTime = obj.startTime;
                            objdata.endTime = "";
                            objdata.startNote = obj.startNote;
                            objdata.endNote = obj.endNote;
                            //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
                            db.Qr_Employee_Daily_Attendance.Add(objdata);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift started Successfully";
                            result.messageMar = "शिफ्ट सुरू";
                            return result;
                        }
                        else
                        {
                            result.status = "Error";
                            result.message = "Contact To Administrator";
                            result.messageMar = "प्रशासकाशी संपर्क साधा";
                            return result;
                        }
                    }

                    catch
                    {

                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        return result;
                    }


                }
                else
                {

                    try
                    {
                        Qr_Employee_Daily_Attendance objdata = db.Qr_Employee_Daily_Attendance.Where(c => c.startDate == EntityFunctions.TruncateTime(obj.startDate) && c.qrEmpId == obj.qrEmpId && (c.endTime == "" || c.endTime == null)).FirstOrDefault();
                        if (objdata != null)
                        {

                            objdata.qrEmpId = obj.qrEmpId;
                            objdata.endDate = obj.startDate;
                            objdata.endLat = obj.endLat;
                            objdata.endLong = obj.endLong;
                            objdata.endTime = obj.endTime;
                            objdata.endNote = obj.endNote;
                            //objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            ///////////////////////////////////////////////////////////////////

                            Qr_Location loc = new Qr_Location();
                            loc.empId = obj.qrEmpId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            db.Qr_Location.Add(loc);

                            ///////////////////////////////////////////////////////////////////

                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            return result;
                        }


                        else
                        {
                            Qr_Employee_Daily_Attendance objdata2 = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.qrEmpId && (c.endTime == "" || c.endTime == null)).OrderByDescending(c => c.qrEmpDaId).FirstOrDefault();
                            objdata2.qrEmpId = obj.qrEmpId;
                            objdata2.endDate = DateTime.Now;
                            objdata2.endLat = obj.endLat;
                            objdata2.endLong = obj.endLong;
                            objdata2.endTime = obj.endTime;
                            objdata2.endNote = obj.endNote;
                            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                            ///////////////////////////////////////////////////////////////////

                            Qr_Location loc = new Qr_Location();
                            loc.empId = obj.qrEmpId;
                            loc.datetime = DateTime.Now;
                            loc.lat = obj.endLat;
                            loc.@long = obj.endLong;
                            loc.address = Address(obj.endLat + "," + obj.endLong);
                            if (loc.address != "")
                            { loc.area = area(loc.address); }
                            else
                            {
                                loc.area = "";
                            }
                            db.Qr_Location.Add(loc);

                            ///////////////////////////////////////////////////////////////////

                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Shift ended successfully";
                            result.messageMar = "शिफ्ट संपले";
                            return result;
                        }
                    }
                    catch
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        return result;
                    }
                }
            }


        }

        public Result SaveSupervisorAttendence(BigVQREmployeeAttendenceVM obj,int type)
        {
            Result result = new Result();

            if (type == 0)
            {
      
                HSUR_Daily_Attendance data = dbMain.HSUR_Daily_Attendance.Where(c => c.userId == obj.qrEmpId && c.ip_address==null && c.login_device== null && (c.endTime == null || c.endTime == "")).FirstOrDefault();
                if (data != null)
                {
                    data.endTime = obj.startTime;
                    data.daEndDate = obj.startDate;
                    data.endLat = obj.startLat;
                    data.endLong = obj.startLong;
                    data.OutbatteryStatus = obj.batteryStatus;
                    dbMain.SaveChanges();
                }
                try
                {
                    HSUR_Daily_Attendance objdata = new HSUR_Daily_Attendance();

                    var isActive = dbMain.EmployeeMasters.Where(c => c.EmpId == obj.qrEmpId && c.isActive == true && c.type=="SA").FirstOrDefault();
                    if (isActive != null)
                    {

                        objdata.userId = obj.qrEmpId;
                        objdata.daDate = obj.startDate;
                        objdata.endLat = "";
                        objdata.startLat = obj.startLat;
                        objdata.startLong = obj.startLong;
                        objdata.startTime = obj.startTime;
                        objdata.endTime = "";
                        objdata.EmployeeType = obj.EmployeeType;
                        objdata.batteryStatus= obj.batteryStatus;
                        dbMain.HSUR_Daily_Attendance.Add(objdata);
                        UR_Location loc = new UR_Location();
                        loc.empId = obj.qrEmpId;
                        loc.datetime = DateTime.Now;
                        loc.lat = obj.startLat;
                        loc.@long = obj.startLong;
                        loc.batteryStatus = obj.batteryStatus;
                        loc.type = 1;
                        loc.address = Address(obj.endLat + "," + obj.endLong);
                        if (loc.address != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        dbMain.UR_Location.Add(loc);
                        dbMain.SaveChanges();
                        result.status = "success";
                        result.message = "Shift started Successfully";
                        result.messageMar = "शिफ्ट सुरू";
                        result.isAttendenceOff = false;
                        return result;
                    }
                    else
                    {
                        result.status = "Error";
                        result.message = "Contact To Administrator";
                        result.messageMar = "प्रशासकाशी संपर्क साधा";
                        return result;
                    }
                }

                catch
                {

                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    return result;
                }


            }
            else
            {

                try
                {
                    HSUR_Daily_Attendance objdata = dbMain.HSUR_Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.startDate) && c.userId == obj.qrEmpId && c.ip_address == null && c.login_device == null && (c.endTime == "" || c.endTime == null)).FirstOrDefault();
                    if (objdata != null)
                    {

                        objdata.userId = obj.qrEmpId;
                        objdata.daEndDate = obj.endDate;
                        objdata.endLat = obj.endLat;
                        objdata.endLong = obj.endLong;
                        objdata.endTime = obj.endTime;
                        objdata.EmployeeType = obj.EmployeeType;
                        objdata.OutbatteryStatus = obj.batteryStatus;
                        //objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                        ///////////////////////////////////////////////////////////////////

                        UR_Location loc = new UR_Location();
                        loc.empId = obj.qrEmpId;
                        loc.datetime = DateTime.Now;
                        loc.lat = obj.endLat;
                        loc.@long = obj.endLong;
                        loc.batteryStatus = obj.batteryStatus;
                        loc.type = 1;
                        loc.address = Address(obj.endLat + "," + obj.endLong);
                        if (loc.address != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        dbMain.UR_Location.Add(loc);

                        ///////////////////////////////////////////////////////////////////

                        dbMain.SaveChanges();
                        result.status = "success";
                        result.message = "Shift ended successfully";
                        result.messageMar = "शिफ्ट संपले";
                        result.isAttendenceOff = true;

                        return result;
                    }


                    else
                    {
                        HSUR_Daily_Attendance objdata2 = dbMain.HSUR_Daily_Attendance.Where(c => c.userId == obj.qrEmpId && c.ip_address == null && c.login_device == null && (c.endTime == "" || c.endTime == null)).OrderByDescending(c => c.daID).FirstOrDefault();
                        objdata2.userId = obj.qrEmpId;
                        objdata2.daEndDate = obj.endDate;
                        objdata2.endLat = obj.endLat;
                        objdata2.endLong = obj.endLong;
                        objdata2.endTime = obj.endTime;
                        objdata2.EmployeeType = obj.EmployeeType;
                        objdata2.OutbatteryStatus = obj.batteryStatus;

                        //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

                        ///////////////////////////////////////////////////////////////////

                        UR_Location loc = new UR_Location();
                        loc.empId = obj.qrEmpId;
                        loc.datetime = DateTime.Now;
                        loc.lat = obj.endLat;
                        loc.@long = obj.endLong;
                        loc.batteryStatus = obj.batteryStatus;
                        loc.type = 1;
                        loc.address = Address(obj.endLat + "," + obj.endLong);
                        if (loc.address != "")
                        { loc.area = area(loc.address); }
                        else
                        {
                            loc.area = "";
                        }
                        dbMain.UR_Location.Add(loc);

                        ///////////////////////////////////////////////////////////////////

                        dbMain.SaveChanges();
                        result.status = "success";
                        result.message = "Shift ended successfully";
                        result.messageMar = "शिफ्ट संपले";
                        result.isAttendenceOff = true;
                        return result;
                    }
                }
                catch
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                   return result;
                }
            }



        }
        
        public List<SyncResult1> SaveQrEmployeeAttendenceOffline(List<BigVQREmployeeAttendenceVM> obj, int AppId)
        {
            List<SyncResult1> result = new List<SyncResult1>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                Qr_Employee_Daily_Attendance attendance = new Qr_Employee_Daily_Attendance();
                foreach (var x in obj)
                {
                    try
                    {
                        bool _IsInSync = false, _IsOutSync = false;
                        var user = db.QrEmployeeMasters.Where(c => c.qrEmpId == x.qrEmpId).FirstOrDefault();

                        if (user.isActive == true)
                        {

                            var objdata = db.Qr_Employee_Daily_Attendance.Where(c => c.startDate == EntityFunctions.TruncateTime(x.startDate) && c.qrEmpId == x.qrEmpId && (c.endTime == "" || c.endTime == null)).FirstOrDefault();
                            if (objdata != null)
                            {

                                objdata.qrEmpId = x.qrEmpId;
                                objdata.startLat = x.startLat;
                                objdata.startLong = x.startLong;
                                objdata.startTime = x.startTime;
                                objdata.startDate = x.startDate;
                                objdata.endDate = x.endDate;
                                objdata.endLat = x.endLat;
                                objdata.endLong = x.endLong;
                                objdata.endTime = x.endTime;
                                objdata.startNote = x.startNote;
                                objdata.endNote = x.endNote;
                                objdata.batteryStatus = x.batteryStatus;

                                _IsInSync = true;

                                if (x.endLat != null && x.endLong != null)
                                {
                                    string Time2 = x.endTime;
                                    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                    string t2 = date2.ToString("hh:mm:ss tt");
                                    string dt2 = Convert.ToDateTime(x.endDate).ToString("MM/dd/yyyy");
                                    DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                    Qr_Location loc = new Qr_Location();
                                    loc.empId = x.qrEmpId;
                                    loc.datetime = edate;
                                    loc.lat = x.endLat;
                                    loc.@long = x.endLong;
                                    loc.batteryStatus = x.batteryStatus;
                                    loc.address = Address(x.endLat + "," + x.endLong);
                                    if (loc.address != "")
                                    {
                                        loc.area = area(loc.address);
                                    }
                                    else
                                    {
                                        loc.area = "";
                                    }

                                    loc.IsOffline = true;
                                    loc.CreatedDate = DateTime.Now;

                                    db.Qr_Location.Add(loc);
                                    _IsOutSync = true;
                                }

                                db.SaveChanges();
                            }
                            else
                            {
                                attendance.qrEmpId = x.qrEmpId;
                                attendance.startLat = x.startLat;
                                attendance.startLong = x.startLong;
                                attendance.startTime = x.startTime;
                                attendance.startDate = x.startDate;

                                if (x.endDate.Equals(DateTime.MinValue))
                                {
                                    attendance.endDate = null;
                                }
                                else
                                {
                                    attendance.endDate = x.endDate;
                                }

                                attendance.endLat = x.endLat;
                                attendance.endLong = x.endLong;
                                attendance.endTime = x.endTime;
                                attendance.startNote = x.startNote;
                                attendance.endNote = x.endNote;
                                attendance.batteryStatus = x.batteryStatus;

                                db.Qr_Employee_Daily_Attendance.Add(attendance);

                                _IsInSync = true;

                                if (x.endLat != null && x.endLong != null)
                                {
                                    string Time2 = x.endTime;
                                    DateTime date2 = DateTime.Parse(Time2, System.Globalization.CultureInfo.CurrentCulture);
                                    string t2 = date2.ToString("hh:mm:ss tt");
                                    string dt2 = Convert.ToDateTime(x.endDate).ToString("MM/dd/yyyy");
                                    DateTime? edate = Convert.ToDateTime(dt2 + " " + t2);

                                    Qr_Location loc = new Qr_Location();
                                    loc.empId = x.qrEmpId;
                                    loc.datetime = edate;
                                    loc.lat = x.endLat;
                                    loc.@long = x.endLong;
                                    loc.batteryStatus = x.batteryStatus;
                                    loc.address = Address(x.endLat + "," + x.endLong);
                                    if (loc.address != "")
                                    {
                                        loc.area = area(loc.address);
                                    }
                                    else
                                    {
                                        loc.area = "";
                                    }

                                    loc.IsOffline = true;
                                    loc.CreatedDate = DateTime.Now;

                                    db.Qr_Location.Add(loc);
                                    _IsOutSync = true;
                                }

                                db.SaveChanges();

                            }

                            result.Add(new SyncResult1()
                            {
                                ID = x.OfflineId,
                                status = "success",
                                message = "Shift started Successfully",
                                messageMar = "शिफ्ट सुरू",
                                IsInSync = _IsInSync,
                                IsOutSync = _IsOutSync,
                            });

                        }

                    }
                    catch
                    {
                        result.Add(new SyncResult1()
                        {
                            ID = x.OfflineId,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                            IsInSync = false,
                            IsOutSync = false,
                        });
                        return result;
                    }
                }

            }


            //if (type == 0)
            //{
            //    //Daily_Attendance data = db.Daily_Attendance.Where(c => c.daDate == EntityFunctions.TruncateTime(obj.daDate) && c.userId == obj.userId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
            //    Qr_Employee_Daily_Attendance data = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.qrEmpId && (c.endTime == null || c.endTime == "")).FirstOrDefault();
            //    if (data != null)
            //    {
            //        data.endTime = obj.startTime;
            //        data.endDate = obj.startDate;
            //        data.endLat = obj.startLat;
            //        data.endLong = obj.startLong;
            //        db.SaveChanges();
            //    }
            //    try
            //    {
            //        Qr_Employee_Daily_Attendance objdata = new Qr_Employee_Daily_Attendance();

            //        objdata.qrEmpId = obj.qrEmpId;
            //        objdata.startDate = obj.startDate;
            //        objdata.endLat = "";
            //        objdata.startLat = obj.startLat;
            //        objdata.startLong = obj.startLong;
            //        objdata.startTime = obj.startTime;
            //        objdata.endTime = "";
            //        objdata.startNote = obj.startNote;
            //        objdata.endNote = obj.endNote;
            //        //   objdata.startAddress = Address(obj.startLat + "," + obj.startLong); 
            //        db.Qr_Employee_Daily_Attendance.Add(objdata);
            //        db.SaveChanges();
            //        result.status = "success";
            //        result.message = "Shift started Successfully";
            //        result.messageMar = "शिफ्ट सुरू";
            //        return result;
            //    }

            //    catch
            //    {

            //        result.status = "error";
            //        result.message = "Something is wrong,Try Again.. ";
            //        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
            //        return result;
            //    }


            //}
            //else
            //{

            //    try
            //    {
            //        Qr_Employee_Daily_Attendance objdata = db.Qr_Employee_Daily_Attendance.Where(c => c.startDate == EntityFunctions.TruncateTime(obj.startDate) && c.qrEmpId == obj.qrEmpId && (c.endTime == "" || c.endTime == null)).FirstOrDefault();
            //        if (objdata != null)
            //        {

            //            objdata.qrEmpId = obj.qrEmpId;
            //            objdata.endDate = obj.startDate;
            //            objdata.endLat = obj.endLat;
            //            objdata.endLong = obj.endLong;
            //            objdata.endTime = obj.endTime;
            //            objdata.endNote = obj.endNote;
            //            //objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

            //            ///////////////////////////////////////////////////////////////////

            //            Qr_Location loc = new Qr_Location();
            //            loc.empId = obj.qrEmpId;
            //            loc.datetime = DateTime.Now;
            //            loc.lat = obj.endLat;
            //            loc.@long = obj.endLong;
            //            loc.address = Address(obj.endLat + "," + obj.endLong);
            //            if (loc.address != "")
            //            { loc.area = area(loc.address); }
            //            else
            //            {
            //                loc.area = "";
            //            }
            //            db.Qr_Location.Add(loc);

            //            ///////////////////////////////////////////////////////////////////

            //            db.SaveChanges();
            //            result.status = "success";
            //            result.message = "Shift ended successfully";
            //            result.messageMar = "शिफ्ट संपले";
            //            return result;
            //        }


            //        else
            //        {
            //            Qr_Employee_Daily_Attendance objdata2 = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.qrEmpId && (c.endTime == "" || c.endTime == null)).OrderByDescending(c => c.qrEmpDaId).FirstOrDefault();
            //            objdata2.qrEmpId = obj.qrEmpId;
            //            objdata2.endDate = DateTime.Now;
            //            objdata2.endLat = obj.endLat;
            //            objdata2.endLong = obj.endLong;
            //            objdata2.endTime = obj.endTime;
            //            objdata2.endNote = obj.endNote;
            //            //       objdata.endAddress = Address(objdata.endLat + "," + objdata.endLong);

            //            ///////////////////////////////////////////////////////////////////

            //            Qr_Location loc = new Qr_Location();
            //            loc.empId = obj.qrEmpId;
            //            loc.datetime = DateTime.Now;
            //            loc.lat = obj.endLat;
            //            loc.@long = obj.endLong;
            //            loc.address = Address(obj.endLat + "," + obj.endLong);
            //            if (loc.address != "")
            //            { loc.area = area(loc.address); }
            //            else
            //            {
            //                loc.area = "";
            //            }
            //            db.Qr_Location.Add(loc);

            //            ///////////////////////////////////////////////////////////////////

            //            db.SaveChanges();
            //            result.status = "success";
            //            result.message = "Shift ended successfully";
            //            result.messageMar = "शिफ्ट संपले";
            //            return result;
            //        }
            //    }
            //    catch
            //    {
            //        result.status = "error";
            //        result.message = "Something is wrong,Try Again.. ";
            //        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
            //        return result;
            //    }
            //}

            return result;
        }


        //add by neha(22 may 2019)
        public Result SaveQrHPDCollections(BigVQRHPDVM obj, int AppId, string referanceid, int gcType)
        {
            Result result = new Result();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    DateTime Dateeee = DateTime.Now;
                    var atten = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == obj.userId & c.startDate == EntityFunctions.TruncateTime(Dateeee)).FirstOrDefault();

                    if (atten != null)
                    {
                        coordinates p = new coordinates()
                        {
                            lat = Convert.ToDouble(obj.Lat),
                            lng = Convert.ToDouble(obj.Long)
                        };
                        List<List<coordinates>> lstPoly = new List<List<coordinates>>();
                        List<coordinates> poly = new List<coordinates>();
                        AppAreaMapVM ebm = GetEmpBeatMapByUserId(AppId);
                        lstPoly = ebm.AppAreaLatLong;
                        int polyId = 0;
                        if (lstPoly != null && lstPoly.Count > polyId)
                        {
                            poly = lstPoly[polyId];
                        }


                        obj.IsIn = IsPointInPolygon(poly, p);


                        if ((obj.IsIn == true && appdetails.IsAreaActive == true) || (appdetails.IsAreaActive == false))
                        {

                            if (gcType == 5)
                            {
                                var dump = db.StreetSweepingDetails.Where(x => x.ReferanceId == referanceid).FirstOrDefault();
                                if (dump != null)
                                {
                                    if ((string.IsNullOrEmpty(obj.name)) == false)
                                    {
                                        dump.SSName = obj.name;
                                    }
                                    if ((string.IsNullOrEmpty(obj.namemar)) == false)
                                    {
                                        dump.SSNameMar = obj.namemar;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Address)) == false)
                                    {
                                        dump.SSAddress = obj.Address;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Lat)) == false)
                                    {
                                        dump.SSLat = obj.Lat;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Long)) == false)
                                    {
                                        dump.SSLong = obj.Long;
                                    }

                                    dump.lastModifiedDate = DateTime.Now;


                                    if (obj.areaId > 0 && (string.IsNullOrEmpty(obj.areaId.ToString())) == false)
                                    {
                                        dump.areaId = obj.areaId;
                                    }
                                    if (obj.zoneId > 0 && (string.IsNullOrEmpty(obj.zoneId.ToString())) == false)
                                    {
                                        dump.zoneId = obj.zoneId;
                                    }
                                    if (obj.wardId > 0 && (string.IsNullOrEmpty(obj.wardId.ToString())) == false)
                                    {
                                        dump.wardId = obj.wardId;
                                    }
                                    if (obj.userId > 0 && (string.IsNullOrEmpty(obj.userId.ToString())) == false)
                                    {
                                        dump.userId = obj.userId;
                                    }
                                    //if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    //{
                                    //    dump.QRCodeImage = obj.QRCodeImage;
                                    //}
                                    if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    {
                                        obj.QRCodeImage = obj.QRCodeImage.Replace("data:image/jpeg;base64,", "");
                                        dump.BinaryQrCodeImage = Convert.FromBase64String(obj.QRCodeImage);
                                    }
                                    //////////////////////////////////////////////////////////////////
                                    obj.date = DateTime.Now;
                                    obj.ReferanceId = referanceid;

                                    db.Qr_Location.Add(FillLocationDetails(obj, AppId, false));
                                    //////////////////////////////////////////////////////////////////

                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }
                                else
                                {
                                    result.status = "error";
                                    result.message = "Invalid Dump Yard ID";
                                    result.messageMar = "अवैध डंप यार्ड आयडी ";
                                }

                            }
                            if (gcType == 4)
                            {
                                var dump = db.LiquidWasteDetails.Where(x => x.ReferanceId == referanceid).FirstOrDefault();
                                if (dump != null)
                                {
                                    if ((string.IsNullOrEmpty(obj.name)) == false)
                                    {
                                        dump.LWName = obj.name;
                                    }
                                    if ((string.IsNullOrEmpty(obj.namemar)) == false)
                                    {
                                        dump.LWNameMar = obj.namemar;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Address)) == false)
                                    {
                                        dump.LWAddreLW = obj.Address;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Lat)) == false)
                                    {
                                        dump.LWLat = obj.Lat;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Long)) == false)
                                    {
                                        dump.LWLong = obj.Long;
                                    }

                                    dump.lastModifiedDate = DateTime.Now;


                                    if (obj.areaId > 0 && (string.IsNullOrEmpty(obj.areaId.ToString())) == false)
                                    {
                                        dump.areaId = obj.areaId;
                                    }
                                    if (obj.zoneId > 0 && (string.IsNullOrEmpty(obj.zoneId.ToString())) == false)
                                    {
                                        dump.zoneId = obj.zoneId;
                                    }
                                    if (obj.wardId > 0 && (string.IsNullOrEmpty(obj.wardId.ToString())) == false)
                                    {
                                        dump.wardId = obj.wardId;
                                    }
                                    if (obj.userId > 0 && (string.IsNullOrEmpty(obj.userId.ToString())) == false)
                                    {
                                        dump.userId = obj.userId;
                                    }
                                    //if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    //{
                                    //    dump.QRCodeImage = obj.QRCodeImage;
                                    //}
                                    if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    {
                                        obj.QRCodeImage = obj.QRCodeImage.Replace("data:image/jpeg;base64,", "");
                                        dump.BinaryQrCodeImage = Convert.FromBase64String(obj.QRCodeImage);
                                    }
                                    //////////////////////////////////////////////////////////////////
                                    obj.date = DateTime.Now;
                                    obj.ReferanceId = referanceid;

                                    db.Qr_Location.Add(FillLocationDetails(obj, AppId, false));
                                    //////////////////////////////////////////////////////////////////

                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }
                                else
                                {
                                    result.status = "error";
                                    result.message = "Invalid Dump Yard ID";
                                    result.messageMar = "अवैध डंप यार्ड आयडी ";
                                }

                            }
                            if (gcType == 3)
                            {
                                var dump = db.DumpYardDetails.Where(x => x.ReferanceId == referanceid).FirstOrDefault();
                                if (dump != null)
                                {
                                    if ((string.IsNullOrEmpty(obj.name)) == false)
                                    {
                                        dump.dyName = obj.name;
                                    }
                                    if ((string.IsNullOrEmpty(obj.namemar)) == false)
                                    {
                                        dump.dyNameMar = obj.namemar;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Address)) == false)
                                    {
                                        dump.dyAddress = obj.Address;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Lat)) == false)
                                    {
                                        dump.dyLat = obj.Lat;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Long)) == false)
                                    {
                                        dump.dyLong = obj.Long;
                                    }

                                    dump.lastModifiedDate = DateTime.Now;


                                    if (obj.areaId > 0 && (string.IsNullOrEmpty(obj.areaId.ToString())) == false)
                                    {
                                        dump.areaId = obj.areaId;
                                    }
                                    if (obj.zoneId > 0 && (string.IsNullOrEmpty(obj.zoneId.ToString())) == false)
                                    {
                                        dump.zoneId = obj.zoneId;
                                    }
                                    if (obj.wardId > 0 && (string.IsNullOrEmpty(obj.wardId.ToString())) == false)
                                    {
                                        dump.wardId = obj.wardId;
                                    }
                                    if (obj.userId > 0 && (string.IsNullOrEmpty(obj.userId.ToString())) == false)
                                    {
                                        dump.userId = obj.userId;
                                    }
                                    //if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    //{
                                    //    dump.QRCodeImage = obj.QRCodeImage;
                                    //}
                                    if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    {
                                        obj.QRCodeImage = obj.QRCodeImage.Replace("data:image/jpeg;base64,", "");
                                        dump.BinaryQrCodeImage = Convert.FromBase64String(obj.QRCodeImage);
                                    }
                                    //////////////////////////////////////////////////////////////////
                                    obj.date = DateTime.Now;
                                    obj.ReferanceId = referanceid;
                                    db.Qr_Location.Add(FillLocationDetails(obj, AppId, false));
                                    //////////////////////////////////////////////////////////////////

                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }
                                else
                                {
                                    result.status = "error";
                                    result.message = "Invalid Dump Yard ID";
                                    result.messageMar = "अवैध डंप यार्ड आयडी ";
                                }

                            }
                            else if (gcType == 2)
                            {
                                var gp = db.GarbagePointDetails.Where(x => x.ReferanceId == referanceid).FirstOrDefault();

                                if (gp != null)
                                {
                                    if ((string.IsNullOrEmpty(obj.name.ToString())) == false)
                                    {
                                        gp.gpName = obj.name;
                                    }
                                    if ((string.IsNullOrEmpty(obj.namemar.ToString())) == false)
                                    {
                                        gp.gpNameMar = obj.namemar;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Address.ToString())) == false)
                                    {
                                        gp.gpAddress = obj.Address;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Lat.ToString())) == false)
                                    {
                                        gp.gpLat = obj.Lat;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Long.ToString())) == false)
                                    {
                                        gp.gpLong = obj.Long;
                                    }

                                    gp.modified = DateTime.Now;

                                    if (obj.areaId > 0 && (string.IsNullOrEmpty(obj.areaId.ToString())) == false)
                                    {
                                        gp.areaId = obj.areaId;
                                    }
                                    if (obj.zoneId > 0 && (string.IsNullOrEmpty(obj.zoneId.ToString())) == false)
                                    {
                                        gp.zoneId = obj.zoneId;
                                    }
                                    if (obj.wardId > 0 && (string.IsNullOrEmpty(obj.wardId.ToString())) == false)
                                    {
                                        gp.wardId = obj.wardId;
                                    }
                                    if (obj.userId > 0 && (string.IsNullOrEmpty(obj.userId.ToString())) == false)
                                    {
                                        gp.userId = obj.userId;
                                    }


                                    //////////////////////////////////////////////////////////////////
                                    obj.date = DateTime.Now;
                                    db.Qr_Location.Add(FillLocationDetails(obj, AppId, false));
                                    //////////////////////////////////////////////////////////////////


                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }
                                else
                                {
                                    result.status = "error";
                                    result.message = "Invalid Garbage Point ID";
                                    result.messageMar = "अवैध कचरा पॉइंट आयडी";
                                }
                            }
                            else if (gcType == 1)
                            {
                                var house = db.HouseMasters.Where(x => x.ReferanceId == referanceid).FirstOrDefault();
                                if (house != null)
                                {
                                    if ((string.IsNullOrEmpty(obj.houseNumber.ToString())) == false)
                                    {
                                        house.houseNumber = obj.houseNumber;
                                    }
                                    if ((string.IsNullOrEmpty(obj.name.ToString())) == false)
                                    {
                                        house.houseOwner = obj.name;
                                    }
                                    if ((string.IsNullOrEmpty(obj.namemar.ToString())) == false)
                                    {
                                        house.houseOwnerMar = obj.namemar;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Address.ToString())) == false)
                                    {
                                        house.houseAddress = obj.Address;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Lat.ToString())) == false)
                                    {
                                        house.houseLat = obj.Lat;
                                    }
                                    if ((string.IsNullOrEmpty(obj.Long.ToString())) == false)
                                    {
                                        house.houseLong = obj.Long;
                                    }

                                    house.modified = DateTime.Now;

                                    if (obj.areaId > 0 && (string.IsNullOrEmpty(obj.areaId.ToString())) == false)
                                    {
                                        house.AreaId = obj.areaId;
                                    }
                                    if (obj.zoneId > 0 && (string.IsNullOrEmpty(obj.zoneId.ToString())) == false)
                                    {
                                        house.ZoneId = obj.zoneId;
                                    }
                                    if (obj.wardId > 0 && (string.IsNullOrEmpty(obj.wardId.ToString())) == false)
                                    {
                                        house.WardNo = obj.wardId;
                                    }
                                    if (obj.userId > 0 && (string.IsNullOrEmpty(obj.userId.ToString())) == false)
                                    {
                                        house.userId = obj.userId;
                                    }
                                    if ((string.IsNullOrEmpty(obj.mobileno)) == false)
                                    {
                                        house.houseOwnerMobile = obj.mobileno;
                                    }

                                    if ((string.IsNullOrEmpty(obj.wastetype)) == false)
                                    {
                                        house.WasteType = obj.wastetype;
                                    }

                                    //if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    //{
                                    //    house.QRCodeImage = obj.QRCodeImage;
                                    //}

                                    if ((string.IsNullOrEmpty(obj.QRCodeImage)) == false)
                                    {
                                        obj.QRCodeImage = obj.QRCodeImage.Replace("data:image/jpeg;base64,", "");
                                        house.BinaryQrCodeImage = Convert.FromBase64String(obj.QRCodeImage);
                                    }

                                    //////////////////////////////////////////////////////////////////
                                    obj.date = DateTime.Now;
                                    db.Qr_Location.Add(FillLocationDetails(obj, AppId, false));
                                    //////////////////////////////////////////////////////////////////


                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Uploaded successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }
                                else
                                {
                                    result.status = "error";
                                    result.message = "Invalid House ID";
                                    result.messageMar = "अवैध घर आयडी";
                                }

                            }
                        }
                        else
                        {
                            result.message = "Your outside the area,please go to inside the area.. ";
                            result.messageMar = "तुम्ही क्षेत्राबाहेर आहात.कृपया परिसरात जा..";
                            result.status = "error";
                            return result;
                        }
                    }
                    else
                    {
                        result.message = "Your duty is currently off, please start again.. ";
                        result.messageMar = "आपली ड्यूटी सध्या बंद आहे, कृपया पुन्हा सुरू करा..";
                        result.status = "error";
                        return result;
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

            if(result.status == "success")
            {
                if (appdetails != null)
                {
                    appdetails.FAQ = "1";
                    dbMain.SaveChanges();
                }
               // List<AppDetail> AppDetailss = dbMain.Database.SqlQuery<AppDetail>("exec [Update_Trigger]").ToList();
            }
        }

        /// <summary>
        /// //this method used for Qr scanify only
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        /// 

        public Qr_Location FillLocationDetails(BigVQRHPDVM obj, int AppId, bool IsOffline)
        {
            var distCount = "";
            string addre = string.Empty, batteryStatus = string.Empty;
            Qr_Location loc = new Qr_Location();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var locc = db.SP_UserLatLongDetail(obj.userId, 1).FirstOrDefault();

                if (locc == null || locc.lat == "" || locc.@long == "")
                {
                    string a = obj.Lat;
                    string b = obj.Long;

                    var dist = db.SP_DistanceCount(Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(obj.Lat), Convert.ToDouble(obj.Long)).FirstOrDefault();
                    distCount = dist.Distance_in_KM.ToString();
                }
                else
                {
                    var dist = db.SP_DistanceCount(Convert.ToDouble(locc.lat), Convert.ToDouble(locc.@long), Convert.ToDouble(obj.Lat), Convert.ToDouble(obj.Long)).FirstOrDefault();
                    distCount = dist.Distance_in_KM.ToString();
                }

                loc.Distnace = Convert.ToDecimal(distCount);
                obj.Address = addre;
                loc.datetime = obj.date; //DateTime.Now;
                loc.lat = obj.Lat;
                loc.@long = obj.Long;
                loc.address = obj.Address;
                //loc.batteryStatus = batteryStatus;
                loc.area = (obj.Address != "") ? area(loc.address) : "";
                loc.empId = obj.userId;
                loc.type = 1;
                loc.ReferanceID = obj.ReferanceId;
                loc.IsOffline = (IsOffline == true ? true : false);
                loc.CreatedDate = DateTime.Now;


            }

            return loc;
        }


        public List<CollectionSyncResult> SaveQrHPDCollectionsOffline(List<BigVQRHPDVM> obj, int AppId)
        {
            List<CollectionSyncResult> result = new List<CollectionSyncResult>();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    
                    foreach (var item in obj)
                    {
                       

                        if (item.gcType == 5)
                        {
                            var dump = db.StreetSweepingDetails.Where(x => x.ReferanceId == item.ReferanceId).FirstOrDefault();
                            if (dump != null)
                            {

                                if (!string.IsNullOrEmpty(item.name))
                                {
                                    dump.SSName = item.name;
                                }
                                if (!string.IsNullOrEmpty(item.namemar))
                                {
                                    dump.SSNameMar = item.namemar;
                                }
                                if (!string.IsNullOrEmpty(item.Address))
                                {
                                    dump.SSAddress = item.Address;
                                }
                                if (!string.IsNullOrEmpty(item.Lat))
                                {
                                    dump.SSLat = item.Lat;
                                }
                                if (!string.IsNullOrEmpty(item.Long))
                                {
                                    dump.SSLong = item.Long;
                                }

                                dump.lastModifiedDate = item.date; //DateTime.Now;

                                if (item.areaId > 0 && (string.IsNullOrEmpty(item.areaId.ToString())) == false)
                                {
                                    dump.areaId = item.areaId;
                                }
                                if (item.zoneId > 0 && (string.IsNullOrEmpty(item.zoneId.ToString())) == false)
                                {
                                    dump.zoneId = item.zoneId;
                                }
                                if (item.wardId > 0 && (string.IsNullOrEmpty(item.wardId.ToString())) == false)
                                {
                                    dump.wardId = item.wardId;
                                }
                                if (item.userId > 0 && (string.IsNullOrEmpty(item.userId.ToString())) == false)
                                {
                                    dump.userId = item.userId;
                                }
                                if (!string.IsNullOrEmpty(item.QRCodeImage))
                                {
                                    dump.QRCodeImage = item.QRCodeImage;
                                }
                                db.Qr_Location.Add(FillLocationDetails(item, AppId, true));

                                db.SaveChanges();

                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "success",
                                    message = "Uploaded successfully",
                                    messageMar = "सबमिट यशस्वी",
                                });
                            }
                            else
                            {
                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "error",
                                    message = "Invalid Dump Yard ID",
                                    messageMar = "अवैध डंप यार्ड आयडी ",
                                });
                            }

                        }


                        if (item.gcType == 4)
                        {
                            var dump = db.LiquidWasteDetails.Where(x => x.ReferanceId == item.ReferanceId).FirstOrDefault();
                            if (dump != null)
                            {

                                if (!string.IsNullOrEmpty(item.name))
                                {
                                    dump.LWName = item.name;
                                }
                                if (!string.IsNullOrEmpty(item.namemar))
                                {
                                    dump.LWNameMar = item.namemar;
                                }
                                if (!string.IsNullOrEmpty(item.Address))
                                {
                                    dump.LWAddreLW = item.Address;
                                }
                                if (!string.IsNullOrEmpty(item.Lat))
                                {
                                    dump.LWLat = item.Lat;
                                }
                                if (!string.IsNullOrEmpty(item.Long))
                                {
                                    dump.LWLong = item.Long;
                                }

                                dump.lastModifiedDate = item.date; //DateTime.Now;

                                if (item.areaId > 0 && (string.IsNullOrEmpty(item.areaId.ToString())) == false)
                                {
                                    dump.areaId = item.areaId;
                                }
                                if (item.zoneId > 0 && (string.IsNullOrEmpty(item.zoneId.ToString())) == false)
                                {
                                    dump.zoneId = item.zoneId;
                                }
                                if (item.wardId > 0 && (string.IsNullOrEmpty(item.wardId.ToString())) == false)
                                {
                                    dump.wardId = item.wardId;
                                }
                                if (item.userId > 0 && (string.IsNullOrEmpty(item.userId.ToString())) == false)
                                {
                                    dump.userId = item.userId;
                                }
                                if (!string.IsNullOrEmpty(item.QRCodeImage))
                                {
                                    dump.QRCodeImage = item.QRCodeImage;
                                }
                                db.Qr_Location.Add(FillLocationDetails(item, AppId, true));

                                db.SaveChanges();

                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "success",
                                    message = "Uploaded successfully",
                                    messageMar = "सबमिट यशस्वी",
                                });
                            }
                            else
                            {
                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "error",
                                    message = "Invalid Dump Yard ID",
                                    messageMar = "अवैध डंप यार्ड आयडी ",
                                });
                            }

                        }

                        if (item.gcType == 3)
                        {
                            var dump = db.DumpYardDetails.Where(x => x.ReferanceId == item.ReferanceId).FirstOrDefault();
                            if (dump != null)
                            {

                                if (!string.IsNullOrEmpty(item.name))
                                {
                                    dump.dyName = item.name;
                                }
                                if (!string.IsNullOrEmpty(item.namemar))
                                {
                                    dump.dyNameMar = item.namemar;
                                }
                                if (!string.IsNullOrEmpty(item.Address))
                                {
                                    dump.dyAddress = item.Address;
                                }
                                if (!string.IsNullOrEmpty(item.Lat))
                                {
                                    dump.dyLat = item.Lat;
                                }
                                if (!string.IsNullOrEmpty(item.Long))
                                {
                                    dump.dyLong = item.Long;
                                }

                                dump.lastModifiedDate = item.date; //DateTime.Now;

                                if (item.areaId > 0 && (string.IsNullOrEmpty(item.areaId.ToString())) == false)
                                {
                                    dump.areaId = item.areaId;
                                }
                                if (item.zoneId > 0 && (string.IsNullOrEmpty(item.zoneId.ToString())) == false)
                                {
                                    dump.zoneId = item.zoneId;
                                }
                                if (item.wardId > 0 && (string.IsNullOrEmpty(item.wardId.ToString())) == false)
                                {
                                    dump.wardId = item.wardId;
                                }
                                if (item.userId > 0 && (string.IsNullOrEmpty(item.userId.ToString())) == false)
                                {
                                    dump.userId = item.userId;
                                }
                                if (!string.IsNullOrEmpty(item.QRCodeImage))
                                {
                                    dump.QRCodeImage = item.QRCodeImage;
                                }
                                db.Qr_Location.Add(FillLocationDetails(item, AppId, true));

                                db.SaveChanges();

                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "success",
                                    message = "Uploaded successfully",
                                    messageMar = "सबमिट यशस्वी",
                                });
                            }
                            else
                            {
                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "error",
                                    message = "Invalid Dump Yard ID",
                                    messageMar = "अवैध डंप यार्ड आयडी ",
                                });
                            }

                        }

                        if (item.gcType == 2)
                        {
                            var gp = db.GarbagePointDetails.Where(x => x.ReferanceId == item.ReferanceId).FirstOrDefault();

                            if (gp != null)
                            {
                                if (!string.IsNullOrEmpty(item.name.ToString()))
                                {
                                    gp.gpName = item.name;
                                }
                                if (!string.IsNullOrEmpty(item.namemar.ToString()))
                                {
                                    gp.gpNameMar = item.namemar;
                                }
                                if (!string.IsNullOrEmpty(item.Address.ToString()))
                                {
                                    gp.gpAddress = item.Address;
                                }
                                if (!string.IsNullOrEmpty(item.Lat.ToString()))
                                {
                                    gp.gpLat = item.Lat;
                                }
                                if (!string.IsNullOrEmpty(item.Long.ToString()))
                                {
                                    gp.gpLong = item.Long;
                                }

                                gp.modified = item.date; //DateTime.Now;

                                if (item.areaId > 0 && (string.IsNullOrEmpty(item.areaId.ToString())) == false)
                                {
                                    gp.areaId = item.areaId;
                                }
                                if (item.zoneId > 0 && (string.IsNullOrEmpty(item.zoneId.ToString())) == false)
                                {
                                    gp.zoneId = item.zoneId;
                                }
                                if (item.wardId > 0 && (string.IsNullOrEmpty(item.wardId.ToString())) == false)
                                {
                                    gp.wardId = item.wardId;
                                }
                                if (item.userId > 0 && (string.IsNullOrEmpty(item.userId.ToString())) == false)
                                {
                                    gp.userId = item.userId;
                                }

                                db.Qr_Location.Add(FillLocationDetails(item, AppId, true));

                                db.SaveChanges();

                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "success",
                                    message = "Uploaded successfully",
                                    messageMar = "सबमिट यशस्वी",
                                });
                            }
                            else
                            {
                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "error",
                                    message = "Invalid Garbage Point ID",
                                    messageMar = "अवैध कचरा पॉइंट आयडी",
                                });
                            }
                        }

                        if (item.gcType == 1)
                        {
                            string houseid1 = item.ReferanceId;
                            string[] houseList = houseid1.Split(',');

                            if (houseList.Length > 1)
                            {
                                item.ReferanceId = houseList[0];
                                item.wastetype = houseList[1];

                            }
                            var house = db.HouseMasters.Where(x => x.ReferanceId == item.ReferanceId).FirstOrDefault();
                            if (house != null)
                            {
                                if (!string.IsNullOrEmpty(item.houseNumber.ToString()))
                                {
                                    house.houseNumber = item.houseNumber;
                                }
                                if (!string.IsNullOrEmpty(item.name.ToString()))
                                {
                                    house.houseOwner = item.name;
                                }
                                if (!string.IsNullOrEmpty(item.namemar.ToString()))
                                {
                                    house.houseOwnerMar = item.namemar;
                                }
                                if (!string.IsNullOrEmpty(item.Address.ToString()))
                                {
                                    house.houseAddress = item.Address;
                                }
                                if (!string.IsNullOrEmpty(item.Lat.ToString()))
                                {
                                    house.houseLat = item.Lat;
                                }
                                if (!string.IsNullOrEmpty(item.Long.ToString()))
                                {
                                    house.houseLong = item.Long;
                                }

                                house.modified = item.date; //DateTime.Now;

                                if (item.areaId > 0 && (string.IsNullOrEmpty(item.areaId.ToString())) == false)
                                {
                                    house.AreaId = item.areaId;
                                }
                                if (item.zoneId > 0 && (string.IsNullOrEmpty(item.zoneId.ToString())) == false)
                                {
                                    house.ZoneId = item.zoneId;
                                }
                                if (item.wardId > 0 && (string.IsNullOrEmpty(item.wardId.ToString())) == false)
                                {
                                    house.WardNo = item.wardId;
                                }
                                if (item.userId > 0 && (string.IsNullOrEmpty(item.userId.ToString())) == false)
                                {
                                    house.userId = item.userId;
                                }

                                if (!string.IsNullOrEmpty(item.mobileno))
                                {
                                    house.houseOwnerMobile = item.mobileno;
                                }

                                if (!string.IsNullOrEmpty(item.wastetype))
                                {
                                    house.WasteType = item.wastetype;
                                }
                                if (!string.IsNullOrEmpty(item.QRCodeImage))
                                {
                                    house.QRCodeImage = item.QRCodeImage;
                                }
                                db.Qr_Location.Add(FillLocationDetails(item, AppId, true));

                                db.SaveChanges();

                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "success",
                                    message = "Uploaded successfully",
                                    messageMar = "सबमिट यशस्वी",
                                });
                            }
                            else
                            {
                                result.Add(new CollectionSyncResult()
                                {
                                    ID = Convert.ToInt32(item.OfflineId),
                                    status = "error",
                                    message = "Invalid House ID",
                                    messageMar = "अवैध घर आयडी",
                                });
                            }

                        }
                    }

                    return result;


                }
                catch (Exception ex)
                {
                    result.Add(new CollectionSyncResult()
                    {
                        ID = 0,
                        message = "Something is wrong,Try Again.. ",
                        messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..",
                        status = "error",
                    });
                    return result;
                }


            }

        }


        public bool IsPointInPolygon(List<coordinates> poly, coordinates p)
        {
            bool inside = false;

            if (poly != null && poly.Count > 0)
            {
                double minX = poly[0].lat ?? 0;
                double maxX = poly[0].lat ?? 0;
                double minY = poly[0].lng ?? 0;
                double maxY = poly[0].lng ?? 0;

                for (int i = 1; i < poly.Count; i++)
                {
                    coordinates q = poly[i];
                    minX = Math.Min(q.lat ?? 0, minX);
                    maxX = Math.Max(q.lat ?? 0, maxX);
                    minY = Math.Min(q.lng ?? 0, minY);
                    maxY = Math.Max(q.lng ?? 0, maxY);
                }


                if (p.lat < minX || p.lat > maxX || p.lng < minY || p.lng > maxY)
                {
                    return false;
                }
                for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
                {
                    if ((poly[i].lng > p.lng) != (poly[j].lng > p.lng) &&
                         p.lat < (poly[j].lat - poly[i].lat) * (p.lng - poly[i].lng) / (poly[j].lng - poly[i].lng) + poly[i].lat)
                    {
                        inside = !inside;
                    }
                }
                return inside;

            }
            return inside;
        }

        public AppAreaMapVM GetEmpBeatMapByUserId(int userId)
        {
            AppAreaMapVM empBeatMap = new AppAreaMapVM();
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (userId > 0)
                    {
                        var model = db.AppDetails.Where(x => x.AppId == userId).FirstOrDefault();
                        if (model != null)
                        {
                            empBeatMap = fillEmpBeatMapVM(model);
                        }
                        else
                        {
                           
                        }
                    }
                    else
                    {
                     

                    }

                }
            }
            catch (Exception ex)
            {
                return empBeatMap;
            }

            return empBeatMap;
        }

        private AppAreaMapVM fillEmpBeatMapVM(AppDetail data)
        {
            AppAreaMapVM model = new AppAreaMapVM();
            model.AppId = data.AppId;
            model.AppName = data.AppName;
            model.AppAreaLatLong = ConvertStringToLatLong(data.AppAreaLatLong);

            return model;
        }

        public List<List<coordinates>> ConvertStringToLatLong(string strCord)
        {
            List<List<coordinates>> lstCord = new List<List<coordinates>>();
            string[] lstPoly = strCord.Split(':');
            if (lstPoly.Length > 0)
            {
                for (var i = 0; i < lstPoly.Length; i++)
                {
                    List<coordinates> poly = new List<coordinates>();
                    string[] lstLatLong = lstPoly[i].Split(';');
                    if (lstLatLong.Length > 0)
                    {
                        for (var j = 0; j < lstLatLong.Length; j++)
                        {
                            coordinates cord = new coordinates();
                            string[] strLatLong = lstLatLong[j].Split(',');
                            if (strLatLong.Length == 2)
                            {
                                cord.lat = Convert.ToDouble(strLatLong[0]);
                                cord.lng = Convert.ToDouble(strLatLong[1]);
                            }
                            poly.Add(cord);
                        }

                    }
                    lstCord.Add(poly);
                }
            }
            return lstCord;
        }
        public Result SaveWard(CMSBWardVM Ward, int AppId)
        {
            MainService = new MainService();
            Result result = new Result();
            result = MainService.SaveWardDetail(Ward, AppId);
            return result;
        }

        public Result SaveArea(CMSBAreaVM Area, int AppId)
        {
            MainService = new MainService();
            Result result = new Result();
            result = MainService.SaveAreaDetail(Area, AppId);
            return result;
        }

        public Result SaveVehicleType(SBVehicleType VehicleType, int AppId)
        {
            MainService = new MainService();
            Result result = new Result();
            result = MainService.SaveVehicleTypeDetail(VehicleType, AppId);
            return result;
        }

        public List<CMSBUserLocationMapVM> GetUserAttenRoute(int _AppId, int daId)
        {
            screenService = new ScreenService(_AppId);
            return screenService.GetUserAttenRoute(daId);
        }

        public List<SBWorkDetails> GetQrWorkHistory(int userId, int year, int month, int appId)
        {
            List<SBWorkDetails> obj = new List<SBWorkDetails>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetQrWorkHistory(userId, year, month).ToList();
                foreach (var x in data)
                {

                    obj.Add(new SBWorkDetails()
                    {
                        date = (x.Day).ToString(),
                        houseCollection = checkIntNull(x.HouseCollection.ToString()),
                        pointCollection = checkIntNull(x.PointCollection.ToString()),
                        DumpYardCollection = checkIntNull(x.DumpYardCollection.ToString()),
                        LiquidCollection = checkIntNull(x.LiquidCollection.ToString()),
                        StreetCollection = checkIntNull(x.StreetCollection.ToString()),
                    });
                }

            }
            return obj;
        }

        //add by neha
        public List<BigVQrworkhistorydetails> GetQrWorkHistoryDetails(DateTime date, int AppId, int userId)
        {
            List<BigVQrworkhistorydetails> obj = new List<BigVQrworkhistorydetails>();
            var appdetails = dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {

                var data = db.DumpYardDetails.Where(c => EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(date) && c.userId == userId).ToList();

                foreach (var x in data)
                {

                    obj.Add(new BigVQrworkhistorydetails()
                    {
                        Date = Convert.ToDateTime(x.lastModifiedDate).ToString("MM/dd/yyyy"),
                        time = Convert.ToDateTime(x.lastModifiedDate).ToString("HH:mm"),
                        DumpYardNo = x.ReferanceId,
                        type = 3

                    });

                }

                var data1 = db.GarbagePointDetails.Where(c => EntityFunctions.TruncateTime(c.modified) == EntityFunctions.TruncateTime(date) && c.userId == userId).ToList();
                foreach (var y in data1)
                {
                    obj.Add(new BigVQrworkhistorydetails()
                    {
                        Date = Convert.ToDateTime(y.modified).ToString("MM/dd/yyyy"),
                        time = Convert.ToDateTime(y.modified).ToString("HH:mm"),
                        PointNo = y.ReferanceId,
                        type = 2
                    });
                }

                var data2 = db.HouseMasters.Where(c => EntityFunctions.TruncateTime(c.modified) == EntityFunctions.TruncateTime(date) && c.userId == userId).ToList();
                foreach (var z in data2)
                {
                    obj.Add(new BigVQrworkhistorydetails()
                    {
                        Date = Convert.ToDateTime(z.modified).ToString("MM/dd/yyyy"),
                        time = Convert.ToDateTime(z.modified).ToString("HH:mm"),
                        HouseNo = z.ReferanceId,
                        type = 1

                    });
                }
                var data3 = db.LiquidWasteDetails.Where(c => EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(date) && c.userId == userId).ToList();
                foreach (var z in data3)
                {
                    obj.Add(new BigVQrworkhistorydetails()
                    {
                        Date = Convert.ToDateTime(z.lastModifiedDate).ToString("MM/dd/yyyy"),
                        time = Convert.ToDateTime(z.lastModifiedDate).ToString("HH:mm"),
                        LiquidNo = z.ReferanceId,
                        type = 4

                    });
                }
                var data4 = db.StreetSweepingDetails.Where(c => EntityFunctions.TruncateTime(c.lastModifiedDate) == EntityFunctions.TruncateTime(date) && c.userId == userId).ToList();
                foreach (var z in data4)
                {
                    obj.Add(new BigVQrworkhistorydetails()
                    {
                        Date = Convert.ToDateTime(z.lastModifiedDate).ToString("MM/dd/yyyy"),
                        time = Convert.ToDateTime(z.lastModifiedDate).ToString("HH:mm"),
                        StreetNo = z.ReferanceId,
                        type = 5

                    });
                }
                return obj.OrderBy(c => c.Date).OrderBy(c => c.time).ToList();

            }
        }

        public List<CMSBEmplyeeIdelGrid> GetEmployeeIdelTime(int appId, DateTime fdate, DateTime tdate, int UserId, int Offset, int Fetch_Next, string SearchString)
        {
            List<CMSBEmplyeeIdelGrid> obj = new List<CMSBEmplyeeIdelGrid>();
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {

                var data = db.SP_IdelTime(UserId, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.userName.Contains(SearchString) || c.StartAddress.Contains(SearchString) || c.LastTime.Contains(SearchString) || c.address.Contains(SearchString) || c.StartTime.Contains(SearchString) || Convert.ToString(c.IdelTime).Contains(SearchString)

                    || c.userName.ToLower().Contains(SearchString) || c.StartAddress.ToLower().Contains(SearchString) || c.address.ToLower().Contains(SearchString) || c.LastTime.ToLower().Contains(SearchString) || c.StartTime.ToLower().Contains(SearchString) || Convert.ToString(c.IdelTime).ToLower().Contains(SearchString)

                       || c.userName.ToUpper().Contains(SearchString) || c.StartAddress.ToUpper().Contains(SearchString) || c.address.ToUpper().Contains(SearchString) || c.LastTime.ToUpper().Contains(SearchString) || c.StartTime.ToUpper().Contains(SearchString) || Convert.ToString(c.IdelTime).ToUpper().Contains(SearchString)).ToList();

                    data = model.ToList();
                }

                foreach (var x in data)
                {
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                    string workHours = spWorkMin.ToString(@"hh\:mm");
                    obj.Add(new CMSBEmplyeeIdelGrid()
                    {
                        UserName = x.userName,
                        Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                        StartTime = x.StartTime,
                        EndTime = x.LastTime,
                        StartAddress = x.StartAddress,
                        EndAddress = x.address,
                        IdelTime = workHours,
                        startlat = x.StarLat,
                        startlong = x.StartLog,
                        endlat = x.lat,
                        endlong = x.@long,
                        userId = x.userId
                    });
                }

                return obj.Skip(Fetch_Next).Take(Offset).ToList();
            }
        }

        public List<CMSBEmpolyeeSummaryGrid> GetEmployeeSummary(int appId, DateTime fdate, DateTime tdate, int? UserId, int Offset, int Fetch_Next, string SearchString)
        {
            List<CMSBEmpolyeeSummaryGrid> obj = new List<CMSBEmpolyeeSummaryGrid>();
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                //var data = db.SP_EmployeeSummary(UserId, fdate,).ToList();
                var data = db.SP_EmployeeSummary(fdate, tdate, UserId == 0 ? null : UserId).ToList();

                //var data2 = data1.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();

                foreach (var x in data)
                {
                    //TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                    //string workHours = spWorkMin.ToString(@"hh\:mm");

                    string EndDate = "";
                    if (x.Enddate == null)
                    {
                        EndDate = "";
                    }
                    else
                    {
                        EndDate = Convert.ToDateTime(x.Enddate).ToString("dd/MM/yyyy");
                    }

                    obj.Add(new CMSBEmpolyeeSummaryGrid()
                    {
                        UserName = x.userName,
                        userId = x.userId,
                        // Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                        daDate = Convert.ToDateTime(x.Startdate).ToString("dd/MM/yyyy"),
                        StartTime = x.StartTime,
                        DaEndDate = EndDate,
                        EndTime = x.EndTime,
                        Totalhousecollection = (x.Totalhousecollection).ToString(),
                        Totaldumpyard = (x.Totaldumpyard).ToString(),
                        //Totaldistance = "0", //string.Format("{0:0.0}", (x.to)).ToString(),
                        Totaldistance = string.Format("{0:0.0}", (x.Totaldistance)).ToString(),
                        BatteryStatus = x.BatteryStatus,
                    });
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = obj.Where(c => c.UserName.Contains(SearchString) || c.daDate.Contains(SearchString) || c.UserName.Contains(SearchString)

                     || c.daDate.ToLower().Contains(SearchString) || c.UserName.ToLower().Contains(SearchString)).ToList();

                    obj = model.ToList();
                }

                if (UserId > 0)
                {
                    var model = obj.Where(c => c.userId == UserId).ToList();

                    obj = model.ToList();
                }
                return obj.OrderByDescending(c => c.daID).Skip(Fetch_Next).Take(Offset).ToList();
            }
        }


        public BigVQRHPDVM2 GetScanifyHouseDetailsData(int appId, string ReferenceId, int gcType)
        {
            BigVQRHPDVM2 Details = new BigVQRHPDVM2();

            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var model = db.SP_HousePointDumpDetails_Scanify(ReferenceId, gcType).FirstOrDefault();

                if (gcType == 1)
                {
                    if (model != null)
                    {
                        Details.houseId = model.houseId.ToString();
                        Details.wardId = model.WardNoId;
                        Details.areaId = model.AreaId;
                        Details.zoneId = model.zoneId;
                        Details.Address = model.Address;
                        Details.houseNumber = model.HouseNumber;
                        Details.mobileno = model.MobileNumber;
                        Details.name = model.Name;
                        Details.namemar = model.NameMar;
                        Details.QRCode = ThumbnaiUrlCMS + model.Images.Trim();
                        Details.ReferanceId = model.ReferanceId;
                        Details.Lat = model.Lat;
                        Details.Long = model.Long;
                        Details.gcType = gcType;
                        Details.Status = "Success";
                        Details.Message = "Successfully";
                    }
                    else
                    {
                        Details.Status = "error";
                        Details.Message = "HouseID not exists";
                    }
                }

                else if (gcType == 2)
                {
                    if (model != null)
                    {
                        Details.gpId = model.houseId;
                        Details.wardId = model.WardNoId;
                        Details.areaId = model.AreaId;
                        Details.zoneId = model.zoneId;
                        Details.Address = model.Address;
                        Details.houseNumber = model.HouseNumber;
                        Details.mobileno = model.MobileNumber;
                        Details.name = model.Name;
                        Details.namemar = model.NameMar;
                        Details.QRCode = ThumbnaiUrlCMS + model.Images.Trim();
                        Details.ReferanceId = model.ReferanceId;
                        Details.Lat = model.Lat;
                        Details.Long = model.Long;
                        Details.gcType = gcType;
                        Details.Status = "Success";
                        Details.Message = "Successfully";
                    }
                    else
                    {
                        Details.Status = "error";
                        Details.Message = "PointID not exists";
                    }
                }
                else if (gcType == 3)
                {
                    if (model != null)
                    {
                        Details.dyId = model.houseId.ToString();
                        Details.wardId = model.WardNoId;
                        Details.areaId = model.AreaId;
                        Details.zoneId = model.zoneId;
                        Details.Address = model.Address;
                        Details.houseNumber = model.HouseNumber;
                        Details.mobileno = model.MobileNumber;
                        Details.name = model.Name;
                        Details.namemar = model.NameMar;
                        Details.QRCode = ThumbnaiUrlCMS + model.Images.Trim();
                        Details.ReferanceId = model.ReferanceId;
                        Details.Lat = model.Lat;
                        Details.Long = model.Long;
                        Details.gcType = gcType;
                        Details.Status = "Success";
                        Details.Message = "Successfully";
                    }
                    else
                    {
                        Details.Status = "error";
                        Details.Message = "DumpYardID not exists";
                    }
                }


                return Details;
            }
        }


        #region Dustbin 
        public Result SaveDustBinDetails(string DeviceId, string lat, string Long, string Distance, string Temp, string S1, string S2)
        {
            var binMaster = dbMain.BinMasters.Where(c => c.DeviceId == DeviceId).FirstOrDefault();
            //dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();

            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(Convert.ToInt32(binMaster.AppId)))
            {
                try
                {
                    BinLatLong model = new BinLatLong();
                    model.lat = lat;
                    model.@long = Long;
                    model.Distance = Distance;
                    model.Temp = Temp;
                    model.S1 = S1;
                    model.S2 = S2;
                    model.CreatedDate = DateTime.Now;
                    db.BinLatLongs.Add(model);
                    db.SaveChanges();
                    result.status = "success";
                    result.message = "Uploaded successfully";
                    result.messageMar = "सबमिट यशस्वी";
                }
                catch
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    return result;
                }
                return result;
            }

        }
        #endregion

        #region HouseOnmap

        public List<CMSBHouseLocationOnMap> GetHouseLocationOnMap(int AppId, DateTime gcdate, int userid, int areaid, int WardNo)
        {

            List<CMSBHouseLocationOnMap> houseLocation = new List<CMSBHouseLocationOnMap>();
            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(gcdate), userid == -1 ? 0 : userid, areaid, WardNo, null, null, 0).ToList();
                foreach (var x in data)
                {

                    DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
                    //string gcTime = x.gcDate.ToString();
                    houseLocation.Add(new CMSBHouseLocationOnMap()
                    {
                        houseId = Convert.ToInt32(x.houseId),
                        ReferanceId = x.ReferanceId,
                        houseOwnerName = x.houseOwner,
                        houseOwnerMobile = x.houseOwnerMobile,
                        houseAddress = x.houseAddress,
                        gcDate = dt.ToString("dd-MM-yyyy"),
                        gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
                                                         //string gcTime = x.gcDate.ToString(),
                                                         //gcTime = x.gcDate.ToString("hh:mm tt"),
                                                         //myDateTime.ToString("HH:mm:ss")
                        ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
                        //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
                        houseLat = x.houseLat,
                        houseLong = x.houseLong,
                        // address = x.houseAddress,
                        //vehcileNumber = x.v,
                        //userMobile = x.mobile,
                        garbageType = x.garbageType,
                    });
                }
                return houseLocation;
            }
        }


        //public List<CMSBHouseLocationOnMap> GetHouseLocationOnMap(int AppId, DateTime gcdate, int userid)
        //{

        //    List<CMSBHouseLocationOnMap> houseLocation = new List<CMSBHouseLocationOnMap>();
        //    using (var db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        int AreaId = 0;

        //        var data = db.SP_HouseOnMapDetails(Convert.ToDateTime(gcdate), userid, AreaId).ToList();

        //        foreach (var x in data)
        //        {

        //            DateTime dt = DateTime.Parse(x.gcDate == null ? DateTime.Now.ToString() : x.gcDate.ToString());
        //            //string gcTime = x.gcDate.ToString();
        //            houseLocation.Add(new CMSBHouseLocationOnMap()
        //            {
        //                houseId = Convert.ToInt32(x.houseId),
        //                ReferanceId = x.ReferanceId,
        //                houseOwnerName = x.houseOwner,
        //                houseOwnerMobile = x.houseOwnerMobile,
        //                houseAddress = x.houseAddress,
        //                gcDate = dt.ToString("dd-MM-yyyy"),
        //                gcTime = dt.ToString("h:mm tt"), // 7:00 AM // 12 hour clock
        //                                                 //string gcTime = x.gcDate.ToString(),
        //                                                 //gcTime = x.gcDate.ToString("hh:mm tt"),
        //                                                 //myDateTime.ToString("HH:mm:ss")
        //                                                 ///date = Convert.ToDateTime(x.datt).ToString("dd/MM/yyyy"),
        //                //time = Convert.ToDateTime(x.datt).ToString("hh:mm:ss tt"),
        //                houseLat = x.houseLat,
        //                houseLong = x.houseLong,
        //                // address = x.houseAddress,
        //                //vehcileNumber = x.v,
        //                //userMobile = x.mobile,
        //                garbageType = x.garbageType == null ? -1 : x.garbageType


        //            });
        //        }
        //        return houseLocation;
        //    }
        //}
        #endregion

        #region Citizen 

        public CitizenMobileDetails GetMobileDetails(int AppId, string ReferanceId, string FCMID)
        {
            CitizenMobileDetails obj = new CitizenMobileDetails();

            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var model = db.HouseMasters.Where(x => x.ReferanceId == ReferanceId).FirstOrDefault();
                if (model != null)
                {
                    obj.Status = "Success";
                    obj.MobileNo = model.houseOwnerMobile;
                    obj.Message = "HouseID exists";
                    obj.MessageMar = "घर अस्तित्वात आहे.";
                    obj.ReferenceId = ReferanceId;
                    //model.FCMID = FCMID;
                    db.SaveChanges();
                }
                else
                {
                    obj.Status = "error";
                    obj.MobileNo = "";
                    obj.Message = "HouseID not exists";
                    obj.MessageMar = "घर अस्तित्वात नाही";
                    obj.ReferenceId = ReferanceId;
                }
            }

            return obj;
        }


        public CitizenMobileOTP GetSendOTP(int AppId, string ReferanceId, string _Mobile)
        {
            CitizenMobileOTP obj = new CitizenMobileOTP();

            string characters = "1234567890";

            string otp = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
            {
                var model = db.HouseMasters.Where(x => x.ReferanceId == ReferanceId).FirstOrDefault();
                if (model != null)
                {
                    model.houseOwnerMobile = _Mobile;
                   // db.SaveChanges();
                    string msg = "Your OTP is " + otp + ". Do not Share it with anyone by any means. This is confidential and to be used by you only. ICTSBM";

                    sendSMS(msg, _Mobile);
                    if (AppId == 3068 || AppId == 3098 || AppId==3107 || AppId == 3109 || AppId == 3111)
                    {
                        sendSMSNgp(msg, _Mobile);
                    }
                    obj.Status = "Success";
                    obj.OTP = otp;
                    obj.Message = "OTP sent successfully.";
                    obj.MessageMar = "ओटीपी यशस्वीरित्या पाठवले.";
                    obj.MobileNo = _Mobile;
                }
                else
                {
                    obj.Status = "error";
                    obj.Message = "HouseID not exists";
                    obj.MessageMar = "घर अस्तित्वात नाही";
                }
            }



            return obj;
        }

        public Result1 SaveDeviceDetails(int appId, string ReferanceId, string FCMID, string DeviceID, string Mobile)
        {
            screenService = new ScreenService(appId);
            Result1 result = new Result1();
            result = screenService.SaveDeviceDetails(appId, ReferanceId, FCMID, DeviceID, Mobile);
            return result;
        }

        public Result1 SaveDeviceDetailsClear(int appId, string DeviceID, string ReferenceID)
        {
            screenService = new ScreenService(appId);
            Result1 result = new Result1();
            result = screenService.SaveDeviceDetailsClear(appId, DeviceID, ReferenceID);
            return result;
        }

        //public List<CitizenQuestionMaster> GetQuestions(int AppId)
        //{
        //    List<CitizenQuestionMaster> obj = new List<CitizenQuestionMaster>();

        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        var model = db.QuestionMasters.ToList();
        //        if (model != null)
        //        {
        //            foreach (var x in model)
        //            {
        //                obj.Add(new CitizenQuestionMaster()
        //                {
        //                    QuestionId = x.ID,
        //                    Question=x.Question
        //                });
        //            }
        //        }
        //    }
        //    return obj;
        //}

        //public Result GetAnswerDetails(string Json, int AppId, int UserID)
        //{
        //    Result Result = new Result();
        //    using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
        //    {
        //        var model = db.AnswerDetails.Where(x => x.UserID == UserID & EntityFunctions.TruncateTime(x.AnsDate) == EntityFunctions.TruncateTime(DateTime .Now)).FirstOrDefault();
        //        if (model != null)
        //        {
        //            Result.status = "error";
        //            Result.message = "Already answered.";
        //            Result.messageMar = "आधीच उत्तर दिले आहे.";
        //            Result.isAttendenceOff = false;
        //        }
        //        else
        //        {
        //            AnswerDetail Answer = new AnswerDetail();
        //            Answer.UserID = UserID;
        //            Answer.Json = Json;
        //            Answer.AnsDate = DateTime.Now;
        //            db.AnswerDetails.Add(Answer);
        //            db.SaveChanges();

        //            Result.status = "success";
        //            Result.message = "Answer submitted successfully.";
        //            Result.messageMar = "उत्तर यशस्वीरित्या सबमिट केला.";
        //            Result.isAttendenceOff = false;
        //        }
        //    }
        //        return Result;
        //}


        public GPHousedetailsVM GetGPHouseDetails(int appId, string ReferanceId)
        {
            GPHousedetailsVM GPHouse = new GPHousedetailsVM();

            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.HouseDetails_ReferanceId(ReferanceId).FirstOrDefault();
                if (data != null)
                {
                    GPHouse.Name = data.Name;
                    GPHouse.NameMar = data.NameMar;
                    GPHouse.Ward = data.Ward;
                    GPHouse.Area = data.Area;
                    GPHouse.ReferanceId = data.ReferanceId;
                    GPHouse.zone = data.Zone;
                }

            }
            return GPHouse;
        }

        public List<CitizenCTPTAddress> GetCTPTAddress(int appId)
        {
            List<CitizenCTPTAddress> Location = new List<CitizenCTPTAddress>();
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SauchalayAddresses.Where(c => c.Lat != null && c.Long != null).ToList();
                foreach (var x in data)
                {
                    Location.Add(new CitizenCTPTAddress()
                    {
                        SauchalayID = x.SauchalayID,
                        Address = x.Address,
                        Lat = x.Lat,
                        Long = x.Long,
                        Name = x.Name,
                    });
                }
                return Location;
            }
        }

        #endregion


        // Added by Neha 24 july 2019
        public List<CMSBUserLocationMapVM> GetHouseAttenRoute(int _AppId, int daId)
        {
            screenService = new ScreenService(_AppId);
            return screenService.GetHouseAttenRoute(daId);
        }


        // House Scanify API Code

        public SBUser CheckSupervisorUserLogin(string userName, string password, string EmpType)
        {
            SBUser user = new SBUser();
            if (EmpType == "A")
            {
                user = SupervisorLogin(userName, password, EmpType);
            }
            else
            {
                user.name = "";
                user.userId = 0;
                user.userLoginId = "";
                user.userPassword = "";
                user.status = "error";
                user.gtFeatures = false;
                user.EmpType = "";
                user.imiNo = "";
                user.message = "Employee Type Does not Match.";
                user.messageMar = "कर्मचारी प्रकार जुळत नाही.";
            }
            return user;
        }

        public SBUser SupervisorLogin(string userName, string password, string EmpType)
        {
            SBUser user = new SBUser();
            var obj = dbMain.EmployeeMasters.Where(c => c.LoginId == userName & c.Password == password & c.isActive == true).FirstOrDefault();
            if (obj == null)
            {
                user.name = "";
                user.userId = 0;
                user.userLoginId = "";
                user.userPassword = "";
                user.status = "error";
                user.gtFeatures = false;
                user.EmpType = "";
                user.imiNo = "";
                user.message = "UserName or Passward not Match.";
                user.messageMar = "वापरकर्ता नाव किंवा पासवर्ड जुळत नाही.";
            }
            else if (obj != null && obj.LoginId == userName && obj.Password == password)
            {

                user.name = obj.EmpName;
                user.userId = obj.EmpId;
                user.userLoginId = obj.LoginId;
                user.userPassword = obj.Password;
                user.EmpType = checkNull(obj.type); ;
                user.imiNo = "";
                user.type = "";
                user.gtFeatures = true;
                user.status = "success"; user.message = "Login Successfully"; user.messageMar = "लॉगिन यशस्वी";
            }

            return user;
        }

        public List<NameULB> GetUlb(int userId, string EmpType, string Status)
        {
            List<NameULB> obj = new List<NameULB>();
            var ids = dbMain.EmployeeMasters.Where(c => c.EmpId == userId).Select(c => c.isActiveULB).FirstOrDefault();
            if (ids != null)
            {

                string[] authorsList = ids.Split(',');
                foreach (string author in authorsList)
                {
                    if (Status == "false")
                    {
                        var data = dbMain.AppDetails.Where(c => c.AppId.ToString().ToLower().Contains(author.ToLower())).ToList();
                        foreach (var x in data)
                        {
                            obj.Add(new NameULB()
                            {
                                ulb = (x.AppName.ToString()),
                                appid = (x.AppId),
                                faq = x.FAQ,
                            });
                        }
                    }
                    else if (Status == "true")
                    {
                        var data = dbMain.AppDetails.Where(c => c.AppId.ToString().ToLower().Contains(author.ToLower()) && c.FAQ != "0").ToList();
                        foreach (var x in data)
                        {
                            obj.Add(new NameULB()
                            {
                                ulb = (x.AppName.ToString()),
                                appid = (x.AppId),
                                faq = x.FAQ,
                            });
                        }
                    }
                }
            }
            else
            {
                if (Status == "false")
                {
                    var data = dbMain.AppDetails.Where(c => c.IsActive == true && c.AppId !=3088 && c.AppId != 3068).ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new NameULB()
                        {
                            ulb = (x.AppName.ToString()),
                            appid = (x.AppId),
                            faq = x.FAQ,
                        });
                    }
                }
                else if (Status == "true")
                {
                    var data = dbMain.AppDetails.Where(c => c.IsActive == true && c.FAQ != "0" && c.AppId != 3088 && c.AppId != 3068).ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new NameULB()
                        {
                            ulb = (x.AppName.ToString()),
                            appid = (x.AppId),
                            faq = x.FAQ,
                        });
                    }
                }


            }

            return obj.OrderBy(c => c.ulb).ToList();
        }

        public HSDashboard GetSelectedUlbData(int userId, string EmpType, int appId)
        {
            HSDashboard model = new HSDashboard();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(appId))
                {
                    DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
                    var appdetails = dbm.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();
                    var data = db.SP_HouseScanifyDetails(appId).First();
                    if (data != null)
                    {
                        model.AppId = appdetails.AppId;
                        model.AppName = appdetails.AppName;
                        model.TotalHouse = data.TotalHouse;
                        model.TotalHouseUpdated = data.TotalHouseUpdated;
                        model.TotalHouseUpdated_CurrentDay = data.TotalHouseUpdated_CurrentDay;
                        model.TotalPoint = data.TotalPoint;
                        model.TotalPointUpdated = data.TotalPointUpdated;
                        model.TotalPointUpdated_CurrentDay = data.TotalPointUpdated_CurrentDay;
                        model.TotalDump = data.TotalDump;
                        model.TotalDumpUpdated = data.TotalDumpUpdated;
                        model.TotalDumpUpdated_CurrentDay = data.TotalDumpUpdated_CurrentDay;

                        model.TotalLiquid = data.TotalLiquid;
                        model.TotalLiquidUpdated = data.TotalLiquidUpdated;
                        model.TotalLiquidUpdated_CurrentDay = data.TotalLiquidUpdated_CurrentDay;

                        model.TotalStreet = data.TotalStreet;
                        model.TotalStreetUpdated = data.TotalStreetUpdated;
                        model.TotalStreetUpdated_CurrentDay = data.TotalStreetUpdated_CurrentDay;

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

        public List<HSEmployee> GetQREmployeeList(int userId, string EmpType, int appId)
        {
            List<HSEmployee> obj = new List<HSEmployee>();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(appId))
                {
                    {
                        var data = db.QrEmployeeMasters.Where(c => c.isActive == true).ToList();
                        foreach (var x in data)
                        {
                            obj.Add(new HSEmployee()
                            {
                                EmployeeName = (x.qrEmpName.ToString()),
                                EmployeeID = (x.qrEmpId),
                            });
                        }
                    }

                    return obj.OrderBy(c => c.EmployeeName).ToList();
                }
            }
            catch (Exception)
            {
                return obj;
            }

        }

        public List<HouseScanifyEmployeeDetails> GetQREmployeeDetailsList(int userId, string EmpType, int appId, int QrEmpID,bool status)
        {
            List<HouseScanifyEmployeeDetails> obj = new List<HouseScanifyEmployeeDetails>();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(appId))
                {
                    {
                        if (QrEmpID != 0)
                        {
                            var data = db.QrEmployeeMasters.Where(c => c.qrEmpId == QrEmpID).ToList();
                            foreach (var x in data)
                            {
                                obj.Add(new HouseScanifyEmployeeDetails()
                                {
                                    qrEmpId = x.qrEmpId,
                                    qrEmpName = x.qrEmpName.ToString(),
                                    qrEmpLoginId = x.qrEmpLoginId,
                                    qrEmpPassword = x.qrEmpPassword,
                                    qrEmpMobileNumber = x.qrEmpMobileNumber,
                                    qrEmpAddress = x.qrEmpAddress,
                                    type = x.type,
                                    typeId = x.typeId,
                                    imoNo = x.imoNo,
                                    bloodGroup = x.bloodGroup,
                                    isActive = x.isActive,
                                    target = x.target,
                                    lastModifyDate = x.lastModifyDate,
                                });
                            }
                        }
                        else
                        {
                            var data = db.QrEmployeeMasters.Where(c => c.isActive == status).ToList();
                            foreach (var x in data)
                            {
                                obj.Add(new HouseScanifyEmployeeDetails()
                                {
                                    qrEmpId = x.qrEmpId,
                                    qrEmpName = x.qrEmpName.ToString(),
                                    qrEmpLoginId = x.qrEmpLoginId,
                                    qrEmpPassword = x.qrEmpPassword,
                                    qrEmpMobileNumber = x.qrEmpMobileNumber,
                                    qrEmpAddress = x.qrEmpAddress,
                                    type = x.type,
                                    typeId = x.typeId,
                                    imoNo = x.imoNo,
                                    bloodGroup = x.bloodGroup,
                                    isActive = x.isActive,
                                    target = x.target,
                                    lastModifyDate = x.lastModifyDate,
                                });
                            }
                        }

                    }

                    return obj.OrderByDescending(c=>c.qrEmpId).ToList();
                }
            }
            catch (Exception)
            {
                return obj;
            }

        }

        public IEnumerable<HouseScanifyDetailsGridRow> GetHouseScanifyDetails(int qrEmpId, DateTime FromDate, DateTime Todate, int appId)
        {
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                List<HouseScanifyDetailsGridRow> obj = new List<HouseScanifyDetailsGridRow>();
                var data = db.SP_HouseScanify(FromDate, Todate, qrEmpId).ToList();//Select(x => new HouseScanifyDetailsGridRow
                                                                                 //{
                                                                                 //    qrEmpId = x.qrEMpId,
                                                                                 //    qrEmpName = x.qrEmpName,
                                                                                 //    qrEmpNameMar = x.qrEmpNameMar,
                                                                                 //    qrEmpMobileNumber = x.qrEmpMobileNumber,
                                                                                 //    qrEmpAddress = x.qrEmpAddress,
                                                                                 //    qrEmpLoginId = x.qrEmpLoginId,
                                                                                 //    qrEmpPassword = x.qrEmpPassword,
                                                                                 //    isActive = x.isActive,
                                                                                 //    bloodGroup = x.bloodGroup,
                                                                                 //    lastModifyDate = x.lastModifyDate,
                                                                                 //    HouseCount = x.HouseCount,
                                                                                 //    PointCount = x.PointCount,
                                                                                 //    DumpCount = x.DumpCount,
                                                                                 //    LiquidCount = x.LiquidCount,
                                                                                 //    StreetCount = x.StreetCount,


                //}).ToList();

                foreach (var x in data)
                {
                    var data1 = db.Qr_Employee_Daily_Attendance.Where(c => c.qrEmpId == x.qrEMpId && c.startDate >= EntityFunctions.TruncateTime(FromDate) && c.startDate <= EntityFunctions.TruncateTime(Todate)).ToList();

                    if (data1.Count != 0)
                    {
                        obj.Add(new HouseScanifyDetailsGridRow()
                        {
                            qrEmpId = x.qrEMpId,
                            qrEmpName = x.qrEmpName,
                            qrEmpNameMar = x.qrEmpNameMar,
                            qrEmpMobileNumber = x.qrEmpMobileNumber,
                            qrEmpAddress = x.qrEmpAddress,
                            qrEmpLoginId = x.qrEmpLoginId,
                            qrEmpPassword = x.qrEmpPassword,
                            isActive = x.isActive,
                            bloodGroup = x.bloodGroup,
                            lastModifyDate = x.lastModifyDate,
                            HouseCount = x.HouseCount,
                            PointCount = x.PointCount,
                            DumpCount = x.DumpCount,
                            LiquidCount = x.LiquidCount,
                            StreetCount = x.StreetCount,
                        });
                    }


                }

                return obj.OrderByDescending(c => c.LiquidCount).OrderByDescending(c => c.HouseCount).OrderByDescending(c => c.StreetCount);

            }
        }

        public IEnumerable<HSAttendanceGrid> GetAttendanceDetails(int userId, DateTime FromDate, DateTime Todate, int appId)
        {
            List<HSAttendanceGrid> obj = new List<HSAttendanceGrid>();
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {

                var data = (from t1 in db.Qr_Employee_Daily_Attendance
                            join t2 in db.QrEmployeeMasters on t1.qrEmpId equals t2.qrEmpId
                            select new
                            {
                                t1.qrEmpDaId,
                                t1.qrEmpId,
                                t1.startDate,
                                t1.endDate,
                                t1.startTime,
                                t1.endTime,
                                t1.startLat,
                                t1.startLong,
                                t1.endLat,
                                t1.endLong,
                                t1.startNote,
                                t1.endNote,
                                t2.qrEmpName,

                            }).OrderByDescending(c => c.startDate).ThenByDescending(c => c.startTime).ToList();

                //return obj.OrderBy(c => c.Date).ThenByDescending(c => c.StartTime);

                //var data = db.Qr_Employee_Daily_Attendance.OrderByDescending(c => c.qrEmpDaId).ToList();

                if (Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                {
                    data = data.OrderByDescending(c => c.qrEmpDaId).Where(c => (c.startDate == FromDate || c.endDate == FromDate || c.endTime == "")).ToList();
                }
                else
                {

                    data = data.OrderByDescending(c => c.qrEmpDaId).Where(c => (c.startDate >= FromDate && c.startDate <= Todate) || (c.startDate >= FromDate && c.startDate <= Todate)).ToList();
                }

                if (userId > 0)
                {
                    var model = data.OrderByDescending(c => c.qrEmpDaId).Where(c => c.qrEmpId == userId).ToList();

                    data = model.ToList();
                }

                foreach (var x in data)
                {

                    DateTime cDate = DateTime.Now;

                    TimeSpan timespan = new TimeSpan(00, 00, 00);
                    DateTime time = DateTime.Now.Add(timespan);
                    string displayTime = time.ToString("hh:mm tt");


                    string displayTime1 = Convert.ToDateTime(x.startDate).ToString("MM/dd/yyyy");
                    string sTime = Convert.ToDateTime(x.startTime).ToString("HH:mm:ss");

                    var a = (Convert.ToDateTime(x.startDate).ToString("MM/dd/yyyy"));
                    var b = x.endDate == null ? Convert.ToDateTime(cDate).ToString("MM/dd/yyyy") : Convert.ToDateTime(x.endDate).ToString("MM/dd/yyyy");

                    string Time1 = (x.startTime).ToString();
                    string Time2 = ((x.endTime == "" ? displayTime : x.endTime).ToString());

                    DateTime startDate = Convert.ToDateTime(a + " " + Time1);
                    DateTime endDate = Convert.ToDateTime(b + " " + Time2);
                    var houseCount = db.HouseMasters.Where(c => c.modified >= startDate && c.modified <= endDate && c.userId == x.qrEmpId).Count();
                    var liquidCount = db.LiquidWasteDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();
                    var streetCount = db.StreetSweepingDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();
                    var dumpyardcount = db.DumpYardDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();

                    string endate = "";
                    if (x.endDate == null)
                    {
                        endate = "";
                    }
                    else
                    {
                        endate = Convert.ToDateTime(x.endDate).ToString("dd/MM/yyyy");
                    }
                    obj.Add(new HSAttendanceGrid()
                    {
                        qrEmpDaId = x.qrEmpDaId,
                        qrEmpId = Convert.ToInt32(x.qrEmpId),
                        userName = x.qrEmpName,
                        startDate = Convert.ToDateTime(x.startDate).ToString("dd/MM/yyyy"),
                        endDate = endate,
                        startTime = checkNull(x.startTime),
                        endTime = checkNull(x.endTime),
                        startLat = checkNull(x.startLat),
                        startLong = checkNull(x.startLong),
                        endLat = checkNull(x.endLat),
                        endLong = checkNull(x.endLong),
                        startNote = checkNull(x.startNote),
                        endNote = checkNull(x.endNote),
                        CompareDate = x.startDate,
                        HouseCount = houseCount,
                        LiquidCount = liquidCount,
                        StreetCount = streetCount,
                        DumpYardCount = dumpyardcount,
                        daDateTIme = (displayTime1 + " " + sTime)



                    });
                }

                return obj.OrderByDescending(c => c.qrEmpDaId).ToList();
            }

        }

        public IEnumerable<HSHouseDetailsGrid> GetHouseDetails(int userId, DateTime FromDate, DateTime Todate, int appId, string ReferanceId)
        {
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_HouseDetailsApp(FromDate, Todate, userId, ReferanceId).Select(x => new HSHouseDetailsGrid
                {
                    houseId = x.houseId,
                    Name = x.qrEmpName,
                    Lat = x.houseLat,
                    Long = x.houseLong,
                    QRCodeImage = x.QRCodeImage,
                    ReferanceId = x.ReferanceId,
                    modifiedDate = x.modified.HasValue ? Convert.ToDateTime(x.modified).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatus=x.QRStatus,


                }).ToList();

                return data;
            }
        }

        public IEnumerable<HSDumpYardDetailsGrid> GetDumpYardDetails(int userId, DateTime FromDate, DateTime Todate, int appId)
        {
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_DumpYardDetailsApp(FromDate, Todate, userId).Select(x => new HSDumpYardDetailsGrid
                {
                    dyId = x.dyId,
                    Name = x.qrEmpName,
                    Lat = x.dyLat,
                    Long = x.dyLong,
                    QRCodeImage = x.QRCodeImage,
                    ReferanceId = x.ReferanceId,
                    modifiedDate = x.lastModifiedDate.HasValue ? Convert.ToDateTime(x.lastModifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatus=x.QRStatus,


                }).ToList();

                return data;
            }
        }

        public IEnumerable<HSLiquidDetailsGrid> GetLiquidDetails(int userId, DateTime FromDate, DateTime Todate, int appId)
        {
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_LiquidDetailsApp(FromDate, Todate, userId).Select(x => new HSLiquidDetailsGrid
                {
                    LWId = x.LWId,
                    Name = x.qrEmpName,
                    Lat = x.LWLat,
                    Long = x.LWLong,
                    QRCodeImage = x.QRCodeImage,
                    ReferanceId = x.ReferanceId,
                    modifiedDate = x.lastModifiedDate.HasValue ? Convert.ToDateTime(x.lastModifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatus = x.QRStatus,

                }).ToList();

                return data;
            }
        }

        public IEnumerable<HSStreetDetailsGrid> GetStreetDetails(int userId, DateTime FromDate, DateTime Todate, int appId)
        {
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_StreetDetailsApp(FromDate, Todate, userId).Select(x => new HSStreetDetailsGrid
                {
                    SSId = x.SSId,
                    Name = x.qrEmpName,
                    Lat = x.SSLat,
                    Long = x.SSLong,
                    QRCodeImage = x.QRCodeImage,
                    ReferanceId = x.ReferanceId,
                    modifiedDate = x.lastModifiedDate.HasValue ? Convert.ToDateTime(x.lastModifiedDate).ToString("dd/MM/yyyy hh:mm tt") : "",
                    QRStatus = x.QRStatus,

                }).ToList();

                return data;
            }
        }


        public List<UserRoleDetails> UserRoleList(int userId, string EmpType, bool status, int EmpId)
        {
            List<UserRoleDetails> obj = new List<UserRoleDetails>();
            try
            {
                if (EmpId != 0)
                {
                    var data = dbMain.EmployeeMasters.Where(c => c.isActive == status && c.EmpId == EmpId).ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new UserRoleDetails()
                        {
                            EmpId = x.EmpId,
                            EmpName = x.EmpName.ToString(),
                            LoginId = x.LoginId,
                            Password = x.Password,
                            EmpMobileNumber = x.EmpMobileNumber,
                            EmpAddress = x.EmpAddress,
                            type = x.type,
                            isActive = x.isActive,
                            isActiveULB = x.isActiveULB,
                            LastModifyDateEntry = Convert.ToDateTime(x.lastModifyDateEntry).ToString("dd-MM-yyyy hh:mm"),
                        });
                    }
                }
                else
                {
                    var data = dbMain.EmployeeMasters.Where(c => c.isActive == status).ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new UserRoleDetails()
                        {
                            EmpId = x.EmpId,
                            EmpName = x.EmpName.ToString(),
                            LoginId = x.LoginId,
                            Password = x.Password,
                            EmpMobileNumber = x.EmpMobileNumber,
                            EmpAddress = x.EmpAddress,
                            type = x.type,
                            isActive = x.isActive,
                            isActiveULB = x.isActiveULB,
                            LastModifyDateEntry = Convert.ToDateTime(x.lastModifyDateEntry).ToString("dd-MM-yyyy hh:mm"),
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                return obj;
            }

            return obj.OrderByDescending(c => c.EmpId).ToList();
        }


        public CollectionSyncResult SaveAddEmployee(HouseScanifyEmployeeDetails obj, int AppId)
        {
            CollectionSyncResult result = new CollectionSyncResult();
            QrEmployeeMaster objdata = new QrEmployeeMaster();
            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    if (obj.qrEmpId != 0)
                    {
                        var model = db.QrEmployeeMasters.Where(c => c.qrEmpId == obj.qrEmpId).FirstOrDefault();
                        if (model != null)
                        {

                            //var isrecord = db.QrEmployeeMasters.Where(x => x.qrEmpName == obj.qrEmpName && x.isActive == true).FirstOrDefault();
                            //if (isrecord == null)
                            //{

                                //var isrecord1 = db.QrEmployeeMasters.Where(x => x.qrEmpLoginId == obj.qrEmpLoginId && x.isActive == true).FirstOrDefault();
                                //var isrecord2 = db.UserMasters.Where(x => x.userLoginId == obj.qrEmpLoginId && x.isActive == true).FirstOrDefault();
                                //if (isrecord1 == null && isrecord2 == null)
                                //{

                                    model.qrEmpId = obj.qrEmpId;
                                    model.qrEmpName = obj.qrEmpName;
                                   // model.qrEmpLoginId = obj.qrEmpLoginId;
                                    model.qrEmpPassword = obj.qrEmpPassword;
                                    model.qrEmpMobileNumber = obj.qrEmpMobileNumber;
                                    model.qrEmpAddress = obj.qrEmpAddress;
                                    model.type = "Employee";
                                    model.typeId = 1;
                                    model.imoNo = obj.imoNo;
                                    model.bloodGroup = "0";
                                    model.isActive = obj.isActive;

                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Employee Details Updated successfully";
                                    result.messageMar = "कर्मचारी तपशील यशस्वीरित्या अद्यतनित केले";
                                //}
                                //else
                                //{
                                //    result.status = "Error";
                                //    result.message = "This LoginId Is Already Exist !";
                                //    result.messageMar = "हे लॉगिनआयडी आधीच अस्तित्वात आहे !";
                                //    return result;
                                //}
                            //}
                            //else
                            //{
                            //    result.status = "Error";
                            //    result.message = "Name Already Exist";
                            //    result.messageMar = "नाव आधीपासून अस्तित्वात आहे..";
                            //    return result;
                            //}
                           
                        }
                        else
                        {
                            result.message = "This Employee Not Available.";
                            result.messageMar = "कर्मचारी उपलब्ध नाही.";
                            result.status = "error";
                            return result;

                        }

                    }
                    else
                    {
                        var isrecord = db.QrEmployeeMasters.Where(x => x.qrEmpName == obj.qrEmpName && x.isActive == true).FirstOrDefault();
                        if(isrecord == null)
                        {
                            var isrecord1 = db.QrEmployeeMasters.Where(x => x.qrEmpLoginId == obj.qrEmpLoginId && x.isActive == true).FirstOrDefault();
                            var isrecord2 = db.UserMasters.Where(x => x.userLoginId == obj.qrEmpLoginId && x.isActive == true).FirstOrDefault();
                            if (isrecord1 == null && isrecord2 == null)
                            {


                                objdata.qrEmpName = obj.qrEmpName;
                                objdata.qrEmpLoginId = obj.qrEmpLoginId;
                                objdata.qrEmpPassword = obj.qrEmpPassword;
                                objdata.qrEmpMobileNumber = obj.qrEmpMobileNumber;
                                objdata.qrEmpAddress = obj.qrEmpAddress;
                                objdata.type = "Employee";
                                objdata.typeId = 1;
                                //objdata.imoNo = obj.imoNo;
                                objdata.imoNo = null;
                                objdata.bloodGroup = "0";
                                objdata.isActive = obj.isActive;

                                db.QrEmployeeMasters.Add(objdata);
                                db.SaveChanges();
                                result.status = "success";
                                result.message = "Employee Details Added successfully";
                                result.messageMar = "कर्मचारी तपशील यशस्वीरित्या जोडले";
                                return result;
                            }
                            else
                            {
                                result.status = "Error";
                                result.message = "This LoginId Is Already Exist !";
                                result.messageMar = "हे लॉगिनआयडी आधीच अस्तित्वात आहे !";
                                return result;
                            }


                        }
                        else
                        {
                            result.status = "Error";
                            result.message = "Name Already Exist";
                            result.messageMar = "नाव आधीपासून अस्तित्वात आहे..";
                            return result;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.status = "error";
                    return result;
                }


            }

            return result;
        }

        public CollectionQRStatusResult UpdateQRstatus(HSHouseDetailsGrid obj, int AppId)
        {
            CollectionQRStatusResult result = new CollectionQRStatusResult();
            HouseMaster objdata = new HouseMaster();
            using (var db = new DevSwachhBharatNagpurEntities(AppId))
            {
                try
                {
                    if (obj.ReferanceId != null)
                    {
                        var model = db.HouseMasters.Where(c => c.ReferanceId == obj.ReferanceId).FirstOrDefault();
                        if (model != null)
                        {
                         
                            model.QRStatus = obj.QRStatus;
                            model.QRStatusDate = DateTime.Now;
                            db.SaveChanges();
                            result.ReferanceId = obj.ReferanceId;
                            result.status = "success";
                            result.message = "Record Updated successfully";
                            result.messageMar = "रेकॉर्ड यशस्वीरित्या अद्यतनित केले";
                            return result;
                        }
                        else
                        {
                            var dump = db.DumpYardDetails.Where(c => c.ReferanceId == obj.ReferanceId).FirstOrDefault();
                            if (dump != null)
                            {

                                dump.QRStatus = obj.QRStatus;
                                dump.QRStatusDate = DateTime.Now;
                                db.SaveChanges();
                                result.ReferanceId = obj.ReferanceId;
                                result.status = "success";
                                result.message = "Record Updated successfully";
                                result.messageMar = "रेकॉर्ड यशस्वीरित्या अद्यतनित केले";
                                return result;
                            }
                            else
                            {
                                var street = db.StreetSweepingDetails.Where(c => c.ReferanceId == obj.ReferanceId).FirstOrDefault();
                                if (street != null)
                                {

                                    street.QRStatus = obj.QRStatus;
                                    street.QRStatusDate = DateTime.Now;
                                    db.SaveChanges();
                                    result.ReferanceId = obj.ReferanceId;
                                    result.status = "success";
                                    result.message = "Record Updated successfully";
                                    result.messageMar = "रेकॉर्ड यशस्वीरित्या अद्यतनित केले";
                                    return result;
                                }
                                else
                                {
                                    var liquid = db.LiquidWasteDetails.Where(c => c.ReferanceId == obj.ReferanceId).FirstOrDefault();
                                    if (liquid != null)
                                    {

                                        liquid.QRStatus = obj.QRStatus;
                                        liquid.QRStatusDate = DateTime.Now;
                                        db.SaveChanges();
                                        result.ReferanceId = obj.ReferanceId;
                                        result.status = "success";
                                        result.message = "Record Updated successfully";
                                        result.messageMar = "रेकॉर्ड यशस्वीरित्या अद्यतनित केले";
                                        return result;
                                    }
                                    else
                                    {
                                        result.ReferanceId = obj.ReferanceId;
                                        result.message = "This Record Not Available.";
                                        result.messageMar = "रेकॉर्ड उपलब्ध नाही.";
                                        result.status = "error";
                                        return result;

                                    }
                                }
                            }
                        }
                        

                    }

                }
                catch (Exception ex)
                {
                    result.ReferanceId = obj.ReferanceId;
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    result.status = "error";
                    return result;
                }


            }

            return result;
        }



        public CollectionSyncResult SaveAddUserRole(UserRoleDetails obj)
        {
            CollectionSyncResult result = new CollectionSyncResult();
            EmployeeMaster objdata = new EmployeeMaster();

            try
            {
                if (obj.EmpId != 0)
                {
                    var model = dbMain.EmployeeMasters.Where(c => c.EmpId == obj.EmpId).FirstOrDefault();
                    if (model != null)
                    {
                        model.EmpId = obj.EmpId;
                        model.EmpName = obj.EmpName;
                        model.LoginId = obj.LoginId;
                        model.Password = obj.Password;
                        model.EmpMobileNumber = obj.EmpMobileNumber;
                        model.EmpAddress = obj.EmpAddress;
                        model.type = obj.type;
                        model.isActive = obj.isActive;
                        model.isActiveULB = obj.isActiveULB;
                        model.lastModifyDateEntry = DateTime.Now;


                        dbMain.SaveChanges();
                        result.status = "success";
                        result.message = "User Role Details Updated successfully";
                        result.messageMar = "वापरकर्ता भूमिका तपशील यशस्वीरित्या अद्यतनित केले";
                    }
                    else
                    {
                        result.message = "This User Role Not Available.";
                        result.messageMar = "वापरकर्ता भूमिका उपलब्ध नाही.";
                        result.status = "error";
                        return result;

                    }

                }
                else
                {
                    objdata.EmpName = obj.EmpName;
                    objdata.LoginId = obj.LoginId;
                    objdata.Password = obj.Password;
                    objdata.EmpMobileNumber = obj.EmpMobileNumber;
                    objdata.EmpAddress = obj.EmpAddress;
                    objdata.type = obj.type;
                    objdata.isActive = obj.isActive;
                    objdata.isActiveULB = obj.isActiveULB;
                    objdata.lastModifyDateEntry = DateTime.Now;

                    dbMain.EmployeeMasters.Add(objdata);
                    dbMain.SaveChanges();
                    result.status = "success";
                    result.message = "User Role Added successfully";
                    result.messageMar = "वापरकर्ता भूमिका तपशील यशस्वीरित्या जोडले";
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                result.status = "error";
                return result;
            }


            return result;
        }

        #region RFID 
        public Result SaveRfidDetails(string ReaderId, string TagId, string Lat, string Long, string Type, string DT)
        {
            var rfid = dbMain.RFID_Master.Where(c => c.ReaderID == ReaderId).FirstOrDefault();
            var AppId = rfid.AppID;
            //dbMain.AppDetails.Where(c => c.AppId == AppId).FirstOrDefault();

            //var date = Convert.ToDateTime(DT);
            // int size = 10;

            List<string> a = new List<string>();
            for (int i = 0; i < DT.Length; i += 10)
            {
                if ((i + 10) < DT.Length)
                    a.Add(DT.Substring(i, 10));
                else
                    a.Add(DT.Substring(i));
            }
            var z = a[0] + " " + a[1];

            Result result = new Result();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(Convert.ToInt32(AppId)))
            {
                try
                {
                    var house = db.HouseMasters.Where(c => c.RFIDTagId == TagId).FirstOrDefault();
                    if (house != null)
                    {
                        GarbageCollectionDetail model = new GarbageCollectionDetail();
                        Location objLoc = new Location();
                        var exist = db.GarbageCollectionDetails.Where(c => c.houseId == house.houseId && c.gcDate == EntityFunctions.TruncateTime(DateTime.Now)).Any();
                        if (exist != true)
                        {
                            if (house.houseId != null)
                            {
                                model.houseId = house.houseId;
                                model.RFIDReaderId = ReaderId;
                                model.RFIDTagId = TagId;
                                model.Lat = Lat;
                                model.Long = Long;
                                model.garbageType = Convert.ToInt32(Type);
                                model.gcDate = Convert.ToDateTime(z);
                                model.CreatedDate = DateTime.Now;
                                model.SourceId = 2;
                                objLoc.RFIDReaderId = ReaderId;
                                objLoc.RFIDTagId = TagId;
                                objLoc.SourceId = 2;
                                objLoc.ReferanceID = house.ReferanceId;
                                objLoc.datetime = DateTime.Now;
                                objLoc.lat = Lat;
                                objLoc.@long = Long;
                            }

                            db.GarbageCollectionDetails.Add(model);
                            db.Locations.Add(objLoc);
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                        }
                        else
                        {
                            result.status = "success";
                            result.message = "Uploaded successfully";
                        }
                    }
                    else
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                    }
                }
                catch (Exception ex)
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    return result;
                }
                return result;
            }

        }

        #endregion
    }
}