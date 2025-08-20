using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Laboratorios.Infraestructure.Repositories
{
    public class DocumentChangesLogRepository : IDocumentChangesLogRepository
    {
        private readonly LaboratoriosContext _context;

        public DocumentChangesLogRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DocumentChangesLog>> GetDocumentChangesLog()
        {
            try
            {
                var document = await _context.DocumentChangesLog.ToListAsync();
                return document;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
