using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerRepository _ContainerRepository;

        public ContainerController(IContainerRepository ContainerRepository)
        {
            _ContainerRepository = ContainerRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetContainer()
        {
            try
            {
               var Container = await _ContainerRepository.GetContainer();
               return Ok(Container);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertContainer([FromBody] Container Container)
        {
            try
            {
                var user = await _ContainerRepository.InsertContainer(Container);
                if (user.containerId != 0)
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
