using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Newtonsoft.Json.Linq;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileManagerRepository _fileManagerRepository;

        public FilesController(IFileManagerRepository fileManagerRepository)
        {
            _fileManagerRepository = fileManagerRepository;
        }

        [HttpPost/*, Authorize*/]
        public object? GetFileManager([FromBody] JObject data)
        {
            try
            {
               var files = _fileManagerRepository.GetFileManager(data);
               return Ok(files);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpGet]
        [Route("GetImage")]
        public object? GetImage(string? path)
        {
            try
            {
                var image = _fileManagerRepository.GetImage(path);
                return image;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost]
        [Route("Download")]
        public object? Download([FromForm]string downloadInput)
        {
            try
            {
                var file = _fileManagerRepository.Download(downloadInput);
                return file;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost]
        [Route("Upload")]
        public object? Upload([FromForm] string? path, IList<IFormFile>? uploadFiles, [FromForm] string? action)
        {
            try
            {
                var files = _fileManagerRepository.Upload(path, uploadFiles, action);
                return files;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDocument")]
        public object? GetDocument(string path, bool isPDF)
       {
            try
            {
                var files = _fileManagerRepository.getDocument(path, isPDF);
                return files;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
