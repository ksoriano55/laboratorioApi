using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IMatrixRepository
    {
        Task<IEnumerable<Matrix>> GetMatrix();

        Matrix InsertMatrix(Matrix matrix);
        Matrix EditMatrix(Matrix matrix);
    }
}
