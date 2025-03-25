using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderPaymentConfig : IEntityTypeConfiguration<SaleOrderPayment>
    {
        public void Configure(EntityTypeBuilder<SaleOrderPayment> builder)
        {
            builder.HasKey(k => new { k.SaleOrderId, k.MeanOfPaymentId, k.Position });
            builder.Property(p => p.Amount).HasPrecision(18, 2);
            builder.Property(p => p.Reference).HasMaxLength(50);
            builder.Property(p => p.MeanOfPaymentCode).HasMaxLength(2);
            builder.Property(p => p.Tipo).HasMaxLength(1);
            builder.Property(p => p.Plazo).HasMaxLength(2);
        }
    }
}
