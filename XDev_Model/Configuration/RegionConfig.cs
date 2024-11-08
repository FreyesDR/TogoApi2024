using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Cities)
                .WithOne(o => o.Region)
                .HasForeignKey(f => f.RegionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Addresses)
                .WithOne(o => o.Region)
                .HasForeignKey(f => f.RegionId)
                .IsRequired(false);
        }
    }
}
