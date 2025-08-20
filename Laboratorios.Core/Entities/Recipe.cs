
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class Recipe
    {
        [Key]
        public int recipeId {get; set;}
        public int analisysId {get; set;}
        public int? matrixId {get; set;}
	    public string treeCode {get; set;}
        public string name {get; set;}
        public bool status {get; set;}
        public bool isFav {get; set;}
        public bool isOriginal {get; set; }
        public string? nameSAP {get; set; }
        public string? wareHouse {get; set; }
        public int? recipeRef { get; set;}
        public int areaId { get; set; }
        [ForeignKey("recipeId")]
        public virtual ICollection<RecipeDetail> RecipeDetail {get; set;}
    }
}
