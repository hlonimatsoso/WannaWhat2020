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
    }
}
