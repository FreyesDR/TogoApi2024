using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PartnerEconomicActivityConfig : IEntityTypeConfiguration<PartnerEconomicActivity>
    {
        public void Configure(EntityTypeBuilder<PartnerEconomicActivity> builder)
        {            
            builder.HasKey("Id");
            builder.HasIndex(p => p.PartnerId).IsUnique(false);
            builder.HasIndex(p => p.EconomicActivityId).IsUnique(false);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
