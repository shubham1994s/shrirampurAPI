using SwachBharat.API.Bll.Services;
using SwachhBharat.API.Bll.Services.AllServices;
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
    public class GameRepository : IGameRepository
    {
        IGameService gameService;
        public DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        public List<SBGamesVM> GetMenu(int LanguageID)
        {
            //gameService = new GameService(_AppId);
            gameService = new GameService();
            return gameService.GetMenu(LanguageID);
        }

        public List<SBGamesQuestionVM> GetGameQuestions(int GameMasterID, int LanguageID)
        {
            //gameService = new GameService(_AppId);
            gameService = new GameService();
            return gameService.GetGameQuestions(GameMasterID , LanguageID);
        }

        public Result SavePlayerData(SBGamePlayerVM playerRaw)
        {
            //gameService = new GameService(_AppId);
            gameService = new GameService();
            Result dd = gameService.SavePlayerData(playerRaw);
            return dd;
        }

        public List<SBGamePlayerDetailsVM> GamePlayerDetails(string DeviceId, int AppId,int LanguageID, string Mobile)
        {
            //gameService = new GameService(_AppId);
            gameService = new GameService();
            return gameService.GamePlayerDetails(DeviceId, AppId, LanguageID, Mobile);
        }

        public SBGamePlayerDetailsVM GamePlayerDetailsSingle(string DeviceId, int AppId, string Mobile)
        {
            gameService = new GameService();
            return gameService.GamePlayerDetailsSingle(DeviceId, AppId, Mobile);
        }

        

        public List<SBGameAnswerTypeVM> AnswerType(int GameMasterID)
        {
            //gameService = new GameService(_AppId);
            gameService = new GameService();
            return gameService.AnswerType(GameMasterID);
        }
        public Result SaveRegistrationData(SBGamePlayerVM playerRaw , int _AppId)
        {
            gameService = new GameService();
            Result dd = gameService.SaveRegistrationData(playerRaw, _AppId);
            return dd;
        }

        public GameResult GetGameOTP(string Mobile)
        {
            gameService = new GameService();
            GameResult dd = gameService.GetGameOTP(Mobile);
            return dd;
        }
        public GameResult GetChangeMobileOTP(string Mobile, string NewMobile, string DeviceID, int AppId)
        {
            gameService = new GameService();
            GameResult dd = gameService.GetChangeMobileOTP(Mobile, NewMobile, DeviceID, AppId);
            return dd;
        }

        public Result SaveChangeMobile(SBGamePlayerVM playerRaw)
        {
            gameService = new GameService();
            Result dd = gameService.SaveChangeMobile(playerRaw);
            return dd;
        }


    }
}
