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
            this.connections = new List<string>();
            this.msgs = new List<string>();

        }

        [Inject]
        public ISignalRClient SignalRClient { get; set; }

        public List<string> notifications;
        public List<string> connections;

        public List<string> msgs;


        protected override void OnInitialized()
        {
            notifications = new List<string>();
            connections = new List<string>();
            msgs = new List<string>();

            base.OnInitialized();
        }

        public async void Connect()
        {
            
            await this.SignalRClient.ConnectToServer();
            this.SignalRClient.SignalRConnection.Connection.On<string>(Constatants.Notificationhub_OnNewConnection, NewConnection);
            this.SignalRClient.SignalRConnection.Connection.On<string>(Constatants.Notificationhub_OnDisconnection, Disconnection);
            this.SignalRClient.SignalRConnection.Connection.On<string>(Constatants.Notificationhub_Notification, Notification);
            this.SignalRClient.SignalRConnection.Connection.On<string>(Constatants.Notificationhub_OnInboundMessage, InboundMessage);

            StateHasChanged();
        }

        public void NewConnection(string connId)
        {
            this.connections.Add(connId);
            StateHasChanged();
        }


        public void Disconnection(string connId)
        {
            this.connections.Remove(connId);
            StateHasChanged();
        }


        public void Notification(string note)
        {
            this.notifications.Add(note);
            StateHasChanged();
        }


        public void InboundMessage(string msg)
        {
            this.msgs.Add(msg);
            StateHasChanged();
        }

    }
}
