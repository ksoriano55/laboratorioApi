using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;

        public PackageController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetPackages()
        {
            try
            {
                var data = await _packageRepository.GetPackages();
                return Ok(data);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("GetDetail")]
        public async Task<IActionResult> GetPackageDetail(int id)
        {
            try
            {
                var data = await _packageRepository.GetPackageDetail(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync(Package package)
        {
            try
            {
                var data = await _packageRepository.CreateAsync(package);
                return Ok(data);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insertDetail")]
        public async Task<IActionResult> CreateDetailAsync(PackageDetail packageDetail)
        {
            try
            {
                var data = await _packageRepository.CreateDetailAsync(packageDetail);
                return Ok(data);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpDelete, Authorize]
        public async Task<IActionResult> DeleteDetailAsync(int id)
        {
            try
            {
                var data = await _packageRepository.DeleteDetailAsync(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
