using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IMetodologyRepository
    {
        Task<IEnumerable<Metodology>> GetMetodology();
        Metodology InsertMetodology(Metodology metodology);
        Metodology EditMetodology(Metodology metodology);
    }
}
