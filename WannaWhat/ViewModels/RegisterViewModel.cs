using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WannaWhat.ViewModels
{
    public class RegisterViewModel
    {

        public RegisterViewModel()
        {
            this.PersonalInfo = new PersonalInfoViewModel { };
            this.Personality = new PersonalityViewModel { };
        }

        public PersonalInfoViewModel PersonalInfo { get; set; }

        public PersonalityViewModel Personality { get; set; }


    }
}
