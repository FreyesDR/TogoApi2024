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
            builder.Property(p => p.IDCode).HasMaxLength(5);
            builder.Property(p => p.IDCode2).HasMaxLength(5);
            builder.Property(p => p.IDNumber2).HasMaxLength(20);
            builder.Property(p => p.CountryCode).HasMaxLength(4);
            builder.Property(p => p.CountryName).HasMaxLength(50);
            builder.Property(p => p.RegionCode).HasMaxLength(4);
            builder.Property(p => p.RegionName).HasMaxLength(50);
            builder.Property(p => p.CityCode).HasMaxLength(4);
            builder.Property(p => p.CityName).HasMaxLength(50);
            builder.Property(p => p.EcoActivityCode).HasMaxLength(5);
            builder.Property(p => p.EcoActivityName).HasMaxLength(200);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);
            builder.Property(p => p.TypePerson).HasMaxLength(1);
        }
    }
}
