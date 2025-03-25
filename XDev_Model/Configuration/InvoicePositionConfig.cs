using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class InvoicePositionConfig : IEntityTypeConfiguration<InvoicePosition>
    {
        public void Configure(EntityTypeBuilder<InvoicePosition> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.MaterialCode).HasMaxLength(20);
            builder.Property(p => p.MaterialTypeCode).HasMaxLength(2);
            builder.Property(p => p.MaterialName).HasMaxLength(100);
            builder.Property(p => p.UnitMeasureCode).HasMaxLength(4);
            builder.Property(p => p.UnitMeasureAltCode).HasMaxLength(2);
            builder.Property(p => p.GrossPrice).HasPrecision(18,5);
            builder.Property(p => p.NetPrice).HasPrecision(18, 5);
            builder.Property(p => p.Quantity).HasPrecision(18, 3);
            builder.Property(p => p.DiscountAmount).HasPrecision(18, 2);
            builder.Property(p => p.TaxAmount).HasPrecision(18, 2);
            builder.Property(p => p.NetAmount).HasPrecision(18, 2);
            builder.Property(p => p.PriceType).HasMaxLength(2);
            

            builder.HasMany(m => m.Conditions)
                .WithOne(o => o.InvoicePosition)
                .HasForeignKey(f => f.InvoicePositionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
