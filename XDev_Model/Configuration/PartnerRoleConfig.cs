using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class PartnerRoleConfig : IEntityTypeConfiguration<PartnerRole>
    {
        public void Configure(EntityTypeBuilder<PartnerRole> builder)
        {            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(2);
            builder.Property(p => p.Name).HasMaxLength(25);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.Roles)
                .WithOne(m => m.Role)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
