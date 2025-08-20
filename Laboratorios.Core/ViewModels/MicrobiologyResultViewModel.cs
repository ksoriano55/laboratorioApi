using Laboratorios.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.ViewModels
{
    public class MicrobiologyResultsViewModel
    {
        [Key]
        public Int64 Id { get; set; }
        public int areaId { get; set; }
        public string correlative { get; set; } = string.Empty;
        public int matrixId { get; set; }
        public int analisysId { get; set; }
        public int sampleId { get; set; }
        public int? microResultId { get; set; }
        public int? metodologyId { get; set; }
        public string? lote { get; set; }
        public DateTime? readingDate { get; set; }
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
        public int? userReading { get; set; }
        public bool? complete { get; set; }
        public string? comments { get; set; }
        public int? productionOrderId { get; set; }
        public int? _productionOrderId { get; set; }
        public bool? status { get; set; }
        public string? request { get; set; }
        public string? customer { get; set; }
        public bool? Isreanalisys { get; set; } = false;
        public DateTime? dateCreated { get; set; }
        [ForeignKey("productionOrderId")]
        public virtual ProductionOrder? ProductionOrder { get; set; }
    }
}
