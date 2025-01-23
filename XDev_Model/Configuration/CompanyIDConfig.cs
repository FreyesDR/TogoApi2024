using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class CompanyIDConfig : IEntityTypeConfiguration<CompanyID>
    {
        public void Configure(EntityTypeBuilder<CompanyID> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.CompanyId).IsUnique(false);
            builder.HasIndex(p => p.IDTypeId).IsUnique(false);
            builder.Property(p => p.DocumentNumber).HasMaxLength(30);
            builder.Property(p => p.NIFNum).HasMaxLength(1);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
 