namespace Laboratorios.Core.Entities
{
    public class Sample
    {
        public Sample()
        {
            incomeDetail = new HashSet<IncomeDetail>();
        }
        public int sampleId { get; set; }
        public int nsample { get; set; }
        public int? incomeId { get; set; }
        public string correlative { get; set; } = string.Empty;
        public int year { get; set; }
        public int sampleTypeId { get; set; }
        public int matrixId { get; set; }
        public int? usercreated { get; set; }
        public DateTime? datecreated { get; set; }
        public bool? included { get; set; }
        public string? collectionPoint { get; set; }
        public string? serviceRequest { get; set; }
        public int? quotationNumber { get; set; }
        public string? observations { get; set; }
        public string? identification { get; set; }
        public virtual ICollection<IncomeDetail> incomeDetail { get; set; }
        public virtual ICollection<PhisycalChemistryResults>? phisycalChemistryResults { get; set; }
        public virtual ICollection<MicrobiologyResults>? microbiologyResults { get; set; }
        public virtual Matrix? Matrix { get; set; }
    }
}
