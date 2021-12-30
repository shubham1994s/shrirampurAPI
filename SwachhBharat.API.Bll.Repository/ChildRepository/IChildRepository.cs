using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Repository.ChildRepository
{
    public interface IChildRepository
    {
        CMSBDashBoardVM GetDashBoardDetails();
        List<CMSBDashBoardVM> getEmployeeTargetData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId);
    }
}
