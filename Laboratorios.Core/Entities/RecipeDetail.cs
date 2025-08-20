
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace Laboratorios.Core.Entities
{
    public class RecipeDetail
    {
        [Key]
        public int recipeDetailId { get; set; }
        public int recipeId {get; set;}
        public string itemCode { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public decimal quantity { get; set; }
        public string parentItem { get; set; } = string.Empty;
        public string itemName { get; set; } = string.Empty;
        public decimal? nOfTubes { get; set; }
        public decimal? nOfTubesUsed { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? usedQuantity { get; set; }

        [NotMapped]
        public bool? included { get; set; }
    }
}
