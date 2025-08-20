using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IDocumentTypeRepository
    {
        Task<IEnumerable<DocumentType>> GetDocumentType();

        DocumentType InsertDocumentType(DocumentType documentType);
        Task saveDocument(IFormFile document, int documentTypeId);

    }
}
