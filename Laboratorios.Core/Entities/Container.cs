using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class Container
    {
        [Key]
        public int containerId { get; set; }
        public string description { get; set; } = string.Empty;
        public decimal value { get; set; }
        public bool status { get; set; }
    }
}
