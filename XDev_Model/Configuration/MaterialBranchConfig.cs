using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class MaterialBranchConfig : IEntityTypeConfiguration<MaterialBranch>
    {
        public void Configure(EntityTypeBuilder<MaterialBranch> builder)
        {
            builder.HasKey(k => new { k.MaterialId, k.BranchId });
            builder.Property(p=> p.PriceSale).HasPrecision(18,2);
            builder.Property(p => p.PricePurchase).HasPrecision(18, 2);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);  
            
            
        }
    }
}
