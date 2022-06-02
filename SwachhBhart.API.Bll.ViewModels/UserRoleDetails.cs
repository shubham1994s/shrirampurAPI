using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels
{
    public class UserRoleDetails
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpNameMar { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string EmpMobileNumber { get; set; }
        public string EmpAddress { get; set; }
        public string type { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string isActiveULB { get; set; }

       // public Nullable<System.DateTime> lastModifyDateEntry { get; set; }

        public string LastModifyDateEntry { get; set; }

    }
}
