using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeControlSupplyDetailController : ControllerBase
    {
        private readonly IIncomeControlSupplyDetailRepository _incomeControlSupplyRepository;

        public IncomeControlSupplyDetailController(IIncomeControlSupplyDetailRepository incomeControlSupplyRepository)
        {
            _incomeControlSupplyRepository = incomeControlSupplyRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetIncomeControlSupplyDetail(int incomeControlId)
        {
            try
            {
               var incomeControlSupply = await _incomeControlSupplyRepository.GetIncomeControlSupplyDetail(incomeControlId);
               return Ok(incomeControlSupply);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertIncomeControlSupplyDetail([FromBody] IncomeControlSupplyDetail incomeControlSupply)
        {
            try
            {
                var user = _incomeControlSupplyRepository.InsertIncomeControlSupplyDetail(incomeControlSupply);
                if (user.incomeControlId != 0)
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
