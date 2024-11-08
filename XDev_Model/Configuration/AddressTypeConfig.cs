using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(i => i.Code).IsUnique(false);
            builder.Property(p => p.Code).HasMaxLength(2);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Addresses)
                .WithOne(o => o.AddressType)
                .HasForeignKey(o => o.AddressTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
