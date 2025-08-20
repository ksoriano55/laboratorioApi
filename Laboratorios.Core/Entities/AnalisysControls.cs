using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class AnalisysControls
    {
        [Key]
        public int analisysControlId { get; set; }
        public int analisysId { get; set; }
        public int? matrixId { get; set; }
        public bool? includeBco{ get; set; }
        public bool? includeSTD{ get; set; }
        public bool? includeDuplicate{ get; set; }
        public bool? includeBcoF{ get; set; }
        public bool? includeMxF{ get; set; }
        public bool? isHighLow{ get; set; }
    }
}
