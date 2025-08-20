using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorsRepository _vendorsRepository;

        public VendorsController(IVendorsRepository vendorsRepository)
        {
            _vendorsRepository = vendorsRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetVendors(int? date, int? assessmentType)
        {
            try
            {
               var vendors = await _vendorsRepository.GetVendors(date, assessmentType);
               return Ok(vendors);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertVendors([FromBody] Vendors vendors)
        {
            try
            {
                var user = _vendorsRepository.InsertVendors(vendors);
                if (user.vendorId != 0)
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
