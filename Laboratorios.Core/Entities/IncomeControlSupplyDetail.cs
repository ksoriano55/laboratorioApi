using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class IncomeControlSupplyDetail
    {
        [Key]
        public int incomeControlDetailId{get; set;}
        public int incomeControlId{get; set; }
        public string description{get; set;}= string.Empty;
        public string presentation{get; set;}=string.Empty;
        public int quantity{get; set;}
    }
}
