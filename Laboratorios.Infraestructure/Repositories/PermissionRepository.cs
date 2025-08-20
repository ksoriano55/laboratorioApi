using Microsoft.EntityFrameworkCore;
using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Infraestructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly LaboratoriosContext _context;

        public PermissionRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            var permissions = new List<Permission>();
            try {
               permissions = await _context.Permission.ToListAsync();

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }
            return permissions;
        }
        public async Task<Permission> InsertPermission(Permission permission)
        {
            var newPermission = new Permission();
            try
            {
                var exist = _context.Permission.Where(x => x.Route == permission.Route).FirstOrDefault();
                if (exist != null)
                {
                    throw new InvalidOperationException("Ya existe el permiso");
                }

                newPermission.Name = permission.Name;
                newPermission.Route = permission.Route;
                newPermission.Status = true;
                newPermission.Menu = permission.Menu;
                _context.Permission.Add(newPermission);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
            return newPermission;
        }
        public async Task<Permission> EditPermission(Permission permission)
        {
            try
            {
                var permisionDB = await _context.Permission.Where(x => x.PermissionId == permission.PermissionId).FirstOrDefaultAsync();
                if (permisionDB == null)
                {
                    throw new InvalidOperationException("No existe Rol");
                }

                permisionDB.Name = permission.Name;
                permisionDB.Route = permission.Route;
                permisionDB.Status = permission.Status;
                permisionDB.Menu = permission.Menu;
                _context.Entry(permisionDB);
                _context.SaveChanges();
                return permisionDB;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

        public async Task<IEnumerable<PermissionRol>> GetPermissionByRol(int rolId)
        {
            var permissionRol = await _context.PermissionRol.Where(x => x.RolId == rolId && x.Status == true).ToListAsync();
            return permissionRol;
        }
        public async Task<IEnumerable<PermissionRol>> GetPermissionByNoRol(int rolId)
        {
            var permissionRol = await _context.PermissionRol.Where(x => x.RolId == rolId && x.Status == true).Select(x => x.PermissionId).ToListAsync();
            var permission = await _context.Permission.ToListAsync();
            permission.RemoveAll(x => permissionRol.Contains(x.PermissionId));
            return permission.Select(x=> new PermissionRol
            {
                PermissionId = x.PermissionId,
                RolId = rolId,
                Status = x.Status
            }).ToList();
        }

        public async Task<List<PermissionRol>> AssingPermission(List<PermissionRol> permisos)
        {
            try
            {
                foreach (var item in permisos)
                {
                    _context.PermissionRol.Add(item);
                }
                await _context.SaveChangesAsync();

                return permisos;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

        public Task DeletePermissionRol(int PermissionRolId)
        {
            try
            {
                var UserRol = _context.PermissionRol.FirstOrDefault(x => x.RolPermissionId == PermissionRolId);
                _context.Remove(UserRol);
                _context.SaveChangesAsync();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

    }
}
