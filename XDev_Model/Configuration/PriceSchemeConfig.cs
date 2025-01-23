using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PriceSchemeConfig : IEntityTypeConfiguration<PriceScheme>
    {
        public void Configure(EntityTypeBuilder<PriceScheme> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(5);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Conditions)
                .WithOne(o => o.PriceScheme)
                .HasForeignKey(f => f.PriceSchemeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
