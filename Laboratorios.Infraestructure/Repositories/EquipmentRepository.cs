using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
namespace Laboratorios.Infraestructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly LaboratoriosContext _context;

        public EquipmentRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Equipment>> GetEquipment()
        {
            try
            {
                var equipments = await _context.Equipment.Include(x=>x.certificates).ToListAsync();
                return equipments;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Equipment InsertEquipment(Equipment equipment)
        {
            try
            {
                if (equipment.equipmentId > 0)
                {
                    _context.Entry(equipment).State = EntityState.Modified;
                }
                else
                {
                    _context.Equipment.Add(equipment);
                }
                _context.SaveChanges();

                return equipment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<IEnumerable<CVMProgram>> GetCVMProgram(/*int matrixId*/)
        {
            try
            {
                return await _context.CVMProgram.FromSqlRaw("EXECUTE dbo.CVMProgram" /*@matrixId=" + matrixId*/).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public EquipmentCertificate insert(EquipmentCertificate equipment)
        {
            try
            {
                var cloudinary = new Cloudinary(new Account("dtjqcrcb9", "272859698923212", "6n8Oexh-NRcn-UjqGxmBiuT0UXM"));
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(equipment.file.FileName, equipment.file.OpenReadStream()),
                    PublicId = equipment.file.FileName,
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                equipment.url = uploadResult.Url.ToString();
                _context.EquipmentCertificate.Add(equipment);
                _context.SaveChanges();

                return equipment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public EquipmentCertificate delete(EquipmentCertificate equipment)
        {
            try
            {
                _context.Remove(equipment);
                _context.SaveChanges();

                return equipment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
