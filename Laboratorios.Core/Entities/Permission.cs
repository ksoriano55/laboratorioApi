using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Menu { get; set; } = string.Empty;
    }
}
