using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderConfig : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(p => p.Number).IsUnique();
            builder.Property(p => p.Number).HasMaxLength(20);
            builder.Property(p => p.RefDocument).HasMaxLength(20);
            builder.Property(p => p.TaxAmount).HasPrecision(18, 2);
            builder.Property(p => p.NetAmount).HasPrecision(18, 2);
            builder.Property(p => p.Per1).HasPrecision(18, 5);
            builder.Property(p => p.Ret1).HasPrecision(18, 5);
            builder.Property(p => p.Ret10).HasPrecision(18, 5);
            builder.Property(p => p.DiscountPorcent).HasPrecision(18, 2);
            builder.Property(p => p.Assignment).HasMaxLength(50);
            builder.Property(p => p.CurrencyCode).HasMaxLength(4);
            builder.Property(p => p.PointSaleCode).HasMaxLength(4);
            builder.Property(p => p.RecintoFiscalCode).HasMaxLength(4);
            builder.Property(p => p.RegimenExportCode).HasMaxLength(20);
            builder.Property(p => p.IncoTerms).HasMaxLength(100);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Positions)
                .WithOne(o => o.SaleOrder)
                .HasForeignKey( f=> f.SaleOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.SaleOrderSporadicPartner)
                .WithOne(o => o.SaleOrder)
                .HasForeignKey<SaleOrderSporadicPartner>(f => f.SaleOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Payments)
                .WithOne(o => o.SaleOrder)
                .HasForeignKey(f => f.SaleOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(n => n.SaleOrderType)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
