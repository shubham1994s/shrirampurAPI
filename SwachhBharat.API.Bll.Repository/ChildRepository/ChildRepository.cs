using SwachBharat.API.Bll.Services;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Repository.ChildRepository
{
    public class ChildRepository : IChildRepository
    {
        private int AppID;
        IScreenService screenService;

        public ChildRepository(int AppId)
        {
            screenService = new ScreenService(AppId);

            AppID = AppId;
        }

        public CMSBDashBoardVM GetDashBoardDetails()
        {
            return screenService.GetDashBoardDetailsData();
        }

        //Added By Nishikant(04 June 2019)
        public List<CMSBDashBoardVM> getEmployeeTargetData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<CMSBDashBoardVM> obj = new List<CMSBDashBoardVM>();
            using (var db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_EmployeeTarget(fdate, tdate, userId).ToList();

                foreach (var x in data)
                {
                    obj.Add(new CMSBDashBoardVM()
                    {
                        UserName = x.userName,
                        Target = (x.gcTarget==null ? "0" : x.gcTarget),
                        FromDate = Convert.ToDateTime(x.fromDate).ToString("dd/MM/yyyy"),
                        ToDate = Convert.ToDateTime(x.ToDate).ToString("dd/MM/yyyy"),
                        _Count = Convert.ToInt32(x.Count),
                    });
                }
                return obj;
            }
        }

    }
}
