using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIAWebApi.BL;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;

namespace FIAWebApi.Controllers
{
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
            return await service.GetAllAsync(search?.ToLower() ,year, quarter, sortBy?.ToLower(), sortDesc);
        }

        [HttpGet("{tin}")]
        public async Task<ActionResult<EnterpriseApiDTO>> Get(string tin)
        {
            (var enterprise, Exception ex) = await service.GetAsync(tin);
            
            if(ex != null) 
            {
                if(ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if(ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                StatusCode(500);
            }
            return Ok(enterprise);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EnterpriseApiDTO enterprise)
        {
            var ex = await service.CreateAsync(enterprise);
            if(ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if(ex is AlreadyExistsException)
                {
                    return Conflict(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut("{tin}")]
        public async Task<ActionResult> Put(string tin, [FromBody] EnterpriseApiDTO enterprise)
        {
            var ex = await service.UpdateAsync(tin, enterprise);
            if(ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok();
        } 

        [HttpDelete("{tin}")]
        public async Task<ActionResult<EnterpriseApiDTO>> Delete(string tin)
        {
            (var enterprise, Exception ex) = await service.DeleteAsync(tin);
            if (ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok(enterprise);
        }
    }
}
