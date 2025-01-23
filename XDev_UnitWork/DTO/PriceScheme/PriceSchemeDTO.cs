using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.PriceScheme
{
    public class PriceSchemeDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public List<PriceSchemeConditionDTO> Conditions { get; set; } = new List<PriceSchemeConditionDTO>();
    }
}
