using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableNMPController : ControllerBase
    {
        private readonly ITableNMPRepository _tableNMPRepository;

        public TableNMPController(ITableNMPRepository tableNMPRepository)
        {
            _tableNMPRepository = tableNMPRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetTables()
        {
            try
            {
               var tableNMP = await _tableNMPRepository.GetTables();
               return Ok(tableNMP);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertTable([FromBody] TableNMP tableNMP)
        {
            try
            {
                var user = _tableNMPRepository.InsertTable(tableNMP);
                if (user.tableId != 0)
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
        public async Task<IActionResult> EditTable([FromBody] TableNMP tableNMP)
        {
            try
            {
                var user = _tableNMPRepository.EditTable(tableNMP);
                if (user.tableId != 0)
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
