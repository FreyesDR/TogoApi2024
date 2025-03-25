using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class RegimenExportConfig : IEntityTypeConfiguration<RegimenExport>
    {
        public void Configure(EntityTypeBuilder<RegimenExport> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(20);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
