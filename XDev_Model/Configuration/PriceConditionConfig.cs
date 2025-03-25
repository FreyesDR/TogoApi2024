using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PriceConditionConfig : IEntityTypeConfiguration<PriceCondition>
    {
        public void Configure(EntityTypeBuilder<PriceCondition> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(3);
            builder.Property(p => p.AltCode).HasMaxLength(4);
            builder.Property(p => p.Name).HasMaxLength(30);
            builder.Property(p => p.Type).HasMaxLength(1);    
            builder.Property(p => p.Source).HasMaxLength(1);
            builder.Property(p => p.Value).HasPrecision(18,2);
            builder.Property(p => p.ValueType).HasMaxLength(1);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.PriceSchemeCondition)
                .WithOne(o => o.PriceCondition)
                .HasForeignKey(f => f.PriceConditionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
