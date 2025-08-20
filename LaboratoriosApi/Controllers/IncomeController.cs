using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Core.ViewModels;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeController(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetIncome(int year)
        {
            try
            {
                var income = await _incomeRepository.GetIncome(year);
                return Ok(income);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet("{incomeId}"),Authorize]
        public async Task<IActionResult> GetIncomeById(int incomeId)
        {
            try
            {
                var income = await _incomeRepository.GetIncomeById(incomeId);
                return Ok(income);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet,Authorize]
        [Route("Details")]
        public async Task<IActionResult> GetDetailsIncome(int year)
        {
            try
            {
                var income = await _incomeRepository.GetDetailsIncome(year);
                return Ok(income);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }    
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertIncome([FromBody] Income income)
        {
            try
            {
                var user = _incomeRepository.InsertIncome(income);
                if (user.incomeId != 0)
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
        public async Task<IActionResult> EditIncome([FromBody] Income income, bool? draft)
        {
            try
            {
                var user = _incomeRepository.EditIncome(income, draft);
                if (user.incomeId != 0)
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
        [Route("DeleteIncomeDetail")]
        public async Task<IActionResult> DeleteIncomeDetail([FromBody] IncomeDetail incomeDetail)
        {
            try
            {
                var user = _incomeRepository.DeleteIncomeDetail(incomeDetail);
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

        [HttpPost, Authorize]
        [Route("UpdateSample")]
        public async Task<IActionResult> UpdateSample([FromBody] Sample sample)
        {
            try
            {
                var user = _incomeRepository.UpdateSample(sample);
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

        [HttpPost, Authorize]
        [Route("UpdateDateIncome")]
        public async Task<IActionResult> UpdateDateIncome([FromBody] IncomeView income)
        {
            try
            {
                var user = _incomeRepository.EditIncomeDate(income);
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
