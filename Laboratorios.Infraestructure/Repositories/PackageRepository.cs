using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class PackageRepository: IPackageRepository
    {
        private readonly LaboratoriosContext _context;

        public PackageRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Package>> GetPackages()
        {
           return await _context.Package.ToListAsync();
        }

        public async Task<IEnumerable<PackageDetail>> GetPackageDetail(int id)
        {
            return await _context.PackageDetail.Where(x=>x.packageId == id).ToListAsync();
        }

        public async Task<Package> CreateAsync(Package package)
        {
            if (package.id > 0)
            {
                _context.Entry(package).State = EntityState.Modified;
            }
            else {
                _context.Add(package);
            }
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<PackageDetail> CreateDetailAsync(PackageDetail packageDetail)
        {
            _context.Add(packageDetail); 
            await _context.SaveChangesAsync();
            return packageDetail;
        }

        public async Task<PackageDetail> DeleteDetailAsync(int id)
        {
            var packageDetail = await _context.PackageDetail.AsNoTracking().Where(x => x.id == id).FirstOrDefaultAsync(); 
            if(packageDetail != null)
                _context.Remove(packageDetail);
            await _context.SaveChangesAsync();
            return packageDetail ?? new PackageDetail();
        }
    }
}
