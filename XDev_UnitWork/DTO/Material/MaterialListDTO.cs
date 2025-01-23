namespace XDev_UnitWork.DTO.Material
{
    public class MaterialListDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public string MaterialType { get; set; }
        public string MaterialTypeCode { get; set; }
        public string UnitMeasureCode { get; set; }
        public Guid UnitMeasureId {  get; set; }
        public decimal Price {  get; set; }
        public string PriceType { get; set; }
        public string OldCode { get; set; }        
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
    }
}
