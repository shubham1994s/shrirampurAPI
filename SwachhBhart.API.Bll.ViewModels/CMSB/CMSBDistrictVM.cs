using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
   public  class CMSBDistrictVM
    {
        public int id { get; set; }
        public int state_id { get; set; }
        public string district_name { get; set; }
        public string district_name_mar { get; set; }
        public string statename { get; set; }

    }
}
