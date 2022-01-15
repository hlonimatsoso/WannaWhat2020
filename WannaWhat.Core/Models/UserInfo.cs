using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WannaWhat.Core;
using WannaWhat.ViewModels;

namespace WannaWhat.Core.Models
{
    public class UserInfo
    {

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

        public UserInfo(PersonalInfoViewModel vm) 
        {
            this.DOB = vm.DOB;
            this.Age = (byte)(DateTime.Now.Year - this.DOB.Year);
            this.Gender = vm.Gender;
            this.FullName = vm.Name;
            this.Surname = vm.Surname;
        }


        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }

        [Key]
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Surname { get; set; }

        public DateTime DOB { get; set; }

        public char Gender { get; set; }

        public byte Age { get; set; } = 0;

        public BodyType? BodyType { get; set; } = Core.BodyType.None;

        public byte Height { get; set; } = 0;

        public EyeColor? EyeColor { get; set; } = Core.EyeColor.None;

        public HairColor? HairColor { get; set; } = Core.HairColor.None;

     


    }
}
