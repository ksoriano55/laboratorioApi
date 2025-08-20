using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesOutsourcedController : ControllerBase
    {
        private readonly ISamplesOutsourcedRepository _samplesOutsourcedRepository;

        public SamplesOutsourcedController(ISamplesOutsourcedRepository samplesOutsourcedRepository)
        {
            _samplesOutsourcedRepository = samplesOutsourcedRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetSamplesOutsourced(int year)
        {
            try
            {
               var samplesOutsourced = await _samplesOutsourcedRepository.GetSamplesOutsourced(year);
               return Ok(samplesOutsourced);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertSamplesOutsourced([FromBody] SamplesOutsourced samplesOutsourced)
        {
            try
            {
                var user = _samplesOutsourcedRepository.InsertSamplesOutsourced(samplesOutsourced);
                if (user.outsourcedId != 0)
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
