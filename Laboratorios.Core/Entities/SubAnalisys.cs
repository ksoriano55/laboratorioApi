using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class SubAnalisys
    {
        [Key]
        public int subanalisysId { get; set; }
        public int analisysId { get; set; }
        public string name { get; set; } = string.Empty;
        public bool status { get; set; }
    }
}
