using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Core.Models;
using WannaWhat.DTOs;
using WannaWhat.ViewModels;

namespace WannaWhat.UserApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<WannaWhatUser> _userManager;
        public UserController(UserManager<WannaWhatUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            var user = new WannaWhatUser { UserName = userForRegistration.UserName, Email = userForRegistration.Email };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                var badReq = new UserRegistrationErrorResponse();
                badReq.errors = new Errors();
                badReq.errors.Email = errors.ToList();


                return BadRequest();
            }

            return StatusCode(201);
        }
    }
}
