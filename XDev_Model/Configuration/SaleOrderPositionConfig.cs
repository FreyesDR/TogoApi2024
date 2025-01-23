using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderPositionConfig : IEntityTypeConfiguration<SaleOrderPosition>
    {
        public void Configure(EntityTypeBuilder<SaleOrderPosition> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.MaterialCode).HasMaxLength(20);
            builder.Property(p => p.MaterialTypeCode).HasMaxLength(2);
            builder.Property(p => p.MaterialName).HasMaxLength(100);
            builder.Property(p => p.UnitMeasureCode).HasMaxLength(4);
            builder.Property(p => p.Quantity).HasPrecision(18, 3);            
            builder.Property(p => p.DiscountAmount).HasPrecision(18, 2);
            builder.Property(p => p.TaxAmount).HasPrecision(18, 2);
            builder.Property(p => p.NetAmount).HasPrecision(18, 2);
            builder.Property(p => p.PriceType).HasMaxLength(2);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Conditions)
                .WithOne(o => o.SaleOrderPosition)
                .HasForeignKey(f => f.SaleOrderPositionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
