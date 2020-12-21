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

        public NotificationsController(IHubContext<NotificationHub> hub)
        {
            this._hubContext = hub;
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
    }
}
