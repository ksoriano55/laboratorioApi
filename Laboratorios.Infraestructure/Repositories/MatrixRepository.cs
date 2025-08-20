using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorios.Infraestructure.Repositories
{
    public class MatrixRepository : IMatrixRepository
    {
        private readonly LaboratoriosContext _context;

        public MatrixRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Matrix>> GetMatrix()
        {
            try
            {
                var matrixdb = await _context.Matrix.ToListAsync();
                return matrixdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Matrix InsertMatrix(Matrix matrix)
        {
            try
            {
                _context.Matrix.Add(matrix);
                _context.SaveChanges();

                return matrix;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Matrix EditMatrix(Matrix matrix)
        {
            try
            {
                _context.Entry(matrix).State = EntityState.Modified;
                _context.SaveChanges();

                return matrix;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
