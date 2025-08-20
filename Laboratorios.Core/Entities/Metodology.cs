using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class Metodology
    {
        public int metodologyId { get; set; }
        public string name { get; set; } = String.Empty;
        public bool status { get; set; }
        public int matrixId { get; set; }
        public int appointmentId { get; set; }
        public string method { get; set; }
        public int? appointmentId1 { get; set; }
        public int? appointmentId2 { get; set; }
        public string? norma { get; set; }
        public int analisysId { get; set; }
        public string? norma1 { get; set; }
        public string? norma2 { get; set; }
        public virtual Appointment? Methods { get; set; }
        [ForeignKey("appointmentId1")]
        public virtual Appointment? Normas { get; set; }

    }
}
