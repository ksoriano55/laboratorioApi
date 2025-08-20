using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Laboratorios.Infraestructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly LaboratoriosContext _context;

        public AreaRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Area>> GetArea()
        {
            try
            {
                var areadb = await _context.Area.ToListAsync();
                return areadb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<AreaViewModel>> GetAreaAssigned()
        {
            try
            {
                var areadb = await _context.Matrix_Area.Include(x => x.Area).Include(x => x.Matrix).Select(x => new AreaViewModel
                {
                    areaId = x.areaId,
                    matrixId = x.matrixId,
                    name = x.Area.name,
                    status = x.Area.status
                }).ToListAsync();
                return areadb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Area InsertArea(Area area)
        {
            try
            {
                _context.Area.Add(area);
                _context.SaveChanges();

                return area;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Area EditArea(Area area)
        {
            try
            {
                _context.Entry(area).State = EntityState.Modified;
                _context.SaveChanges();

                return area;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task<IEnumerable<Area>> GetAreaByMatrix(int matrixId)
        {
            var MatrixArea = await _context.Matrix_Area.Where(x => x.matrixId == matrixId).Select(x => x.areaId).ToListAsync();
            var Area = await _context.Area.Where(x => MatrixArea.Contains(x.areaId)).ToListAsync();
            return Area;
        }


        public async Task<IEnumerable<Area>> GetAreaByNotMatrix(int matrixId)
        {
            var MatrixArea = await _context.Matrix_Area.Where(x => x.matrixId == matrixId).Select(x => x.areaId).ToListAsync();
            var Area = await _context.Area.ToListAsync();
            Area.RemoveAll(x => MatrixArea.Contains(x.areaId));
            return Area;
        }

        public async Task<Matrix_Area> AssingArea(int matrixId, int areaId, bool action)
        {
            try
            {
                var areaDB = await _context.Matrix_Area.Where(x => x.areaId == areaId && x.matrixId == matrixId).FirstOrDefaultAsync();
                if (areaDB == null)
                {
                    areaDB = new Matrix_Area();
                    areaDB.matrixId = matrixId;
                    areaDB.areaId = areaId;
                    _context.Matrix_Area.Add(areaDB);
                }
                else
                {
                    _context.Matrix_Area.Remove(areaDB);
                    _context.Entry(areaDB);
                }
                _context.SaveChanges();
                return areaDB;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }
    }
}
