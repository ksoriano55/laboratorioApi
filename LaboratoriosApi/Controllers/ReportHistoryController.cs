using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportHistoryController : Controller
    {
        private readonly IReportHistoryRepository _reportRepository;

        public ReportHistoryController(IReportHistoryRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet, Authorize]
        public async Task<IEnumerable<_ReportHistory>> getHistoryRepository(int year)
        {
            return await _reportRepository.getHistoryRepository(year);
        }
        [HttpPatch, Authorize]
        public async Task<ReportHistory> update(ReportHistory reportHistory)
        {
            return await _reportRepository.update(reportHistory);
        }
    }
}
