using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class SaleOrderTypeConfig : IEntityTypeConfiguration<SaleOrderType>
    {
        public void Configure(EntityTypeBuilder<SaleOrderType> builder)
        {
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(5);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.PdfFormName).HasMaxLength(50);
            builder.Property(p => p.Inventory).HasMaxLength(1);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);           
        }
    }
}
