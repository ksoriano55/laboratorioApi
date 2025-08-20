using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface IPackageRepository
    {
       Task<IEnumerable<Package>> GetPackages();
       Task<IEnumerable<PackageDetail>> GetPackageDetail(int id);
        Task<Package> CreateAsync(Package package);
       Task<PackageDetail> CreateDetailAsync(PackageDetail packageDetail);
       Task<PackageDetail> DeleteDetailAsync(int id);
    }
}
