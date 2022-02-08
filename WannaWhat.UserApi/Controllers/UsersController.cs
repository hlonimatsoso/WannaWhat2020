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
            GeneralResponseDTO<bool> responseDto = new GeneralResponseDTO<bool>();

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                response.errors.PersonalInfoName = errors.ToList();         // TODO: FIX        
                responseDto.Payload = false;
                responseDto.Errors = errors.ToList();
                return BadRequest(responseDto);
            }

            await _userManager.AddToRoleAsync(user, Constatants.Roles_User);

            responseDto.IsValid = true;
            responseDto.Payload = true;
            response.IsValid = true;
            response.Status = 200;

            return Ok(responseDto);
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

        [HttpGet("{username}/addRole/{roleName}")]
        public async Task<IActionResult> AddRoleToUser([FromRoute] string username, [FromRoute] string roleName)
        {
            IActionResult result;
            IdentityResult idResult = new IdentityResult();
            GeneralResponseDTO<bool> response = new GeneralResponseDTO<bool>();
            var user = await _userManager.FindByNameAsync(username);
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            

            if(role == null)
            {
                response.IsValid = false;
                response.Description = "Role not found!";
                return result = BadRequest(response);
            }

            if (user == null)
            {
               return result = BadRequest("No such user found.");
            }
            

            idResult = await _userManager.AddToRoleAsync(user, role.Name);
            response.IsValid = idResult.Succeeded;
            response.Payload = idResult.Succeeded;
            result = Ok(response);

            

            return result;
        }
    }
}
