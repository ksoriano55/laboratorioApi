using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class ShelvesRepository : IShelvesRepository
    {
        private readonly LaboratoriosContext _context;

        public ShelvesRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shelves>> GetShelves()
        {
            try
            {
                var Shelvess = await _context.Shelves.ToListAsync();
                return Shelvess;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Shelves InsertShelves(Shelves Shelves)
        {
            try
            {
                if (Shelves.id > 0)
                {
                    _context.Entry(Shelves).State = EntityState.Modified;
                }
                else
                {
                    _context.Shelves.Add(Shelves);
                }
                _context.SaveChanges();

                return Shelves;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
