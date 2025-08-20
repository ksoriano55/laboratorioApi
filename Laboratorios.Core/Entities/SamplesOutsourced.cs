using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class SamplesOutsourced
    {
        [Key]
        public int outsourcedId { get; set; }
        public DateTime dateAdmission { get; set; }
        public string correlative { get; set; } = string.Empty;
        public string outsourcedCode { get; set; } = string.Empty;
        public int? vendorId { get; set; }
        public DateTime? sendDate { get; set; }
        public string? quantity { get; set; } = string.Empty;
        public int? userId { get; set; }
        public string? observations { get; set; } = string.Empty;
        [NotMapped]
        public string? status { get; set; } = string.Empty;
    }
}
