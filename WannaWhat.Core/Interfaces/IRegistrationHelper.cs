using System;
using System.Collections.Generic;
using System.Text;
using WannaWhat.Core.Models;

namespace WannaWhat.Core.Interfaces
{
    public interface IRegistrationHelper
    {
        void InsertUser(UserInfo info);
        UserInfo GetUserInfo(string id);

        UserInventory GetUserInventory(string id);

    }
}
