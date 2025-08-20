using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IMasterListRepository
    {
        Task<IEnumerable<MasterList>> GetMasterList();
        Task<IEnumerable<LogHistory>> GetLogHistory();
        Task insert(IFormFile document, MasterList masterDocument,DocumentChangesLog documentChanges, string action, bool changeVersion, int userId);
    }
}
