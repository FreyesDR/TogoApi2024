using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EndPointPolicyConfig : IEntityTypeConfiguration<EndPointPolicy>
    {
        public void Configure(EntityTypeBuilder<EndPointPolicy> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.MethodHttp).IsUnique(false);
            builder.Property(p => p.MethodHttp).HasMaxLength(15);
            builder.HasIndex(i => i.MethodPath).IsUnique(false);
            builder.Property(p => p.MethodPath).HasMaxLength(200);
            builder.Property(p => p.PolicyParams).HasMaxLength(200);
            builder.Property(p => p.Module).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
