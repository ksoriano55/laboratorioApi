using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public partial class PermissionRol
    {
        [Key]
        public int RolPermissionId { get; set; }
        public int RolId { get; set; }
        public int PermissionId { get; set; }
        public bool Status { get; set; }
        public virtual Permission? Permission { get; set; }
        //public virtual Roles Roles { get; set; }
    }
}
