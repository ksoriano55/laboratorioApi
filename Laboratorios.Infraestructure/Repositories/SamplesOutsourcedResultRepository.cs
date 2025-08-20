using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class SamplesOutsourcedResultRepository : ISamplesOutsourcedResultRepository
    {
        private readonly LaboratoriosContext _context;

        public SamplesOutsourcedResultRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SamplesOutsourcedResult>> GetSamplesOutsourcedResults(int outsourcedId)
        {
            try
            {
                var SamplesOutsourcedResultdb = await _context.SamplesOutsourcedResult.Where(x=>x.outsourcedId == outsourcedId).ToListAsync();
                return SamplesOutsourcedResultdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<SamplesOutsourcedResultViewModel>> GetSamplesOutsourcedResultViews(int outsourcedId)
        {
            try
            {
                return await _context.SamplesOutsourcedResultViewModel.FromSqlRaw("EXECUTE dbo.GetAnalisysOutsourced @id=" + outsourcedId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task InsertSamplesOutsourcedResult(IEnumerable<SamplesOutsourcedResult> SamplesOutsourcedResult)
        {
            try
            {
                foreach(var item in SamplesOutsourcedResult)
                {
                    if (item.outsourcedResultId>0)
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.SamplesOutsourcedResult.Add(item);
                    }
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
