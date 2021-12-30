using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class SBDashboardVM
    {
        public Nullable<int> Attendance { get; set; }
        public Nullable<int> HouseCollection { get; set; }
        public Nullable<int> PointCollection { get; set; }
        public Nullable<int> DumpingYardCollection { get; set; }
        //public Nullable<int> TotalComplaint { get; set; }
    }
}
