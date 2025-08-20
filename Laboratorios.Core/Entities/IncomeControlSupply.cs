using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class IncomeControlSupply
    {
        [Key]
        public int incomeControlId{get; set;}
        public DateTime receptionDate{get; set;}
        public DateTime requestDate{get; set;}
        public int purchaseOrder{get; set;}
        public int supplyRequest{get; set;}
        public int vendorId{get; set;}
        public int? assessmentId { get; set; }
        public int? areaId {get; set;}
    }
}
