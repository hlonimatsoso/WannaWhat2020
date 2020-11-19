using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Core.Models
{
    public class UserInterest
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }


        public string InterestId { get; set; }

        [ForeignKey("InterestId")]

        public Interest Interest { get; set; }


    }
}
