using SwachhBharat.API.Bll.Services.AllServices;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.WasteManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Repository.Repository
{
    public class WMRepository : IWMRepository
    {
        IWMScreenService screenService;
        public List<GarbageCategoryVM> GetGarbageCategory(int appId)
        {

            List<GarbageCategoryVM> obj = new List<GarbageCategoryVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.WM_GarbageCategory.ToList();

                foreach (var x in data)
                {
                    obj.Add(new GarbageCategoryVM()
                    {
                        CategoryID = x.CategoryID,
                        GarbageCategory = x.Category,
                    });
                }

            }
            return obj;

        }

        public List<GarbageSubCategoryVM> GetGarbageSubCategory(int appId, int _CategoryID)
        {

            List<GarbageSubCategoryVM> obj = new List<GarbageSubCategoryVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                if (_CategoryID >0)
                {
                    var data = db.WM_GarbageSubCategory.Where(c => c.CategoryID == _CategoryID).ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new GarbageSubCategoryVM()
                        {
                            SubCategoryID = x.SubCategoryID,
                            GarbageSubCategory = x.SubCategory,
                            CategoryID = x.CategoryID
                        });
                    }
                }
                else
                {
                    var data = db.WM_GarbageSubCategory.ToList();
                    foreach (var x in data)
                    {
                        obj.Add(new GarbageSubCategoryVM()
                        {
                            SubCategoryID = x.SubCategoryID,
                            GarbageSubCategory = x.SubCategory,
                            CategoryID = x.CategoryID
                        });
                    }
                }
            }
            return obj;

        }

        public List<Result2> GarbageDetails(int appId, List<GarbageDetailsVM> obj)
        {
            screenService = new WMScreenService(appId);
            List<Result2> result = screenService.GarbageDetails(appId, obj);
            return result;
        }
        public List<Result2> GarbageSales(int appId, List<GarbageSalesVM> obj)
        {
            screenService = new WMScreenService(appId);
            List<Result2> result = screenService.GarbageSales(appId, obj);
            return result;
        }

        public List<GarbageHistoryVM> GetGarbageHistory(int userId, int year, int month, int appId)
        {
            List<GarbageHistoryVM> obj = new List<GarbageHistoryVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetGarbageCountDetailsTotal(userId, year, month).ToList();

                foreach (var x in data)
                {
                    obj.Add(new GarbageHistoryVM()
                    {
                        UserId = x.UserId,
                        Date = Convert.ToDateTime(x.Date).ToString("MM-dd-yyy"),
                        GarbageCount = x.GarbageCount,
                        //SalesCount = x.SalesCount,
                    });
                }

            }
            return obj;
        }

        public List<GarbageHistoryDetailsVM> GetUserGarbageDetails(DateTime date, int appId, int userId)
        {
            int _year = date.Year;
            int _month = date.Month;
            int _day = date.Day;

            List<GarbageHistoryDetailsVM> obj = new List<GarbageHistoryDetailsVM>();
            using (DevSwachhBharatNagpurEntities db = new DevSwachhBharatNagpurEntities(appId))
            {
                var data = db.GetGarbageCountDetailsHistory(userId, _year, _month, _day).ToList();

                foreach (var x in data)
                {
                    obj.Add(new GarbageHistoryDetailsVM()
                    {
                        Time = Convert.ToDateTime(x.CreatedDate).ToString("hh:mm tt"),
                        SubCategory = x.GarbageSubCategory,
                        Category = x.Category,
                        Weight = Convert.ToDecimal(x.Weight),
                        //PartyName = "",
                        //Amount = 0,
                    });
                }

            }
            return obj.ToList();
        }
        
    }
}
