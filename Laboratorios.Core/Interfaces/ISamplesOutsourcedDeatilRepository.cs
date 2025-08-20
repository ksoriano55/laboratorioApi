using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface ISamplesOutsourcedDetailRepository
    {
        Task<IEnumerable<SamplesOutsourcedDetail>> GetSamplesOutsourcedDetail(int outsourcedId);

        SamplesOutsourcedDetail InsertSamplesOutsourcedDetail(SamplesOutsourcedDetail category);
        Task DeleteSamplesOutsourcedDetail(SamplesOutsourcedDetail detalle);
    }
}
