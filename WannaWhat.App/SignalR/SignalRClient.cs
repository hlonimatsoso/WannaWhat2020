using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using WannaWhat.App.Interfaces;

namespace WannaWhat.App.SignalR
{
    public class SignalRClient : ISignalRClient
    {

        public SignalRClient(ISignalRConnection conn)
        {
            SignalRConnection = conn;
            SignalRConnection.Connection.Closed += Connection_Closed;

            //Connection.Connection.On<string>("OnInboundMessage", OnInboundMessage);
            //Connection.Connection.On<string>("OnNewConnection", OnNewConnection);
            //Connection.Connection.On<string>("OnDisonnection", OnDisonnection);

        }

        public Task Connection_Closed(Exception arg)
        {
            SignalRConnection.ConnectionStatus = ConnectionStatus.Disconnected;
            SignalRConnection.IsConnected = false;
            return SignalRConnection.ConnectToServer();
        }

        public ISignalRConnection SignalRConnection { get; set ; }

        public async Task ConnectToServer()
        {
            await SignalRConnection.ConnectToServer();
        }

        //public Task OnInboundMessage(string msg)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task OnNewConnection(string connId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task OnDisonnection(string connId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
