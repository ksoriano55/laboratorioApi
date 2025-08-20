using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IVendorAssessmentRepository
    {
        Task<IEnumerable<VendorAssessment>> GetAllVendorAssessment();
        Task<IEnumerable<VendorAssessment>> GetVendorAssessment(int id);

        VendorAssessment InsertVendorAssessment(VendorAssessment category);
    }
}
