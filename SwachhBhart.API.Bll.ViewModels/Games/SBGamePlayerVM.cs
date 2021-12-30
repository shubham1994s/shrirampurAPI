using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels.Games
{
    public class SBGamePlayerVM
    {
        public int ID { get; set; }
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string NewMobile { get; set; }
        public int Score { get; set; }
        public string  DeviceId { get; set; }
        public int GameId { get; set; }
        public int AppId { get; set; }

    }
}
