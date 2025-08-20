using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IMicrobiologyResultsRepository
    {
        Task<IEnumerable<MicrobiologyResults>> GetMicrobiologyResults();
        Task InsertMicrobiologyResults(IEnumerable<MicrobiologyResults> resultados);
        Task<IEnumerable<MicrobiologyResultsViewModel>> GetAnalisysMicrobiology(int incomeId);
        Task<IEnumerable<MicrobiologyResultsViewModel>> GetAllResultsMicrobiology(DateTime date, DateTime endDate);
        Task<IEnumerable<MicrobiologyResultsViewModel>> GetBlankAnalisysMicrobiology(int matrixId);
        Task<IEnumerable<Sample>> GetSamples(int year);
    }
}
