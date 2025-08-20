using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class UnitOfMeasurement
    {
        public int unitOfMeasurementId { get; set; }
        public string unitOfMeasurement { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public int areaId { get; set; }
    }
}
