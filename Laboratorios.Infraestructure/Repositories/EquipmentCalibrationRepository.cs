using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class EquipmentCalibrationRepository : IEquipmentCalibrationRepository
    {
        private readonly LaboratoriosContext _context;

        public EquipmentCalibrationRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EquipmentCalibration>> GetEquipmentCalibrations()
        {
            try
            {
                var calibrations = await _context.EquipmentCalibration.ToListAsync();
                return calibrations;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public EquipmentCalibration InsertEquipmentCalibration(EquipmentCalibration calibration)
        {
            try
            {
                if (calibration.calibrationId > 0)
                {
                    _context.Entry(calibration).State = EntityState.Modified;
                }
                else
                {
                    _context.EquipmentCalibration.Add(calibration);
                }
                _context.SaveChanges();

                return calibration;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
