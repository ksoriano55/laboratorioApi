using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Equipment
    {
        public int equipmentId { get; set; }
        public string? lastCode { get; set; } = string.Empty; 
        public string description { get; set; } = string.Empty;
        public string model { get; set; } = string.Empty;
        public string serialNumber { get; set; } = string.Empty;
        public int locationId { get; set; }
        public int areaId { get; set; }
        public bool isSGC { get; set; }
        public bool status { get; set; }
        public string maker { get; set; }=string.Empty;
        public string? range { get; set; }
	    public string? resolution { get; set; }
	    public bool? includeManual { get; set; } 
	    public bool? isProcedure { get; set; }
	    public string? procedureCode { get; set; }
	    public bool? includeVerification { get; set; }
	    public string? frequencyVerification { get; set; }
	    public DateTime? dateVerification { get; set; }
	    public bool? includeMaintenance { get; set; }
	    public string? frequencyMaintenance { get; set; }
	    public DateTime? dateMaintenance { get; set; }
	    public bool? includeCalibration { get; set; }
        public string? autorization { get; set; }

        public virtual ICollection<EquipmentCertificate>? certificates { get; set; }
    }
}
