using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class RecintoFiscalConfig : IEntityTypeConfiguration<RecintoFiscal>
    {
        public void Configure(EntityTypeBuilder<RecintoFiscal> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(4);            
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
