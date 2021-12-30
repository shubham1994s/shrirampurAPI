using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Repository.Repository
{
    public interface IGameRepository
    {
        List<SBGamesVM> GetMenu(int LanguageID);
        List<SBGamesQuestionVM> GetGameQuestions(int GameMasterID , int LanguageID);
        Result SavePlayerData(SBGamePlayerVM playerRaw );
        List<SBGamePlayerDetailsVM> GamePlayerDetails(string DeviceId, int AppId, int LanguageID ,string Mobile);
        SBGamePlayerDetailsVM GamePlayerDetailsSingle(string DeviceId, int AppId, string Mobile);
        List<SBGameAnswerTypeVM> AnswerType(int GameMasterID);
        Result SaveRegistrationData(SBGamePlayerVM playerRaw , int _AppId);
        GameResult GetGameOTP(string Mobile);
        GameResult GetChangeMobileOTP(string Mobile, string NewMobile, string DeviceID, int AppId);
        Result SaveChangeMobile(SBGamePlayerVM playerRaw);
    }
}