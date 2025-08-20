using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.ViewModels
{
    public class CVMProgram
    {
        [Key]
        public Int64 Id { get; set; }
        public int equipmentId { get; set; }
        public int areaId { get; set; }
        public bool isSGC { get; set; }
        public string process { get; set; } = string.Empty;
        public string january { get; set; } = string.Empty;    
        public string february { get; set; } = string.Empty;    
        public string march { get; set; } = string.Empty;    
        public string april { get; set; } = string.Empty;    
        public string may { get; set; } = string.Empty;    
        public string june { get; set; } = string.Empty;    
        public string july { get; set; } = string.Empty;    
        public string august { get; set; } = string.Empty;    
        public string september { get; set; } = string.Empty;    
        public string october { get; set; } = string.Empty;    
        public string november { get; set; } = string.Empty;    
        public string december { get; set; } = string.Empty;

    }
}
