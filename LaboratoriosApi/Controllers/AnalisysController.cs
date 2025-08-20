using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Laboratorios.Infraestructure.Repositories;
using System.Xml.Linq;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalisysController : ControllerBase
    {
        private readonly IAnalisysRepository _analisysRepository;

        public AnalisysController(IAnalisysRepository analisysRepository)
        {
            _analisysRepository = analisysRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAnalisys()
        {
            try
            {
               var analisys = await _analisysRepository.GetAnalisys();
               return Ok(analisys);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("AnalisysAssigned")]

        public async Task<IActionResult> GetAnalisysAssigned()
        {
            try
            {
                var analisys = await _analisysRepository.GetAnalisysAssigned();
                return Ok(analisys);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertAnalisys([FromBody] Analisys analisys)
        {
            try
            {
                var user = _analisysRepository.InsertAnalisys(analisys);
                if (user.analisysId != 0)
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
        public async Task<IActionResult> EditAnalisys([FromBody] Analisys analisys)
        {
            try
            {
                var user = _analisysRepository.EditAnalisys(analisys);
                if (user.analisysId != 0)
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
        [Route("AnalisysByArea")]
        public async Task<IActionResult> GetAnalisysByArea(int areaId)
        {
            var analisys = await _analisysRepository.GetAnalisysByArea(areaId);
            return Ok(analisys);
        }

        [HttpGet, Authorize]
        [Route("AnalisysByNotArea")]
        public async Task<IActionResult> GetAnalisysByNotArea(int areaId)
        {
            var analisys = await _analisysRepository.GetAnalisysByNotArea(areaId);
            return Ok(analisys);
        }

        [HttpPost, Authorize]
        [Route("AssingAnalisys")]
        public async Task<IActionResult> AssingAnalisys(int areaId, int analisysId, bool action)
        {
            var roles = await _analisysRepository.AssingAnalisys(areaId, analisysId, action);
            return Ok(roles);
        }

        [HttpPost, Authorize]
        [Route("AssingControls")]
        public async Task<IActionResult> AssingControls([FromBody]AnalisysControls data)
        {
            try
            {
                var controls = await _analisysRepository.AssingControls(data);
                if (data.analisysControlId != 0)
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
        [Route("AnalisysControls")]
        public async Task<IActionResult> GetAnalisysControls(int analisysId)
        {
            var analisys = await _analisysRepository.GetAnalisysControls(analisysId);
            return Ok(analisys);
        }

    }
}
