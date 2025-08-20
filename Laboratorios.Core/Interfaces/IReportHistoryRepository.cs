using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IReportHistoryRepository
    {
        public Task<IEnumerable<_ReportHistory>> getHistoryRepository(int year);
        public Task<ReportHistory> update(ReportHistory reportHistory);
    }
}
