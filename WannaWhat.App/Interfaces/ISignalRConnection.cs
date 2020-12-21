//using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WannaWhat.App.Interfaces
{
    public interface ISignalRConnection
    {
        HubConnection Connection { get; set; }

        ConnectionStatus ConnectionStatus { get; set; }

        bool IsConnected { get; set; }

        string Url { get; set; }

        Task ConnectToServer();
    }
}