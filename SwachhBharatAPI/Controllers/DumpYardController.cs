using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SwachhBharat.API.Bll.Repository;
using SwachhBhart.API.Bll.ViewModels;
using System.IO;
using System.Collections.Specialized;
using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBharatAPI.Dal.DataContexts;
using System.Threading.Tasks;
using SwachhBharatAPI.Models;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api")]
    public class DumpYardController : ApiController
    {
        IRepository _RepositoryApi;
        DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
        [HttpPost]
        [Route("Save/DumpYardCollection")]
        public async Task<CollectionResult> MediaUpload1()
        {

            _RepositoryApi = new Repository();
            SBGarbageCollectionView gcDetail = new SBGarbageCollectionView();
            CollectionResult objres = new CollectionResult();
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            IEnumerable<string> headervalue2 = Request.Headers.GetValues("batteryStatus");
            var batteryStatus = headervalue2.FirstOrDefault().ToString();
            string[] impath = new string[2];
            //string[] arr = new string[4];
            int i = 0;
            try
            {
                string imagePath, FileName;

                var AppId = Convert.ToInt32(headerValue1.FirstOrDefault());

                var objmain = dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault();
                var AppDetailURL = objmain.baseImageUrl + objmain.basePath + objmain.Collection + "/";

                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    objres.status = "Failed";
                    objres.message = (new HttpResponseException(HttpStatusCode.UnsupportedMediaType)).ToString();
                    return objres;
                }
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access form data
                NameValueCollection formData = provider.FormData;
                //access files
                IList<HttpContent> files = provider.Files;
                HttpContent file1, file2;
                impath = new string[files.Count];
                string Source = AppDetailURL;
                bool exist = Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(objmain.basePath + objmain.Collection));
                if (!exist)
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(objmain.basePath + objmain.Collection));

                //access files
                //string filename = String.Empty;
                if (files.Count == 0)
                {
                    file1 = null;
                    imagePath = "";
                    FileName = "";
                }
                else
                {
                    foreach (var item in files)
                    {
                        string Fil = item.Headers.ContentDisposition.FileName.Trim('\"');
                        FileName = string.Join("", Fil.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

                        imagePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(objmain.basePath + objmain.Collection + "/" + FileName));
                        impath[i] = FileName;
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        Stream input = await item.ReadAsStreamAsync();
                        using (Stream file3 = File.OpenWrite(imagePath))
                        {
                            input.CopyTo(file3);
                            //close file
                            file3.Close();
                        }
                        //  objRep.UploadFileToFTP(imagePath, SUser);
                        i++;
                    }

                }
                string Filepath = Source;
                gcDetail.userId = int.Parse(formData["userId"]);
                //if (formData["houseId"] == null)
                //{
                //    gcDetail.houseId = "";
                //}
                //else {
                //    gcDetail.houseId = formData["houseId"];
                //    gcDetail.gcType = 1;
                //}
                if (formData["dyId"] == null)
                {
                    gcDetail.dyId = "";
                }
                else {
                    gcDetail.dyId = formData["dyId"];
                    gcDetail.gcType = 3;
                }
                gcDetail.Lat = formData["Lat"];
                gcDetail.Long = formData["Long"];
                gcDetail.note = formData["note"];
                gcDetail.totalGcWeight = Convert .ToDecimal(formData["totalGcWeight"]);
                gcDetail.totalDryWeight = Convert.ToDecimal(formData["totalDryWeight"]);
                gcDetail.totalWetWeight = Convert.ToDecimal(formData["totalWetWeight"]);
                gcDetail.garbageType = Convert.ToInt32(formData["garbageType"]);
                gcDetail.vehicleNumber = formData["vehicleNumber"];
                // gcDetail.gcDate = Convert.ToString(formData["gcDate"]);             
                string imageStart = "", imageEnd = "";
                imageStart = Convert.ToString(formData["beforeImage"]);
                imageEnd = Convert.ToString(formData["AfterImage"]);
                string Image = "";
                if (impath.Length == 0 || impath[0] == null)
                {
                    gcDetail.gpBeforImage = "";
                    gcDetail.gpAfterImage = "";
                }
                else
                {
                    if (imageStart == "" || imageStart == string.Empty || imageStart == null)
                    {
                        gcDetail.gpBeforImage = "";
                        if (imageEnd != "" || imageEnd != string.Empty || imageEnd != null)

                        {
                            gcDetail.gpAfterImage = impath[0];
                        }
                    }
                    else
                    {
                        gcDetail.gpBeforImage = impath[0];


                        if (impath.Length == 0 || i <= 1)
                        {
                            gcDetail.gpAfterImage = "";
                        }
                        else
                        {
                            if (imageEnd != "" || imageEnd != string.Empty || imageEnd != null)

                            {
                                gcDetail.gpAfterImage = impath[1];
                            }
                        }
                    }
                }
                CollectionResult detail = _RepositoryApi.SaveDumpYardCollection(gcDetail, AppId, 0, batteryStatus);
                if (detail.message == "")
                {
                    objres.name = "";
                    objres.status = "error";
                    objres.message = "Record not inserted";
                    objres.messageMar = "रेकॉर्ड सबमिट केले नाही";
                    return objres;
                }
                objres.name = detail.name;
                objres.status = detail.status;
                objres.messageMar = detail.messageMar;
                objres.message = detail.message;
                objres.mobile = detail.mobile;
                objres.nameMar = detail.nameMar;
                objres.isAttendenceOff = detail.isAttendenceOff;
                return objres;
            }
            catch (Exception ee)
            {
                objres.mobile = "";
                objres.nameMar = "";
                objres.name = "";
                objres.status = "error";
                objres.message = "Something is wrong,Try Again.. ";
                objres.messageMar = "काहीतरी चुकीचे आहे, पुन्हा प्रयत्न करा..";
            }
            objres.mobile = "";
            objres.nameMar = "";
            objres.name = "";
            objres.status = "error";
            objres.message = "Record not inserted";
            objres.messageMar = "रेकॉर्ड सबमिट केले नाही";
            return objres;
        }
    }
}
