using XDev_Model.Entities;

namespace XDev_UnitWork.DTO
{
    public class AddressTypeDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }        
    }
}
