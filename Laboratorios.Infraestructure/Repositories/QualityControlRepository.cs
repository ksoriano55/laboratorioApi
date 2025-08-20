using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class QualityControlRepository: IQualityControlRepository
    {
        private readonly LaboratoriosContext _context;

        public QualityControlRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QualityControl>> GetQualityControls()
        {
            try
            {
                var controls = await _context.QualityControl.ToListAsync();
                return controls;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public QualityControl InsertQualityControl(QualityControl qualityControl)
        {
            try
            {
                if (qualityControl.qualityControlId > 0)
                {
                    _context.Entry(qualityControl).State = EntityState.Modified;
                }
                else
                {
                    _context.QualityControl.Add(qualityControl);
                }
                _context.SaveChanges();

                return qualityControl;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
