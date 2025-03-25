using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Admin
{
    public class EndPointPolicyDTO:AuditEntityDTO
    {
        public Guid Id { get; set; }
        public string MethodHttp { get; set; } // "GET", "POST", etc.        
        public string Description { get; set; }
        public string Module { get; set; }        
        public string PolicyName { get; set; }
        public string PolicyParams { get; set; }
    }
}
