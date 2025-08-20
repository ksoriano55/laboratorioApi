using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.Entities
{
    public class ReportHistory
    {
        [Key]
        public int reportId { get; set; }
        public int incomeId { get; set; }
        public string? correlativeFQ { get; set; }
        public string? correlativeMB { get; set; }
        public DateTime? draftDate { get; set; }
        public DateTime? checkMB { get; set; }
        public DateTime? checkFQ { get; set; }
        public string? comments { get; set; }
        public DateTime? _sendDate { get; set; }
        public virtual Income? Income { get; set; }
    }
}
