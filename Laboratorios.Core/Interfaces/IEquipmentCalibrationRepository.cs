using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface IEquipmentCalibrationRepository
    {
        Task<IEnumerable<EquipmentCalibration>> GetEquipmentCalibrations();

        EquipmentCalibration InsertEquipmentCalibration(EquipmentCalibration calibration);
    }
}
