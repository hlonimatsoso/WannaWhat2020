using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.App.Pages
{
    public class BasePage : ComponentBase
    {
        [Parameter]
        public string Heading { get; set; }

        [Parameter]
        public string Description { get; set; }

        public List<string> Errors { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }


        protected override void OnInitialized()
        {
            //this.VM = new RegisterViewModel { };
            this.Errors = new List<string>();

            base.OnInitialized();
        }
    }
}
