using Laboratorios.Core.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class PhisycalChemistryResults
    {
        public PhisycalChemistryResults()
        {

        }

        public PhisycalChemistryResults(PhisycalChemistryResultsViewModel item)
        {
            phisycalChemistryId = item.phisycalChemistryId == null ? 0 : (int)item.phisycalChemistryId;
            sampleId = item.sampleId;
            metodologyId = item.metodologyId == null ? 0 : (int)item.metodologyId;
            unitOfMeasurementId = item.unitOfMeasurementId == null ? 0 : (int)item.unitOfMeasurementId;
            analisysId = item.analisysId;
            userReading = item.userReading == null ? 0 : (int)item.userReading;
            complete = item.complete;
            comments = item.comments;
            result = item.result;
            resultTwo = item.resultTwo;
            sampleVolume = item.sampleVolume;
            finalVolume = item.finalVolume;
            reading = item.reading;
            readingDuplicate = item.readingDuplicate;
            sampleBlank = item.sampleBlank;
            factor = item.factor;
            concentration = item.concentration;
            edta = item.edta;
            averageDuplicate = item.averageDuplicate;
            atMethod = item.atMethod;
            userApprove = item.userApprove;
            readingDate = item.readingDate;
            analisysDate = item.analisysDate;
            isDuplicate = item.isDuplicate;
            factor1 = item.factor1;
            factor2 = item.factor2;
            factor3 = item.factor3;
            factor4 = item.factor4;
            capsulemx1 = item.capsulemx1;
            capsulemx2 = item.capsulemx2;
            capsulemx3 = item.capsulemx3;
            productionOrderId = item.productionOrderId;
            isreanalisys = item.isreanalisys;
        }

        [Key]
        public int phisycalChemistryId { get; set; }
        public int sampleId { get; set; }
        public int metodologyId { get; set; }
        public int unitOfMeasurementId { get; set; }
        public int analisysId { get; set; }
        public int userReading { get; set; }
        public bool? complete { get; set; }
        public string? comments { get; set; }
        public string? result { get; set; }
        public string? resultTwo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? sampleVolume { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? finalVolume { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? reading { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? readingDuplicate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? sampleBlank { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? factor { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? concentration { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? edta { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? averageDuplicate { get; set; }
        public int? atMethod { get; set; }
        public int? userApprove { get; set; }
        public DateTime? readingDate { get; set; }
        public DateTime? analisysDate { get; set; }
        public bool? isDuplicate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? factor1 { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? factor2 { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? factor3 { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? factor4 { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? capsulemx1 { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? capsulemx2 { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? capsulemx3 { get; set; }
        public int? productionOrderId { get; set; }
        public bool? isreanalisys { get; set; }
        public virtual Metodology? Metodology { get; set; }
        public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public virtual Analisys? Analisys { get; set; }

    }
}
