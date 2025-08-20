using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var customers = await _userRepository.GetUsers();
            return Ok(customers);
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertUser([FromBody] User Usuario)
        {
            try
            {
                var user = await _userRepository.InsertUser(Usuario);
                if (user.UserId != 0)
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
        [Route("update")]
        public async Task<IActionResult> EditUser([FromBody] User Usuario)
        {
            try
            {
                var user = await _userRepository.EditUser(Usuario);
                if (user.UserId != 0)
                {
                    return Ok("Ok");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }

    }
}
