using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly LaboratoriosContext _context;

        public LocationRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Location>> GetLocation()
        {
            try
            {
                var locations = await _context.Location.ToListAsync();
                return locations;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Location InsertLocation(Location location)
        {
            try
            {
                if (location.locationId > 0)
                {
                    _context.Entry(location).State = EntityState.Modified;
                }
                else
                {
                    _context.Location.Add(location);
                }
                _context.SaveChanges();

                return location;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
