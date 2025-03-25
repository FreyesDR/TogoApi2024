using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingConfig : IEntityTypeConfiguration<EBilling>
    {
        public void Configure(EntityTypeBuilder<EBilling> builder)
        {
            builder.HasKey("Id");            
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.Property(p => p.UrlTest).HasMaxLength(100);
            builder.Property(p => p.UrlSigner).HasMaxLength(100);
            builder.Property(p => p.UrlProd).HasMaxLength(100);
			builder.Property(p => p.CertPathProd).HasMaxLength(100);
			builder.Property(p => p.CertPathTest).HasMaxLength(100);
			builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Companies)
                .WithOne(o => o.EBilling)
                .HasForeignKey(f => f.EBillingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Taxs)
                .WithOne(o => o.EBilling)
                .HasForeignKey(f => f.EBillingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
