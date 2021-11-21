using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.App.Interfaces;
using WannaWhat.DTOs;
using WannaWhat.ViewModels;

namespace WannaWhat.App.Pages
{
    public class RegisterBase : BasePage
    {
        public RegisterViewModel _userForRegistration = new RegisterViewModel();
        [Inject]
        public IAuthService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [Inject]
        protected IMatToaster Toaster { get; set; }
        public async Task Register(MouseEventArgs e)
        {
            ShowRegistrationErrors = false;
            var result = await AuthenticationService.RegisterUser(VM);
            if (result is UserRegistrationErrorResponse )
            {

                Errors = ((UserRegistrationErrorResponse)result).errors.Email;
                ShowRegistrationErrors = true;
                Toaster.Add("Something bombed out", MatToastType.Danger, "Ooops!");

            }
            else if(result is UserRegistrationResponseDto)
            {
                Toaster.Add("We are done..", MatToastType.Success, "Registration complete!");
                NavigationManager.NavigateTo("/");
            }
        }

        public RegisterViewModel VM { get; set; }

        public string Message { get; set; }


        public void SaveUser()
        {
            Message = "Saving...";
            Console.WriteLine("Saving");
            Message = $"User :'{VM.UserName}' with password '{VM.Password}'";
        }

        public void Save(MouseEventArgs e)
        {
            Message = "Saving...";
            Console.WriteLine("Saving");
            Message = $"User :'{VM.UserName}' with password '{VM.Password}'";
        }

        protected override void OnInitialized()
        {
            this.VM = new RegisterViewModel { };

            base.OnInitialized();
        }

    }

}