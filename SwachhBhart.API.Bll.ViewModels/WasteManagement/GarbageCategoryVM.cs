using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBhart.API.Bll.ViewModels.WasteManagement
{
    public class GarbageCategoryVM
    {
        public int CategoryID { get; set; }
        public string GarbageCategory { get; set; }
    }

    public class GarbageSubCategoryVM
    {
        public int SubCategoryID { get; set; }
        public string GarbageSubCategory { get; set; }
        public Nullable<int> CategoryID { get; set; }

    }

    public class GarbageDetailsVM
    {
        public int ID { get; set; }
        public int GarbageDetailsID { get; set; }
        public int SubCategoryID { get; set; }
        public decimal Weight { get; set; }
        public int UserID { get; set; }
        public int UnitID { get; set; }
        public int Source { get; set; }
        public DateTime gdDate { get; set; }
    }

    public class GarbageSalesVM
    {
        public int ID { get; set; }
        public int SubCategoryID { get; set; }
        public string PartyName { get; set; }
        public decimal SalesWeight { get; set; }
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public int UnitID { get; set; }
    }
    
    public class GarbageHistoryVM
    {
        public Nullable<int> UserId { get; set; }
        public string Date { get; set; }
        public Nullable<int> GarbageCount { get; set; }
        public Nullable<int> SalesCount { get; set; }
    }

    public class GarbageHistoryDetailsVM
    {
        public string Time { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public decimal Weight { get; set; }
        //public string PartyName { get; set; }
        //public decimal Amount { get; set; }
    }

}
