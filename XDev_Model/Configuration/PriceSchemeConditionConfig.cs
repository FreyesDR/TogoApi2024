using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PriceSchemeConditionConfig : IEntityTypeConfiguration<PriceSchemeCondition>
    {
        public void Configure(EntityTypeBuilder<PriceSchemeCondition> builder)
        {
            builder.HasKey("Id");            
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
