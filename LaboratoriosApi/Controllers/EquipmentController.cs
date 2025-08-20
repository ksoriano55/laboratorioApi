using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Infraestructure.Repositories;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetEquipment()
        {
            try
            {
               var equipment = await _equipmentRepository.GetEquipment();
               return Ok(equipment);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertEquipment([FromBody] Equipment equipment)
        {
            try
            {
                var user = _equipmentRepository.InsertEquipment(equipment);
                if (user.equipmentId != 0)
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
        [Route("GetCVMProgram")]
        public async Task<IActionResult> GetCVMProgram(/*int matrixId*/)
        {
            try
            {
                var program = await _equipmentRepository.GetCVMProgram(/*matrixId*/);
                return Ok(program);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insertCertificate")]
        public async Task<IActionResult> insert([FromForm] EquipmentCertificate data)
        {
            try
            {
                var program = _equipmentRepository.insert(data);
                return Ok(program);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("deleteCertificate")]
        public async Task<IActionResult> delete(EquipmentCertificate data)
        {
            try
            {
                var program = _equipmentRepository.delete(data);
                return Ok(program);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
