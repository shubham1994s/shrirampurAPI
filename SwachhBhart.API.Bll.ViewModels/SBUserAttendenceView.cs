using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
   public class SBUserAttendenceView
    {
        public int daID { get; set; }
        public int userId { get; set; }                
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string daDate { get; set; }
        public string vtId { get; set; }        
    }
}
