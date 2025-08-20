using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentCalibrationController : ControllerBase
    {
        private readonly IEquipmentCalibrationRepository _equipmentCalibrationRepository;

        public EquipmentCalibrationController(IEquipmentCalibrationRepository equipmentCalibrationRepository)
        {
            _equipmentCalibrationRepository = equipmentCalibrationRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetEquipmentCalibration()
        {
            try
            {
               var equipmentCalibration = await _equipmentCalibrationRepository.GetEquipmentCalibrations();
               return Ok(equipmentCalibration);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertEquipmentCalibration([FromBody] EquipmentCalibration equipmentCalibration)
        {
            try
            {
                var user = _equipmentCalibrationRepository.InsertEquipmentCalibration(equipmentCalibration);
                if (user.calibrationId != 0)
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
