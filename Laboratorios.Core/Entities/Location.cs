using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class Location
    {
        [Key]
        public int locationId { get; set; }
        public string name { get; set; } = string.Empty;
        public bool status { get; set; }
    }
}
