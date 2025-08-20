using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorios.Infraestructure.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly LaboratoriosContext _context;

        public DocumentTypeRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DocumentType>> GetDocumentType()
        {
            try
            {
                var documentTypes = await _context.DocumentType.ToListAsync();
                return documentTypes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public DocumentType InsertDocumentType(DocumentType documentType)
        {
            try
            {
                if (documentType.documentTypeId > 0)
                {
                    _context.Entry(documentType).State = EntityState.Modified;
                }
                else
                {
                    _context.DocumentType.Add(documentType);
                }
                _context.SaveChanges();

                return documentType;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Task saveDocument(IFormFile? document, int documentTypeId)
        {
            var documentType = _context.DocumentType.AsNoTracking().Where(x => x.documentTypeId == documentTypeId).FirstOrDefault();
            if(documentType != null)
            {
                if (document != null)
                {
                    string sfdtText = "";
                    Syncfusion.EJ2.DocumentEditor.WordDocument documentWord = Syncfusion.EJ2.DocumentEditor.WordDocument.Load(document.OpenReadStream(), Syncfusion.EJ2.DocumentEditor.FormatType.Docx);
                    sfdtText = Newtonsoft.Json.JsonConvert.SerializeObject(documentWord);
                    documentWord.Dispose();
                    documentType.defaultDocument = sfdtText;
                }
                _context.Entry(documentType).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            throw new InvalidOperationException("Hubo un error");
        }

    }
}
