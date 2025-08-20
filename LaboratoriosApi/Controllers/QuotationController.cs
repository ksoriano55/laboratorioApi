using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationRepository _quotationRepository;

        public QuotationController(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetQuotation(int docNum)
        {
            try
            {
               var quotation = await _quotationRepository.GetQuotation(docNum);
               return Ok(quotation);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }
    }
}
