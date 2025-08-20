using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Entities
{
    public partial class Matrix_Area
    {
        [Key]
        public int MatrixAreaId { get; set; }
        public int matrixId { get; set; }
        public int areaId { get; set; }

        public virtual Matrix Matrix { get; set; }
        public virtual Area Area { get; set; }
    }
}
