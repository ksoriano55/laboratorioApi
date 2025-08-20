using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission> InsertPermission(Permission permission);
        Task<Permission> EditPermission(Permission permission);
        Task<IEnumerable<PermissionRol>> GetPermissionByRol(int rolId);
        Task<IEnumerable<PermissionRol>> GetPermissionByNoRol(int rolId);
        Task<List<PermissionRol>> AssingPermission(List<PermissionRol> permisos);
        Task DeletePermissionRol(int permissionRolId);
    }
}
