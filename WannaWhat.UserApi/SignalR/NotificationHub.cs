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
            return Clients.All.SendAsync(Constatants.Notificationhub_OnNewConnection, connId);
        }

        private Task OnDisconnection(string connId)
        {
            return Clients.All.SendAsync(Constatants.Notificationhub_OnDisconnection, connId);
        }

        public Task SendMessageToEveryone(string msg)
        {
            return Clients.All.SendAsync(Constatants.Notificationhub_OnInboundMessage, msg);
        }

        public Task SendMessageToCaller(string msg)
        {
            return Clients.Caller.SendAsync(Constatants.Notificationhub_OnInboundMessage, msg);
        }


        public Task SendMessageToUser(string connectionId, string msg)
        {
            return Clients.Client(connectionId).SendAsync(Constatants.Notificationhub_OnInboundMessage, msg);
        }
    }
}
