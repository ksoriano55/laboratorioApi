using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetArea()
        {
            try
            {
               var area = await _areaRepository.GetArea();
               return Ok(area);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpGet, Authorize]
        [Route("AreaAssigned")]

        public async Task<IActionResult> GetAreaAssigned()
        {
            try
            {
                var area = await _areaRepository.GetAreaAssigned();
                return Ok(area);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertArea([FromBody] Area area)
        {
            try
            {
                var user = _areaRepository.InsertArea(area);
                if (user.areaId != 0)
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
        public async Task<IActionResult> EditArea([FromBody] Area area)
        {
            try
            {
                var user = _areaRepository.EditArea(area);
                if (user.areaId != 0)
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
        [Route("AreaByMatrix")]
        public async Task<IActionResult> GetAreaByMatrix(int matrixId)
        {
            var area = await _areaRepository.GetAreaByMatrix(matrixId);
            return Ok(area);
        }

        [HttpGet, Authorize]
        [Route("AreaByNotMatrix")]
        public async Task<IActionResult> GetAreaByNotMatrix(int matrixId)
        {
            var area = await _areaRepository.GetAreaByNotMatrix(matrixId);
            return Ok(area);
        }

        [HttpPost,Authorize]
        [Route("AssingArea")]
        public async Task<IActionResult> AssingArea(int matrixId, int areaId, bool action)
        {
            var area = await _areaRepository.AssingArea(matrixId, areaId, action);
            return Ok(area);
        }
    }
}
