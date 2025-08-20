using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrobiologyResultsController : ControllerBase
    {
        private readonly IMicrobiologyResultsRepository _microbiologyResultsRepository;

        public MicrobiologyResultsController(IMicrobiologyResultsRepository microbiologyResultsRepository)
        {
            _microbiologyResultsRepository = microbiologyResultsRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMicrobiologyResults()
        {
            try
            {
               var microbiologyResults = await _microbiologyResultsRepository.GetMicrobiologyResults();
               return Ok(microbiologyResults);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpGet, Authorize]
        [Route("GetAllResultsMicrobiology")]
        public async Task<IActionResult> GetAllResultsMicrobiology(DateTime date, DateTime endDate)
        {
            try
            {
                var microbiologyResults = await _microbiologyResultsRepository.GetAllResultsMicrobiology(date,endDate);
                return Ok(microbiologyResults);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetAnalisysMicrobiology")]
        public async Task<IActionResult> GetMicrobiologyResults(int incomeId)
        {
            try
            {
                var microbiologyResults = await _microbiologyResultsRepository.GetAnalisysMicrobiology(incomeId);
                return Ok(microbiologyResults);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetBlankAnalisysMicrobiology")]
        public async Task<IActionResult> GetBlankMicrobiologyResults(int matrixId)
        {
            try
            {
                var microbiologyResults = await _microbiologyResultsRepository.GetBlankAnalisysMicrobiology(matrixId);
                return Ok(microbiologyResults);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertMicrobiologyResults(List<MicrobiologyResults> microbiologyResults)
        {
            try
            {
                var user = _microbiologyResultsRepository.InsertMicrobiologyResults(microbiologyResults);
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

        [HttpGet, Authorize]
        [Route("samples")]
        public async Task<IActionResult> GetSamples(int year)
        {
            try
            {
                var samples = await _microbiologyResultsRepository.GetSamples(year);
                return Ok(samples.OrderByDescending(x=>x.datecreated));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
