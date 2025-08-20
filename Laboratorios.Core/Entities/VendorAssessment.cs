using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class VendorAssessment
    {
        [Key]
        public int assessmentId { get; set; }
        public int assessmentTypeId { get; set; }
        public int vendorId { get; set; }
        public DateTime assessmentDate { get; set; } 
        public string data { get; set; } = string.Empty;
        public int userId { get; set; }
        public int note { get; set; }
        public int? serviceTypeId { get; set; }
        public DateTime? receptionDate { get; set; }
        public int? serviceNote { get; set; }
        public string? comments { get; set; }
    }
}
