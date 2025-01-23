namespace XDev_Model.Entities
{
    public class Material: AuditEntity
    {
        public Guid Id { get; set; }
        public string Code {  get; set; }
        public string Name { get; set; }
        public Guid MaterialTypeId { get; set; }
        public MaterialType MaterialType { get; set; }
        public string OldCode { get; set; }
        public string PriceType { get; set; } // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado
        public decimal Price { get; set; }
        public Guid UnitMeasureId { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; } 
        
    }
}
