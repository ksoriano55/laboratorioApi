using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Laboratorios.Core.Entities
{
    public class ProductionOrder
    {
        public ProductionOrder() { }
        public ProductionOrder(ProductionOrder _ProductionOrder) {
            productionOrderId = _ProductionOrder.productionOrderId;
            areaId = _ProductionOrder.areaId;
            resultId = _ProductionOrder.resultId;
            status = _ProductionOrder.status;
            documentNumber = _ProductionOrder.documentNumber;
            dueDate = _ProductionOrder.dueDate;
            itemNo = _ProductionOrder.itemNo;
            refeLab = _ProductionOrder.refeLab;
            recipeId = _ProductionOrder.recipeId;
            customerName = _ProductionOrder.customerName;
            isreanalisys = _ProductionOrder.isreanalisys;
            ProductionOrderDetail = _ProductionOrder.ProductionOrderDetail.Select(_item => new ProductionOrderDetail
            {
                baseQuantity = _item.baseQuantity,
                include = _item.include,
                itemName = _item.itemName,
                itemNo = _item.itemNo,
                nOfTubesUsed = _item.nOfTubesUsed,
                plannedQuantity = _item.plannedQuantity,
                productionOrderDetailId = _item.productionOrderDetailId,
                productionOrderId = _item.productionOrderId,
            }).ToList();
            recipe = _ProductionOrder.recipe;
        }
        public int productionOrderId {get; set;}
        public int? areaId {get; set;}
	    public int? resultId  {get; set;}
	    public bool? status {get; set;}
        public string? documentNumber {get; set;}
        public DateTime? dueDate {get; set;}
        public string itemNo { get; set; } = string.Empty;
        public string? refeLab { get; set; } = string.Empty;
        public int? recipeId { get; set; }
        public string? customerName { get; set; } = string.Empty;
        public bool? isreanalisys { get; set; }
        public int? documentType { get; set; }
        public int? docEntry { get; set; }
        //public bool? sync { get; set; }
        [ForeignKey("productionOrderId")]
        public virtual ICollection<ProductionOrderDetail> ProductionOrderDetail { get; set; } = new HashSet<ProductionOrderDetail>();
        [ForeignKey("recipeId")]
        public virtual Recipe? recipe { get; set; }
        [NotMapped]
        public DateTime? _dueDate { get; set; }

    }
}
