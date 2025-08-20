using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IAutenticacionRepository
    {
        Task<Response> Login(string username, string password);
    }
}
