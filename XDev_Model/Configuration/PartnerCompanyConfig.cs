using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PartnerCompanyConfig : IEntityTypeConfiguration<PartnerCompany>
    {
        public void Configure(EntityTypeBuilder<PartnerCompany> builder)
        {            
            builder.HasKey(k => new { k.PartnerId, k.CompanyId });
            builder.HasIndex(p => p.PartnerId).IsUnique(false);
            builder.HasIndex(p => p.CompanyId).IsUnique(false);
        }
    }
}
