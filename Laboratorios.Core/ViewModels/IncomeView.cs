using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class IncomeView
    {
        public int incomeId { get; set; }
        public string? cusName { get; set; }
        public string? phone { get; set; }
        public DateTime? sendDate { get; set; }
        public string? path { get; set; }
    }
}
