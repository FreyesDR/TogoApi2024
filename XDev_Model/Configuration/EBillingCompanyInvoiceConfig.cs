using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingCompanyInvoiceConfig : IEntityTypeConfiguration<EBillingCompanyInvoice>
    {
        public void Configure(EntityTypeBuilder<EBillingCompanyInvoice> builder)
        {
            builder.HasKey(k => new { k.EBillingId, k.CompanyId, k.InvoiceTypeId });
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
            
        }
    }
}
