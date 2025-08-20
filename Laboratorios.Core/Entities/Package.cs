namespace Laboratorios.Core.Entities
{
    public class Package
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public bool status { get; set; }
    }
    public class PackageDetail
    {
        public int id { get; set; }
        public int packageId { get; set; }
        public int areaId { get; set; }
        public int analisysId { get; set; }
    }
}
