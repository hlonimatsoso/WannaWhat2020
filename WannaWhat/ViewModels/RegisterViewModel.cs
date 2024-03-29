﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WannaWhat.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }


        public RegisterViewModel()
        {
            this.PersonalInfo = new PersonalInfoViewModel { };
            this.Personality = new PersonalityViewModel { };
        }

        public PersonalInfoViewModel PersonalInfo { get; set; }

        public PersonalityViewModel Personality { get; set; }


    }
}
