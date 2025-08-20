using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class ReportHistoryRepository : IReportHistoryRepository
    {
        private readonly LaboratoriosContext _context;

        public ReportHistoryRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<_ReportHistory>> getHistoryRepository(int year) {
            try
            {
                return await _context._ReportHistory.Where(x=>x.datereception.Date.Year == year).ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReportHistory> update(ReportHistory reportHistory)
        {
            try
            {
                if(reportHistory.reportId > 0)
                {
                    var _reportDB = await _context.ReportHistory.AsNoTracking().Where(x => x.reportId == reportHistory.reportId).FirstOrDefaultAsync();
                    if(_reportDB is null)
                    {
                        throw new Exception("No existe el control de este informe");
                    }
                    _reportDB.checkMB = reportHistory.checkMB;
                    _reportDB.checkFQ = reportHistory.checkFQ;
                    _reportDB.correlativeFQ = reportHistory.correlativeFQ;
                    _reportDB.correlativeMB = reportHistory.correlativeMB;
                    _reportDB._sendDate = reportHistory._sendDate;
                    _context.Entry(reportHistory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return reportHistory;
                }
                throw new Exception("No existe el control de este informe");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
