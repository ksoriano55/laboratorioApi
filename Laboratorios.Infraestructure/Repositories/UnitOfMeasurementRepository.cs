using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorios.Infraestructure.Repositories
{
    public class UnitOfMeasurementRepository : IUnitOfMeasurementRepository
    {
        private readonly LaboratoriosContext _context;

        public UnitOfMeasurementRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UnitOfMeasurement>> GetUnitOfMeasurement()
        {
            try
            {
                var unitOfMeasurementdb = await _context.UnitOfMeasurement.ToListAsync();
                return unitOfMeasurementdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public UnitOfMeasurement InsertUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
        {
            try
            {
                _context.UnitOfMeasurement.Add(unitOfMeasurement);
                _context.SaveChanges();

                return unitOfMeasurement;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public UnitOfMeasurement EditUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
        {
            try
            {
                _context.Entry(unitOfMeasurement).State = EntityState.Modified;
                _context.SaveChanges();

                return unitOfMeasurement;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
