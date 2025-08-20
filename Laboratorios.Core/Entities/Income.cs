using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorios.Core.Entities
{
    public class Income
    {
        public Income()
        {
            samples = new HashSet<Sample>();
        }
        public int incomeId { get; set; }
        public int quotationId { get; set; }
        public string? cusCompany { get; set; } = string.Empty;
        public string? cusName { get; set; } = string.Empty;
        public string? address { get; set; } = string.Empty;
        public bool collected { get; set; }
        public DateTime? datecollected { get; set; }
        public DateTime? dateendcollected { get; set; }
        public DateTime datereception { get; set; }
        public string planSS { get; set; }
        public int nSamples { get; set; }
        public DateTime? sendDate { get; set;}
        public string? phone { get; set; }
        public string? path { get; set; }
        public int? customerId { get; set; }
        public virtual ICollection<Sample> samples { get; set; }
        //Campos de auditoria
        public int usercreated { get; set; }
        public DateTime datecreated { get; set; }
        public int? usermodified { get; set; }
        public string? reportVersion { get; set; } = string.Empty;
        public DateTime? datemodified { get; set; }
        [NotMapped]
        public DateTime? _datecollected { get; set; }
        [NotMapped]
        public DateTime? _dateendcollected { get; set; }
        [NotMapped]
        public DateTime? _datereception { get; set; }

    }
}
