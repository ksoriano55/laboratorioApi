using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? contact { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public bool status { get; set; }
        public int createdUser { get; set; }
        public int? modifiedUser { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
    }
}
