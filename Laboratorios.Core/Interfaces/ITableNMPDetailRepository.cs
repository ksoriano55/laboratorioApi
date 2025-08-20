using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface ITableNMPDetailRepository
    {
        Task<IEnumerable<ResultViewModel>> GetTables();
        Task<IEnumerable<TableNMPDetail>> GetTables(TableNMP tableNMP);
        TableNMPDetail InsertTableDetail(TableNMPDetail table);
        TableNMPDetail EditTableDetail(TableNMPDetail table);
        Task DeleteTableDetail(TableNMPDetail tableNMPDetail);

    }
}
