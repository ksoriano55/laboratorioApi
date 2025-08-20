using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class SamplesOutsourcedDetail
    {
        [Key]
        public int outsourcedDetailId { get; set; }
        public int outsourcedId { get; set; }
        public int analisysId { get; set; }
        public DateTime? receptionDate { get; set; }
        //public string? analisysResult { get; set; } = string.Empty;
    }
}
