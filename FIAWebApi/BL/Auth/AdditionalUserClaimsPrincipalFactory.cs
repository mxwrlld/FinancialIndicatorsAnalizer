﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using FIADbContext.Model.DTO;

namespace FIAWebApi.BL.Auth
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserDbDTO, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(UserManager<UserDbDTO> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(UserDbDTO user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.FirstName ?? ""));
            claims.Add(new Claim("FirstName", user.FirstName ?? ""));
            claims.Add(new Claim("MiddleName", user.MiddleName ?? ""));
            claims.Add(new Claim("LastName", user.LastName ?? ""));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(",", await UserManager.GetRolesAsync(user))));

            identity.AddClaims(claims);
            return principal;
        }
    }
}
