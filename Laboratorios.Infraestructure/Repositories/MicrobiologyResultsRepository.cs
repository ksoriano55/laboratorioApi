using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class MicrobiologyResultsRepository : IMicrobiologyResultsRepository
    {
        private readonly LaboratoriosContext _context;

        public MicrobiologyResultsRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MicrobiologyResults>> GetMicrobiologyResults()
        {
            try
            {
                var microbiologyResultsdb = await _context.MicrobiologyResults.ToListAsync();
                return microbiologyResultsdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task InsertMicrobiologyResults(IEnumerable<MicrobiologyResults> microbiologyResults)
        {
            try
            {
                foreach(var item in microbiologyResults)
                {
                    
                    item.readingDate = item.date1;
                    if (item.readingEndDate!=null)
                    {
                        item.readingEndDate = item.date2;
                    }
                    if (item.microResultId>0)
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.MicrobiologyResults.Add(item);
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

        public async Task<IEnumerable<MicrobiologyResultsViewModel>> GetAllResultsMicrobiology(DateTime date, DateTime endDate)
        {
            try
            {
                return await _context.MicrobiologyResultsViewModel.FromSqlRaw($"EXECUTE dbo.GetAllResultsMicrobiology {date.Date.ToString("yyyyMMdd")},{endDate.Date.ToString("yyyyMMdd")}").ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IEnumerable<MicrobiologyResultsViewModel>> GetAnalisysMicrobiology(int incomeId)
        {
            try
            {
                return await _context.MicrobiologyResultsViewModel.FromSqlRaw("EXECUTE dbo.GetAnalisysMicrobiology @incomeId=" + incomeId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<MicrobiologyResultsViewModel>> GetBlankAnalisysMicrobiology(int matrixId)
        {
            try
            {
                return await _context.MicrobiologyResultsViewModel.FromSqlRaw("EXECUTE dbo.GetAnalisysBlank @matrixId=" + matrixId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Sample>> GetSamples(int year)
        {
            try
            {
                var samples = new List<Sample>();
                /*Se agrega esta validación especial como pokayoke por falta de información en tabla de Muestras*/
                if (year == 2023)
                {
                    samples = await _context.Sample.Where(x => x.incomeId == null && x.observations == null && x.sampleId < 6012).ToListAsync();
                }
                else
                {
                    samples = await _context.Sample.Where(x => x.incomeId == null && x.observations == null && x.datecreated.Value.Year==year).ToListAsync();
                }
                return samples;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
