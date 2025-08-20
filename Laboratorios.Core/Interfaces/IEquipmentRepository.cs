using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetEquipment();
        Task<IEnumerable<CVMProgram>> GetCVMProgram();
        Equipment InsertEquipment(Equipment equipment);
        EquipmentCertificate insert (EquipmentCertificate equipmentCertificate);
        EquipmentCertificate delete (EquipmentCertificate equipmentCertificate);
    }
}
