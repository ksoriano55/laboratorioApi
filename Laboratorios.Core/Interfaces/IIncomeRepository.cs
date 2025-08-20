using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<_Income>> GetIncome(int year);
        Task<Income> GetIncomeById(int incomeId);
        Income InsertIncome(Income income);
        Income EditIncome(Income income, bool? draft);
        Task EditIncomeDate(IncomeView income);
        Task DeleteIncomeDetail(IncomeDetail incomeDetail);
        Task<IEnumerable<IncomeViewModel>> GetDetailsIncome(int year);
        Task UpdateSample(Sample sample);
    }
}
