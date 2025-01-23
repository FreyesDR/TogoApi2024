namespace XDev_Model.Entities
{
    public class UnitMeasure:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string AltCode { get; set; }
        public string Name { get; set; }
    }
}
