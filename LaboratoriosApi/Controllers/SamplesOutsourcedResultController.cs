using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesOutsourcedResultController : ControllerBase
    {
        private readonly ISamplesOutsourcedResultRepository _SamplesOutsourcedResultRepository;

        public SamplesOutsourcedResultController(ISamplesOutsourcedResultRepository SamplesOutsourcedResultRepository)
        {
            _SamplesOutsourcedResultRepository = SamplesOutsourcedResultRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetSamplesOutsourcedResults(int outsourcedId)
        {
            try
            {
               var SamplesOutsourcedResult = await _SamplesOutsourcedResultRepository.GetSamplesOutsourcedResults(outsourcedId);
               return Ok(SamplesOutsourcedResult);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpGet("{outsourcedId}"), Authorize]
        public async Task<IActionResult> GetSamplesOutsourcedResultViews(int outsourcedId)
        {
            try
            {
                var SamplesOutsourcedResult = await _SamplesOutsourcedResultRepository.GetSamplesOutsourcedResultViews(outsourcedId);
                return Ok(SamplesOutsourcedResult);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> InsertSamplesOutsourcedResult(List<SamplesOutsourcedResult> SamplesOutsourcedResult)
        {
            try
            {
                var user = _SamplesOutsourcedResultRepository.InsertSamplesOutsourcedResult(SamplesOutsourcedResult);
                if (user.IsCompleted)
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
