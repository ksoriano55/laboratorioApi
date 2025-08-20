using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Laboratorios.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableNMPDetailController : ControllerBase
    {
        private readonly ITableNMPDetailRepository _tableNMPDetailRepository;

        public TableNMPDetailController(ITableNMPDetailRepository tableNMPDetailRepository)
        {
            _tableNMPDetailRepository = tableNMPDetailRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                var tableNMPDetail = await _tableNMPDetailRepository.GetTables();
                return Ok(tableNMPDetail);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> GetTables(TableNMP table)
        {
            try
            {
               var tableNMPDetail = await _tableNMPDetailRepository.GetTables(table);
               return Ok(tableNMPDetail);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertTable([FromBody] TableNMPDetail tableNMPDetail)
        {
            try
            {
                var user = _tableNMPDetailRepository.InsertTableDetail(tableNMPDetail);
                if (user.tableDetailId != 0)
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
        public async Task<IActionResult> EditTable([FromBody] TableNMPDetail tableNMPDetail)
        {
            try
            {
                var user = _tableNMPDetailRepository.EditTableDetail(tableNMPDetail);
                if (user.tableDetailId != 0)
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
        [Route("delete")]
        public async Task<IActionResult> DeleteIncomeDetail([FromBody] TableNMPDetail tableNMPDetail)
        {
            try
            {
                var user = _tableNMPDetailRepository.DeleteTableDetail(tableNMPDetail);
                if (user.IsCompleted)
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
