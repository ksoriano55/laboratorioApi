using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface ISamplesOutsourcedRepository
    {
        Task<IEnumerable<SamplesOutsourced>> GetSamplesOutsourced(int year);

        SamplesOutsourced InsertSamplesOutsourced(SamplesOutsourced category);
    }
}
