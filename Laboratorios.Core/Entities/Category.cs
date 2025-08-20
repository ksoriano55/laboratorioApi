using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class Category
    {
        public int categoryId { get; set; }
        public string name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public int lastIdentity { get; set; }
        public bool status { get; set; }
    }
}
