using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeControlSupplyController : ControllerBase
    {
        private readonly IIncomeControlSupplyRepository _incomeControlSupplyRepository;

        public IncomeControlSupplyController(IIncomeControlSupplyRepository incomeControlSupplyRepository)
        {
            _incomeControlSupplyRepository = incomeControlSupplyRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetIncomeControlSupply()
        {
            try
            {
               var incomeControlSupply = await _incomeControlSupplyRepository.GetIncomeControlSupply();
               return Ok(incomeControlSupply);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertIncomeControlSupply([FromBody] IncomeControlSupply incomeControlSupply)
        {
            try
            {
                var user = _incomeControlSupplyRepository.InsertIncomeControlSupply(incomeControlSupply);
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
