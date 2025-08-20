using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class VendorAssessmentRepository : IVendorAssessmentRepository
    {
        private readonly LaboratoriosContext _context;

        public VendorAssessmentRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VendorAssessment>> GetAllVendorAssessment()
        {
            try
            {
                var assessment = await _context.VendorAssessment.ToListAsync();
                return assessment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IEnumerable<VendorAssessment>> GetVendorAssessment(int id)
        {
            try
            {
                var assessment = await _context.VendorAssessment.Where(x=>x.vendorId==id).ToListAsync();
                return assessment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public VendorAssessment InsertVendorAssessment(VendorAssessment assessment)
        {
            try
            {
                if (assessment.assessmentId > 0)
                {
                    _context.Entry(assessment).State = EntityState.Modified;
                }
                else
                {
                    _context.VendorAssessment.Add(assessment);
                }
                _context.SaveChanges();

                return assessment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
