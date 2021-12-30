using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
  public  class CMSBEmpolyeeSummaryGrid
    {
        public int daID { get; set; }
        public string UserName { get; set; }
        public string daDate { get; set; }
        public string StartTime { get; set; }
        public string DaEndDate { get; set; }
        public string EndTime { get; set; }
        public string IdelTime { get; set; }
        public string Totalhousecollection { get; set; }
        public string Totaldistance { get; set; }
        public string Totaldumpyard { get; set; }
        public int userId { get; set; }
        public string BatteryStatus { get; set; }
    }
}
