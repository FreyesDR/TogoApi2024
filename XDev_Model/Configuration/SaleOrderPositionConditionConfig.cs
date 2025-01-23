using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderPositionConditionConfig : IEntityTypeConfiguration<SaleOrderPositionCondition>
    {
        public void Configure(EntityTypeBuilder<SaleOrderPositionCondition> builder)
        {
            builder.HasKey(k => new { k.SaleOrderPositionId, k.PriceConditionId });
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(3);
            builder.Property(p => p.Name).HasMaxLength(30);
            builder.Property(p => p.Type).HasMaxLength(1);
            builder.Property(p => p.Source).HasMaxLength(1);
            builder.Property(p => p.Value).HasPrecision(18, 7);
            builder.Property(p => p.ValueType).HasMaxLength(1);
            builder.Property(p => p.BaseCondition).HasPrecision(18, 7);
            builder.Property(p => p.ValueCondition).HasPrecision(18, 7);
        }
    }
}
