using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IDocumentChangesLogRepository
    {
        Task<IEnumerable<DocumentChangesLog>> GetDocumentChangesLog();
    }
}
