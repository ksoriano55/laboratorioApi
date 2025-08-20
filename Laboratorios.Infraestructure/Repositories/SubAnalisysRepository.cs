using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class SubAnalisysRepository : ISubAnalisysRepository
    {
        private readonly LaboratoriosContext _context;

        public SubAnalisysRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SubAnalisys>> GetSubAnalisys()
        {
            try
            {
                var SubAnalisyss = await _context.SubAnalisys.ToListAsync();
                return SubAnalisyss;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public SubAnalisys insert(SubAnalisys SubAnalisys)
        {
            try
            {
                if (SubAnalisys.subanalisysId > 0)
                {
                    _context.Entry(SubAnalisys).State = EntityState.Modified;
                }
                else
                {
                    _context.SubAnalisys.Add(SubAnalisys);
                }
                _context.SaveChanges();

                return SubAnalisys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
