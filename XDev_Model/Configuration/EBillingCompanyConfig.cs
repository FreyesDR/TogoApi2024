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
            builder.Property(p => p.ApiUser).HasMaxLength(20);
            builder.Property(p => p.ApiKeyTest).HasMaxLength(50);
            builder.Property(p => p.ApiKeyProd).HasMaxLength(50);
            builder.Property(p => p.PrivateKeyTest).HasMaxLength(50);
            builder.Property(p => p.PrivateKeyProd).HasMaxLength(50);
            builder.Property(p => p.SmtpService).HasMaxLength(1);
            builder.Property(p => p.SmtpHost).HasMaxLength(30);
            builder.Property(p => p.SmtpUserName).HasMaxLength(100);
            builder.Property(p => p.FromName).HasMaxLength(100);
            builder.Property(p => p.CcEmail1).HasMaxLength(100);
            builder.Property(p => p.CcEmail2).HasMaxLength(100);
            builder.Property(p => p.FromName).HasMaxLength(100);
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
