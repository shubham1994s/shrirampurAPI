using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class SBAdmin
    {
        public string adminId { get; set; }

        public string adminLoginId { get; set; }
        public string adminPassword { get; set; }
        //public string name { get; set; }
        //public string mobileNumber { get; set; }
        //public string address { get; set; }
        //public string profileImage { get; set; }
        //public string imiNo { get; set; }
        // public string type { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string messageMar { get; set; }
        public bool? gtFeatures { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppName_mar { get; set; }
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string databaseName { get; set; }
    }
}
