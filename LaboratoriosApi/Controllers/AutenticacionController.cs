using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionRepository _AuthRepository;

        public AutenticacionController(IAutenticacionRepository bancosRepository)
        {
            _AuthRepository = bancosRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] Credenciales credenciales)
        {
            try
            {
                var result = await _AuthRepository.Login(credenciales.Username, credenciales.Password);
                if(result.StatusCode == 200)
                {
                    return Ok(result);
                }
                if(result.StatusCode == 403)
                {
                    return Unauthorized("No tiene acceso al sistema.");
                }
                return Unauthorized("Usuario o Contraseña incorrectos.");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
