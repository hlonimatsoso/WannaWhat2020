using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WannaWhat.Core.Models
{
    public class WannaWhatUser : IdentityUser
    {

        public bool IsActive { get; set; }

        public UserInfo UserInfo { get; set; }

        public List<UserInterest> UserInerests { get; set; }

        public List<UserPersonality> UserPersonalities { get; set; }

    }
}
