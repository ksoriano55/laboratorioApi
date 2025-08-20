using Laboratorios.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.ViewModels
{
    public class _ReportProductionOrderDetails
    {
        [Key]
        public Int64 Id { get; set; }
        public string? Cliente { get; set; } = string.Empty;
        public int Orden_Produccion { get; set; }
        public int? Id_SAP { get; set; }
        public string Area { get; set; } = string.Empty;
        public string Fecha { get; set; } = string.Empty;
        public string Receta { get; set; } = string.Empty;
        public string? Referencia { get; set; } = string.Empty;
        public string Insumo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad_Utilizada { get; set; }
    }
}
