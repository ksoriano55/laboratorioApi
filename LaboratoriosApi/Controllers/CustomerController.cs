using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerController(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var Customer = await _CustomerRepository.GetCustomer();
                return Ok(Customer);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertCustomer([FromBody] Customer Customer)
        {
            try
            {
                string jwtEncoded = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                var jwtDecoded = new JwtSecurityTokenHandler().ReadToken(jwtEncoded) as JwtSecurityToken;
                var userId = Convert.ToInt32(jwtDecoded.Claims.FirstOrDefault(j => j.Type.EndsWith("id")).Value);
                var user = _CustomerRepository.InsertCustomer(Customer, userId);
                if (user.id != 0)
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
