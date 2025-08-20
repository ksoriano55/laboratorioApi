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
    public class TableNMPRepository : ITableNMPRepository
    {
        private readonly LaboratoriosContext _context;

        public TableNMPRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TableNMP>> GetTables()
        {
            try
            {
                var matrixdb = await _context.TableNMP.ToListAsync();
                return matrixdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public TableNMP InsertTable(TableNMP matrix)
        {
            try
            {
                _context.TableNMP.Add(matrix);
                _context.SaveChanges();

                return matrix;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public TableNMP EditTable(TableNMP matrix)
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
