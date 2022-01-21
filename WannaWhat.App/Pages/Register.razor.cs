using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public bool RegisterDisabled
        {
            get
            {
                return string.IsNullOrEmpty(this.VM.UserName) || string.IsNullOrEmpty(this.VM.Password) || string.IsNullOrEmpty(this.VM.Email);
            }
        }

        public List<string> Errors { get; set; }
        [Inject]
        protected IMatToaster Toaster { get; set; }
        public async Task Register(MouseEventArgs e)
        {
            ShowRegistrationErrors = false;
            UserRegistrationResponse result = await AuthenticationService.RegisterUser(VM);
            Console.WriteLine($"Register.registrationResult: {result}");

            if (result.IsValid)
            {
                Toaster.Add("We are done..", MatToastType.Success, "Registration complete!");
                NavigationManager.NavigateTo("/");

            }
            else
            {
                foreach (string error in result.errors.PersonalInfoName)
                {
                    Errors.Add(error);
                    Console.WriteLine($"Added name error: {error}");

                }

                foreach (string error in result.errors.PersonalInfoSurname)
                {
                    Errors.Add(error);
                    Console.WriteLine($"Added last name error: {error}");

                }
                ShowRegistrationErrors = true;
                Toaster.Add("Something bombed out chief", MatToastType.Danger, "Ooops!");
                Console.WriteLine($"Register.Errors: {JsonSerializer.Serialize(Errors)}");

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
            this.Errors = new List<string>();

            base.OnInitialized();
        }

    }

}