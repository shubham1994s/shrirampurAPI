using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
   public class CMSBTalukaVM
    {

        public int id { get; set; }
        public string name { get; set; }
        public string name_mar { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Nullable<int> stateid { get; set; }
        public Nullable<int> districtid { get; set; }
        public string statename { get; set; }
        public string districtname { get; set; }
    }
}
