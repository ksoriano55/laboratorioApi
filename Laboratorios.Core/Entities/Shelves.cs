using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class Shelves
    {
        [Key]
        public int id { get; set; }
        public string code { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int locationId { get; set; }
        public bool status { get; set; }
    }
}
