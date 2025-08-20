using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface IReportRepository
    {
        Task<Income> GetReportInformation(int incomeId);
        Task<object> GetReportInformationOutsourced(int incomeId);
    }
}
