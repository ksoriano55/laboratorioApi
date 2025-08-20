using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class QualityControl
    {
        public int qualityControlId { get; set; }
        public int analisysId { get; set; }
        public DateTime creationDate { get; set; }
        public string? energyLamp { get; set; }
        public string? curve { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? correlative { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal initialSTD { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? endSTD { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal realValue { get; set; }
    }
}
