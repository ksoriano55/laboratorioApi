using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.ViewModels
{
    public class SamplesOutsourcedResultViewModel
    {
        [Key]
        public Int64 Id { get; set; }
        public int? outsourcedResultId { get; set; }
        public int outsourcedId { get; set; }
        public int subanalisysId { get; set; }
        public int? unitOfMeasurementId { get; set; }
        public string? result { get; set; }
        public string? method { get; set; }
        public string? norma { get; set; }
        public string name { get; set; } = string.Empty;
        public int? appointmentId { get; set; }
        public int? appointmentId1 { get; set; }

    }
    public class SamplesOutsourcedResultViewModelReport
    {
        [Key]
        public int outsourcedResultId { get; set; }
        public int outsourcedId { get; set; }
        public int subanalisysId { get; set; }
        public int unitOfMeasurementId { get; set; }
        public string result { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
        public string norma { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string correlative { get; set; } = string.Empty;
        public string unitOfMeasurement { get; set; } = string.Empty;
        public string _method { get; set; } = string.Empty;
        public string _norma { get; set; } = string.Empty;
    }
}
