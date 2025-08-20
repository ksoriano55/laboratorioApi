using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> InsertUser(User usuario);
        Task<User> EditUser(User usuario);
    }
}
