using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Infraestructure.Repositories
{
    public class SamplesOutsourcedRepository : ISamplesOutsourcedRepository
    {
        private readonly LaboratoriosContext _context;

        public SamplesOutsourcedRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SamplesOutsourced>> GetSamplesOutsourced(int year)
        {
            try
            {
                var samplesOutsourceds = await _context.SamplesOutsourced.Where(x=>x.dateAdmission.Date.Year == year)
                    .ToListAsync();
                return samplesOutsourceds;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public SamplesOutsourced InsertSamplesOutsourced(SamplesOutsourced samplesOutsourced)
        {
            try
            {
                if (samplesOutsourced.outsourcedId > 0)
                {
                    _context.Entry(samplesOutsourced).State = EntityState.Modified;
                }
                else
                {
                    _context.SamplesOutsourced.Add(samplesOutsourced);
                }
                _context.SaveChanges();

                return samplesOutsourced;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
