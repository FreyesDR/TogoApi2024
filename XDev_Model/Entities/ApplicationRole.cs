using Microsoft.AspNetCore.Identity;
using XDev_Model.Interfaces;

namespace XDev_Model.Entities
{
    public class ApplicationRole : IdentityRole, IAuditEntity
    {
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
