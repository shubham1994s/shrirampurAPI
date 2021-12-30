using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
  public  class SBVehicleType
    {
        public int vtId { get; set; }
        public string description { get; set; }
        public string descriptionMar { get; set; }
        public bool ? isActive { get; set; }
    }
}
