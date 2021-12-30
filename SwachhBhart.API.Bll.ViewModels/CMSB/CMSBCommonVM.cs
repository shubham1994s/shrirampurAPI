using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class CMSBCommonVM
    {
        #region Zone
        public Nullable<int> ZoneID { get; set; }
        public string Zone { get; set; }
        #endregion

        #region Ward
        public int WardID { get; set; }
        public string WardNo { get; set; }
        public string Zonename { get; set; }
        #endregion

    }
}
