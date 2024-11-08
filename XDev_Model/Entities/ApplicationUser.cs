using Microsoft.AspNetCore.Identity;
using XDev_Model.Interfaces;

namespace XDev_Model.Entities
{
    public class ApplicationUser: IdentityUser, IAuditEntity
    {
        public string Name { get; set; }        
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }        
    }
}
