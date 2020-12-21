using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.App.Interfaces
{
   public interface ISignalRClient
    {
        ISignalRConnection SignalRConnection { get; set; }

        Task ConnectToServer();

        Task Connection_Closed(Exception arg);

        //Task OnInboundMessage(string msg);

        //Task OnNewConnection(string connId);

        //Task OnDisonnection(string connId);


    }
}
