using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WannaWhat.Core.Models
{
    public class WannaWhatUser : IdentityUser
    {

        //[Key]
        //public int WannaWhatUserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsOnline { get; set; }


        public UserInfo UserInfo { get; set; }

        public List<UserInterest> UserInerests { get; set; }

        public List<UserPersonality> UserPersonalities { get; set; }

        public List<UserMoods> UserMoods { get; set; }


    }
}
