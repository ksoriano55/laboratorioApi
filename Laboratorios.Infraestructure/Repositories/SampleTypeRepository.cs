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
    public class SampleTypeRepository : ISampleTypeRepository
    {
        private readonly LaboratoriosContext _context;

        public SampleTypeRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SampleType>> GetSampleType()
        {
            try
            {
                var sampleTypedb = await _context.SampleType.ToListAsync();
                return sampleTypedb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public SampleType InsertSampleType(SampleType sampleType)
        {
            try
            {
                _context.SampleType.Add(sampleType);
                _context.SaveChanges();

                return sampleType;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public SampleType EditSampleType(SampleType sampleType)
        {
            try
            {
                _context.Entry(sampleType).State = EntityState.Modified;
                _context.SaveChanges();

                return sampleType;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
