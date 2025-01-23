using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

namespace XDev_Model.Configuration
{
    public class IDTypeConfig : IEntityTypeConfiguration<IDType>
    {
        public void Configure(EntityTypeBuilder<IDType> builder)
        {
            
            builder.HasKey("Id");
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code).HasMaxLength(5);
            builder.Property(p => p.AltCode).HasMaxLength(3);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.LastUpdatedBy).HasMaxLength(256);

            builder.HasMany(m => m.PartnerIDS)
                .WithOne(o => o.IDType)
                .HasForeignKey(f => f.IDTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.CompanyIDS)
                .WithOne(o => o.IDType)
                .HasForeignKey(f => f.IDTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
