namespace Laboratorios.Core.Entities
{
    public class Drafts
    {
        public string DocObjectCode { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public ICollection<DocumentLines> DocumentLines { get; set; }

    }

    public class DocumentLines
    {
        public string? ItemCode { get; set; } = string.Empty;
        public decimal? Quantity { get; set; }
        public string? UseBaseUnits { get; set; }
    }
}
