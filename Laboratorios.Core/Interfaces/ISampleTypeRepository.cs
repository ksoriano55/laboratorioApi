using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface ISampleTypeRepository
    {
        Task<IEnumerable<SampleType>> GetSampleType();

        SampleType InsertSampleType(SampleType sampleType);
        SampleType EditSampleType(SampleType sampleType);
    }
}
