using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachhBharatAPI.Dal.DataContexts;
using System.Web.Mvc;
using SwachhBhart.API.Bll.ViewModels;

namespace SwachBharat.API.Bll.Services
{
    public class MainService : AppMainService, IMainService
    {
        #region Manage User Custom Chagnes
        public int GetUserAppId(string UserId)
        {
            int AppId = 0;
            AppId = dbMain.UserInApps.Where(x => x.UserId == UserId).Select(x => x.AppId).FirstOrDefault();

            return AppId;
        }

        #endregion


        public Result SaveStateDetail(CMSBStatesVM data)
        {
            Result result = new Result();
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.id > 0)
                    {
                        var model = db.country_states.Where(x => x.id == data.id).FirstOrDefault();
                        if (model != null)
                        {
                            model.country_name = data.country_name;
                            model.state_name = data.state_name;
                            model.state_name_mar = data.state_name_mar;
                            model.id = data.id;
                            db.SaveChanges();

                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                            //result.name = "";
                        }
                        else
                        {
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            //result.name = "";
                            result.status = "error";
                        }
                    }
                    else
                    {
                        var state = FillStateDataModel(data);
                        db.country_states.Add(state);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                //result.name = "";
                result.status = "error";
                return result;
            }
        }

        private country_states FillStateDataModel(CMSBStatesVM data)
        {
            country_states model = new country_states();
            model.country_name = data.country_name;
            model.state_name = data.state_name;
            model.state_name_mar = data.state_name_mar;
            model.id = data.id;
            return model;
        }


        public Result SaveZone(CMSBZoneVM data ,int AppID)
        {
            Result result = new Result();

            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(AppID))
                {
                    if (data.zoneId > 0)
                    {
                        var model = db.ZoneMasters.Where(x => x.zoneId == data.zoneId).FirstOrDefault();
                        if (model != null)
                        {
                            model.zoneId = data.zoneId;
                            model.name = data.name;
                            db.SaveChanges();
                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                        }
                        else
                        {
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            result.status = "error";
                        }
                    }
                    else
                    {
                        ZoneMaster obj = new ZoneMaster();
                        obj.name = data.name;
                        obj.zoneId = data.zoneId;
                        db.ZoneMasters.Add(obj);
                        db.SaveChanges();
                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                result.status = "error";
                return result;
            }
        }

        #region Ward
        public Result SaveWardDetail(CMSBWardVM data, int AppId)
        {
            Result result = new Result();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    if (data.Id > 0)
                    {
                        var model = db.WardNumbers.Where(x => x.Id == data.Id).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.Id;
                            model.WardNo = data.WardNo;
                            model.zoneId = data.zoneId;
                            db.SaveChanges();

                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                            //result.name = "";
                        }
                        else
                        {
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            //result.name = "";
                            result.status = "error";
                        }
                    }
                    else
                    {
                        var Ward = FillWardDataModel(data);
                        db.WardNumbers.Add(Ward);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                //result.name = "";
                result.status = "error";
                return result;
            }
        }

        private WardNumber FillWardDataModel(CMSBWardVM data)
        {
            WardNumber model = new WardNumber();
            model.Id = data.Id;
            model.WardNo = data.WardNo;
            model.zoneId = data.zoneId;
            return model;
        }


        #endregion

        #region Area
        public Result SaveAreaDetail(CMSBAreaVM data, int AppId)
        {
            Result result = new Result();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    if (data.id > 0)
                    {
                        var model = db.TeritoryMasters.Where(x => x.Id == data.id).FirstOrDefault();
                        if (model != null)
                        {
                            model.Id = data.id;
                            model.wardId = data.wardId;
                            model.Area = (string.IsNullOrEmpty(data.area) ? null : data.area );
                            model.AreaMar = (string.IsNullOrEmpty(data.areaMar) ? null : data.areaMar); //data.areaMar;
                            db.SaveChanges();

                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                            //result.name = "";
                        }
                        else
                        {
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            //result.name = "";
                            result.status = "error";
                        }
                    }
                    else
                    {
                        var Area = FillAreaDataModel(data);
                        db.TeritoryMasters.Add(Area);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                //result.name = "";
                result.status = "error";
                return result;
            }
        }

        private TeritoryMaster FillAreaDataModel(CMSBAreaVM data)
        {
            TeritoryMaster model = new TeritoryMaster();
            model.Id = data.id;
            model.wardId = data.wardId;
            model.Area = data.area;
            model.AreaMar = data.areaMar;
            return model;
        }


        #endregion

        #region VehicleType
        public Result SaveVehicleTypeDetail(SBVehicleType data, int AppId)
        {
            Result result = new Result();
            try
            {
                using (var db = new DevSwachhBharatNagpurEntities(AppId))
                {
                    if (data.vtId > 0)
                    {
                        var model = db.VehicleTypes.Where(x => x.vtId == data.vtId).FirstOrDefault();
                        if (model != null)
                        {
                            model.vtId = data.vtId;
                            model.description = data.description;
                            model.descriptionMar = data.descriptionMar;
                            model.isActive = data.isActive;
                            db.SaveChanges();

                            result.status = "success";
                            result.message = "Uploaded successfully";
                            result.messageMar = "सबमिट यशस्वी";
                            //result.name = "";
                        }
                        else
                        {
                            result.message = "Something is wrong,Try Again.. ";
                            result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                            //result.name = "";
                            result.status = "error";
                        }
                    }
                    else
                    {
                        var VehicleType = FillVehicleDataModel(data);
                        db.VehicleTypes.Add(VehicleType);
                        db.SaveChanges();

                        result.status = "success";
                        result.message = "Saved successfully";
                        result.messageMar = "सबमिट यशस्वी";
                        //result.name = "";
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                result.message = "Something is wrong,Try Again.. ";
                result.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
                //result.name = "";
                result.status = "error";
                return result;
            }
        }
        private VehicleType FillVehicleDataModel(SBVehicleType data)
        {
            VehicleType model = new VehicleType();
            model.vtId = data.vtId;
            model.description = data.description;
            model.descriptionMar = data.descriptionMar;
            model.isActive = data.isActive;
            return model;
        }

        #endregion

    }
}
