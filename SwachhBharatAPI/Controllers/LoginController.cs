using SwachhBharat.API.Bll.Repository.Repository;
using SwachhBhart.API.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwachhBharatAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class LoginController : ApiController
    {
        IRepository objRep;

        // GET: api/users
        [Route("Login")]
        [HttpPost]
        public SBUser GetLogin(SBUser objlogin)
        {
            IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            var id = headerValue1.FirstOrDefault();
            int AppId = int.Parse(id);

            IEnumerable<string> headerValue2 = Request.Headers.GetValues("EmpType");
            var EmpType = headerValue2.FirstOrDefault();


            objRep = new Repository();
            SBUser objresponse = objRep.CheckUserLogin(objlogin.userLoginId, objlogin.userPassword, objlogin.imiNo, AppId,EmpType);
            return objresponse;
        }

        //Added By Saurbh
        // GET: api/admins
        [Route("AdminLogin")]
        [HttpPost]
        public SBAdmin GetAdminLogin(SBAdmin objadminlogin)
        {
            //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
            //var id = headerValue1.FirstOrDefault();
            //int AppId = int.Parse(id);
            
            objRep = new Repository();

            SBAdmin objresponse = objRep.CheckAdminLogin(objadminlogin.adminLoginId,objadminlogin.adminPassword);
            return objresponse;
        }


        ////Added By Saurbh (16 May 19)
        //// GET: api/admins
        //[Route("EmployeeLogin")]
        //[HttpPost]
        //public BigVQrEmployeeVM GetQrEmployeeLogin(BigVQrEmployeeVM objEmployeeLogin)
        //{
        //    //IEnumerable<string> headerValue1 = Request.Headers.GetValues("appId");
        //    //var id = headerValue1.FirstOrDefault();
        //    //int AppId = int.Parse(id);

        //    objRep = new Repository();

        //    BigVQrEmployeeVM objresponse = objRep.CheckQrEmployeeLogin(objEmployeeLogin.qrEmpLoginId, objEmployeeLogin.qrEmpPassword);
        //    return objresponse;
        //}
    }
}
