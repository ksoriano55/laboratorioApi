using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{

    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> GetRoles();

        Task<Roles> InsertRoles(Roles rol);
        Task<Roles> EditRol(Roles rol);
        Task<IEnumerable<User_Roles>> GetRolesByUser(int userId);
        Task<IEnumerable<User_Roles>> GetRolesByNotUser(int userId);
        Task<List<User_Roles>> AssingRoles(List<User_Roles> roles);
        Task DeleteUserRol(int userRolId);
    }
}
