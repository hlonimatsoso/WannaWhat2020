using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Core.Interfaces;
using WannaWhat.Core.Models;
using WannaWhat.Data;

namespace WannaWhat.IdentityServer.Services
{
    public class RegistrationService : IRegistrationHelper
    {

        public WannaWhatDbContext DbContext { get; set; }


        public RegistrationService(WannaWhatDbContext cxt)
        {
            DbContext = cxt;
        }

        public void InsertUser(UserInfo info)
        {
            DbContext.UserInfo.Add(info);
            DbContext.SaveChanges();
        }

        public UserInfo GetUserInfo(string id)
        {
            UserInfo result = null;
            if (DbContext.UserInfo.Count() > 0)
                result = DbContext.UserInfo.Find(id);
            return result;
        }

        public UserInventory GetUserInventory(string id)
        {
            UserInventory result = null;
            if (DbContext.UserInventory.Count() > 0)
                result = DbContext.UserInventory.Find(id);
            return result;
        }
    }
}
