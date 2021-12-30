using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class SBGameController : ApiController
    {
        IGameRepository objGameRep;

        [HttpGet]
        [Route("Get/GetMenu")]
        public List<SBGamesVM> GetMenu()
        {
            objGameRep = new GameRepository();

            //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            //var id = headerValue1.FirstOrDefault();
            //int AppId = int.Parse(id);

            IEnumerable<string> headerValue1 = Request.Headers.GetValues("LanguageID");
            var _LanguageID = headerValue1.FirstOrDefault();
            int LanguageID = int.Parse(_LanguageID);

            List<SBGamesVM> obj = new List<SBGamesVM>();
            obj = objGameRep.GetMenu(LanguageID);
            return obj;
        }

        [HttpGet]
        [Route("Get/GetGameQuestions")]
        public List<SBGamesQuestionVM> GetGameQuestions()
        {
            objGameRep = new GameRepository();
            
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("LanguageID");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("GameMasterID");

            //var id = headerValue1.FirstOrDefault();
            //int AppId = int.Parse(id);

            var gameMasterID = headerValue2.FirstOrDefault();
            int GameMasterID = int.Parse(gameMasterID);

            var _LanguageID = headerValue1.FirstOrDefault();
            int LanguageID = int.Parse(_LanguageID);

            List<SBGamesQuestionVM> obj = new List<SBGamesQuestionVM>();
            obj = objGameRep.GetGameQuestions(GameMasterID, LanguageID);
            return obj;
        }

        [HttpPost]
        [Route("Save/PlayerDetails")]
        public Result AddPlayerDetails(SBGamePlayerVM playerRaw)
        {
            objGameRep = new GameRepository();
            //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            //var appId = headerValue1.FirstOrDefault();
            //int _AppId = int.Parse(appId);

            Result playerDetails = objGameRep.SavePlayerData(playerRaw);
            return playerDetails;
        }

        [HttpGet]
        [Route("Get/GamePlayerDetails")]
        public JObject GamePlayerDetails()
        {
            objGameRep = new GameRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("DeviceId");
            IEnumerable<string> headerValue3 = Request.Headers.GetValues("LanguageID");
            IEnumerable<string> headerValue4 = Request.Headers.GetValues("Mobile");

            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            var _DeviceId = headerValue2.FirstOrDefault();
            string DeviceId = (_DeviceId);

            var _LanguageID = headerValue3.FirstOrDefault();
            int LanguageID = int.Parse(_LanguageID);

            var _Mobile = headerValue4.FirstOrDefault();
            string Mobile = _Mobile.ToString();

            List<SBGamePlayerDetailsVM> obj = new List<SBGamePlayerDetailsVM>();
            obj = objGameRep.GamePlayerDetails(DeviceId, AppId, LanguageID, Mobile);

            SBGamePlayerDetailsVM objSingle = new SBGamePlayerDetailsVM();
            objSingle = objGameRep.GamePlayerDetailsSingle(DeviceId, AppId, Mobile);

            return JSonGame(obj , objSingle); 
        }

        private JObject JSonGame(List<SBGamePlayerDetailsVM> obj , SBGamePlayerDetailsVM objSingle)
        {
            string jsonString = string.Empty;

            jsonString += "{\n " + "PlayerDetails" + ": " + JsonConvert.SerializeObject(objSingle, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + ",";

            jsonString += "\n " + "Score" + ": " + JsonConvert.SerializeObject(obj, Formatting.Indented , new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }) + "}";

            JObject json = JObject.Parse(jsonString);
            return json;
        }

        [HttpGet]
        [Route("Get/AnswerType")]
        public List<SBGameAnswerTypeVM> AnswerType()
        {
            objGameRep = new GameRepository();
            //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headerValue2 = Request.Headers.GetValues("GameMasterID");

            //var id = headerValue1.FirstOrDefault();
            //int AppId = int.Parse(id);

            var _GameMasterID = headerValue2.FirstOrDefault();
            int GameMasterID = int.Parse(_GameMasterID);

            List<SBGameAnswerTypeVM> obj = new List<SBGameAnswerTypeVM>();
            obj = objGameRep.AnswerType(GameMasterID);
            return obj;
        }

        [HttpPost]
        [Route("Save/RegistrationDetails")]
        public Result AddRegistrationDetails(SBGamePlayerVM playerRaw)
        {
            objGameRep = new GameRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("AppId");
            var appId = headerValue1.FirstOrDefault();
            int _AppId = int.Parse(appId);

            Result playerDetails = objGameRep.SaveRegistrationData(playerRaw, _AppId);
            return playerDetails;
        }

        [HttpGet]
        [Route("Get/GetGameOTP")]
        public GameResult GameOTP()
        {
            objGameRep = new GameRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("Mobile");
            var Mobile = headerValue1.FirstOrDefault();
            string _Mobile = Mobile;
            GameResult obj = objGameRep.GetGameOTP(_Mobile);
            return obj;
        }

        [HttpGet]
        [Route("Get/GetChangeMobileOTP")]
        public GameResult ChangeMobileOTP()
        {
            objGameRep = new GameRepository();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("OldMobile");
            var Mobile = headerValue1.FirstOrDefault();
            string _Mobile = Mobile;

            IEnumerable<string> headerValue2 = Request.Headers.GetValues("NewMobile");
            var NewMobile = headerValue2.FirstOrDefault();
            string _NewMobile = NewMobile;

            IEnumerable<string> headerValue3 = Request.Headers.GetValues("DeviceID");
            var DeviceID = headerValue3.FirstOrDefault();
            string _DeviceID = DeviceID;

            IEnumerable<string> headerValue4 = Request.Headers.GetValues("AppId");
            var AppId = headerValue4.FirstOrDefault();
            int _AppId = int.Parse(AppId);

            GameResult obj = objGameRep.GetChangeMobileOTP(_Mobile, _NewMobile, _DeviceID, _AppId);
            return obj;
        }

        [HttpPost]
        [Route("Save/ChangeMobile")]
        public Result AddChangeMobile(SBGamePlayerVM playerRaw)
        {
            objGameRep = new GameRepository();
            Result playerDetails = objGameRep.SaveChangeMobile(playerRaw);
            return playerDetails;
        }
    }
}
