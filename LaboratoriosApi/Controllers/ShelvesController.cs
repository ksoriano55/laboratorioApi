using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly IShelvesRepository _ShelvesRepository;

        public ShelvesController(IShelvesRepository ShelvesRepository)
        {
            _ShelvesRepository = ShelvesRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetShelves()
        {
            try
            {
                var Shelves = await _ShelvesRepository.GetShelves();
                return Ok(Shelves);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertShelves([FromBody] Shelves Shelves)
        {
            try
            {
                var user = _ShelvesRepository.InsertShelves(Shelves);
                if (user.id != 0)
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
