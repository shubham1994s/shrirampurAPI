using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.API.Bll.Services
{
    public interface IMainService
    {

        #region State
        Result SaveStateDetail(CMSBStatesVM data);
        #endregion

        Result SaveZone(CMSBZoneVM data ,int AppID);

        Result SaveWardDetail(CMSBWardVM Ward, int AppId);

        #region Area
        Result SaveAreaDetail(CMSBAreaVM Area, int AppId);

        #endregion

        Result SaveVehicleTypeDetail(SBVehicleType VehicleType, int AppId);

    }
}
