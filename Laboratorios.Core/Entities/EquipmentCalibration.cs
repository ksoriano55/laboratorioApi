using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class EquipmentCalibration
    {
        [Key]
        public int calibrationId {get; set;}
        public int equipmentId {get; set;}
        public string certificate {get; set;} = string.Empty;
        public int period {get; set;}
        public DateTime lastCalibrationDate {get; set;}
        public bool status {get; set;}
        public int newCalibrationInterval {get; set;}
        public DateTime nextCalibrationDate {get; set;}
    }
}
