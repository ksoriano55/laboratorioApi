using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class TableNMP
    {
        [Key]
        public int tableId { get; set; }
        public int matrixId { get; set; }
        public string unitofmeasurement { get; set; }
        public bool status { get; set; }
        //public List<TableNMPDetail>? TableNMPDetail { get; set; }

    }
}
