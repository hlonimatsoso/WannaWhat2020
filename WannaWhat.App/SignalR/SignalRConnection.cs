//using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.App.Interfaces;

namespace WannaWhat.App.SignalR
{
    public class SignalRConnection : ISignalRConnection
    {

        public SignalRConnection()
        {
            this.ConnectionStatus = ConnectionStatus.NONE;
            this.IsConnected = false;
            this.Url = Constatants.Notificationhub_URL;
            this.Connection = new HubConnectionBuilder().WithUrl(Url).Build();
        }

        public HubConnection Connection { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public bool IsConnected { get; set; }
        public string Url { get; set; }

        public List<string> notifications { get; set; }

        public async Task ConnectToServer()
        {
            Connection = new HubConnectionBuilder().WithUrl(Url).Build();
            await Connection.StartAsync();
            ConnectionStatus = ConnectionStatus.Connected;
            IsConnected = true;
            Connection.Closed += _connection_Closed;
            Connection.On<string>("notification", s => { this.notifications.Add(s); });
            //StateHasChanged();
        }

        private async Task _connection_Closed(Exception arg)
        {
            ConnectionStatus = ConnectionStatus.Disconnected;
            IsConnected = false;
            //StateHasChanged();

            await Connection.StartAsync();
        }
    }
}
