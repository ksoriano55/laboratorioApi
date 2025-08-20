using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypeController(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetDocumentType()
        {
            try
            {
               var documentType = await _documentTypeRepository.GetDocumentType();
               return Ok(documentType);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertDocumentType([FromBody] DocumentType documentType)
        {
            try
            {
                var user = _documentTypeRepository.InsertDocumentType(documentType);
                if (user.documentTypeId != 0)
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
        [Route("saveDocument")]
        public async Task<IActionResult> saveDocument([FromForm] IFormFile? document, [FromForm] int documentTypeId)
        {
            try
            {
                await _documentTypeRepository.saveDocument(document, documentTypeId);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
