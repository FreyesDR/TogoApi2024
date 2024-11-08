using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique(true);
            builder.Property(p => p.Code).HasMaxLength(4);
            builder.HasIndex(i => i.CodeMH).IsUnique(false);
            builder.Property(p => p.CodeMH).HasMaxLength(5);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Regions)
                .WithOne(o => o.Country)
                .HasForeignKey(f => f.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Addresses)
                .WithOne(o => o.Country)
                .HasForeignKey(f => f.CountryId)
                .IsRequired(false);
        }
    }
}
