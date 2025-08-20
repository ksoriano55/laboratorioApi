using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class LogHistory
    {
        public int logHistoryId { get; set; }
        public string userName { get; set; } = string.Empty;
        public string action { get; set; } = string.Empty;
        public DateTime date { get; set; }
    }
}
