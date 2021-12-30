using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
  public  class Result
    {
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }

        public string emptype { get; set; }
    }

    public class Result1
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string MessageMar { get; set; }
        //public bool isAttendenceOff { get; set; }
    }

    public class Result2
    {
        public int ID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
    }

    public class GameResult
    {
        public string OTP { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool isAttendenceOff { get; set; }
    }
}
