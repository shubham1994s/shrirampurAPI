using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
   public class CollectionResult
    {
        public string name { get; set; }
        public string nameMar { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }
    }

    public class CollectionSyncResult
    {
        public int ID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }
    }

    public class CollectionQRStatusResult
    {
        public  string ReferanceId { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
    }

}
