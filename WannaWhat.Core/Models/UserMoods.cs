using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WannaWhat.Core;

namespace WannaWhat.Core.Models
{
    public class UserMoods
    {

        [Key]
        public string UserMoodId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }

        public string MoodId { get; set; }

        [ForeignKey("MoodId")]
        public Mood Mood { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsActive { get; set; }


        public string CustomNote { get; set; }

    }
}
