using Laboratorios.Core.Interfaces;
using Newtonsoft.Json.Linq;
using Syncfusion.Blazor.FileManager;
using Newtonsoft.Json;
using Syncfusion.Blazor.FileManager.Base;
using Microsoft.AspNetCore.Http;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace Laboratorios.Infraestructure.Repositories
{
    public class FileManagerRepository : IFileManagerRepository
    {
        public PhysicalFileProvider operation;
        public string basePath;
        //private string route = @"\\10.163.1.4\Usuarios\Recursos";
        //private string route = @"\\10.163.1.4\Usuarios\Recursos\SGC";
        private string route = @"\\10.163.1.4\Usuarios\SGC";
        //private string route = @"\\172.16.146.65\C$\Users\SGC";

        public FileManagerRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.basePath = hostingEnvironment.ContentRootPath;
            this.operation = new Syncfusion.Blazor.FileManager.PhysicalFileProvider();
            this.operation.RootFolder(route);
        }
        public FileManagerRepository()
        {
            this.operation = new Syncfusion.Blazor.FileManager.PhysicalFileProvider();
            this.operation.RootFolder(route);
        }

        public object? GetFileManager(JObject data)
        {
            try
            {
                FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(data.ToString());
                //args.Path = Path;
                if (args.Action == "delete" || args.Action == "rename")
                {
                    if ((args.TargetPath == null) && (args.Path == ""))
                    {
                        FileManagerResponse response = new FileManagerResponse();
                        response.Error = new ErrorDetails { Code = "401", Message = "Restricted to modify the root folder." };
                        return this.operation.ToCamelCase(response);
                    }
                }
                System.Net.ServicePointManager.Expect100Continue = false;

                switch (args.Action)
                {
                    case "read":
                        // reads the file(s) or folder(s) from the given path.
                        return this.operation.ToCamelCase(this.operation.GetFiles(args.Path, args.ShowHiddenItems));
                    case "delete":
                        // deletes the selected file(s) or folder(s) from the given path.
                        return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
                    case "copy":
                        // copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return this.operation.ToCamelCase(this.operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                    case "move":
                        // cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return this.operation.ToCamelCase(this.operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                    case "details":
                        // gets the details of the selected file(s) or folder(s).
                        return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names, args.Data));
                    case "create":
                        // creates a new folder in a given path.
                        return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
                    case "search":
                        // gets the list of file(s) or folder(s) from a given path based on the searched key string.
                        return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
                    case "rename":
                        // renames a file or folder.
                        return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }            
        }

        public object GetImage(string path)
        {
            return this.operation.GetImage(path, null, false, null, null);
        }
        public object Download(string downloadInput)
        {
            FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
            return this.operation.Download(args.Path, args.Names, args.Data);
        }

        public object Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            FileManagerResponse uploadResponse;
            uploadResponse = operation.Upload(path, uploadFiles, action, null);
            if (uploadResponse.Error != null)
            {
                throw new InvalidOperationException(uploadResponse.Error.Message);
            }
            return new object();
        }

        public object getDocument(string path, bool isPDF)
        {
            try
            {
                FileInfo fi = new FileInfo(path);
                string[] Names = { fi.Name };
                var route = @"/" + fi.DirectoryName.Replace("C:\\", "") + @"/";
                var file = this.operation.Download(route, Names, new FileManagerDirectoryContent());
                Stream stream = file.FileStream;
                if (fi.Extension.Contains(".pdf"))
                {
                    var data = ReadAllBytes(stream);
                    stream.Close();
                    return data;
                }
                else
                {    
                    if (isPDF)
                    {
                        Syncfusion.DocIO.DLS.WordDocument newdocument = new Syncfusion.DocIO.DLS.WordDocument();
                        newdocument.Document.Open(stream, Syncfusion.DocIO.FormatType.Docx);
                        DocIORenderer render = new DocIORenderer();
                        PdfDocument pdfDocument = render.ConvertToPDF(newdocument);
                        newdocument.Dispose();
                        render.Dispose();
                        MemoryStream outputStream = new MemoryStream();
                        pdfDocument.Save(outputStream);
                        pdfDocument.Dispose();
                        stream.Close();
                        return ReadAllBytes(outputStream);
                    }
                    string sfdtText = "";
                    Syncfusion.EJ2.DocumentEditor.WordDocument document = Syncfusion.EJ2.DocumentEditor.WordDocument.Load(stream, Syncfusion.EJ2.DocumentEditor.FormatType.Docx);
                    sfdtText = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                    document.Dispose();
                    stream.Close();
                    return sfdtText;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ReadAllBytes(Stream instream)
        {
            byte[]? result = null;
            if (instream is MemoryStream)
            {
                result = ((MemoryStream)instream).ToArray();
                return Convert.ToBase64String(result);
            }

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
                return Convert.ToBase64String(result);
            }
        }

        public object Delete(JObject data)
        {
            FileManagerResponse uploadResponse;
            FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(data.ToString());

            uploadResponse = operation.Delete(args.Path, args.Names);
            if (uploadResponse.Error != null)
            {
                throw new InvalidOperationException(uploadResponse.Error.Message);
            }
            return new object();
        }
    }
}
