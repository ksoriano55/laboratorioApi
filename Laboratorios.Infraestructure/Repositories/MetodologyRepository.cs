using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorios.Infraestructure.Repositories
{
    public class MetodologyRepository : IMetodologyRepository
    {
        private readonly LaboratoriosContext _context;

        public MetodologyRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Metodology>> GetMetodology()
        {
            try
            {
                var metodologydb = await _context.Metodology.ToListAsync();
                return metodologydb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Metodology InsertMetodology(Metodology metodology)
        {
            try
            {
                _context.Metodology.Add(metodology);
                _context.SaveChanges();

                return metodology;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Metodology EditMetodology(Metodology metodology)
        {
            try
            {
                _context.Entry(metodology).State = EntityState.Modified;
                _context.SaveChanges();

                return metodology;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
