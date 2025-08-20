using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public string description { get; set; }
        public bool ismethod { get; set; }
        public bool status { get; set; }
    }
}
