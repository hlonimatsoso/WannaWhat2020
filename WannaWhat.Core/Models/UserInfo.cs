using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WannaWhat.Core;

namespace WannaWhat.Core.Models
{
    public class UserInfo
    {

        //[Key]
        //public int UserInfoId { get; set; }


        public UserInfo()
        {
            DOB = new DateTime(1990, 01, 01);

        }

        public UserInfo(DateTime dob, char gender)
        {
            DOB = dob;
            Gender = gender;
            Age = (byte)(DateTime.Now.Year - DOB.Year);
        }


  

        public DateTime DOB { get; set; }

        public char Gender { get; set; }

        public byte Age { get; set; } = 0;

        public BodyType? BodyType { get; set; } = Core.BodyType.None;

        public byte Height { get; set; } = 0;

        public EyeColor? EyeColor { get; set; } = Core.EyeColor.None;

        public HairColor? HairColor { get; set; } = Core.HairColor.None;

        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }

        [Key]
        public string UserId { get; set; }


    }
}
