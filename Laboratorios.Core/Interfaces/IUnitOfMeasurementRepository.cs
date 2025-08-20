using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IUnitOfMeasurementRepository
    {
        Task<IEnumerable<UnitOfMeasurement>> GetUnitOfMeasurement();
        UnitOfMeasurement InsertUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement);
        UnitOfMeasurement EditUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement);
    }
}
