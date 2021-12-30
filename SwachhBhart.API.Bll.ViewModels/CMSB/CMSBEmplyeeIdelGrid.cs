using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class CMSBEmplyeeIdelGrid
    {
        public string UserName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IdelTime { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public Nullable<int> userId { get; set; }
        public string startlat { get; set; }
        public string startlong { get; set; }
        public string endlat { get; set; }
        public string endlong { get; set; }

    }
}
