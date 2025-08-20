namespace Laboratorios.Core.Entities
{
    public class IncomeDetail
    {
        public int incomeDetailId { get; set; }
        public int sampleId { get; set; }
        public int areaId { get; set; }
        public int analisysId { get; set; }
        public bool? onSite { get; set; }
        public virtual Analisys? Analisys { get; set; }
    }
}
