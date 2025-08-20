using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface ISamplesOutsourcedResultRepository
    {
        Task<IEnumerable<SamplesOutsourcedResult>> GetSamplesOutsourcedResults(int outsourcedId);
        Task<IEnumerable<SamplesOutsourcedResultViewModel>> GetSamplesOutsourcedResultViews(int outsourcedId);
        Task InsertSamplesOutsourcedResult(IEnumerable<SamplesOutsourcedResult> resultados);
    }
}
