using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Laboratorios.Core.ViewModels;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhisycalChemistryResultsController : ControllerBase
    {
        private readonly IPhisycalChemistryResultsRepository _phisycalChemistryResultsRepository;

        public PhisycalChemistryResultsController(IPhisycalChemistryResultsRepository phisycalChemistryResultsRepository)
        {
            _phisycalChemistryResultsRepository = phisycalChemistryResultsRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetPhisycalChemistryResults(DateTime date, DateTime endDate, int analisysId, int areaId)
        {
            try
            {
                var results = await _phisycalChemistryResultsRepository.GetAnalisysPhisycalChemistry(date, endDate, analisysId, areaId);
                return Ok(results);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }


        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertPhisycalChemistryResults(List<PhisycalChemistryResultsViewModel> results)
        {
            try
            {
                var response = _phisycalChemistryResultsRepository.InsertPhisycalChemistryResults(results);
                if (response.IsCompleted)
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
        [Route("GetConfigurationResults")]
        public async Task<IActionResult> GetConfigurationResults()
        {
            try
            {
                var results = await _phisycalChemistryResultsRepository.GetConfigurationResults();
                return Ok(results);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetQualityAssurance")]
        public async Task<IActionResult> GetQualityAssurance(int analisysId, int year, int opt)
        {
            try
            {
                var results = await _phisycalChemistryResultsRepository.GetQualityAssurance(analisysId,year,opt);
                return Ok(results);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
