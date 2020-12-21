using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.UserApi.SignalR;

namespace WannaWhat.UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly NotificationHub _hub;


        public NotificationsController(IHubContext<NotificationHub> hubCon, NotificationHub hub)
        {
            this._hubContext = hubCon;
            this._hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string name, [FromQuery] string msg)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("notification", $"Message from '{name}' : '{msg}'");
            }
            catch (Exception)
            {

                throw;
            }

            return Ok("Notification was sent successfully.");
        }


        [HttpPost("SendToUser")]
        public async Task<IActionResult> SendToUser(string connId, string msg)
        {
            try
            {
                await _hubContext.Clients.Client(connId).SendAsync(Constatants.Notificationhub_OnInboundMessage, msg);

                //await _hub.SendMessageToUser(connId, msg);
            }
            catch (Exception)
            {

                throw;
            }

            return Ok("Notification was sent successfully.");
        }
    }
}
