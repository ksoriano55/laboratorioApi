using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> GetArea();
        Task<IEnumerable<AreaViewModel>> GetAreaAssigned();
        Area InsertArea(Area area);
        Area EditArea(Area area);

        Task<IEnumerable<Area>> GetAreaByMatrix(int matrixId);
        Task<IEnumerable<Area>> GetAreaByNotMatrix(int matrixId);
        Task<Matrix_Area> AssingArea(int matrixId, int areaId, bool action);
    }
}
