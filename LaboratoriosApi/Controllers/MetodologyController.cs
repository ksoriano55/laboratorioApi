using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodologyController : ControllerBase
    {
        private readonly IMetodologyRepository _metodologyRepository;

        public MetodologyController(IMetodologyRepository metodologyRepository)
        {
            _metodologyRepository = metodologyRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMetodology()
        {
            try
            {
               var metodology = await _metodologyRepository.GetMetodology();
               return Ok(metodology);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertMetodology([FromBody] Metodology metodology)
        {
            try
            {
                var user = _metodologyRepository.InsertMetodology(metodology);
                if (user.metodologyId != 0)
                {
                    return Ok("Ok");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }

        [HttpPost, Authorize]
        [Route("edit")]
        public async Task<IActionResult> EditMetodology([FromBody] Metodology metodology)
        {
            try
            {
                var user = _metodologyRepository.EditMetodology(metodology);
                if (user.metodologyId != 0)
                {
                    return Ok("Ok");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }
    }
}
