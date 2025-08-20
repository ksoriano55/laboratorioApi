using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationRecipesController : ControllerBase
    {
        private readonly IConfigurationRecipesRepository _configurationRepository;

        public ConfigurationRecipesController(IConfigurationRecipesRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetConfigurationRecipes()
        {
            try
            {
               var configuration = await _configurationRepository.GetConfigurationRecipes();
               return Ok(configuration);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertConfigurationRecipes([FromBody] ConfigurationRecipes configuration)
        {
            try
            {
                var user = _configurationRepository.InsertConfigurationRecipes(configuration);
                if (user.configurationId != 0)
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
