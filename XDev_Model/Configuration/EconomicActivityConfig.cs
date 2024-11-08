using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class EconomicActivityConfig : IEntityTypeConfiguration<EconomicActivity>
    {
        public void Configure(EntityTypeBuilder<EconomicActivity> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(5);
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.CompanyEconomicActivities)
                .WithOne(o => o.EconomicActivity)
                .HasForeignKey(f => f.EconomicActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.PartnerEconomicActivities)
                .WithOne(o => o.EconomicActivity)
                .HasForeignKey(f => f.EconomicActivityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
