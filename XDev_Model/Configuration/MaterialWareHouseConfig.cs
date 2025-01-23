using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class MaterialWareHouseConfig : IEntityTypeConfiguration<MaterialWareHouse>
    {
        public void Configure(EntityTypeBuilder<MaterialWareHouse> builder)
        {
            builder.HasKey(k => new { k.MaterialId, k.BranchId, k.WareHouseId });
            builder.Property(p => p.Stock).HasPrecision(18, 3);
            builder.Property(p => p.SoldStock).HasPrecision(18, 3);
            builder.Property(p => p.PurchasedStock).HasPrecision(18, 3);
            builder.Property(p => p.LockedStock).HasPrecision(18, 3);
            builder.Property(p => p.InTransitStock).HasPrecision(18, 3);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
