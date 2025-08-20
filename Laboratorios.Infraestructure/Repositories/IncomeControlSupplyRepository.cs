using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class IncomeControlSupplyRepository : IIncomeControlSupplyRepository
    {
        private readonly LaboratoriosContext _context;

        public IncomeControlSupplyRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IncomeControlSupply>> GetIncomeControlSupply()
        {
            try
            {
                var incomeControlSupplys = await _context.IncomeControlSupply.ToListAsync();
                return incomeControlSupplys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public IncomeControlSupply InsertIncomeControlSupply(IncomeControlSupply incomeControlSupply)
        {
            try
            {
                if (incomeControlSupply.incomeControlId > 0)
                {
                    _context.Entry(incomeControlSupply).State = EntityState.Modified;
                }
                else
                {
                    _context.IncomeControlSupply.Add(incomeControlSupply);
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
