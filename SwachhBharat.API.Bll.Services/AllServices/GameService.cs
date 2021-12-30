using SwachBharat.API.Bll.Services;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Services.AllServices
{
    public class GameService : AppService, IGameService
    {
        private int AppID;
        public GameService(int AppId) : base(AppId)
        {
            AppID = AppId;
        }


        public GameService()
        {
           
        }

        public List<SBGamesVM> GetMenu(int LanguageID)
        {
            List<SBGamesVM> gameData = new List<SBGamesVM>();
            DateTime newdate = DateTime.Now.Date;
            var datt = newdate;
            var data = dbMain.GameMasters.ToList();
          
            foreach (var x in data)
            {
                string gamename = string.Empty;

                if (LanguageID == 1)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameName;
                }
                else if (LanguageID == 2)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameMar;
                }
                else if (LanguageID == 3)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameHindi;
                }
                gameData.Add(new SBGamesVM()
                {
                    GameMasterID = x.GameId,
                    GameName = gamename, //x.GameName,
                    //GameNameMar=x.GameNameMar,
                    //GameNameHindi = x.GameNameHindi,
                });
            }

            return gameData;
        }

        public List<SBGamesQuestionVM> GetGameQuestions(int GameMasterID , int LanguageID) //int appId, int GameMasterID)
        {
            List<SBGamesQuestionVM> gameData = new List<SBGamesQuestionVM>();
            
            var data = dbMain.GameDetails.Where(x => x.GameMasterID == GameMasterID).ToList();
            
           
            foreach (var x in data)
            {
                string gamename = string.Empty;
                string slogan = string.Empty;
                if (LanguageID == 1)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameMasterID).FirstOrDefault().GameName;
                    slogan = dbMain.Game_Slogan.Where(c => c.ID == x.SloganID).FirstOrDefault().Slogan;
                }
                else if (LanguageID == 2)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameMasterID).FirstOrDefault().GameNameMar;
                    slogan = dbMain.Game_Slogan.Where(c => c.ID == x.SloganID).FirstOrDefault().SloganMar;
                }
                else if (LanguageID == 3)
                {
                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameMasterID).FirstOrDefault().GameNameHindi;
                    slogan = dbMain.Game_Slogan.Where(c => c.ID == x.SloganID).FirstOrDefault().SloganHindi;
                }

                gameData.Add(new SBGamesQuestionVM()
                {
                    GameDetailsID = x.GameDetailsID,
                    GameName = gamename,
                    Slogan = slogan, 
                    Image = x.ImageUrl,
                    RightAnswer= dbMain.Game_AnswerType.Where(c => c.AnswerTypeId == x.RightAnswerID).FirstOrDefault().AnswerTypeMar,
                    AnswerTypeId = Convert.ToInt32(x.RightAnswerID),
                    Point = Convert.ToString(x.Point),
                });
                
            }

            var random = new Random();
            //random.Next(gameData.ToList());
            return gameData.OrderBy(x => random.Next()).Take(10).ToList();
        }

        public Result SavePlayerData(SBGamePlayerVM data) //, int _AppId)
        {
            Result result = new Result();
            if (data.AppId > 0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(data.AppId))
                {
                    try
                    {
                        //var type = FillPlayerDataModel(data);
                        //db.GamePlayerDetails.Add(type);
                        var PlayerDetails = db.GamePlayerDetails.Where(c => c.DeviceId == data.DeviceId & c.PlayerId == data.PlayerId && c.GameId == data.GameId).FirstOrDefault();

                        var houseDetails = db.HouseMasters.Where(x => x.ReferanceId == data.PlayerId).FirstOrDefault();

                        if (PlayerDetails != null)
                        {
                            PlayerDetails.PlayerId = data.PlayerId;
                            PlayerDetails.GameId = data.GameId;
                            PlayerDetails.Name =  (houseDetails!= null ? houseDetails.houseOwner :  data.Name) ;
                            PlayerDetails.Mobile = (houseDetails != null ? houseDetails.houseOwnerMobile : data.Mobile);
                            PlayerDetails.Score = PlayerDetails.Score + data.Score;
                            PlayerDetails.DeviceId = data.DeviceId;
                            PlayerDetails.Created = DateTime.Now;
                            db.SaveChanges();

                            result.status = "success";
                            result.message = "Saved successfully";
                            result.messageMar = "सबमिट यशस्वी";
                        }
                        

                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        return result;
                    }
                }

            }
            else
            {
                try
                {
                    //var type = FillPlayerDataModel(data);
                    //dbMain.GamePlayerDetails.Add(type);

                    //var PlayerDetails = dbMain.GamePlayerDetails.Where(c => c.DeviceId == data.DeviceId & c.PlayerId == data.PlayerId && c.GameId == data.GameId).FirstOrDefault();

                    var PlayerDetails = dbMain.GamePlayerDetails.Where(c => c.Mobile == data.Mobile & c.PlayerId == data.PlayerId && c.GameId == data.GameId).FirstOrDefault();

                    if (PlayerDetails != null)
                    {
                        PlayerDetails.PlayerId = data.PlayerId;
                        PlayerDetails.GameId = data.GameId;
                        PlayerDetails.Name = data.Name;
                        PlayerDetails.Mobile = data.Mobile;
                        PlayerDetails.Score = PlayerDetails.Score + data.Score;
                        PlayerDetails.DeviceId = data.DeviceId;
                        PlayerDetails.Created = DateTime.Now;
                        dbMain.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
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

        public List<SBGamePlayerDetailsVM> GamePlayerDetails(string DeviceId ,int AppId ,int LanguageID, string Mobile)  //, string DeviceId)
        {
            List<SBGamePlayerDetailsVM> gameData = new List<SBGamePlayerDetailsVM>();

            if (AppId > 0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    var data = db.GamePlayerDetails.Where(x => x.DeviceId == DeviceId && x.Mobile == Mobile).ToList();
                    if (data != null)
                    {
                        foreach (var x in data)
                        {
                            string gamename = string.Empty;
                            switch (LanguageID)
                            {
                                case 1:
                                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameName;
                                    break;
                                case 2:
                                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameMar;
                                    break;
                                case 3:
                                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameHindi;
                                    break;
                                default:
                                    gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameName;
                                    break;
                            }
                            gameData.Add(new SBGamePlayerDetailsVM()
                            {
                                GameId = Convert.ToString(x.GameId),
                                GameName = gamename,
                                Score = Convert.ToInt32(x.Score),
                            });
                        }
                       
                    }
                }
               
            }
            else
            {
                //var data = dbMain.GamePlayerDetails.Where(x => x.DeviceId == DeviceId && x.Mobile == Mobile).ToList();
                var data = dbMain.GamePlayerDetails.Where(x => x.Mobile == Mobile).ToList();
                if (data != null)
                {
                    foreach (var x in data)
                    {
                        string gamename = string.Empty;
                        
                        switch (LanguageID)
                        {
                            case 1:
                                gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameName;
                                break;
                            case 2:
                                gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameMar;
                                break;
                            case 3:
                                gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameNameHindi;
                                break;
                            default:
                                gamename = dbMain.GameMasters.Where(c => c.GameId == x.GameId).FirstOrDefault().GameName;
                                break;
                        }

                        gameData.Add(new SBGamePlayerDetailsVM()
                        {
                            GameId = Convert.ToString(x.GameId),
                            GameName = gamename,
                            Score = Convert.ToInt32(x.Score),
                        });
                    }
                }
            }
            
            return gameData;
        }

        public SBGamePlayerDetailsVM GamePlayerDetailsSingle(string DeviceId, int AppId, string Mobile) 
        {
            SBGamePlayerDetailsVM gameData = new SBGamePlayerDetailsVM();

            if (AppId > 0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    var data = db.GamePlayerDetails.Where(x => x.DeviceId == DeviceId && x.Mobile == Mobile).FirstOrDefault();
                    if (data != null)
                    {
                        gameData.ID = data.ID;
                        gameData.PlayedId = Convert.ToString(data.PlayerId);
                        //gameData.GameId = Convert.ToString(data.GameId);
                        gameData.Name = data.Name;
                        gameData.Mobile = data.Mobile;
                        //gameData.Score = Convert.ToInt32(data.Score);
                        // gameData.DeviceId = Convert.ToString(data.DeviceId);
                    }
                }

            }
            else
            {
                //var data = dbMain.GamePlayerDetails.Where(x => x.DeviceId == DeviceId).FirstOrDefault();
                var data = dbMain.GamePlayerDetails.Where(x => x.Mobile == Mobile).FirstOrDefault();
                if (data != null)
                {
                    gameData.ID = data.ID;
                    gameData.PlayedId = Convert.ToString(data.PlayerId);
                    //gameData.GameId = Convert.ToString(data.GameId);
                    gameData.Name = data.Name;
                    gameData.Mobile = data.Mobile;
                    //gameData.Score = Convert.ToInt32(data.Score);
                    //gameData.DeviceId = Convert.ToString(data.DeviceId);
                }
            }

            return gameData;
        }

        public List<SBGameAnswerTypeVM> AnswerType(int GameMasterID)
        {
            List<SBGameAnswerTypeVM> gameData = new List<SBGameAnswerTypeVM>();

            var data = dbMain.Game_AnswerType.Where(c =>c.GameMasterID== GameMasterID).ToList();

            foreach (var x in data)
            {
                gameData.Add(new SBGameAnswerTypeVM()
                {
                    AnswerTypeId = x.AnswerTypeId,
                    AnswerType = Convert.ToString(x.AnswerType),
                    AnswerTypeMar = Convert.ToString(x.AnswerTypeMar),
                    AnswerTypeHindi = Convert.ToString(x.AnswerTypeHindi),
                });
            }

            return gameData;
        }

        public Result SaveRegistrationData(SBGamePlayerVM data, int _AppId)
        {
            Result result = new Result();

            if (_AppId > 0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(_AppId))
                {
                    try
                    {
                        var GameMaster = dbMain.GameMasters.ToList();
                        if (GameMaster != null)
                        {
                            foreach(var item in GameMaster)
                            {
                                //var IsExist = db.GamePlayerDetails.Where(x => x.DeviceId == data.DeviceId && x.GameId == item.GameId && x.PlayerId == data.PlayerId && x.Mobile == data.Mobile).FirstOrDefault();

                                var IsExist = db.GamePlayerDetails.Where(x => x.GameId == item.GameId &&  x.Mobile == data.Mobile).FirstOrDefault();


                                if (IsExist != null)
                                {
                                    data.GameId = item.GameId;
                                    IsExist.GameId = item.GameId;
                                    var houseDetails = db.HouseMasters.Where(x => x.ReferanceId == data.PlayerId).FirstOrDefault();
                                    if (houseDetails != null)
                                    {
                                        IsExist.PlayerId = data.PlayerId;
                                        IsExist.Name = houseDetails.houseOwner;
                                        IsExist.Mobile = houseDetails.houseOwnerMobile;
                                        IsExist.DeviceId = data.DeviceId; 
                                    }
                                    else
                                    {
                                        IsExist.PlayerId = data.PlayerId;
                                        IsExist.Name = data.Name;
                                        IsExist.Mobile = data.Mobile;
                                        IsExist.DeviceId = data.DeviceId;
                                    }

                                }
                                else
                                {
                                    data.GameId = item.GameId;
                                    var type = FillPlayerDataModel(data);
                                    db.GamePlayerDetails.Add(type);
                                }
                                db.SaveChanges();
                            }
                        }

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";

                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        return result;
                    }
                }

            }
            else
            {
                try
                {
                    var GameMaster = dbMain.GameMasters.ToList();
                    if (GameMaster != null)
                    {
                        foreach (var item in GameMaster)
                        {
                            var IsExist = dbMain.GamePlayerDetails.Where(x => x.Mobile == data.Mobile && x.GameId == item.GameId).FirstOrDefault();

                            //var IsExist = dbMain.GamePlayerDetails.Where(x => x.DeviceId == data.DeviceId && x.GameId == item.GameId).FirstOrDefault();

                            if (IsExist != null)
                            {
                                IsExist.PlayerId = data.PlayerId;
                                IsExist.GameId = item.GameId;
                                IsExist.Name = data.Name;
                                IsExist.Mobile = data.Mobile;
                                IsExist.DeviceId = data.DeviceId;
                                //IsExist.Score = data.Score;
                                IsExist.Created = DateTime.Now ;
                            }
                            else
                            {
                                data.GameId = item.GameId;
                                var type = FillPlayerDataModel(data);
                                dbMain.GamePlayerDetails.Add(type);
                            }
                            
                            dbMain.SaveChanges();
                        }
                    }

                    result.status = "success";
                    result.message = "Saved successfully";
                    result.messageMar = "सबमिट यशस्वी";

                    return result;
                }
                catch (Exception ex)
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    return result;
                }
            }

            

        }

        public GameResult GetGameOTP(string Mobile)
        {
            GameResult result = new GameResult();
            string OTP = GetOTPString();
            string msg = "Your OTP is " + OTP + ". Do not share it with anyone by any means. This is confidential and to be used by you only.";
            sendSMS(msg, Mobile);
            result.OTP = OTP;
            result.status = "success";
            return result;
        }

        public GameResult GetChangeMobileOTP(string Mobile, string NewMobile, string DeviceID , int AppId)
        {
            GameResult result = new GameResult();
            bool IsExist = false;
            if (AppId>0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    IsExist = (from p in db.GamePlayerDetails where p.Mobile == Mobile && p.DeviceId == DeviceID select p).Count() > 0;
                }
            }
            else
            {
                IsExist = (from p in dbMain.GamePlayerDetails where p.Mobile == Mobile && p.DeviceId == DeviceID select p).Count() > 0;
            }

            if (IsExist == true)
            {
                string OTP = GetOTPString();
                string msg = "Your OTP is " + OTP + ". Do not share it with anyone by any means. This is confidential and to be used by you only.";
                sendSMS(msg, NewMobile);
                result.OTP = OTP;
                result.status = "success";
            }
            else
            {
                result.status = "error";
                result.message = "Record not exists.";
            }
            return result;
        }
        

        private void sendSMS(string sms, string MobilNumber)
        {
            try
            {
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&msgtype=UNI&message=" + sms + "&response=Y");

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://www.smsjust.com/sms/user/urlsms.php?username=ycagent&pass=yocc@5095&senderid=YOCCAG&dest_mobileno=" + MobilNumber + "&message=" + sms + "&response=Y");

                //Get response from Ozeki NG SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch { }

        }
        private string GetOTPString()
        {
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
            return otp;
        }

        public Result SaveChangeMobile(SBGamePlayerVM data)
        {
            Result result = new Result();

            if (data.AppId > 0)
            {
                using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(data.AppId))
                {
                    try
                    {
                        var GameMaster = dbMain.GameMasters.ToList();
                        if (GameMaster != null)
                        {
                            foreach (var item in GameMaster)
                            {

                                var NewMobileExist = db.GamePlayerDetails.Where(x => x.GameId == item.GameId && x.Mobile == data.NewMobile).FirstOrDefault();
                                if (NewMobileExist != null)
                                {
                                    NewMobileExist.DeviceId = data.DeviceId;
                                    dbMain.SaveChanges();
                                    result.status = "success";
                                    result.message = "Saved successfully";
                                    result.messageMar = "सबमिट यशस्वी";
                                }

                                var IsExist = db.GamePlayerDetails.Where(x => x.DeviceId == data.DeviceId && x.GameId == item.GameId && x.Mobile == data.Mobile).FirstOrDefault();

                                if (IsExist != null)
                                {
                                    IsExist.Mobile = data.NewMobile;
                                    db.SaveChanges();
                                    result.status = "success";
                                    result.message = "Saved successfully";
                                    result.messageMar = "सबमिट यशस्वी";

                                }
                            }
                        }
                       
                        return result;
                    }
                    catch (Exception ex)
                    {
                        result.status = "error";
                        result.message = "Something is wrong,Try Again.. ";
                        result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                        return result;
                    }
                }

            }
            else
            {
                try
                {
                    var GameMaster = dbMain.GameMasters.ToList();
                    if (GameMaster != null)
                    {
                        foreach (var item in GameMaster)
                        {
                            var NewMobileExist = dbMain.GamePlayerDetails.Where(x => x.GameId == item.GameId && x.Mobile == data.NewMobile).FirstOrDefault();
                            if (NewMobileExist != null)
                            {
                                NewMobileExist.DeviceId = data.DeviceId;
                                dbMain.SaveChanges();
                                result.status = "success";
                                result.message = "Saved successfully";
                                result.messageMar = "सबमिट यशस्वी";
                            }

                            var IsExist = dbMain.GamePlayerDetails.Where(x => x.DeviceId == data.DeviceId && x.GameId == item.GameId && x.Mobile == data.Mobile).FirstOrDefault();

                            if (IsExist != null)
                            {
                                IsExist.Mobile = data.NewMobile;
                                dbMain.SaveChanges();
                                result.status = "success";
                                result.message = "Saved successfully";
                                result.messageMar = "सबमिट यशस्वी";
                            }
                        }
                    }
                   

                    return result;
                }
                catch (Exception ex)
                {
                    result.status = "error";
                    result.message = "Something is wrong,Try Again.. ";
                    result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                    return result;
                }
            }

        }

    }
}
