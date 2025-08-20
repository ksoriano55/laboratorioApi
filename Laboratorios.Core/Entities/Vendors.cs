using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Vendors
    {
        [Key]
        public int vendorId { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string services { get; set; }
        public bool status { get; set; }
    }
}
