using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorios.Infraestructure.Repositories
{
    public class AnalisysRepository : IAnalisysRepository
    {
        private readonly LaboratoriosContext _context;

        public AnalisysRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Analisys>> GetAnalisys()
        {
            try
            {
                var analisysdb = await _context.Analisys.ToListAsync();
                return analisysdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IEnumerable<AnalisysViewModel>> GetAnalisysAssigned()
        {
            try
            {
                var analisysdb = await _context.Area_Analisys.Include(x=>x.Analisys).Include(x => x.Area).Select(x => new AnalisysViewModel
                {
                    areaId = x.areaId,
                    analisysId = x.analisysId,
                    name = x.Analisys.name,
                    status = x.Analisys.status
                }).ToListAsync();

                return analisysdb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }


        public Analisys InsertAnalisys(Analisys analisys)
        {
            try
            {
                _context.Analisys.Add(analisys);
                _context.SaveChanges();

                return analisys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Analisys EditAnalisys(Analisys analisys)
        {
            try
            {
                _context.Entry(analisys).State = EntityState.Modified;
                _context.SaveChanges();

                return analisys;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task<IEnumerable<Analisys>> GetAnalisysByArea(int areaId)
        {
            try
            {
                var AreaAnalisys = await _context.Area_Analisys.Where(x => x.areaId == areaId).Select(x => x.analisysId).ToListAsync();
                var Analisys = await _context.Analisys.Where(x => AreaAnalisys.Contains(x.analisysId)).ToListAsync();
                return Analisys;
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }


        public async Task<IEnumerable<Analisys>> GetAnalisysByNotArea(int areaId)
        {
            try
            {
                var AreaAnalisys = await _context.Area_Analisys.Where(x => x.areaId == areaId).Select(x => x.analisysId).ToListAsync();
                var Analisys = await _context.Analisys.ToListAsync();
                Analisys.RemoveAll(x => AreaAnalisys.Contains(x.analisysId));
                return Analisys;
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }
        }

        public async Task<Area_Analisys> AssingAnalisys(int areaId, int analisysId, bool action)
        {
            try
            {
                var analisysDB = await _context.Area_Analisys.Where(x => x.analisysId == analisysId && x.areaId == areaId).FirstOrDefaultAsync();
                if (analisysDB == null)
                {
                    analisysDB = new Area_Analisys();
                    analisysDB.areaId = areaId;
                    analisysDB.analisysId = analisysId;
                    _context.Area_Analisys.Add(analisysDB);
                }
                else
                {
                    _context.Area_Analisys.Remove(analisysDB);
                    _context.Entry(analisysDB);
                }
                _context.SaveChanges();
                return analisysDB;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

        public async Task<IEnumerable<AnalisysControls>> GetAnalisysControls(int analisysId)
        {
            try
            {
                var controls = await _context.AnalisysControls.Where(x => x.analisysId == analisysId).ToListAsync();
                return controls;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }
        public async Task<AnalisysControls> AssingControls(AnalisysControls data)
        {
            try
            {
                if (data.analisysControlId == 0)
                {
                    await _context.AnalisysControls.AddAsync(data);
                }
                else
                {
                    _context.Entry(data).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return data;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

    }
}
