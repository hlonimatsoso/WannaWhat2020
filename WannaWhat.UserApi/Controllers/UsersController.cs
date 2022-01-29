using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Core.Models;
using WannaWhat.DTOs;
using WannaWhat.ViewModels;

namespace WannaWhat.UserApi.Controllers
{

    
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<WannaWhatUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<WannaWhatUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            //var user = new WannaWhatUser { UserName = userForRegistration.UserName, Email = userForRegistration.Email };
            var user = new WannaWhatUser(userForRegistration);

            UserRegistrationResponse response = new UserRegistrationResponse();

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                response.errors.PersonalInfoName = errors.ToList();         // TODO: FIX        

                return BadRequest(response);
            }

            await _userManager.AddToRoleAsync(user, Constatants.Roles_User);

            response.IsValid = true;
            response.Status = 200;

            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> ByUsername([FromRoute] string username)
        {
            IActionResult result;
            var user = await _userManager.FindByNameAsync(username);
            if(user!= null)
            {
                result = Ok(user);
            }
            else
            {
                result = BadRequest("No such user found.");
            }
            return result;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> ByUsername2([FromRoute] string username)
        {
           
            return await this.ByUsername(username);
        }

        [HttpGet("{username}/roles")]
        public async Task<IActionResult> GetUserRoles([FromRoute] string username)
        {
            IActionResult result;
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                result = Ok(await _userManager.GetRolesAsync(user));
            }
            else
            {
                result = BadRequest("No such user found.");
            }

            return result;
        }
    }
}
