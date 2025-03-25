using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingLogConfig : IEntityTypeConfiguration<EBillingLog>
    {
        public void Configure(EntityTypeBuilder<EBillingLog> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(p => p.CompanyId).IsUnique(false);
            builder.HasIndex(p => p.BranchId).IsUnique(false);
            builder.HasIndex(p => p.PointSaleId).IsUnique(false);
            builder.HasIndex(p => p.CodGen).IsUnique(false);
            builder.HasIndex(p => p.InvoiceId).IsUnique(false);
            builder.HasIndex(p => p.SaleOrderId).IsUnique(false);
            builder.Property(p => p.NumControl).HasMaxLength(50);
            builder.Property(p => p.SelloRecibido).HasMaxLength(50);
            builder.Property(p => p.TipoDte).HasMaxLength(4);
            builder.Property(p => p.ResponseStatus).HasMaxLength(20);
            builder.Property(p => p.ResponseStatusCode).HasMaxLength(20);
            builder.Property(p => p.StatusCode).HasMaxLength(4);
        }
    }
}
