using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorAssessmentController : ControllerBase
    {
        private readonly IVendorAssessmentRepository _assessmentRepository;

        public VendorAssessmentController(IVendorAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllVendorAssessment()
        {
            try
            {
                var assessment = await _assessmentRepository.GetAllVendorAssessment();
                return Ok(assessment);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetVendorAssessmentById")]
        public async Task<IActionResult> GetVendorAssessment(int id)
        {
            try
            {
               var assessment = await _assessmentRepository.GetVendorAssessment(id);
               return Ok(assessment);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertVendorAssessment([FromBody] VendorAssessment assessment)
        {
            try
            {
                var user = _assessmentRepository.InsertVendorAssessment(assessment);
                if (user.assessmentId != 0)
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
