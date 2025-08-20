using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Entities
{
    public partial class User_Roles
    {
        [Key]
        public int UserRolId { get; set; }
        public int RolId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }

        public virtual Roles? Rol { get; set; }
        public virtual User? User { get; set; }
    }
}
