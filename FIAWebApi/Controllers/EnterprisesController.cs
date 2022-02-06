using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIAWebApi.BL;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FIAWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        EnterprisesService service;

        public EnterprisesController(EnterprisesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<EnterpriseApiDTO>> Get(string search, int? year, int? quarter, string sortBy, bool sortDesc)
        {
            return await service.GetAllAsync(search?.ToLower(), year, quarter, sortBy?.ToLower(), sortDesc);
        }

        [HttpGet("{tin}")]
        public async Task<ActionResult<EnterpriseApiDTO>> Get(string tin)
        {
            (var enterprise, Exception ex) = await service.GetAsync(tin);
            return ex != null ? Exception2StatusCode(ex) : Ok(enterprise);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EnterpriseApiDTO enterprise)
        {
            var ex = await service.CreateAsync(enterprise);
            return ex != null ? Exception2StatusCode(ex) : Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{tin}")]
        public async Task<ActionResult> Put(string tin, [FromBody] EnterpriseApiDTO enterprise)
        {
            var ex = await service.UpdateAsync(tin, enterprise);
            return ex != null ? Exception2StatusCode(ex) : Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{tin}")]
        public async Task<ActionResult<EnterpriseApiDTO>> Delete(string tin)
        {
            (var enterprise, Exception ex) = await service.DeleteAsync(tin);
            Exception2StatusCode(ex);
            return ex != null ? Exception2StatusCode(ex) : Ok(enterprise);
        }


        [Authorize(Roles = "Manager")]
        [HttpPost("{tin}")]
        public async Task<ActionResult> Post(string tin, [FromBody] FinancialResultApiDTO finRes)
        {
            var userName = HttpContext.User.Identity.Name;
            var ex = await service.AddFinancialResultAsync(userName, tin, finRes);
            return ex != null ? Exception2StatusCode(ex) : Ok();
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
