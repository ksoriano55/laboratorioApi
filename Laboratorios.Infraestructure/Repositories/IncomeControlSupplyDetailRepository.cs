using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class IncomeControlSupplyDetailRepository : IIncomeControlSupplyDetailRepository
    {
        private readonly LaboratoriosContext _context;

        public IncomeControlSupplyDetailRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IncomeControlSupplyDetail>> GetIncomeControlSupplyDetail(int incomeControlId)
        {
            try
            {
                var incomeControlSupplys = await _context.IncomeControlSupplyDetail.Where(x=>x.incomeControlId==incomeControlId).ToListAsync();
                return incomeControlSupplys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public IncomeControlSupplyDetail InsertIncomeControlSupplyDetail(IncomeControlSupplyDetail incomeControlSupply)
        {
            try
            {
                if (incomeControlSupply.incomeControlDetailId > 0)
                {
                    _context.Entry(incomeControlSupply).State = EntityState.Modified;
                }
                else
                {
                    _context.IncomeControlSupplyDetail.Add(incomeControlSupply);
                }
                _context.SaveChanges();

                return incomeControlSupply;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
