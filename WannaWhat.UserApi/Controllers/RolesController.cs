using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Core.Models;

namespace WannaWhat.UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WannaWhatUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<WannaWhatUser> userMrg)
        {
            this._roleManager = roleMgr;
            this._userManager= userMrg;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAllRolls()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }


        //public async Task<IActionResult> Update(string id)
        //{
        //    IdentityRole role = await roleManager.FindByIdAsync(id);
        //    List<WannaWhatUser> members = new List<WannaWhatUser>();
        //    List<WannaWhatUser> nonMembers = new List<WannaWhatUser>();
        //    foreach (WannaWhatUser user in userManager.Users)
        //    {
        //        var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
        //        list.Add(user);
        //    }
        //    return View(new RoleEdit
        //    {
        //        Role = role,
        //        Members = members,
        //        NonMembers = nonMembers
        //    });
        //}


    }

    
}
