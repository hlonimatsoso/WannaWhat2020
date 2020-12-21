using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.UserApi.SignalR
{
    public class NotificationHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            OnNewConnection(Context.ConnectionId);

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            OnDisconnection(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }


        private Task OnNewConnection(string connId)
        {
            return Clients.All.SendAsync("OnNewConnection", connId);
        }

        private Task OnDisconnection(string connId)
        {
            return Clients.All.SendAsync("OnDisconnection", connId);
        }

        public Task SendMessageToEveryone(string msg)
        {
            return Clients.All.SendAsync("OnInboundMessage", msg);
        }

        public Task SendMessageToCaller(string msg)
        {
            return Clients.Caller.SendAsync("OnInboundMessage", msg);
        }


        public Task SendMessageToUser(string connectionId, string msg)
        {
            return Clients.Client(connectionId).SendAsync("InboundMessage", msg);
        }
    }
}
