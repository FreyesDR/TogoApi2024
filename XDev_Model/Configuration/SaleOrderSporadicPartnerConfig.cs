using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderSporadicPartnerConfig : IEntityTypeConfiguration<SaleOrderSporadicPartner>
    {
        public void Configure(EntityTypeBuilder<SaleOrderSporadicPartner> builder)
        {
            builder.HasKey("Id");
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.IDNumber).HasMaxLength(20);
            builder.Property(p => p.Address).HasMaxLength(500);
            builder.Property(p => p.Email).HasMaxLength(256);
            builder.Property(p => p.Phone).HasMaxLength(20);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
        }
    }
}
