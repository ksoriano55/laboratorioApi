using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Laboratorios.Infraestructure.Repositories;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityControlController : ControllerBase
    {

        private readonly IQualityControlRepository _qualityControlRepository;

        public QualityControlController(IQualityControlRepository qualityControlRepository)
        {
            _qualityControlRepository = qualityControlRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetQualityControls()
        {
            try
            {
                var controls = await _qualityControlRepository.GetQualityControls();
                return Ok(controls);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertQualityControl([FromBody] QualityControl qualityControl)
        {
            try
            {
                var user = _qualityControlRepository.InsertQualityControl(qualityControl);
                if (user.qualityControlId != 0)
                {
                    return Ok("Ok");
                }
                return BadRequest("Error al guardar");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }

    }
}
