using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class SamplesOutsourcedDetailRepository : ISamplesOutsourcedDetailRepository
    {
        private readonly LaboratoriosContext _context;

        public SamplesOutsourcedDetailRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SamplesOutsourcedDetail>> GetSamplesOutsourcedDetail(int outsourcedId)
        {
            try
            {
                var samplesOutsourcedDetails = await _context.SamplesOutsourcedDetail.Where(x=>x.outsourcedId==outsourcedId).ToListAsync();
                return samplesOutsourcedDetails;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public SamplesOutsourcedDetail InsertSamplesOutsourcedDetail(SamplesOutsourcedDetail samplesOutsourcedDetail)
        {
            try
            {
                if (samplesOutsourcedDetail.outsourcedDetailId > 0)
                {
                    _context.Entry(samplesOutsourcedDetail).State = EntityState.Modified;
                }
                else
                {
                    _context.SamplesOutsourcedDetail.Add(samplesOutsourcedDetail);
                }
                _context.SaveChanges();

                return samplesOutsourcedDetail;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
        public Task DeleteSamplesOutsourcedDetail(SamplesOutsourcedDetail detalle)
        {
            try
            {
                _context.SamplesOutsourcedDetail.Remove(detalle);
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
