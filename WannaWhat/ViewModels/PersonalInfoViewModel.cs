using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WannaWhat.ViewModels
{
    public class PersonalInfoViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname required.")]

        public string Surname { get; set; }

        public DateTime DOB { get; set; }

        public char Gender { get; set; }
    }
}
