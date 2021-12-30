using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.WasteManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Services.AllServices
{
    public interface IWMScreenService
    {
        List<Result2> GarbageDetails(int appId, List<GarbageDetailsVM> obj);
        List<Result2> GarbageSales(int appId, List<GarbageSalesVM> obj);
    }
}
