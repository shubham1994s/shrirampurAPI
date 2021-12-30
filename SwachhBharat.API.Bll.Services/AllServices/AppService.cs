
using SwachhBharatAPI.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.API.Bll.Services
{
    public abstract class AppService
    {
        protected DevSwachhBharatNagpurEntities db;
        protected DevSwachhBharatMainEntities dbMain;
        public AppService(int AppId)
        {
            db = new DevSwachhBharatNagpurEntities(AppId);
            dbMain = new DevSwachhBharatMainEntities();
        }
        public AppService()
        {
            dbMain = new DevSwachhBharatMainEntities();
        }
    }
}
