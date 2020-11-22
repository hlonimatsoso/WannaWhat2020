using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.UserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetAllClaims()
        {
            return Ok(HttpContext.User.Claims.ToList().Select(x => new { x.Type, x.Value }));
        }
    }
}
