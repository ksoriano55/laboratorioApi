using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class TableNMPDetail
    {
        [Key]
        public int tableDetailId { get; set; }
        public int tableId { get; set; }
        public int tube1 { get; set; }
        public int tube2 { get; set; }
        public int tube3 { get; set; }
        public string result { get; set; }
        public string valueNMP { get; set; }
        [ForeignKey("tableId")]

        public virtual TableNMP? TableNMP { get; set; }
    }
}
