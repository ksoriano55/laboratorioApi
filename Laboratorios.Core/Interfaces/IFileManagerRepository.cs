using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Interfaces
{
    public interface IFileManagerRepository
    {
        object GetFileManager(JObject data);
        object GetImage(string path);
        object Download (string downloadInput);
        object Upload(string path, IList<IFormFile> uploadFiles, string action);
        object getDocument(string path, bool isPDF);
        string ReadAllBytes(Stream instream);

    }
}
