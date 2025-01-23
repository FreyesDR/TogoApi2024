using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingCompanyConfig : IEntityTypeConfiguration<EBillingCompany>
    {
        public void Configure(EntityTypeBuilder<EBillingCompany> builder)
        {
            builder.HasKey(k => new { k.EBillingId, k.CompanyId });
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Invoice)
                .WithOne(o => o.EBillingCompany)
                .HasForeignKey(f => new { f.EBillingId, f.CompanyId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
