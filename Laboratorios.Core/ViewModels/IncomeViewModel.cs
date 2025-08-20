using Laboratorios.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorios.Core.ViewModels
{
    public class IncomeViewModel
    {
        [Key]
        public Int64 id { get; set; }
        public int incomeId { get; set; }
        public int quotationId { get; set; }
        public string? cusCompany { get; set; }
        public string correlative { get; set; }
        public string matrix { get; set; }
        public string area { get; set; }
        public string analisys { get; set; }
        public string selected { get; set; }
        public string? onSite { get; set; }
    }
    public class _Income
    {
        [Key]
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
        public DateTime? sendDate { get; set; }
        public string? phone { get; set; }
        public string? path { get; set; }
        public int? customerId { get; set; }
        //Campos de auditoria
        public int usercreated { get; set; }
        public DateTime datecreated { get; set; }
        public int? usermodified { get; set; }
        public DateTime? datemodified { get; set; }
        public bool containsMicro { get; set; }
        public bool containsFisico { get; set; }
        public bool includeOutsourced { get; set; }
        public bool? pending { get; set; }
        public string samples {  get; set; }= string.Empty;
        public bool? send {  get; set; }
        public bool? checkMB {  get; set; }
        public bool? checkFQ {  get; set; }
    }
}
