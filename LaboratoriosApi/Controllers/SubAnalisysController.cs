using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAnalisysController : ControllerBase
    {
        private readonly ISubAnalisysRepository _SubAnalisysRepository;

        public SubAnalisysController(ISubAnalisysRepository SubAnalisysRepository)
        {
            _SubAnalisysRepository = SubAnalisysRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetSubAnalisys()
        {
            try
            {
               var SubAnalisys = await _SubAnalisysRepository.GetSubAnalisys();
               return Ok(SubAnalisys);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> insert([FromBody] SubAnalisys SubAnalisys)
        {
            try
            {
                var user = _SubAnalisysRepository.insert(SubAnalisys);
                if (user.subanalisysId != 0)
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
