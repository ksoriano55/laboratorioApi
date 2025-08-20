using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class EquipmentCertificate
    {
        [Key]
        public int certificateId { get; set; }
        public int equipmentId { get; set; }
        public string certificate { get; set; } = string.Empty;
        public string? url { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
