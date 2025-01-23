using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PointSaleConfig : IEntityTypeConfiguration<PointSale>
    {
        public void Configure(EntityTypeBuilder<PointSale> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
