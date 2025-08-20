using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class AccesoUsuarioViewModel
    {
        public AccesoUsuarioViewModel()
        {
            this.RolesUsuario = new List<RolesByUsuarioViewModel>();
        }
        public int usuarioId { get; set; }
        public string usuario { get; set; } = string.Empty;
        public List<RolesByUsuarioViewModel> RolesUsuario { get; set; }
    }
}
