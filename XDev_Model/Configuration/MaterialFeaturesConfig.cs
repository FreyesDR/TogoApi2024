using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class MaterialFeaturesConfig : IEntityTypeConfiguration<MaterialFeatures>
    {
        public void Configure(EntityTypeBuilder<MaterialFeatures> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.RangeId).IsUnique();
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
