using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Infraestructure.Repositories;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesOutsourcedDetailController : ControllerBase
    {
        private readonly ISamplesOutsourcedDetailRepository _samplesOutsourcedDetailRepository;

        public SamplesOutsourcedDetailController(ISamplesOutsourcedDetailRepository samplesOutsourcedDetailRepository)
        {
            _samplesOutsourcedDetailRepository = samplesOutsourcedDetailRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetSamplesOutsourcedDetail(int outsourcedId)
        {
            try
            {
               var samplesOutsourcedDetail = await _samplesOutsourcedDetailRepository.GetSamplesOutsourcedDetail(outsourcedId);
               return Ok(samplesOutsourcedDetail);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertSamplesOutsourcedDetail([FromBody] SamplesOutsourcedDetail samplesOutsourcedDetail)
        {
            try
            {
                var user = _samplesOutsourcedDetailRepository.InsertSamplesOutsourcedDetail(samplesOutsourcedDetail);
                if (user.outsourcedDetailId != 0)
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
        [Route("DeleteSamplesOutsourcedDetail")]
        public async Task<IActionResult> DeleteSamplesOutsourcedDetail([FromBody] SamplesOutsourcedDetail detalle)
        {
            try
            {
                var delete = _samplesOutsourcedDetailRepository.DeleteSamplesOutsourcedDetail(detalle);
                if (delete.IsCompleted)
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
