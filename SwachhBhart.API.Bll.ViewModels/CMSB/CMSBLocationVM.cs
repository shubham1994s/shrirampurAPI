using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class CMSBLocationVM
    {
        public int locId { get; set; }
        public string userName { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string lat { get; set; }
        public string longe { get; set; }

        public string  Address { get; set; }
        public int userId { get; set; }
        public string vehicleNumber { get; set; }
        public string userMobile { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }
    }
}
