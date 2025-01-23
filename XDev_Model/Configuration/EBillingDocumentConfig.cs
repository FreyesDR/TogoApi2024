using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EBillingDocumentConfig : IEntityTypeConfiguration<EBillingDocument>
    {
        public void Configure(EntityTypeBuilder<EBillingDocument> builder)
        {
            builder.HasKey(k => new { k.EBillingId, k.Id });
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
