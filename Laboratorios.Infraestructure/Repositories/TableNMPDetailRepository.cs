using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
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
    public class TableNMPDetailRepository : ITableNMPDetailRepository
    {
        private readonly LaboratoriosContext _context;

        public TableNMPDetailRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResultViewModel>> GetTables()
        {
            try
            {
                var matrixdb = await _context.TableNMPDetail.Include(x => x.TableNMP).Select(x=> new ResultViewModel{
                    matrixId = x.TableNMP.matrixId,
                    result  = x.result,
                    valueNMP = x.valueNMP
                }).ToListAsync();
                return matrixdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IEnumerable<TableNMPDetail>> GetTables(TableNMP tableNMP)
        {
            try
            {
                var matrixdb = await _context.TableNMPDetail.Where(x=>x.tableId==tableNMP.tableId).ToListAsync();
                return matrixdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public TableNMPDetail InsertTableDetail(TableNMPDetail matrix)
        {
            try
            {
                _context.TableNMPDetail.Add(matrix);
                _context.SaveChanges();

                return matrix;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public TableNMPDetail EditTableDetail(TableNMPDetail matrix)
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

        public Task DeleteTableDetail(TableNMPDetail tableNMPDetail)
        {
            try
            {
                _context.Remove(tableNMPDetail);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }
    }
}
