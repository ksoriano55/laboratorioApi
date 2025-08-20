using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class VendorsRepository : IVendorsRepository
    {
        private readonly LaboratoriosContext _context;

        public VendorsRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Vendors>> GetVendors(int? date, int? assessmentType)
        {
            try
            {
                var vendors = await _context.Vendors.ToListAsync();
                if(date!=null && assessmentType != null)
                {
                    var assessments = await _context.VendorAssessment.Where(x=>x.assessmentDate.Year==date && x.assessmentTypeId==assessmentType).Select(x => x.vendorId).Distinct().ToListAsync();
                    return vendors.Where(x => assessments.Contains(x.vendorId)).ToList();
                }
                if (date != null)
                {
                    var assessments = await _context.VendorAssessment.Where(x => x.assessmentDate.Year == date).Select(x=>x.vendorId).Distinct().ToListAsync();
                    return vendors.Where(x => assessments.Contains(x.vendorId)).ToList();

                }
                if (assessmentType != null)
                {
                    var assessments = await _context.VendorAssessment.Where(x => x.assessmentTypeId == assessmentType).Select(x => x.vendorId).Distinct().ToListAsync();
                    return vendors.Where(x => assessments.Contains(x.vendorId)).ToList();
                }
                return vendors;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Vendors InsertVendors(Vendors vendors)
        {
            try
            {
                if (vendors.vendorId > 0)
                {
                    _context.Entry(vendors).State = EntityState.Modified;
                }
                else
                {
                    _context.Vendors.Add(vendors);
                }
                _context.SaveChanges();

                return vendors;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
