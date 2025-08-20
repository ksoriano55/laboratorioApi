using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.Entities
{
    public class DocumentChangesLog
    {
       [Key]
       public int changeLogId { get; set; }
       public int masterDocumentId { get; set; }
       public DateTime reviewDate { get; set; }
       public int? lastestVersion { get; set; }
       public string changes { get; set; }
       public string userchanges { get; set; }
       public DateTime? approvalDate { get; set; }
       public DateTime? assignmentDate { get; set; }
       public int? newVersion { get; set; }
       public string assignedTo { get; set; }
    }
}
