using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WannaWhat.ViewModels;

namespace WannaWhat.Core.Models
{
    public class WannaWhatUser : IdentityUser
    {

        public WannaWhatUser()
        {

        }

        public WannaWhatUser(RegisterViewModel vm)
        {
            this.UserName = vm.UserName;
            this.Email = vm.Email;
            this.UserInfo = new UserInfo(vm.PersonalInfo);
            this.UserInventory = new UserInventory();
            this.UserInerests = new List<UserInterest>();
            this.UserPersonalities = new List<UserPersonality>();
            this.UserMoods = new List<UserMoods>();
            this.Roles = new List<IdentityRole> { new IdentityRole { Name = Constatants.Roles_User } };
        }

        //[Key]
        //public int WannaWhatUserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsOnline { get; set; }


        public UserInfo UserInfo { get; set; }

        public UserInventory UserInventory { get; set; }

        public List<UserInterest> UserInerests { get; set; }

        public List<UserPersonality> UserPersonalities { get; set; }

        public List<UserMoods> UserMoods { get; set; }

        public List<IdentityRole> Roles { get; set; }

    }
}
