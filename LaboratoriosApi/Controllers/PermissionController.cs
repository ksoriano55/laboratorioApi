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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> getPermission()
        {
            var permission = await _permissionRepository.GetPermissions();
            return Ok(permission);
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public async Task<IActionResult> InsertPermission([FromBody] Permission permission)
        {
            try
            {
                var result = await _permissionRepository.InsertPermission(permission);
                if (result.PermissionId == 0)
                {
                    return BadRequest("Error al registrar nuevo permiso");
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
        public async Task<IActionResult> EditPermission([FromBody] Permission permission)
        {
            try
            {
                var result = await _permissionRepository.EditPermission(permission);
                if (result.PermissionId == 0)
                {
                    return BadRequest("Error al modificar el permiso.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Ok");
        }


        [HttpGet, Authorize]
        [Route("PermissionByRol")]
        public async Task<IActionResult> GetPermissionByRol(int rolId)
        {
            var permission = await _permissionRepository.GetPermissionByRol(rolId);
            return Ok(permission);
        }

        [HttpGet, Authorize]
        [Route("PermissionByNoRol")]
        public async Task<IActionResult> GetPermissionByNoRol(int rolId)
        {
            var permissions = await _permissionRepository.GetPermissionByNoRol(rolId);
            return Ok(permissions);
        }

        [HttpPost, Authorize]
        [Route("AssingPermission")]
        public async Task<IActionResult> AssingPermission(List<PermissionRol> permisos)
        {
            var permission = await _permissionRepository.AssingPermission(permisos);
            return Ok(permission);
        }

        [HttpPost, Authorize]
        [Route("delete")]
        public async Task<IActionResult> Delete(int permissionRolId)
        {
            try
            {
                var roles = _permissionRepository.DeletePermissionRol(permissionRolId);
                if (roles.IsCompleted)
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
