using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.API.Bll.Services
{
    public interface IScreenService
    {
        CMSBHouseDetailsVM GetHouseDetails(int HouseID);
        Result SaveHouseDetails(CMSBHouseDetailsVM data, int _AppId, int _HouseId);
        Result SaveGarbagePointDetails(CMSBGarbagePointDetailsVM data, int _AppId, int gpId);
        Result SaveDumpYardtDetails(CMSBDumpYardDetailsVM data, int _AppId, int dyId);
        Result SaveEmployeeDetails(CMSBEmployeeDetailsVM data, int _AppId, int UserId);
        List<CMSBUserLocationMapVM> GetUserAttenRoute(int userId);
        CMSBDashBoardVM GetDashBoardDetailsData();
        Result1 SaveDeviceDetails(int appId, string ReferanceId, string FCMID, string DeviceID, string Mobile);
        Result1 SaveDeviceDetailsClear(int appId, string DeviceID, string ReferenceID);
        List<CMSBUserLocationMapVM> GetHouseAttenRoute(int daId);
    }
}
