using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Material
{
    public class MaterialDTO: AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid MaterialTypeId { get; set; }  
        public string MaterialTypeCode {  get; set; }
        public string OldCode { get; set; }
        public string PriceType { get; set; } = "GV"; // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado
        public decimal Price { get; set; }
        public Guid UnitMeasureId { get; set; }
        public string UnitMeasureCode {  get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public short NumType { get; set; }
        
    } 

    public class MaterialSaleDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid MaterialTypeId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialTypeName {  get; set; }
        public string OldCode { get; set; }
        public string PriceType { get; set; } = "GV"; // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado
        public decimal Price { get; set; }
        public Guid UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public short NumType { get; set; }
        public Guid WareHouseId { get; set; }
        
    }
}
