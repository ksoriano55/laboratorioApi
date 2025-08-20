using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class RolesByUsuarioViewModel
    {
        public RolesByUsuarioViewModel()
        {
            this.PermisosRoles = new List<PermisosByRolesViewModel>();
        }
        public int RolId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<PermisosByRolesViewModel> PermisosRoles { get; set; }
    }
}
