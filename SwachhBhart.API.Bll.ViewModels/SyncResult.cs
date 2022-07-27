using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class SyncResult
    {
        public int OfflineId { get; set; }
        public int  ID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }
    }

    public class SyncResult1
    {
        public int ID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }
        public bool IsInSync { get; set; }
        public bool IsOutSync { get; set; }

        public string EmpType { get; set; }
    }


    public class SyncResult2
    {
        public int UserId { get; set; }   
        public bool IsInSync { get; set; }     
        public int batterystatus { get; set; }
        public string imei { get; set; }
      

    }

}
