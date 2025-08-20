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
   
    public class RolesRepository : IRolesRepository
    {
        private readonly LaboratoriosContext _context;

        public RolesRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Roles>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Roles> InsertRoles(Roles rol)
        {
            var roles = new Roles();
            try
            {
                var exist = _context.Roles.Where(x => x.Description == rol.Description).FirstOrDefault();
                if (exist != null)
                {
                    throw new InvalidOperationException("Ya existe Rol");
                }

                roles.Description = rol.Description;
                roles.Status = true;
                _context.Roles.Add(roles);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error al crear el usuario");
            }
            return roles;
        }

        public async Task<Roles> EditRol(Roles rol)
        {
            try
            {
                var rolDB = await _context.Roles.Where(x => x.RolId == rol.RolId).FirstOrDefaultAsync();
                if (rolDB == null)
                {
                    throw new InvalidOperationException("No existe Rol");
                }

                rolDB.Description = rol.Description;
                rolDB.Status = rol.Status;
                _context.Entry(rolDB);
                _context.SaveChanges();
                return rolDB;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
           
        }

        public async Task<IEnumerable<User_Roles>> GetRolesByUser(int userId)
        {
            var RolesUser = await _context.User_Roles.Where(x => x.UserId == userId && x.Status == true).ToListAsync();
            //var Roles = await _context.Roles.Where(x => RolesUser.Contains(x.RolId)).ToListAsync();
            return RolesUser;
        }

        public async Task<IEnumerable<User_Roles>> GetRolesByNotUser(int userId)
        {
            var RolesUser = await _context.User_Roles.Where(x => x.UserId == userId && x.Status == true).Select(x=>x.RolId).ToListAsync();
            var Roles = await _context.Roles.Where(x => x.Status == true).ToListAsync();
            Roles.RemoveAll(x => RolesUser.Contains(x.RolId));
            return Roles.Select(x=>new User_Roles
            {
                RolId = x.RolId,
                UserId = userId,
                Status = true
            }).ToList();
        }

        public async Task<List<User_Roles>> AssingRoles(List<User_Roles> roles)
        {
            try
            {
                foreach(var item in roles)
                {
                   _context.User_Roles.Add(item);
                }
                await _context.SaveChangesAsync();

                return roles;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

        public Task DeleteUserRol(int UserRolId)
        {
            try
            {
                var UserRol = _context.User_Roles.FirstOrDefault(x => x.UserRolId == UserRolId);
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
