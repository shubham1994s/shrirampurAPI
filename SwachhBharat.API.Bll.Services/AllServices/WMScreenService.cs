using SwachBharat.API.Bll.Services;
using SwachhBharatAPI.Dal.DataContexts;
using SwachhBhart.API.Bll.ViewModels;
using SwachhBhart.API.Bll.ViewModels.WasteManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachhBharat.API.Bll.Services.AllServices
{
    public class WMScreenService : AppService , IWMScreenService
    {
        private int AppID;
        public WMScreenService(int AppId) : base(AppId)
        {
            AppID = AppId;
        }

        public List<Result2> GarbageDetails(int AppId, List<GarbageDetailsVM> obj)
        {
            List<Result2> result = new List<Result2>();
            using (var db = new DevSwachhBharatNagpurEntities(AppID))
            {
                WM_Garbage_Details Context = new WM_Garbage_Details();
                foreach (var x in obj)
                {
                    try
                    {
                        Context.SubCategoryID = x.SubCategoryID;
                        Context.Weight = (x.UnitID == 1 ? (x.Weight / 1000) : x.Weight);
                        Context.UserId = x.UserID;
                        Context.Source = x.Source;

                        if (x.gdDate.Equals(DateTime.MinValue))
                        {
                            Context.gdDate = DateTime.Now;
                        }
                        else
                        {
                            Context.gdDate = x.gdDate;
                        }

                        Context.CreatedDate = DateTime.Now;
                        db.WM_Garbage_Details.Add(Context);

                        var IsExist = db.WM_Garbage_Summary.Where(c => c.SubCategoryID == x.SubCategoryID).FirstOrDefault();
                        if (IsExist != null)
                        {
                            IsExist.SubCategoryID = x.SubCategoryID;
                            IsExist.TotalWeight = IsExist.TotalWeight + (x.UnitID == 1 ? (x.Weight / 1000) : x.Weight);
                            IsExist.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            WM_Garbage_Summary summary = new WM_Garbage_Summary();
                            summary.SubCategoryID = x.SubCategoryID;
                            summary.TotalWeight = (x.UnitID == 1 ? (x.Weight / 1000) : x.Weight);
                            summary.CreatedDate = DateTime.Now;
                            db.WM_Garbage_Summary.Add(summary);
                        }
                        
                        db.SaveChanges();

                        result.Add(new Result2()
                        {
                            ID = x.ID,
                            status = "success",
                            message = "Saved successfully",
                            messageMar = "सबमिट यशस्वी"
                        });
                    }
                    catch
                    {
                        result.Add(new Result2()
                        {
                            ID = x.ID,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा.."
                        });
                        
                    }
                    
                }
                return result;
            }
              
        }

        public List<Result2> GarbageSales(int AppId, List<GarbageSalesVM> obj)
        {
            List<Result2> result = new List<Result2>();
            using (var db = new DevSwachhBharatNagpurEntities(AppID))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        WM_Garbage_Sales Context = new WM_Garbage_Sales();
                        foreach (var x in obj)
                        {
                            try
                            {
                                var IsExist = db.WM_Garbage_Summary.Where(c => c.SubCategoryID == x.SubCategoryID).FirstOrDefault();
                                if (IsExist != null && IsExist.TotalWeight > 0)
                                {
                                    //Update Garbage Summary Record

                                    IsExist.SubCategoryID = x.SubCategoryID;
                                    IsExist.TotalWeight = IsExist.TotalWeight - (x.UnitID == 1 ? (x.SalesWeight / 1000) : x.SalesWeight);
                                    IsExist.CreatedDate = DateTime.Now;

                                    //Insert Garbage Sales Record

                                    Context.SubCategoryID = x.SubCategoryID;
                                    Context.PartyName = x.PartyName;
                                    Context.SalesWeight = (x.UnitID == 1 ? (x.SalesWeight / 1000) : x.SalesWeight);
                                    Context.Amount = x.Amount;
                                  //  Context.UserID = x.UserID;
                                    Context.CreatedDate = DateTime.Now;
                                    db.WM_Garbage_Sales.Add(Context);
                                    db.SaveChanges();

                                    result.Add(new Result2()
                                    {
                                        ID = x.SubCategoryID,
                                        status = "success",
                                        message = "Saved successfully",
                                        messageMar = "सबमिट यशस्वी"
                                    });
                                }
                                else
                                {
                                    result.Add(new Result2()
                                    {
                                        ID = x.SubCategoryID,
                                        status = "success",
                                        message = "Insufficient Stock",
                                        messageMar = "अपुरा साठा"
                                    });
                                }
                                
                            }
                            catch
                            {
                                result.Add(new Result2()
                                {
                                    ID = x.SubCategoryID,
                                    status = "error",
                                    message = "Something is wrong,Try Again.. ",
                                    messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा.."
                                });

                            }
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result.Add(new Result2()
                        {
                            ID = 0,
                            status = "error",
                            message = "Something is wrong,Try Again.. ",
                            messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा.."
                        });
                    }
                }
                
                return result;
            }

        }
    }
}
