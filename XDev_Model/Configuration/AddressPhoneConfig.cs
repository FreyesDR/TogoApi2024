using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class AddressPhoneConfig : IEntityTypeConfiguration<AddressPhone>
    {
        public void Configure(EntityTypeBuilder<AddressPhone> builder)
        {
            
            builder.HasKey("Id");
            builder.Property(p => p.Phone).HasMaxLength(80);
            builder.Property(p => p.PhoneExt).HasMaxLength(8);
        }
    }
}
