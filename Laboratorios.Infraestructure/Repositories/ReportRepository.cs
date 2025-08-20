using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO.DLS;
using System.Data;


namespace Laboratorios.Infraestructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly LaboratoriosContext _context;

        public ReportRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<Income> GetReportInformation(int incomeId)
        {
            try
            {
                #pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
                var income = await _context.Income.Where(x=>x.incomeId==incomeId)
                    .Include(x=>x.samples).ThenInclude(x=>x.Matrix)
                    .Include(x=>x.samples).ThenInclude(x=>x.incomeDetail).ThenInclude(x=>x.Analisys)
                    .Include(x => x.samples).ThenInclude(x => x.microbiologyResults).ThenInclude(x => x.UnitOfMeasurement)
                    .Include(x => x.samples).ThenInclude(x => x.microbiologyResults).ThenInclude(x => x.Analisys)
                    .Include(x => x.samples).ThenInclude(x => x.microbiologyResults).ThenInclude(x => x.Metodology).ThenInclude(x => x.Methods)
                    .Include(x => x.samples).ThenInclude(x => x.microbiologyResults).ThenInclude(x => x.Metodology).ThenInclude(x => x.Normas)
                    .Include(x => x.samples).ThenInclude(x => x.phisycalChemistryResults).ThenInclude(x => x.UnitOfMeasurement)
                    .Include(x => x.samples).ThenInclude(x => x.phisycalChemistryResults).ThenInclude(x => x.Analisys)
                    .Include(x => x.samples).ThenInclude(x => x.phisycalChemistryResults).ThenInclude(x => x.Metodology).ThenInclude(x => x.Methods)
                    .Include(x => x.samples).ThenInclude(x => x.phisycalChemistryResults).ThenInclude(x => x.Metodology).ThenInclude(x => x.Normas)
                    .FirstOrDefaultAsync();
                #pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
                return income ?? new Income();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<object> GetReportInformationOutsourced(int incomeId)
        {
            try
            {
                var query = $"EXECUTE dbo.GetResultsOutsourced {incomeId}";
                return await _context.SamplesOutsourcedResultViewModelReport.FromSqlRaw(query).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
