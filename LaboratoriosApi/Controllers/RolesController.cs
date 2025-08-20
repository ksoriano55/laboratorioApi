using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Repositories;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> getRoles()
        {
            var roles = await _rolesRepository.GetRoles();
            return Ok(roles);
        }
        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertRoles([FromBody] Roles rol)
        {
            try
            {
                var roles = await _rolesRepository.InsertRoles(rol);
                if (roles.RolId == 0)
                {
                    return BadRequest("Error al registrar nuevo rol");
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
        public async Task<IActionResult> EditRol([FromBody] Roles rol)
        {
            try
            {
                var roles = await _rolesRepository.EditRol(rol);
                if (roles.RolId == 0)
                {
                    return BadRequest("Error al modificar el rol");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }

        [HttpGet, Authorize]
        [Route("RolesByUser")]
        public async Task<IActionResult> GetRolesByUser(int userId)
        {
            var roles = await _rolesRepository.GetRolesByUser(userId);
            return Ok(roles);
        }


        [HttpGet, Authorize]
        [Route("RolesByNotUser")]
        public async Task<IActionResult> GetRolesByNotUser(int userId)
        {
            var roles = await _rolesRepository.GetRolesByNotUser(userId);
            return Ok(roles);
        }

        [HttpPost, Authorize]
        [Route("AssingRoles")]
        public async Task<IActionResult> AssingRol(List<User_Roles> roles)
        {
            var roless = await _rolesRepository.AssingRoles(roles);
            return Ok(roless);
        }

        [HttpPost, Authorize]
        [Route("delete")]
        public async Task<IActionResult> Delete(int userRolId)
        {
            try
            {
                var roles = _rolesRepository.DeleteUserRol(userRolId);
                if (roles.IsCompleted)
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
