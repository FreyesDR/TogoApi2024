using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class InvoicePaymentConfig : IEntityTypeConfiguration<InvoicePayment>
    {
        public void Configure(EntityTypeBuilder<InvoicePayment> builder)
        {
            builder.HasKey(k => new { k.InvoiceId, k.MeanOfPaymentId, k.Position });
            builder.Property(p => p.Amount).HasPrecision(18, 2);
            builder.Property(p => p.Reference).HasMaxLength(50);
            builder.Property(p => p.Tipo).HasMaxLength(1);
            builder.Property(p => p.Plazo).HasMaxLength(2);
            builder.Property(p => p.MeanOfPaymentCode).HasMaxLength(2);
        }
    }
}
