
namespace Laboratorios.Core.ViewModels
{
    public class LoginViewModel
    {
    }

    public class Credenciales
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class Response
    {
        public string Token { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public AccesoUsuarioViewModel AccesosByUsuario { get; set; }
    }
}
