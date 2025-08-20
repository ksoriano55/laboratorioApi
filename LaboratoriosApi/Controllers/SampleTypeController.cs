using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleTypeController : ControllerBase
    {
        private readonly ISampleTypeRepository _sampleTypeRepository;

        public SampleTypeController(ISampleTypeRepository sampleTypeRepository)
        {
            _sampleTypeRepository = sampleTypeRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetSampleType()
        {
            try
            {
               var sampleType = await _sampleTypeRepository.GetSampleType();
               return Ok(sampleType);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertSampleType([FromBody] SampleType sampleType)
        {
            try
            {
                var user = _sampleTypeRepository.InsertSampleType(sampleType);
                if (user.sampleTypeId != 0)
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
        public async Task<IActionResult> EditSampleType([FromBody] SampleType sampleType)
        {
            try
            {
                var user = _sampleTypeRepository.EditSampleType(sampleType);
                if (user.sampleTypeId != 0)
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
