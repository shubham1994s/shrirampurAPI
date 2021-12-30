using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class CMSBAreaVM
    {
        public int id { get; set; }
        public string areaMar { get; set; }
        public string area { get; set; }

        public Nullable<int> wardId { get; set; }

        public string Wardno { get; set; }
    }
}
