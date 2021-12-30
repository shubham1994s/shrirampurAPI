using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels.Games
{
    public class SBGamePlayerDetailsVM
    {
        public int ID { get; set; }
        public string PlayedId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Score { get; set; }
        public string DeviceId { get; set; }
        public string GameId { get; set; }
        public string GameName { get; set; }
    }
}
