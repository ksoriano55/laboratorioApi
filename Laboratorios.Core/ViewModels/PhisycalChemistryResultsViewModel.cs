

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.ViewModels
{
    public class PhisycalChemistryResultsViewModel
    {
        [Key]
        public Int64 Id { get; set; }
        public DateTime datecreated { get; set; }
        public int sampleId { get; set; }
        public int matrixId { get; set; }
        public string correlative { get; set; }
        public int analisysId { get; set; }
        public int? phisycalChemistryId { get; set; }
        public int? metodologyId { get; set; }
        public int? unitOfMeasurementId { get; set; }
        public int? userReading { get; set; }
        public bool? complete { get; set; }
        public string? comments { get; set; }
        public string? result { get; set; }
        public string? resultTwo { get; set; }
        public decimal? sampleVolume { get; set; }
        public decimal? finalVolume { get; set; }
        public decimal? reading { get; set; }
        public decimal? readingDuplicate { get; set; }
        public decimal? sampleBlank { get; set; }
        public decimal? factor { get; set; }
        public decimal? concentration { get; set; }
        public decimal? edta { get; set; }
        public decimal? averageDuplicate { get; set; }
        public int? atMethod { get; set; }
        public int? userApprove { get; set; }
        public DateTime? readingDate { get; set; }
        public DateTime? analisysDate { get; set; }
        public bool? isDuplicate { get; set; }
        public decimal? factor1 { get; set; }
        public decimal? factor2 { get; set; }
        public decimal? factor3 { get; set; }
        public decimal? factor4 { get; set; }
        public decimal? capsulemx1 { get; set; }
        public decimal? capsulemx2 { get; set; }
        public decimal? capsulemx3 { get; set; }
        public int? productionOrderId { get; set; }
        public int? _productionOrderId { get; set; }
        public bool? isreanalisys { get; set; }
        [NotMapped]
        public int? areaId { get; set; }
    }
}
