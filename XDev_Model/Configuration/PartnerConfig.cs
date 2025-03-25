using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PartnerConfig : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(10);
            builder.HasIndex(p => p.OldCode).IsUnique(false);
            builder.Property(p => p.OldCode).HasMaxLength(15);
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.TradeName).HasMaxLength(100);
            builder.Property(p => p.ContactPersonEmail).HasMaxLength(256);
            builder.Property(p => p.ContactPersonIDNumber).HasMaxLength(30);
            builder.Property(p => p.ContactPersonName).HasMaxLength(100);
            builder.Property(p => p.ContactPersonPhone).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Roles)
                .WithOne(o => o.Partner)
                .HasForeignKey(o => o.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Addresses)
                .WithOne(o => o.Partner)
                .HasForeignKey(f => f.PartnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(m => m.PartnerIDS)
                .WithOne(o => o.Partner)
                .HasForeignKey(o => o.PartnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.EconomicActivities)
                .WithOne(o => o.Partner)
                .HasForeignKey(f => f.PartnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Companies)
                .WithOne(o => o.Partner)
                .HasForeignKey(f => f.PartnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
