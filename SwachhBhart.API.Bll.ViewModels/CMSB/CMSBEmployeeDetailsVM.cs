﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class CMSBEmployeeDetailsVM
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userLoginId { get; set; }
        public string userPassword { get; set; }
        public string userMobileNumber { get; set; }
        public string userAddress { get; set; }
        public string userProfileImage { get; set; }
        public string Type { get; set; }
        public string userNameMar { get; set; }
        public string userEmployeeNo { get; set; }
        public string userDesignation { get; set; }
        public string isActive { get; set; }
        public string bloodGroup { get; set; }
        public string gcTarget { get; set; }
        public string imoNo { get; set; }
        //public Nullable<bool> isActive { get; set; }
    }
}
