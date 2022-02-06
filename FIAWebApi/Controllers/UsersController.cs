using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;
using FIAWebApi.BL;
using Microsoft.AspNetCore.Authorization;

namespace FIAWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService userService;

        public UsersController(UsersService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserProfileApiDto>> Get()
        {
            return await userService.GetProfiles();
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserProfileApiDto>> Get(string userName)
        {
            var profile = await userService.GetProfile(userName);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserProfileCreateApiDto profile)
        {
            var result = await userService.Create(profile);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserProfileCreateApiDto profile)
        {
            var result = await userService.UpdateProfile(profile);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            var result = await userService.Delete(userName);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [HttpPost("{username}/role/{role}")]
        public async Task<ActionResult> PostRole(string userName, string role)
        {
            var result = await userService.AssignRole(userName, role);
            return result != null ? Exception2StatusCode(result) : Ok();
        }

        [HttpDelete("{username}/role/{role}")]
        public async Task<ActionResult> DeleteRole(string userName, string role)
        {
            var result = await userService.RemoveFromRole(userName, role);
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
