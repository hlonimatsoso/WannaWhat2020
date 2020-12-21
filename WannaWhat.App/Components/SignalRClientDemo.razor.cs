using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.App.Interfaces;

namespace WannaWhat.App.Components
{
    public class SignalRClientDemoBase : ComponentBase
    {

        public SignalRClientDemoBase()
        {
            this.notifications = new List<string>();
        }

        [Inject]
        public ISignalRClient SignalRClient { get; set; }

        public List<string> notifications;


        public async void Connect()
        {
            await this.SignalRClient.ConnectToServer();
            this.SignalRClient.SignalRConnection.Connection.On<string>(Constatants.Notificationhub_Notification, s => { this.notifications.Add(s); StateHasChanged(); });
            StateHasChanged();
        }

    }
}
