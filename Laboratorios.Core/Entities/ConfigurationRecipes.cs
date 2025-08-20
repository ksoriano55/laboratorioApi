using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace Laboratorios.Core.Entities
{
    public  class ConfigurationRecipes
    {
        [Key]
        public int configurationId {get; set;}
        public string name {get; set;}  = string.Empty;
        public string description { get; set; } = string.Empty;   
        public decimal dividend {get; set;}
        public decimal divisor {get; set;}
        public string itemCode {get; set;} = string.Empty;
        public bool status {get; set;}
    }
}
