using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class AddressEmailConfig : IEntityTypeConfiguration<AddressEmail>
    {
        public void Configure(EntityTypeBuilder<AddressEmail> builder)
        {
            
            builder.HasKey("Id");
            builder.Property(p => p.Email).HasMaxLength(256);
        }
    }
}
