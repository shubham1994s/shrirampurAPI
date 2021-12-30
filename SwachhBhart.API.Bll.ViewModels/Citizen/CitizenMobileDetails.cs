using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels.Citizen
{
    public class CitizenMobileDetails
    {
        public string Status { get; set; }
        public string ReferenceId { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string MessageMar { get; set; }
    }

    public class CitizenMobileOTP
    {
        public string Status { get; set; }
        public string ReferenceId { get; set; }
        public string OTP { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string MessageMar { get; set; }
    }

    public class CitizenCTPTAddress
    {
        public string SauchalayID { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Name { get; set; }
    }

    //public class CitizenQuestionMaster
    //{
    //    public int QuestionId { get; set; }
    //    public string Question { get; set; }
    //}

    //public class CitizenAnswerDetails
    //{
    //    public int QuestionId { get; set; }
    //    public string Answer { get; set; }
    //    public int userId { get; set; }
    //    public string JSon { get; set; }
    //    public DateTime AnsDate { get; set; }
    //}

}
