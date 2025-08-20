using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class LoginSapViewModel
    {
        public class LoginBody
        {
            public string CompanyDB { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class LoginResponse
        {
            public string SessionId { get; set; }

            public DateTime Token { get; set; } 
        }

    }
}
