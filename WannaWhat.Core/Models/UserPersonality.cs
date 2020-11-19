using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Core.Models
{
    public class UserPersonality
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }


        public string PersonalityId { get; set; }

        [ForeignKey("PersonalityId")]

        public Personality Personality { get; set; }


    }
}
