using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IIncomeControlSupplyDetailRepository
    {
        Task<IEnumerable<IncomeControlSupplyDetail>>GetIncomeControlSupplyDetail(int incomeControlId);

        IncomeControlSupplyDetail InsertIncomeControlSupplyDetail(IncomeControlSupplyDetail incomeControlDetailSupply);
    }
}
