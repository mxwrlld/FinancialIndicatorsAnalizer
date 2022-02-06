using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;
using FIAWebApi.BL;
using FIADbContext.Model.DTO;


namespace FIAWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersService userService;
        private readonly SignInManager<UserDbDTO> signInManager;

        public AccountController(UsersService userService, SignInManager<UserDbDTO> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public class LoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password,
                request.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public class ChangePasswordRequest
        {
            public string NewPassword { get; set; }
        }

        [HttpGet]
        public UserProfileApiDto Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var profile = new UserProfileApiDto()
            {
                UserName = identity.Name,
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = identity.FindFirst("FirstName")?.Value,
                MiddleName = identity.FindFirst("MiddleName")?.Value,
                LastName = identity.FindFirst("LastName")?.Value,
            };
            return profile;
        }

        [HttpGet("roles")]
        public string GetRoles()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var roles = identity.FindFirst(ClaimTypes.Role)?.Value;
            return roles;
        }

        [HttpPut]
        public async Task<ActionResult> PutProfile(UserProfileApiDto profile)
        {
            if (profile.UserName != HttpContext.User.Identity.Name)
            {
                return BadRequest("Имя пользователя некорректно.");
            }
            var result = await userService.UpdateProfile(profile);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [HttpPost("password")]
        public async Task<ActionResult> PostPassword([FromForm] string password)
        {
            var result = await userService.ResetPassword(HttpContext.User.Identity.Name, password);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [AllowAnonymous]
        [Route("registry")]
        [HttpPost]
        public async Task<ActionResult> Registry([FromForm] UserProfileCreateApiDto profile)
        {
            profile.Roles = (new List<string>() { "User" }).Select(role => role);
            var result = await userService.Create(profile);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        private ActionResult Exception2StatusCode(Exception ex)
        {
            if (ex is ArgumentException || ex is SaveChangesException)
            {
                return BadRequest(ex.Message);
            }
            if (ex is KeyNotFoundException)
            {
                return NotFound(ex.Message);
            }
            if (ex is AlreadyExistsException)
            {
                return Conflict(ex.Message);
            }
            return StatusCode(500, ex.Message);
        }
    }
}

