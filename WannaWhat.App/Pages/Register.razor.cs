using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.ViewModels;

namespace WannaWhat.App.Pages
{
    public class RegisterBase : BasePage
    { 
        public RegisterViewModel VM { get; set; }

        public string Message { get; set; }


        public void SaveUser()
        {
            Message = $"User '{VM.PersonalInfo.Name}' Saved";
        }

        protected override void OnInitialized()
        {
            this.VM = new RegisterViewModel { };

            base.OnInitialized();
        }

    }

}