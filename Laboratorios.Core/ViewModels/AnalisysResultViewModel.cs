using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class AnalisysResultsViewModel
    {
        [Key]
        public int microResultId { get; set; }
        public int analisysId { get; set; }
        public string name { get; set; }
        public int sampleId { get; set; }
        public int? metodologyId { get; set; }
        public string? lote { get; set; }
        public DateTime? readingDate { get; set; }
        public string? result { get; set; }
        public int? unitOfMeasurementId { get; set; }
        public DateTime? readingEndDate { get; set; }
        public bool? complete { get; set; }
        public string? comments { get; set; }
    }
}
