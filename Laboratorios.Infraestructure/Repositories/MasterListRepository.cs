using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Xml.Linq;

namespace Laboratorios.Infraestructure.Repositories
{
    public class MasterListRepository : IMasterListRepository
    {
        private readonly LaboratoriosContext _context;
        private readonly FileManagerRepository file = new FileManagerRepository();

        private class Data
        {
            public JObject datos { get; set; }
            public IList<IFormFile> formFiles { get; set; }
            public string route { get; set; }
        }

        public MasterListRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MasterList>> GetMasterList()
        {
            try
            {
                var masterList = await _context.MasterList.Select(x => new MasterList
                {
                    masterDocumentId = x.masterDocumentId,
                    name = x.name,
                    code = x.code,
                    version = x.version,
                    approvalDate = x.approvalDate,
                    nOfPages = x.nOfPages,
                    latestVersion = x.latestVersion,
                    location = x.location,
                    referencesN = x.referencesN,
                    isSGC = x.isSGC,
                    replaceDocument = x.replaceDocument,
                    statusId = x.statusId,
                    categoryId = x.categoryId,
                    documentTypeId = x.documentTypeId,
                    path = x.path,
                    fileType = x.fileType,
                    countChanges = x.countChanges,
                    pendingDocument = x.pendingDocument,
                    statusDocument = x.statusDocument,
                    correlative = x.correlative,
                    includeChanges = _context.DocumentChangesLog.Where(y => y.masterDocumentId == x.masterDocumentId).Count(),
                }).ToListAsync();
                return masterList;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task insert(IFormFile? document, MasterList masterDocument, DocumentChangesLog documentChanges, string action, bool changeVersion, int userId)
        {
            try
            {
                bool cambioVersion = false;
                /*Start Validar si existe un documento con el mismo codigo */
                if (masterDocument.code != null && masterDocument.code != "")
                {
                    var exists = new MasterList();
                    if (masterDocument.masterDocumentId > 0)
                    {
                        exists = _context.MasterList.Where(x => x.code == masterDocument.code && x.masterDocumentId != masterDocument.masterDocumentId && x.statusId != 2).FirstOrDefault();
                    }
                    else
                    {
                        exists = _context.MasterList.Where(x => x.code == masterDocument.code && x.statusId != 2).FirstOrDefault();
                    }
                    if (exists != null)
                    {
                        throw new InvalidOperationException("Ya existe un documento con este código asignado");
                    }
                }
                /*End Validar si existe un documento con el mismo codigo */

                if (action != "borrador" && action != "pendiente" && masterDocument.isOriginal == true)
                {
                    /*Start Generar Codigo por Categoría */
                    if (masterDocument.code == null || masterDocument.code == "")
                    {
                        //Validamos si existe un correlativo obsoleto para reutilizar
                        var code = _context.MasterList.AsNoTracking().Where(x => x.categoryId == masterDocument.categoryId && x.statusId == 2).FirstOrDefault();
                        if (code != null)
                        {
                            masterDocument.code = code.code;
                        }
                        else
                        {
                            var category = _context.Category.AsNoTracking().Where(x => x.categoryId == masterDocument.categoryId).FirstOrDefault();
                            masterDocument.code = category.code + "-" + (category.lastIdentity + 1);
                            category.lastIdentity = category.lastIdentity + 1;
                            _context.Entry(category).State = EntityState.Modified;
                        }
                    }
                    /*End Generar Codigo por Categoría */
                    /* Star Generar Versión del documento*/
                    if (masterDocument.version == null || masterDocument.version == "")
                    {
                        masterDocument.version = "1";
                        masterDocument.latestVersion = "N/A";
                    }
                    else
                    {
                        //Validamos si el conteo de cambios es el necesario para cambiar de versión
                        // o si el usuario acepto cambiar de versión
                        var changes = _context.DocumentType.Where(x => x.documentTypeId == masterDocument.documentTypeId).FirstOrDefault();
                        var countchanges = 0;
                        if (masterDocument.masterDocumentId > 0)
                        {
                            var data = _context.MasterList.AsNoTracking().Where(x => x.masterDocumentId == masterDocument.masterDocumentId).FirstOrDefault();
                            countchanges = data == null ? 0 : (int)(data.countChanges == null ? 0 : data.countChanges);
                        }
                        countchanges = (int)(countchanges + (masterDocument.countChanges == null ? 0 : masterDocument.countChanges));
                        if (changeVersion || countchanges >= changes?.NCCV)
                        {
                            cambioVersion = true;
                            //Si el documento cambia de versión el conteo cambios se vuelve cero
                            masterDocument.countChanges = 0;
                            try
                            {
                                masterDocument.latestVersion = masterDocument.version;
                                masterDocument.version = (Convert.ToInt32(masterDocument.version) + 1).ToString();
                                masterDocument.approvalDate = DateTime.Now;

                                //Se llena información requerida para el registro de cambios
                                if (documentChanges != null)
                                {
                                    documentChanges.approvalDate = DateTime.Now;
                                    documentChanges.assignmentDate = DateTime.Now;
                                    documentChanges.newVersion = Convert.ToInt32(masterDocument.version);
                                    documentChanges.lastestVersion = Convert.ToInt32(masterDocument.version) - 1;
                                }
                            }
                            catch (Exception e)
                            {
                                throw new InvalidOperationException("Este documento no puede cambiar de versión");
                            }
                        }
                    }
                    /* End Generar Versión del documento*/
                }

                /* Start Guardar Documento de Lista Maestra*/
                if (action == "save")
                {
                    if (masterDocument.isOriginal && masterDocument.masterDocumentId == 0)
                    {
                        masterDocument.statusId = 1;
                        //Nuevo Documento de la lista Maestra
                        FileInfo fileInfo = new FileInfo(masterDocument.path);
                        if (fileInfo.Extension == "" && document != null)
                        {
                            masterDocument.path = masterDocument.path + @"\" + document.FileName;
                        }
                        if (document != null)
                        {
                            Task.Run(() => deleteDocument(masterDocument, document)).Wait();
                            Task.Run(() => uploadDocument(masterDocument, document)).Wait();
                            //await uploadDocument(masterDocument, document);
                        }
                        _context.MasterList.Add(masterDocument);

                    }
                    if (masterDocument.isOriginal && masterDocument.masterDocumentId > 0)
                    {
                        //Actualización de registro de Lista Maestra y documento en el servidor. 
                        masterDocument.statusId = 1;
                        if (document != null)
                        {
                            if (cambioVersion)
                            {
                                await replaceDocument(masterDocument, document);
                            }
                            else
                            {
                                Task.Run(() => deleteDocument(masterDocument, document)).Wait();
                            }
                            Task.Run(() => uploadDocument(masterDocument, document)).Wait();
                            //await uploadDocument(masterDocument, document);
                        }
                        _context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    if (!masterDocument.isOriginal && masterDocument.masterDocumentId == 0)
                    {
                        //Guardado de documento normal en el servidor.
                        if (document != null)
                        {
                            Task.Run(() => deleteDocument(masterDocument, document)).Wait();
                            Task.Run(() => uploadDocument(masterDocument, document)).Wait();
                        }
                        //await uploadDocument(masterDocument, document);
                    }
                    if (!masterDocument.isOriginal && masterDocument.masterDocumentId > 0)
                    {
                        //Documento pasa a obsoleto
                        masterDocument.statusId = 2;
                        _context.Entry(masterDocument).State = EntityState.Modified;
                    }
                }
                else if (action == "borrador")
                {
                    if (document == null)
                    {
                        masterDocument.statusId = 5;
                        //_context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    else
                    {
                        string sfdtText = "";
                        Syncfusion.EJ2.DocumentEditor.WordDocument documentWord = Syncfusion.EJ2.DocumentEditor.WordDocument.Load(document.OpenReadStream(), Syncfusion.EJ2.DocumentEditor.FormatType.Docx);
                        sfdtText = Newtonsoft.Json.JsonConvert.SerializeObject(documentWord);
                        documentWord.Dispose();
                        masterDocument.pendingDocument = sfdtText;
                        masterDocument.statusDocument = 5;
                        //return sfdtText;
                    }
                    if (masterDocument.masterDocumentId > 0)
                    {
                        _context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    else
                    {
                        masterDocument.statusId = 5;
                        _context.MasterList.Add(masterDocument);
                    }
                }
                else if (action == "pendiente")
                {
                    if (document == null)
                    {
                        masterDocument.statusId = 3;
                        //_context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    else
                    {
                        string sfdtText = "";
                        Syncfusion.EJ2.DocumentEditor.WordDocument documentWord = Syncfusion.EJ2.DocumentEditor.WordDocument.Load(document.OpenReadStream(), Syncfusion.EJ2.DocumentEditor.FormatType.Docx);
                        sfdtText = Newtonsoft.Json.JsonConvert.SerializeObject(documentWord);
                        documentWord.Dispose();
                        masterDocument.pendingDocument = sfdtText;
                        masterDocument.statusDocument = 3;
                        //return sfdtText;
                    }
                    if (masterDocument.masterDocumentId > 0)
                    {
                        if (masterDocument.pendingDocument != null)
                        {
                            masterDocument.statusDocument = 3;
                        }
                        _context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    else
                    {
                        masterDocument.statusId = 3;
                        if (masterDocument.pendingDocument != null)
                        {
                            masterDocument.statusDocument = 3;
                        }
                        _context.MasterList.Add(masterDocument);
                    }
                }
                else if (action == "obsoleto")
                {
                    masterDocument.statusId = 2;
                    if (masterDocument.masterDocumentId > 0)
                    {
                        _context.Entry(masterDocument).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.MasterList.Add(masterDocument);
                    }
                }
                else
                {
                    masterDocument.statusId = 1;
                    if (document != null) {
                        if (document != null)
                        {
                            await replaceDocument(masterDocument, document);
                            await uploadDocument(masterDocument, document);
                        }
                        masterDocument.statusDocument = null;
                        masterDocument.pendingDocument = null;
                        masterDocument.approvalDate = DateTime.Now;
                    }
                    _context.Entry(masterDocument).State = EntityState.Modified;

                }
                if (documentChanges != null)
                {
                    //Only date 
                    documentChanges.reviewDate = DateTime.Now;
                    _context.DocumentChangesLog.Add(documentChanges);
                }
                var user = _context.User.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if(user != null)
                {
                    _context.LogHistory.Add(new LogHistory{userName=user.Result.UserName,action="Se han realizado cambios en el documento "+masterDocument.name, date= DateTime.Now});
                }
                _context.SaveChanges();
                /* End Guardar Documento de Lista Maestra*/

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task/*<Task>*/ uploadDocument(MasterList masterDocument, IFormFile files)
        {
            try
            {
                //await deleteDocument(masterDocument, files);
                Data data = getData(masterDocument.path, files, "delete");
                var guardado = file.Upload(data.route, data.formFiles, "save");
                await Task.Delay(5000);
                //if (guardado != null)
                //{
                //    return Task<>.CompletedTask;
                //}
                //throw new InvalidOperationException("No se logro Guardar");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }

        private Data getData(string path, IFormFile file, string action, MasterList? masterDocument = null)
        {
            FileInfo fileInfo = new FileInfo(path);
            IList<IFormFile> formFiles = new List<IFormFile>();
            var route = "";
            if (fileInfo.Extension == "")
            {
                route = @"/" + fileInfo.FullName.Replace("C:\\", "") + @"/";
            }
            else
            {
                if (fileInfo.DirectoryName != null)
                    route = @"/" + fileInfo.DirectoryName.Replace("C:\\", "") + @"/";
            }
            formFiles.Add(file);
            JObject data = new JObject();
            data["action"] = action;
            if (action != "rename")
            {
                data["isFile"] = true;
                data["names"] = JToken.FromObject(new string[] { file.FileName });
            }
            data["path"] = route;
            if (action == "move")
            {
                data["targetPath"] = @"/Obsoletos/";
            }
            if (action == "rename")
            {
                data["name"] = file.FileName;
                var split = file.FileName.Split(".");
                data["newName"] = split[0] + "V" + masterDocument.latestVersion + "." + split[1];
            }
            return (new Data() { datos = data, formFiles = formFiles, route = route });
        }

        public Task replaceDocument(MasterList masterDocument, IFormFile document)
        {
            try
            {
                Data data = getData(masterDocument.path, document, "move");
                var retorno = file.GetFileManager(data.datos);
                if (retorno != null)
                {
                    data = getData(@"/Obsoletos/", document, "rename", masterDocument);
                    retorno = file.GetFileManager(data.datos);
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<Task> deleteDocument(MasterList masterDocument, IFormFile document)
        {
            try
            {
                Data data = getData(masterDocument.path, document, "delete");
                var retorno = file.Delete(data.datos);

                //var retorno = file.GetFileManager(data.datos);
                await Task.Delay(5000);
                if (retorno != null)
                {
                    //await Task.Delay(5000);
                    return Task.CompletedTask;
                }
                throw new InvalidOperationException("No se guardo");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task<IEnumerable<LogHistory>> GetLogHistory()
        {
            try
            {
                var logs = await _context.LogHistory.OrderByDescending(c=>c.logHistoryId).ToListAsync();
                return logs;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
