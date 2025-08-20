using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        public UnitOfMeasurementController(IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUnitOfMeasurement()
        {
            try
            {
               var unitOfMeasurement = await _unitOfMeasurementRepository.GetUnitOfMeasurement();
               return Ok(unitOfMeasurement);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertUnitOfMeasurement([FromBody] UnitOfMeasurement unitOfMeasurement)
        {
            try
            {
                var user = _unitOfMeasurementRepository.InsertUnitOfMeasurement(unitOfMeasurement);
                if (user.unitOfMeasurementId != 0)
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
        public async Task<IActionResult> EditUnitOfMeasurement([FromBody] UnitOfMeasurement unitOfMeasurement)
        {
            try
            {
                var user = _unitOfMeasurementRepository.EditUnitOfMeasurement(unitOfMeasurement);
                if (user.unitOfMeasurementId != 0)
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
