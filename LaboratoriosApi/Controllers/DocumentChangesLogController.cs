using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentChangesLogController : ControllerBase
    {
        private readonly IDocumentChangesLogRepository _documentChangesLogRepository;

        public DocumentChangesLogController(IDocumentChangesLogRepository documentChangesLogRepository)
        {
            _documentChangesLogRepository = documentChangesLogRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetDocumentChangesLog()
        {
            try
            {
               var documentChangesLog = await _documentChangesLogRepository.GetDocumentChangesLog();
               return Ok(documentChangesLog);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

    }
}
