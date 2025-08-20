

using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IPhisycalChemistryResultsRepository
    {
        Task InsertPhisycalChemistryResults(IEnumerable<PhisycalChemistryResultsViewModel> resultados);
        Task<IEnumerable<PhisycalChemistryResultsViewModel>> GetAnalisysPhisycalChemistry(DateTime date, DateTime endDate, int analisysId, int areaId);
        Task<IEnumerable<ConfigurationResults>> GetConfigurationResults();
        Task<object> GetQualityAssurance(int analisysId, int year, int opt);
    }
}
