using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Entities
{
    public partial class Area_Analisys
    {
        [Key]
        public int AreaAnalisysId { get; set; }
        public int areaId { get; set; }
        public int analisysId { get; set; }

        public virtual Area Area { get; set; }
        public virtual Analisys Analisys { get; set; }
    }
}
