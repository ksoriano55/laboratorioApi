using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class ConfigurationResults
    {
        [Key]
        public int configurationId { get; set; }
        public int analisysId { get; set; }
        public string json { get; set; } = string.Empty;
        public bool status { get; set; }
        public int? matrixId { get; set; }
    }
}
