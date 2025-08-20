using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class MasterList
    {
        [Key]
        public int masterDocumentId { get; set; }
        public string name { get; set; } = String.Empty;
        public string code { get; set; } = String.Empty;
        public string? version { get; set; }
        public DateTime? approvalDate { get; set; }
        public int? nOfPages { get; set; }
        public string? latestVersion { get; set; }
        public string? location { get; set; }
        public string? referencesN { get; set; }
        public bool isSGC { get; set; }
        public string? replaceDocument { get; set; }
        public int statusId { get; set; }
        public int categoryId { get; set; }
        public int documentTypeId { get; set; }
        public string? path { get; set; }
        public string? fileType { get; set; }
        public int? countChanges { get; set; }
        public string? pendingDocument { get; set; }
        public int? statusDocument { get; set; }
        public string? correlative { get; set; }
        [NotMapped]
        public bool isOriginal { get; set; }
        [NotMapped]
        public int includeChanges { get; set; }
    }
}
