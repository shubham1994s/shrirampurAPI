using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachhBharat.API.Bll.Services.AllServices
{
    public interface IGameService
    {
        List<SBGamesVM> GetMenu( int LanguageID);
        List<SBGamesQuestionVM> GetGameQuestions(int GameMasterID, int LanguageID); //int appId, int GameMasterID);
        Result SavePlayerData(SBGamePlayerVM data);  //, int _AppId);
        List<SBGamePlayerDetailsVM> GamePlayerDetails(string DeviceId, int AppId, int LanguageID, string Mobile);
        SBGamePlayerDetailsVM GamePlayerDetailsSingle(string DeviceId, int AppId, string Mobile);
        List<SBGameAnswerTypeVM> AnswerType(int GameMasterID);
        Result SaveRegistrationData(SBGamePlayerVM data, int _AppId);  //, int _AppId);
        GameResult GetGameOTP(string Mobile);
        GameResult GetChangeMobileOTP(string Mobile, string NewMobile, string DeviceID,int AppId);
        Result SaveChangeMobile(SBGamePlayerVM data); 
        
    }
}