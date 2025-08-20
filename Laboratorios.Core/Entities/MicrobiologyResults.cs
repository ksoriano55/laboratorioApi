using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class MicrobiologyResults
    {
        [Key]
        public int microResultId { get; set; }
        public int sampleId { get; set; }
        public int metodologyId { get; set; }
        public int analisysId { get; set; }
        public string lote { get; set; }
        public DateTime readingDate { get; set; }
        public int? direct { get; set; }
        public int? count1 { get; set; }
        public int? count2 { get; set; }
        public int? count3 { get; set; }
        public int? count4 { get; set; }
        public int? count5 { get; set; }
        public int? directFC { get; set; }
        public int? count1FC { get; set; }
        public int? count2FC { get; set; }
        public int? count3FC { get; set; }
        public int? count4FC { get; set; }
        public int? count5FC { get; set; }
        public string? result { get; set; }
        public int? unitOfMeasurementId { get; set; }
        public DateTime? readingEndDate { get; set; }
        public int userReading { get; set; }
        public bool? complete   { get; set; }
        public string? comments   { get; set; }
        public bool? isreanalisys { get; set; }

        [NotMapped]
        public DateTime date1 { get; set; }
        [NotMapped]
        public DateTime? date2 { get; set; }
        public int? productionOrderId { get; set; }
        public virtual Metodology? Metodology { get; set; }
        public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public virtual Analisys? Analisys { get; set; }
    }
}
