using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels.Games
{
    public class SBGamesQuestionVM
    {
        public int GameDetailsID { get; set; }
        //public int GameMasterID { get; set; }
        public string GameName { get; set; }
        public string GameNameMar { get; set; }
        public string Slogan { get; set; }
        public string Image { get; set; }
        public string RightAnswer { get; set; }
        public int AnswerTypeId { get; set; }
        public string Point { get; set; }

    }
}
