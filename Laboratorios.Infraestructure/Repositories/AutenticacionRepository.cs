using System.Text;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.AccountManagement;

namespace Laboratorios.Infraestructure.Repositories
{
    public class AutenticacionRepository : IAutenticacionRepository
    {
        private readonly LaboratoriosContext _context;
        private IConfiguration _config;
        public AutenticacionRepository(LaboratoriosContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }
        public async Task<Response> Login(string username, string password)
        {
            var usuario = await _context.User.FirstOrDefaultAsync(x=> x.UserName == username && x.Status == true);
            if(usuario == null)
            {
                return new Response { Message = "Usuario no encontrado o inactivo", StatusCode = 403 };
            }
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "GRUPOC"))
            {
                bool isValid = context.ValidateCredentials(username, password);
                if (isValid)
                {
                    var token = GenerateToken(usuario);
                    var accesos =await AccessByUsers(usuario.UserId);
                    return new Response {Message = "Ok", Token = token, StatusCode = 200, AccesosByUsuario = accesos };
                }
                // Usuario no válido
                return new Response { Message = "Credenciales Incorrectas", StatusCode = 401 };
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.UserId.ToString()),
                new Claim("usuario", user.UserName),
                new Claim("nombre", user.UserName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(9),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<AccesoUsuarioViewModel> AccessByUsers(int usuarioId)
        {
            List<AccesoUsuarioViewModel> RolesUsers = await _context.User.Where(x => x.UserId == usuarioId).Select(x => new AccesoUsuarioViewModel
            {
                usuarioId = x.UserId,
                usuario = x.UserName,
                RolesUsuario = _context.User_Roles.Where(r => r.UserId == x.UserId).Select(w => new RolesByUsuarioViewModel
                {
                    RolId = w.RolId,
                    Nombre = w.Rol.Description,
                    PermisosRoles = _context.PermissionRol.Where(p => p.RolId == w.RolId).Select(p => new PermisosByRolesViewModel
                    {
                        Nombre = p.Permission.Name,
                        Ruta = p.Permission.Route,
                        Menu = p.Permission.Menu
                    }).ToList()
                }).ToList()
            }).ToListAsync();

            return RolesUsers.FirstOrDefault();
        }
    }
}
