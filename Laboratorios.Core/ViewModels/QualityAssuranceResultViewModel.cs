using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class QualityAssuranceResultViewModel
    {
        public List<Columns> columns = new List<Columns>();
        public IEnumerable<object>? data = new List<object>();
    }
    public class Columns
    {
        public string dataField { get; set; }
        public string caption { get; set; }
    }
}
