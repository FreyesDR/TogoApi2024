using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PartnerRolesConfig : IEntityTypeConfiguration<PartnerRoles>
    {
        public void Configure(EntityTypeBuilder<PartnerRoles> builder)
        {            
            builder.HasKey(k => new { k.RoleId, k.PartnerId });
            builder.HasIndex(p => p.RoleId).IsUnique(false);
            builder.HasIndex(p => p.PartnerId).IsUnique(false);
        }
    }
}
