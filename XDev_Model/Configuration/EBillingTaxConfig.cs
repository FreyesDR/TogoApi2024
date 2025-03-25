using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingTaxConfig : IEntityTypeConfiguration<EBillingTax>
    {
        public void Configure(EntityTypeBuilder<EBillingTax> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.TaxName).HasMaxLength(100);
            builder.HasIndex(p => p.TaxCode).IsUnique(false);
            builder.Property(p => p.TaxCode).HasMaxLength(4);            
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
