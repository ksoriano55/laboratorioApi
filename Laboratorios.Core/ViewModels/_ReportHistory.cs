using System.ComponentModel.DataAnnotations;

namespace Laboratorios.Core.ViewModels
{
    public class _ReportHistory
    {
        public string? _correlativeFQ { get; set; }
        public string? _correlativeMB { get; set; }
        [Key]
        public int reportId { get; set; }
        public int incomeId { get; set; }
        public string? correlativeFQ { get; set; }
        public string? correlativeMB { get; set; }
        public DateTime? draftDate { get; set; }
        public DateTime? checkMB { get; set; }
        public DateTime? checkFQ { get; set; }
        public string? comments { get; set; }
        public string? cusCompany { get; set; }
        public DateTime datereception { get; set; }
        public DateTime expirationDate { get; set; }
        public DateTime? sendDate { get; set; }
        public DateTime? _sendDate { get; set; }

    }
}