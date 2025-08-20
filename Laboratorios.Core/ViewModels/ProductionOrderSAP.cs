using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class ProductionOrderSAP
    {
        public DateTime DueDate { get; set; }
        public string ItemNo { get; set; } = string.Empty;
        public int PlannedQuantity { get; set; }
        public ICollection<ProductionOrderDetailSAP> ProductionOrderLines { get; set; }
    }

    public class ProductionOrderDetailSAP
    {
        public string ItemNo { get; set; } = string.Empty;
        public decimal BaseQuantity { get; set; } 
        public decimal PlannedQuantity { get; set; } 
        public string ItemType { get; set; } = "pit_Item";
    }

    public class ProductionOrderOject
    {
        public List<ProductionOrder> productionOrders { get; set; }
        public string? packageCode { get; set; } = string.Empty;
    }

    public class ProductionOrderViewModel
    {
        public ICollection<MicrobiologyResultsViewModel>? mbResults { get; set; }
        public ICollection<PhisycalChemistryResultsViewModel>? fqResults { get; set; }
        public ProductionOrder productionOrder { get; set; }
        public bool reanalisys { get; set; }
    }
}
