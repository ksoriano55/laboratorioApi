using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class SamplesOutsourcedResult
    {
        [Key]
        public int outsourcedResultId { get; set; }
        public int outsourcedId { get; set; }
        public int subanalisysId { get; set; }
        public int unitOfMeasurementId { get; set; }
        public string result { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
        public string norma { get; set; } = string.Empty;
        public int? appointmentId { get; set; }
        public int? appointmentId1 { get; set; } 
        public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }
    }
}
