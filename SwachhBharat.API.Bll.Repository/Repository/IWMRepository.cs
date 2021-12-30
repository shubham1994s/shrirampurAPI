using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.WasteManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Repository.Repository
{
    public interface IWMRepository
    {
        List<GarbageCategoryVM> GetGarbageCategory(int appId);
        List<GarbageSubCategoryVM> GetGarbageSubCategory(int appId, int _CategoryID);
        List<Result2> GarbageDetails(int appId, List<GarbageDetailsVM> obj);
        List<Result2> GarbageSales(int appId, List<GarbageSalesVM> obj);
        List<GarbageHistoryVM> GetGarbageHistory(int userid, int year, int month, int appId);
        List<GarbageHistoryDetailsVM> GetUserGarbageDetails(DateTime date, int appId, int userId);
        
    }
}
