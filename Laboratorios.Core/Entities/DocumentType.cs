using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class DocumentType
    {
        public int documentTypeId { get; set; }
        public string name { get; set; } = string.Empty;
        public int NCCV { get; set; }
        public bool status { get; set; }
        public string? defaultDocument { get; set; }
    }
}
