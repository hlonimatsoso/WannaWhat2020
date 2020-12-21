﻿using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WannaWhat.Core.Interfaces;
using WannaWhat.Core.Models;

namespace WannaWhat.IdentityServer.Services
{
    public class WannaWhatProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<WannaWhatUser> _claimsFactory;
        private readonly UserManager<WannaWhatUser> _userManager;
        private readonly IRegistrationHelper _regHelper;

        public WannaWhatProfileService(UserManager<WannaWhatUser> userManager, IUserClaimsPrincipalFactory<WannaWhatUser> claimsFactory, IRegistrationHelper regHelper)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _regHelper = regHelper;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.Identity.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);
            var userInfo = _regHelper.GetUserInfo(user.Id);
            var userInventory = _regHelper.GetUserInventory(user.Id);


            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("is_active", user.IsActive.ToString() ?? "false"));
            claims.Add(new Claim("isOnline", user.IsOnline.ToString() ?? "false"));

            if (userInfo != null)
            {
                claims.Add(new Claim("gender", userInfo.Gender.ToString()));
                claims.Add(new Claim("dob", userInfo.DOB.ToString()));
                claims.Add(new Claim("age", userInfo.Age.ToString()));

            }

            if (userInventory != null)
            {
                claims.Add(new Claim("xp", userInventory.XP.ToString()));
                claims.Add(new Claim("wincCount", userInventory.WincCount.ToString()));
                claims.Add(new Claim("receivedWincCount", userInventory.ReceivedWincCount.ToString()));
                claims.Add(new Claim("heartCount", userInventory.HeartCount.ToString()));
                claims.Add(new Claim("receivedHeartCount", userInventory.ReceivedHeartsCount.ToString()));


            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
