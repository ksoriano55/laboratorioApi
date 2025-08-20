using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Quotation
    {
        public int DocNum { get; set; }
        public int DocEntry { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string ContactPerson  { get; set; }
    }
}
