using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace Laboratorios.Core.Entities
{
    public class ProductionOrderDetail
    {
        public int productionOrderDetailId {get; set;}
        public int productionOrderId {get; set; }
        public string itemNo {get; set;} = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public decimal? baseQuantity  {get; set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal plannedQuantity  {get; set;}
        public string itemName { get; set;} = string.Empty;
        public int? nOfTubesUsed { get; set;}
        [NotMapped]
        public bool? include { get; set; }
    }
}
