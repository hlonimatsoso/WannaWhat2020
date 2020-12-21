using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.App.Components
{
    public class SignalRDemoBase : ComponentBase
    {
        HubConnection _connection = null;
        string url = "https://localhost:5002/notificationhub";
        public string connectionStatus = "Closed";
        public bool isConnected = false;
        public List<string> notifications = new List<string> { };

        public async Task ConnectToServer()
        {
            _connection = new HubConnectionBuilder().WithUrl(url).Build();
            await _connection.StartAsync();
            connectionStatus = "Connected #SmileyFace";
            isConnected = true;
            _connection.Closed += _connection_Closed;
            _connection.On<string>("notification", s => { this.notifications.Add(s); StateHasChanged(); });
            StateHasChanged();
        }

        private async Task _connection_Closed(Exception arg)
        {
            connectionStatus = "DISCONNECTED!!!";
            isConnected = false;
            StateHasChanged();

            await _connection.StartAsync();
        }


    }
}
