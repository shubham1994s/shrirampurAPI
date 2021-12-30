using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
   public class CMSBWardVM
    {

        public int Id { get; set; }
        public string WardNo { get; set; }
        public Nullable <int> zoneId { get; set; }
        public string zonename { get; set; }
    }
}
