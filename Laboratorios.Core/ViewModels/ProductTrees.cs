using Laboratorios.Core.Entities;

namespace Laboratorios.Core.ViewModels
{
    public class ProductTreeLines
    {
        public string ItemCode { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string WareHouse { get; set; } = string.Empty;
        public string ParentItem { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
    }
    public class ProductTrees
    {
        public string TreeCode { get; set; } =  string.Empty;
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public virtual ICollection<ProductTreeLines> ProductTreeLines { get; set; }
    }
}
