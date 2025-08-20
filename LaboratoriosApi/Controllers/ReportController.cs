using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository ReportRepository)
        {
            _reportRepository = ReportRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetReportInformation(int incomeId)
        {
            try
            {
               var Report = await _reportRepository.GetReportInformation(incomeId);
               return Ok(Report);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet("{incomeId}"), Authorize]
        public async Task<IActionResult> GetReportInformationOutsourced(int incomeId)
        {
            try
            {
                var Report = await _reportRepository.GetReportInformationOutsourced(incomeId);
                return Ok(Report);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
