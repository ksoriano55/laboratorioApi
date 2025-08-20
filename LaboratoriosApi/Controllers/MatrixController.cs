using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        private readonly IMatrixRepository _matrixRepository;

        public MatrixController(IMatrixRepository matrixRepository)
        {
            _matrixRepository = matrixRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMatrix()
        {
            try
            {
               var matrix = await _matrixRepository.GetMatrix();
               return Ok(matrix);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertMatrix([FromBody] Matrix matrix)
        {
            try
            {
                var user = _matrixRepository.InsertMatrix(matrix);
                if (user.matrixId != 0)
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
        public async Task<IActionResult> EditMatrix([FromBody] Matrix matrix)
        {
            try
            {
                var user = _matrixRepository.EditMatrix(matrix);
                if (user.matrixId != 0)
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
